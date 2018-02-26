using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;



public class Agent
{
    Object threadLock = new Object();
    public bool forceStop = false;
    public bool oneStepLearn = false;
    private bool m_DisableOptions;
    public Random rand = new Random(212);
    public Environment environment;
    public List<int> EpisodeSteps = new List<int>();
    public List<int> ContextTrack = new List<int>();

    public List<List<int>> E_vals = new List<List<int>>();
    public static double Emin = -0.3; //RLCD // 0,1 çok fazla context oluşmasına sebep olurken 1 yeni contextlerin oluşmasını engelliyor
                                            //bu durumda en iyi değer 0,5 civarı gözüküyor (grid size 20 için -0.5, 30 için -0.6, 40 için -0.8)
    public static double rho = 0.1; //adjustment coefficiant
    //std her bir episode için hesapla
    
    public QClass Q;
    public int currentState;
    public ModelClass Model = new ModelClass();
    double e = 0.1;
    bool m_optsLearned = false;
    List<SubGoal> Subgoals = new List<SubGoal>();
    List<int> SubgoalCounts = new List<int>();
    //public int[] initiationSetMembers = null;
    //public List<InitiationSet> initiationSets = new List<InitiationSet>();

    public Context currentContext;
    public List<Context> contexts = new List<Context>();

    List<double[]> e_updates;
    int discount = 0;

    public int stopEpisode = 0;
    public bool stopByEpisode = false;
    




    public Agent(int startState, Environment env)
    {
        environment = env;
        currentState = 0; //env.CurrentState olacak!!!!!!!!!!!!!!!!!!
        Q = new QClass(this, m_DisableOptions);
        currentContext = Context.NewContext(this, DisableOptions, currentState);
        contexts.Add(currentContext);
        SwitchContext(currentContext);
    }

    public bool DisableOptions
    {
        get
        {
            return m_DisableOptions;
        }
        set
        {
            m_DisableOptions = value;
            Q = new QClass(this, m_DisableOptions);
        }
    }

    public bool checkAndSwitchContext(int s, int a, int sP, double r)
    {
        bool contextChanged = false;
        int maxC = 0;

        double em = 0;
        if (contexts[0].Model[s, a] != null)
        {
            em = contexts[0].Model[s, a].calculateEm(sP, r);
            //if (em < 0)
            //    em = em;
            
        }
        contexts[0].Em = contexts[0].Em + Agent.rho * (em - contexts[0].Em);////////if in içinde de olabilir.olmayan model için işlem yapılmaz 
        double maxE = contexts[0].Em;

        for (int c = 1; c < contexts.Count; c++)
        {
            if (contexts[c].Model[s, a] == null)
                em = 0;
            else
            {
                em = contexts[c].Model[s, a].calculateEm(sP, r);
                
            }
            contexts[c].Em = contexts[c].Em + Agent.rho * (em - contexts[c].Em);////////else in içinde de olabilir.olmayan model için işlem yapılmaz 
            if (maxE < contexts[c].Em)
            {
                maxE = contexts[c].Em;
                maxC = c;
            }
        }
        if (maxE < Emin)
        {
            Context ctx = Context.NewContext(this, DisableOptions, currentState);
            contexts.Add(ctx);
            SwitchContext(ctx);
            contextChanged = true;
        }
        else
        {
            if (contexts[maxC] != currentContext)
            {
                SwitchContext(contexts[maxC]);
                contextChanged = true;
            }
        }
        return contextChanged;
    }

    public bool checkAndSwitchContextNoNew(int s, int a, int sP, double r)
    {
        bool contextChanged = false;
        int maxC = 0;

        double em = 0;
        if (contexts[0].Model[s, a] != null)
            em = contexts[0].Model[s, a].calculateEm(sP, r);
        //if (em < 0)
        //    em = em;
        contexts[0].Em = contexts[0].Em + Agent.rho * (em - contexts[0].Em);

        double maxE = contexts[0].Em;


        for (int c = 1; c < contexts.Count; c++)
        {
            if (contexts[c].Model[s, a] == null)
                em = 0;
            else
                em = contexts[c].Model[s, a].calculateEm(sP, r);
            contexts[c].Em = contexts[c].Em + Agent.rho * (em - contexts[c].Em);

            if (maxE < contexts[c].Em)
            {
                maxE = contexts[c].Em;
                maxC = c;
            }
        }
        if (contexts[maxC] != currentContext)
        {
            SwitchContext(contexts[maxC]);
            contextChanged = true;
        }
        return contextChanged;
    }

  

    public void SwitchContext(Context x)
    {
        //currentContext.currentState = currentState;
        currentContext.e = e;
        //currentContext.initiationSetMembers = initiationSetMembers;
        //currentContext.initiationSets = initiationSets;
        currentContext.m_optsLearned = m_optsLearned;
        currentContext.Model = Model;
        currentContext.Q = Q;
        currentContext.SubgoalCounts = SubgoalCounts;
        currentContext.Subgoals = Subgoals;
        currentContext.EpsilonDiscount = discount;

        //currentState = x.currentState;
        e = x.e;
        //initiationSetMembers = x.initiationSetMembers;
        //initiationSets = x.initiationSets;
        m_optsLearned = x.m_optsLearned;
        Model = x.Model;
        Q = x.Q;
        SubgoalCounts = x.SubgoalCounts;
        Subgoals = x.Subgoals;
        discount = x.EpsilonDiscount;
       
        currentContext = x;
    }

    public int optNonLearnedVals = 0;
    public int optLearnedVals = 0;
    public void LearnPS(int N, int episodes)
    {
        subgoalVar = new List<double>();
        forceStop = false;
        //int stateHold = currentState;

        float alpha = 0.2f, gamma = 0.95f, theta = 0.001f;
        int s = currentState; //Initialize

        List<PriorityObj> PQueue = new List<PriorityObj>();
        visit = new Dictionary<int, int>();
        int epsilon_counter = 0;
        int steps = 0;
        while (true)
        {
            if (forceStop)
                break;

            if (epsilon_counter++ % 100 == 0)
            {
                epsilon_counter = 1;
                e = 0.01 + 0.7 * Math.Pow(Math.E, -(discount++) / 20.0);
            }

            int tempSteps = steps; //Number of steps per episode

            if (EpisodeSteps.Count > 1500)
                forceStop = true;

            //(b) a <- policy(s,Q)
            //int a = e_greedy(Q[s]);
            int a = softmaxWVisits(Q[s]);
           // Q[s][a] -= -0.001;
            double tempVal = Q[s][a];
            //tempFirstVisitHist[s[0], s[1]] += 1;
            //(c) Execute action a  
            int sP = 0;
            double r = 0;
            Context curCtxt = currentContext;
            lock (threadLock)
            {
                executeAction(s, a, ref sP, ref r, ref steps);
            }
            if(!visit.ContainsKey(sP))
                visit.Add(sP, 0);
            Context nexCtxt = currentContext;
            if (curCtxt != nexCtxt)
            {
                //double tmp = curCtxt.Q[s][Q[s].GetMaxAction()];
                //for (int i = 0; i < curCtxt.Q[s].Actions.Count; i++)
                //{
                //    Q[s][i] += curCtxt.Q[s][i];//tmp / 8;
                //}
                continue;
            }
                //SwitchContext(curCtxt);
            int result = sP;
            
            //(d) Model(s,a) <- s', r
            //Model[s[0], s[1], a] = new int[] { sP[0], sP[1], r, steps - tempSteps };
            
                if (Model[s, a] == null)
                {
                    ModelValue m = new ModelValue(s, a, sP, r, steps - tempSteps);
                    Model[s, a] = m;
                }
                else
                    if (Model[s, a].calculateEm(sP, r) > 0)
                    {
                        Model[s, a].Update(s, a, sP, r, steps - tempSteps);
                    }
            //System.Diagnostics.Debug.WriteLine("\tContext: " + currentContext.cID + "\t(s,a,sp): " + s + ", " + a + ", " + sP + "\tTm0: " /*+ (Model[s, a].Tm.Count > 0 ? Model[s,a].Tm[0].ToString() : " ") */ + "\tem0: " + em + "\tEm0: " + contexts[0].Em + "\tEm1: " + (contexts.Count > 1 ? contexts[1].Em.ToString() : "-"));
            
            //(e) p <- |r + gama*maxA'Q(s',a') - Q(s,a)|
            //float p = Math.Abs(r + gamma * getQ(sP[0], sP[1], maxA(sP)) - getQ(s[0], s[1], a));
            double p = Math.Abs(r + gamma * Q[sP][Q[sP].GetMaxAction()] - Q[s][a]);

            //(f) if p > tetha, then insert s,a into PQueue with priority p
            PQueue.Clear();
            if (p > theta)
                InsertQueue(PQueue, s, a, p);

            //(g) Repeat N times while PQueue is not empty
            for (int i = 0; i < N && 0 < PQueue.Count; i++)
            {
                //(-)s, a <- first(PQueue)
                PriorityObj obj = PQueue[0];// dene!!!!!!!!!!!!11
                PQueue.Remove(obj);
                s = obj.State;
                a = obj.Action;

                //(-)s', r <- Modell(s,a)
                sP = Model[s, a].sP;
                r = Model[s, a].reward;
                int tsteps = Model[s, a].steps;

                //(-)Q(s, a) <- Q(s,a) + alpha[r + gama*maxA'Q(s',a') - Q(s,a)]
                //e_updates.Add(new double[] { s, a, Q[s][a] });
                Q[s][a] = Q[s][a] + alpha * (r + Math.Pow(gamma, tsteps) * Q[sP][Q[sP].GetMaxAction()] - Q[s][a]);

                //float t = (float)(getQ(s[0], s[1], a) + alpha * (r + Math.Pow(gamma, tsteps) * getQ(sP[0], sP[1], maxA(sP)) - getQ(s[0], s[1], a)));
                //setQ(s[0], s[1], a, t);

                //(-)Repeat, for all s",a" predicted to lead to s
                List<ModelValue> list = Model.All_SA_PredictedToLeadTo(s);
                for (int j = 0; j < list.Count; j++)
                {
                    int sDP = list[j].s;
                    int aDP = list[j].a;

                    // r" <- predicted reward
                    double rDP = list[j].reward;

                    // p <- |r" + gamma*maxaQ(s,a) - Q(s",a")|
                    p = Math.Abs(rDP + gamma * Q[s][Q[s].GetMaxAction()] - Q[sDP][aDP]);

                    // if p > tetha, then insert s",a" into PQueue with priority p
                    if (p > theta)
                    {
                        InsertQueue(PQueue, sDP, aDP, p);
                    }
                }

            }
            s = result;
            environment.State = s;
            if (environment.isGoal(s))
            {
                //visit = new Dictionary<int, int>();///////////
                //if (subgoalValues != null)
                //{
                //    subgoalValues[s] = 0.1;
                //    for (int f = 0; f < 4; f++)
                //    {
                //        if (Model.StateCount < s)
                //            Model.States.Add(new ModelState(s));
                //        if (Model[s] == null)
                //            Model[s] = new ModelState(s);
                //        if(Model[s].Count <= 4)
                //            Model[s].Add(new ModelValue(s, f, s, 0, 0));
                //    }
                //}
                if (Model.States.Count <= s)
                {
                    Model[s, a] = null;
                    Model.States[s] = new ModelState(s);
                }
                //e = 0.9 * Math.Pow(Math.E, -(discount++) / 50.0); //Update epsilon
                if (!DisableOptions && !m_optsLearned)
                {
                    //initiationSetMembers = new int[Model.StateCount];
                    //initiationSets = new List<InitiationSet>();
                    Subgoals = SearchSubgoals();
                    //Subgoals.Add(new SubGoal(s));
                    SubgoalCounts.Add(Subgoals.Count);
                    double mean = 0;
                    double std = 100;
                    int sc = 10;
                    if (SubgoalCounts.Count > sc)
                    {
                        std = 0;
                        for (int c = SubgoalCounts.Count - sc; c < SubgoalCounts.Count; c++)
                        {
                            mean += SubgoalCounts[c] / (double)sc;
                        }
                        for (int c = SubgoalCounts.Count - sc; c < SubgoalCounts.Count; c++)
                        {
                            std += Math.Pow(SubgoalCounts[c] - mean, 2) / sc;
                        }
                        subgoalVar.Add(std);
                        std = Math.Sqrt(std);
                       
                    }
                    else
                        subgoalVar.Add(-1);


                    if (std < 0.01)
                    {
                        List<OptionN> options = createOptFromSubGoals(Subgoals);
                        currentContext.options = options;
                        optNonLearnedVals = 0;
                        optLearnedVals = 0;
                        for (int i = 0; i < Model.StateCount; i++)
                        {
                            if (Model[i] == null)
                                optNonLearnedVals += 4;
                            else
                            {
                                for (int j = 0; j < Model[i].Count; j++)
                                {
                                    if (Model[i][j] == null || Q[i][j] == 0)
                                        optNonLearnedVals += 1;
                                    else
                                        optLearnedVals += 1;
                                }
                            }
                        }
                        //SearchInitiationSets(Subgoals); //Bu satır if in içinde olmalı (ki öyle(değil mi?))
                        //List<Option> options = CreateOptions();
                        OptionLearn(options);
                        //currentContext.options = options;
                    }
                }

                s = currentState;
                EpisodeSteps.Add(steps);
                ContextTrack.Add(currentContext.cID);
                currentContext.EpisodeSteps.Add(steps);
                steps = 0;
                int calcLength = 9;
                if (currentContext.EpisodeSteps.Count > calcLength)
                {
                    double mean = 0;
                    double std = 0;
                    for (int c = currentContext.EpisodeSteps.Count - calcLength; c < currentContext.EpisodeSteps.Count; c++)
                    {
                        mean += EpisodeSteps[c] / (double)calcLength;
                    }
                    for (int c = currentContext.EpisodeSteps.Count - calcLength; c < currentContext.EpisodeSteps.Count; c++)
                    {
                        std += Math.Pow(currentContext.EpisodeSteps[c] - mean, 2) / calcLength;
                    }
                    std = Math.Sqrt(std);
                    currentContext.std = std;

                    bool stop = true;
                    for (int cx = 0; cx < contexts.Count; cx++)
                    {
                        if (contexts[cx].std > 10)
                            stop = false;
                        //System.Diagnostics.Debug.Write(cx + ":" + contexts[cx].std + "   ");
                    }
                    //System.Diagnostics.Debug.Write("\n");
                    if (stopByEpisode)
                    {
                        if (stopEpisode <= EpisodeSteps.Count)
                            break;
                    }
                    else
                    {
                        if (stop || forceStop)
                            break;
                    }
                }//Varyans ile dene

                //if (!m_optsLearned)
                //{
                //    OptionSearch();
                //    OptionLearn();
                //}
                PQueue.Clear();
                if (curCtxt != nexCtxt)
                    SwitchContext(nexCtxt);
                if (oneStepLearn)
                    break;
                e_updates = new List<double[]>();
            }
        }
        environment.State = currentState;
    }
    public List<double> subgoalVar = new List<double>();

    List<OptionN> createOptFromSubGoals(List<SubGoal> subgoals)
    {
        List<OptionN> options = new List<OptionN>();
        for (int i = 0; i < subgoals.Count; i++)
        {
            SubGoal s = subgoals[i];
            OptionN o = new OptionN(s.State, this);
            options.Add(o);
            for (int j = 0; j < Model[s.State].Count; j++)
            {
                if (Model[s.State][j] == null)
                    continue;
                if (Q[s.State].Actions[j].O != null)
                    continue;
                o.AddSASp(s.State, j, Model[s.State][j].sP);
                constOptionN(subgoals, s, o, Model[s.State][j].sP);
            }
        }
        return options;
    }

    void constOptionN(List<SubGoal> subgoals, SubGoal s, OptionN o, int state)
    {
        if (o.initSet.Keys.Contains(state))
            return;
        if (isSubgoal(state))
            return;
        Q[state].Actions.Add(new Action(o));
        for (int i = 0; i < Model[state].Count; i++)
        {
            if (Model[state][i] != null && Q[state].Actions[i].O == null)
            {
                o.AddSASp(state, i, Model[state][i].sP);
                constOptionN(subgoals, s, o, Model[state][i].sP);
            }
        }

    }

    void OptionLearn(List<OptionN> Options)
    {
        {
            m_optsLearned = true;
            for (int i = 0; i < Options.Count; i++)
            {
                Options[i].Learn();
            }
            //for (int i = 0; i < Qoptions.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Qoptions.GetLength(1); j++)
            //    {
            //        Qoptions[i, j] = StateOptions(i, j);
            //    }
            //}
        }
    }

    //void SearchInitiationSets(List<SubGoal> subgoals)
    //{
    //    int initSetID = 1;
    //    //Subgoal-lar herhangi bir initiation set'e dahil değiller ve hepsinden ayrı olduklarını belirtmek için
    //    //hangi sete dahil olduklarını belirten sayıyı -1 olarak tanımlıyoruz
    //    for (int i = 0; i < subgoals.Count; i++)
    //    {
    //        initiationSetMembers[subgoals[i].State] = -1;
    //    }


    //    //Her subgoal için etrafındaki alanlar araştırılacak
    //    for (int i = 0; i < subgoals.Count; i++)
    //    {
    //        //Subgoal'un bulunduğu state'deki her action için
    //        for (int j = 0; j < Model.States[subgoals[i].State].Count; j++)
    //        {
    //            //Modelde böyle bir state,action ikilisi bulunmuyorsa işlem yapma
    //            if (Model[subgoals[i].State, j] == null)
    //                continue;
    //            //Eğer modele göre bu state action ikilisi ile gidilen state daha bir
    //            //initiation set dahiline alınmamışsa;
    //            if (initiationSetMembers[Model[subgoals[i].State, j].sP] == 0)
    //            {
    //                //Bu state-den belirtilen subgoal a ulaşılabiliyorsa !!! (ki burada problem olabilir) !!! (tek yönlü olursa hiç j de çıkmayabilir)
    //                if (Model[Model[subgoals[i].State, j].sP].LeadsTo(subgoals[i].State))
    //                {
    //                    InitiationSet iSet = new InitiationSet(); //yeni bir initiation set oluştur
    //                    iSet.ID = initSetID;                      //stateleri artık bu ID ile işaretleyeceğiz
    //                    _SearchInitiationSets(Model[subgoals[i].State, j].sP, iSet); //bu initiation set için rekürsif olarak state aramaya başla


    //                    //Eğer initiationset içinde 1'den fazla state bulunuyorsa initiationsetlerin bulunduğu listeye ekle
    //                    if (iSet.States.Count > 0)
    //                    {
    //                        //iSet.States.Add(Model[subgoals[i].State]);
    //                        //iSet.Subgoals.Add(subgoals[i]);
    //                        initiationSets.Add(iSet); 
    //                    }

    //                    initSetID++;
    //                }
    //            }
    //        }
    //    }
    //}

    //void _SearchInitiationSets(int state, InitiationSet initSet)
    //{
    //    int initSetID = initSet.ID;
    //    ModelState m_s = new ModelState(state); //artık optionlar olmayacağı için her option için kendi modelleri oluşturulmalı
    //    initSet.States.Add(m_s);
    //    int n = initSet.States.Count - 1; //n = son elemanın indisi
    //    initiationSetMembers[state] = initSetID; //başka sete eklenmemesi için ID ile işaretleniyor

    //    //Eklelenen model state üzerinde temel eylemler eklenecek
    //    for (int j = 0; j < Model.States[state].Count; j++)
    //    {
    //        //Eğer seçilen eylem option ise eklenmeyecek
    //        if (Q[state].Actions[j].O != null)
    //            continue;

    //        //son girilen state e action ekleniyor
    //        initSet.States[n].Add(Model[state, j]);

    //        //bu action ile gidilen bir yer modelde belirtilmemişse aramaya devam etme
    //        if (Model[state, j] == null)
    //            continue;
            
    //        //gidilen yer bu sete ait değilse yani daha işaretlenmemişse
    //        if (initiationSetMembers[Model[state, j].sP] != initSetID)
    //        {
    //            if (initiationSetMembers[Model[state, j].sP] == -1)
    //            {
    //                for (int i = 0; i < initSet.States.Count; i++)
    //                    if (initSet.States[i].State == Model[state, j].sP)
    //                        goto label;

    //                ModelState ms = new ModelState(Model[state, j].sP);
    //                for (int i = 0; i < Model[ms.State].Count; i++)
    //                    if (Q[ms.State].Actions[i].O == null && Model[ms.State,i] != null)
    //                        ms.Add(new ModelValue(ms.State, i, Model[ms.State, i].sP, 0, 1));

    //                initSet.States.Add(ms);
    //                initSet.Subgoals.Add(getSubgoalFromState(Model[state, j].sP));
    //            }
    //        label:
    //            if (initiationSetMembers[Model[state, j].sP] != -1)
    //                _SearchInitiationSets(Model[state, j].sP, initSet);
    //        }
    //    }
    //}

    SubGoal getSubgoalFromState(int state)
    {
        for (int i = 0; i < Subgoals.Count; i++)
        {
            if (Subgoals[i].State == state)
                return Subgoals[i];
        }
        throw new Exception("Subgoal error!");
    }

    //List<Option> CreateOptions()
    //{
    //    List<Option> options = new List<Option>();
    //    for(int i = 0; i < initiationSets.Count; i++)
    //    {
    //        for (int j = 0; j < initiationSets[i].Subgoals.Count; j++)
    //        {
    //            Option opt = new Option(initiationSets[i].Subgoals[j], initiationSets[i], this);
    //            options.Add(opt);
    //            for (int s = 0; s < initiationSets[i].States.Count; s++)
    //            {
    //                Q[initiationSets[i].States[s].State].Actions.Add(new Action(opt));
    //            }
    //        }
    //    }
    //    return options;
    //}

    public bool isSubgoal(int state)
    {
        lock (threadLock)
        { 
            for (int i = 0; i < Subgoals.Count; i++)
            {
                if (Subgoals[i].State == state)
                    return true;
            }
            return false;
        }
    }

    public double[] subgoalValues;
    //public double[] subgoalValues2;
    //public double[] bcValues;
    List<SubGoal> SearchSubgoals()
    {
        List<SubGoal> result = new List<SubGoal>();
        subgoalValues = new double[Model.States.Count];
        //subgoalValues2 = new double[Model.States.Count];
        //bcValues = new double[Model.States.Count];
        /////////////////////////--------------------------------------DEPTH FIRST SEARCH
        int depth = 3;
        for (int i = 0; i < Model.States.Count; i++) //Modeldeki tüm stateler için
        {
            if (Model.States[i] == null)
                continue;
            List<int> nodes = new List<int>();
            nodes.Add(i);
            for (int j = 0; j < Model.States[i].Count; j++) //Her yönde depth derinliğinde node-ları buluyor
            {
                if (Model.States[i][j] == null) continue;
                search(Model[i, j].sP, nodes, depth);
            }
            nodes.RemoveAt(0);
            int total = nodes.Count;
            double a = 0;
            while (nodes.Count > 0)
            {
                double temp = 0;
                search2(nodes[0], nodes, ref temp);
                a += Math.Pow(temp, 2);
            }
            if (total == 0)
                continue;
            subgoalValues[i] = 1 - (a / Math.Pow(total, 2));
        }
        ////////////////////////////----------------------------DEPTH FIRST SEARCH
            //betweenness centrality
            //double total = 0;
            //for (int u = 1; u < nodes.Count; u++)
            //{
            //    for (int t = 1; t < nodes.Count; t++)
            //    {
            //        if (u == t)
            //            continue;
            //        min = -1;
            //        int sigma = 0;
            //        int sigmaV = 0;
            //        b_cent_search(nodes.ToArray(), u, t, 0, ref sigma, ref sigmaV, 0, false);
            //        if (sigma > 0 || sigmaV > 0)
            //            total += sigmaV / (double)(sigma + sigmaV);
            //    }
            //}

            //    //subgoalValues[i] = total;

            //    ////betweenness centrality (tree)
            //    double total = 0;
            //    //for (int u = 1; u < nodes.Count; u++)
            //    //{
            //    //    for (int t = 1; t < nodes.Count; t++)
            //    //    {
            //    //        if (u == t)
            //    //            continue;
            //    //        int sigma = 0;
            //    //        int sigmaV = 0;
            //    //        Betweenness(nodes, nodes[u], nodes[t], nodes[0], ref sigma, ref sigmaV);
            //    //        if (sigma > 0 || sigmaV > 0)
            //    //            total += sigmaV / (double)(sigma + sigmaV);
            //    //    }
            //    //}

            //    //subgoalValues[i] = total;

        ////////////////////////////////




        //}
        ////////////////////////////-------------------------------------Brandes Betweenness Centrality
        //subgoalValues = BrandesBetweenness2();


        ///////////////////////visit sayıları
        //for (int i = 0; i < Model.StateCount; i++)
        //{
        //    if (Model[i] == null)
        //        continue;
        //    int total = 0;
        //    for (int a = 0; a < Model[i].Count; a++)
        //    {
        //        if (Model[i][a] == null)
        //            continue;
        //        total += Model[i][a].count;
        //    }
        //    subgoalValues2[i] = total;
            
        //}

        //for (int i = 0; i < Model.StateCount; i++)
        //{
        //    if (Model[i] == null)
        //        continue;
        //    double total = 0;
        //    double min = subgoalValues2[i];
        //    double max = subgoalValues2[i];
        //    for (int a = 0; a < Model[i].Count; a++)
        //    {
        //        if (Model[i][a] == null)
        //            continue;
        //        total += subgoalValues2[Model[i][a].sP];
        //        if (subgoalValues2[Model[i][a].sP] < min)
        //            min = subgoalValues2[Model[i][a].sP];
        //        if (subgoalValues2[Model[i][a].sP] > max)
        //            max = subgoalValues2[Model[i][a].sP];
        //    }
        //    total /= 4;
        //    subgoalValues[i] = subgoalValues2[i] / total;
        //}
        ////Visit sayıları ile

        //normalization
        double maxX = subgoalValues.Max();
        double minX = subgoalValues.Min();
        for (int j = 0; j < subgoalValues.Length; j++)
            subgoalValues[j] = (subgoalValues[j] - minX) / (maxX - minX);
        ////////// 

        ////////////////declare subgoals by subgoal values
        for (int N = 0; N < Model.States.Count; N++)
        {
            bool isGoal = true;
            if (Model.States[N] == null)
                continue;
            for (int t = 0; t < Model.States[N].Count; t++)
            {
                //Subgoal olma değeri etrafındakilerden düşük veya aynıysa subgoal değil
                if (Model[N, t] != null && Model[N, t].sP != N && subgoalValues[Model[N, t].sP] >= subgoalValues[N])
                {
                    isGoal = false;
                    break;
                }
            }

            if (isGoal)
                result.Add(new SubGoal(N));
        }
        return result;
    }


    double[] BrandesBetweenness2()
    {
        double[] Cb = new double[Model.StateCount];
        Queue<int> Q = new Queue<int>();
        Stack<int> S = new Stack<int>();
        int[] dist = new int[Model.StateCount];
        List<int>[] Pred = new List<int>[Model.StateCount];
        int[] sig = new int[Model.StateCount];
        double[] delta = new double[Model.StateCount];

        for (int s = 0; s < Model.StateCount; s++)
        {
            if (Model[s] == null) continue;

            //init
            for (int w = 0; w < Model.StateCount; w++)
                Pred[w] = new List<int>();
            for (int t = 0; t < Model.StateCount; t++)
            {
                dist[t] = int.MaxValue;
                sig[t] = 0;
            }
            dist[s] = 0;
            sig[s] = 1;
            Q.Enqueue(s);

            while (Q.Count > 0)
            {
                int v = Q.Dequeue();
                S.Push(v);
                for (int a = 0; a < Model[v].Count; a++)
                {
                    if (Model[v] == null || Model[v][a] == null)
                        continue;
                    int w = Model[v][a].sP;
                    if (Model[Model[v][a].sP] == null)
                        continue;
                    if (dist[w] == int.MaxValue)
                    {
                        dist[w] = dist[v] + 1;
                        Q.Enqueue(w);
                    }

                    if (dist[w] == dist[v] + 1)
                    {
                        sig[w] = sig[w] + sig[v];
                        Pred[w].Add(v);
                    }
                }
            }

            for (int v = 0; v < Model.StateCount; v++)
                delta[v] = 0;
            while (S.Count > 0)
            {
                int w = S.Pop();
                foreach (int v in Pred[w])
                {
                    delta[v] = delta[v] + (sig[v] / (double)sig[w]) * (1 + delta[w]);
                }
                if (w != s)
                    Cb[w] = Cb[w] + delta[w];
            }

        }
        return Cb;
    }

    double[] BrandesBetweenness()
    {
        double[] Cb = new double[Model.StateCount];
        for (int s = 0; s < Model.StateCount; s++)
        {
            if (Model[s] == null)
                continue;
            Stack<int> S = new Stack<int>();
            List<int> P = new List<int>();
            int[] sig = new int[Model.StateCount];
            sig[s] = 1;
            int[] d = new int[Model.StateCount];
            for (int i = 0; i < d.Length; i++) 
                d[i] = -1;
            d[s] = 0;

            Queue<int> Q = new Queue<int>();
            Q.Enqueue(s);
            while (Q.Count > 0)
            {
                int v = Q.Dequeue();
                S.Push(v);
                for (int a = 0; a < Model[s].Count; a++)
                {
                    if (Model[s] == null || Model[s][a] == null)
                        continue;
                    int w = Model[s][a].sP;
                    if (d[w] < 0)
                    {
                        Q.Enqueue(w);
                        d[w] = d[v] + 1;
                    }

                    if (d[w] == d[v] + 1)
                    {
                        sig[w] = sig[w] + sig[v];
                        P.Add(v);
                    }
                }
            }

            double[] dt = new double[Model.StateCount];
            while (S.Count > 0)
            {
                int w = S.Pop();
                for (int v = 0; v < P.Count; v++)
                    dt[v] = dt[v] + (sig[v] / (double)sig[w]) * (1 + dt[w]);
                if (w != s)
                    Cb[w] = Cb[w] + dt[w];
            }
        }
        return Cb;
    }

    void Betweenness(List<int> nodes, int u, int t, int v, ref int sigma, ref int sigmaV)
    {
        sigma = 0;
        sigmaV = 0;
        TreeNode start = new TreeNode(null, u);
        Queue<TreeNode> Q = new Queue<TreeNode>();
        Q.Enqueue(start);
        bool fin = false;
        while (Q.Count > 0)
        {
            TreeNode s = Q.Dequeue();
            for (int j = 0; j < Model.States[s.Value].Count; j++)
            {
                if(Model[s.Value][j] == null)
                    continue;
                int sp = Model[s.Value][j].sP;
                if (nodes.Contains(sp) && !s.FamilyContains(sp))
                {
                    TreeNode temp = new TreeNode(s, sp);
                    if (sp == v)
                        temp.v = true;
                    Q.Enqueue(temp);

                    if (sp == t)
                        fin = true;
                }
            }
            if (fin)
                break;
        }

        if (fin)
        {
            while (Q.Count > 0)
            {
                TreeNode temp = Q.Dequeue();
                if (temp.Value == t)
                {
                    if (temp.v)
                    {
                        sigmaV++;
                        sigma++;
                    }
                    else
                        sigma++;
                }
            }
        }
    }

    int min = -1;
    void b_cent_search(int[] nodes, int u, int t, int v, ref int sigma, ref int sigmaV, int len,bool passV)
    {
        if (min > 0 && min < len)
        {
            return;
        }

        if (u == t)
        {
            if (min < 0) min = len;
            if (min > len)
            {
                sigma = 0;
                sigmaV = 0;
                min = len;
            }

            if (passV)
                sigmaV++;
            else
                sigma++;
            return;
        }

        if (u == v)
        {
            passV = true;
        }

        int[] nnodes = (int[])nodes.Clone();
        int currentState = nnodes[u];
        nnodes[u] = -1;
        for (int i = 0; i < Model.States[currentState].Count; i++)
        {
            if (Model[currentState][i] != null && nnodes.Contains(Model[currentState][i].sP))
            {
                b_cent_search(nnodes, IndexOf(nnodes, Model[currentState][i].sP), t, v,ref sigma,ref sigmaV, len + 1,passV);
            }
        }
    }

    int IndexOf(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == value)
                return i;
        return -1;
    }

    void search2(int state, List<int> nodes, ref double a)
    {
        if (!nodes.Contains(state)) //Düğüm önceden işlenmişse devam etme
            return;
        nodes.Remove(state);        //Düğümün tekrar hesaplanmasını engellemek için listeden çıkar
        if (Model.States[state] == null)
            return;
        a++;
        for (int i = 0; i < Model.States[state].Count; i++)
        {
            if (Model.States[state][i] == null || Model[state, i].sP == state)
                continue;
            search2(Model[state, i].sP, nodes, ref a);
        }
    }

    void search(int state, List<int> nodes, int depth)
    {
        if (depth <= 0)
            return;
        if(!nodes.Contains(state))
            nodes.Add(state);
        
        if (Model.States[state] == null)
            return;
        for (int i = 0; i < Model.States[state].Count; i++)
        {
            if (Model.States[state][i] == null || Model[state, i].sP == state)
                continue;
            search(Model[state, i].sP, nodes, depth - 1);
        }
    }

    
    private int e_greedy(State s)
    {
        double val = rand.NextDouble();
        //Thread.Sleep(500);
        if (val < e)         //Exploration
            return s.GetRandomAction(s.GetMaxAction());
        else
            return s.GetMaxAction();
    }

    double Tau = 0;
    private int softmax(State s)
    {
        double total = 0;
        for (int i = 0; i < s.Actions.Count; i++)
        {
            total += Math.Pow(Math.E,s[i]/0.1);
        }


        double th = 0;
        double r = rand.NextDouble();
        for (int i = 0; i < s.Actions.Count; i++)
        {
            th += Math.Pow(Math.E, s[i]/0.1) / total;
            if (r <= th)
                return i;
        }
        return 0;
    }

    Dictionary<int, int> visit = new Dictionary<int, int>();
    double ri = 0.1; //relative importance
    private int softmaxWVisits(State s)
    {
        if (!visit.Keys.Contains(s.StateID))
            visit.Add(s.StateID, 1);
        else
            visit[s.StateID] += 1;

        double total = 0;
        double totalVisits = 0;
        for (int i = 0; i < s.Actions.Count; i++)
        {
            if(Model[s.StateID] !=null && Model[s.StateID][i] != null)
            {
                if (!visit.ContainsKey(Model[s.StateID][i].sP))
                    visit.Add(Model[s.StateID][i].sP, 0);
                totalVisits += visit[Model[s.StateID][i].sP];
            }
                
            total += Math.Pow(Math.E, s[i] / 0.1);
        }
        double tmp = 0;
        for (int i = 0; i < s.Actions.Count; i++)
        {
            if (Model[s.StateID] != null)
                if (Model[s.StateID][i] != null)
                    tmp += totalVisits - visit[Model[s.StateID][i].sP];
                else
                    tmp += totalVisits - 0;
        }


        double th = 0;
        int max = 0;
        double maxV = 0;
        double r = rand.NextDouble() * 2;
        for (int i = 0; i < s.Actions.Count; i++)
        {
            double val = (1-ri) * Math.Pow(Math.E, s[i] / 0.1) / total + (ri) * (Model[s.StateID] != null && Model[s.StateID][i] != null ? ((totalVisits - visit[Model[s.StateID][i].sP]) / tmp) : totalVisits / tmp);
            th += val;
            if (val > maxV)
            {
                maxV = val;
                max = i;
            }
            //if (r <= th)
            //    return i;
        }
        return max;
    }

    void InsertQueue(List<PriorityObj> Queue, int s, int a, double p)
    {
        for (int i = 0; i < Queue.Count; i++)
        {
            if (s == Queue[i].State &&
                a == Queue[i].Action)
            {
                if (Queue[i].Priority > p)
                    return;
                else
                    Queue.Remove(Queue[i]);
            }
        }
        for (int i = 0; i < Queue.Count; i++)
        {
            if (Queue[i].Priority < p)
            {
                Queue.Insert(i, new PriorityObj(s, a, p));
                return;
            }
        }
        Queue.Add(new PriorityObj(s, a, p));
    }

    bool inopt = false;
    public bool executeAction(int s, int a, ref int sP, ref double r, ref int steps)
    {
        bool isContextChanged = false;
        State st = Q[s];
        if (st.Actions[a].O != null)
        {
            if (inopt)
                throw new Exception("Called an option in an option!");
            inopt = true;
            st.Actions[a].O.Execute(s, ref sP, ref steps);
            inopt = false;
        }
        else
        {
            steps++;
            environment.ExecuteAction(s , a, ref sP, ref r);
            isContextChanged = checkAndSwitchContext(s, a, sP, r);
        }
        return isContextChanged;
    }

    public bool executeActionWS(int s, int a, ref int sP, ref double r, ref int steps)
    {
        bool isContextChanged = false;
        State st = Q[s];
        if (st.Actions[a].O != null)
        {
            if (inopt)
                throw new Exception("Called an option in an option!");
            inopt = true;
            st.Actions[a].O.Execute(s, ref sP, ref steps);
            inopt = false;
        }
        else
        {
            environment.ExecuteAction(s, a, ref sP, ref r);
            isContextChanged = checkAndSwitchContext(s, a, sP, r);
        }
        Thread.Sleep(100);
        return isContextChanged;
    }

//    public Microsoft.Glee.Drawing.Graph ModelToGLEEGraph()
//    {
//        Microsoft.Glee.Drawing.Graph gr = new Microsoft.Glee.Drawing.Graph("Test");
//        for (int i = 0; i < Model.States.Count; i++)
//        {
//            if (Model.States[i] == null) continue;
//            gr.AddNode(i.ToString());
//        }
//        for (int i = 0; i < Model.States.Count; i++)
//        {
//            if (Model.States[i] == null) continue;
//            for (int j = 0; j < Model.States[i].Count; j++)
//            {
//                if (Model[i, j] != null && Model[i,j].s != Model[i,j].sP)
//                {
//                    bool exists = false;
//                    for (int k = 0; k < gr.Edges.Count; k++)
//                    {
//                        if(gr.Edges[k].Source == Model[i, j].sP.ToString() &&
//                            gr.Edges[k].Target == Model[i, j].s.ToString()){
//                                gr.Edges[k].Attr.ArrowHeadAtSource = Microsoft.Glee.Drawing.ArrowStyle.Normal;
//                            exists = true;
//                            break;
//                        }
//                    }
//                    if(!exists)
//                        gr.AddEdge(Model[i, j].s.ToString(), Model[i, j].sP.ToString());
//                }
//            }
//        }
//        return gr;
//    }
}

class PriorityObj
{
    public int State;
    public double Priority;
    public int Action;

    public PriorityObj(int State, int Action, double Priority)
    {
        this.State = State;
        this.Action = Action;
        this.Priority = Priority;
    }
}
