namespace RL_smdp
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.m_buttonLearn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_buttonBatch = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.m_txtstopepisode = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.m_buttonDown = new System.Windows.Forms.Button();
            this.m_buttonRight = new System.Windows.Forms.Button();
            this.m_labelInfo = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.m_buttonStop = new System.Windows.Forms.Button();
            this.m_buttonR = new System.Windows.Forms.Button();
            this.m_buttonInv = new System.Windows.Forms.Button();
            this.m_textMapsSize = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_textRho = new System.Windows.Forms.TextBox();
            this.m_textEmin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_checkDetectCS = new System.Windows.Forms.CheckBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.m_buttonL = new System.Windows.Forms.Button();
            this.m_buttonNone = new System.Windows.Forms.Button();
            this.m_buttonUp = new System.Windows.Forms.Button();
            this.m_buttonActSwitch = new System.Windows.Forms.Button();
            this.m_buttonSave = new System.Windows.Forms.Button();
            this.m_buttonLoad = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.m_buttonAddGrid = new System.Windows.Forms.Button();
            this.m_labelLoc = new System.Windows.Forms.Label();
            this.m_checkDisableOpt = new System.Windows.Forms.CheckBox();
            this.m_buttonRun = new System.Windows.Forms.Button();
            this.m_buttonSetGoalLoc = new System.Windows.Forms.Button();
            this.m_buttonSetAgentLoc = new System.Windows.Forms.Button();
            this.m_buttonAddBlock = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.m_buttonLeft = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_graphicModel = new System.Windows.Forms.PictureBox();
            this.m_gridWorld = new GridWorld();
            this.gridList1 = new GridList();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_graphicModel)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Reset Agent";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_buttonLearn
            // 
            this.m_buttonLearn.Location = new System.Drawing.Point(124, 8);
            this.m_buttonLearn.Name = "m_buttonLearn";
            this.m_buttonLearn.Size = new System.Drawing.Size(61, 23);
            this.m_buttonLearn.TabIndex = 4;
            this.m_buttonLearn.Text = "Learn";
            this.m_buttonLearn.UseVisualStyleBackColor = true;
            this.m_buttonLearn.Click += new System.EventHandler(this.m_buttonLearn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_buttonBatch);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.m_txtstopepisode);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.m_buttonDown);
            this.panel1.Controls.Add(this.m_buttonRight);
            this.panel1.Controls.Add(this.m_labelInfo);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.m_buttonStop);
            this.panel1.Controls.Add(this.m_buttonR);
            this.panel1.Controls.Add(this.m_buttonInv);
            this.panel1.Controls.Add(this.m_textMapsSize);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.m_checkDetectCS);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.m_buttonL);
            this.panel1.Controls.Add(this.m_buttonNone);
            this.panel1.Controls.Add(this.m_buttonUp);
            this.panel1.Controls.Add(this.m_buttonActSwitch);
            this.panel1.Controls.Add(this.m_buttonSave);
            this.panel1.Controls.Add(this.m_buttonLoad);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.m_buttonAddGrid);
            this.panel1.Controls.Add(this.m_labelLoc);
            this.panel1.Controls.Add(this.m_checkDisableOpt);
            this.panel1.Controls.Add(this.m_buttonRun);
            this.panel1.Controls.Add(this.m_buttonSetGoalLoc);
            this.panel1.Controls.Add(this.m_buttonSetAgentLoc);
            this.panel1.Controls.Add(this.m_buttonAddBlock);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.m_buttonLearn);
            this.panel1.Controls.Add(this.m_buttonLeft);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(643, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 535);
            this.panel1.TabIndex = 5;
            // 
            // m_buttonBatch
            // 
            this.m_buttonBatch.Location = new System.Drawing.Point(191, 8);
            this.m_buttonBatch.Name = "m_buttonBatch";
            this.m_buttonBatch.Size = new System.Drawing.Size(45, 23);
            this.m_buttonBatch.TabIndex = 34;
            this.m_buttonBatch.Text = "Batch";
            this.m_buttonBatch.UseVisualStyleBackColor = true;
            this.m_buttonBatch.Click += new System.EventHandler(this.m_buttonBatch_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(101, 265);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 23);
            this.button4.TabIndex = 33;
            this.button4.Text = "AllX";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Export Model";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Stop learn as episode:";
            // 
            // m_txtstopepisode
            // 
            this.m_txtstopepisode.Location = new System.Drawing.Point(132, 90);
            this.m_txtstopepisode.Name = "m_txtstopepisode";
            this.m_txtstopepisode.Size = new System.Drawing.Size(100, 20);
            this.m_txtstopepisode.TabIndex = 30;
            this.m_txtstopepisode.Text = "0";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(135, 67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = "Auto-stop learn";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // m_buttonDown
            // 
            this.m_buttonDown.Location = new System.Drawing.Point(26, 322);
            this.m_buttonDown.Name = "m_buttonDown";
            this.m_buttonDown.Size = new System.Drawing.Size(49, 23);
            this.m_buttonDown.TabIndex = 18;
            this.m_buttonDown.Text = "v";
            this.m_buttonDown.UseVisualStyleBackColor = true;
            this.m_buttonDown.Click += new System.EventHandler(this.m_buttonDown_Click);
            // 
            // m_buttonRight
            // 
            this.m_buttonRight.Location = new System.Drawing.Point(74, 278);
            this.m_buttonRight.Name = "m_buttonRight";
            this.m_buttonRight.Size = new System.Drawing.Size(21, 45);
            this.m_buttonRight.TabIndex = 18;
            this.m_buttonRight.Text = ">";
            this.m_buttonRight.UseVisualStyleBackColor = true;
            this.m_buttonRight.Click += new System.EventHandler(this.m_buttonRight_Click);
            // 
            // m_labelInfo
            // 
            this.m_labelInfo.AutoSize = true;
            this.m_labelInfo.Location = new System.Drawing.Point(147, 444);
            this.m_labelInfo.Name = "m_labelInfo";
            this.m_labelInfo.Size = new System.Drawing.Size(58, 13);
            this.m_labelInfo.TabIndex = 28;
            this.m_labelInfo.Text = "-----------------";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(12, 439);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(95, 23);
            this.button11.TabIndex = 27;
            this.button11.Text = "E Chart";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // m_buttonStop
            // 
            this.m_buttonStop.Location = new System.Drawing.Point(124, 38);
            this.m_buttonStop.Name = "m_buttonStop";
            this.m_buttonStop.Size = new System.Drawing.Size(112, 23);
            this.m_buttonStop.TabIndex = 26;
            this.m_buttonStop.Text = "Force Stop Learn";
            this.m_buttonStop.UseVisualStyleBackColor = true;
            this.m_buttonStop.Click += new System.EventHandler(this.m_buttonStop_Click);
            // 
            // m_buttonR
            // 
            this.m_buttonR.Location = new System.Drawing.Point(50, 300);
            this.m_buttonR.Name = "m_buttonR";
            this.m_buttonR.Size = new System.Drawing.Size(25, 23);
            this.m_buttonR.TabIndex = 25;
            this.m_buttonR.Text = "R";
            this.m_buttonR.UseVisualStyleBackColor = true;
            this.m_buttonR.Click += new System.EventHandler(this.m_buttonR_Click);
            // 
            // m_buttonInv
            // 
            this.m_buttonInv.Location = new System.Drawing.Point(50, 278);
            this.m_buttonInv.Name = "m_buttonInv";
            this.m_buttonInv.Size = new System.Drawing.Size(25, 23);
            this.m_buttonInv.TabIndex = 25;
            this.m_buttonInv.Text = "x";
            this.m_buttonInv.UseVisualStyleBackColor = true;
            this.m_buttonInv.Click += new System.EventHandler(this.button11_Click);
            // 
            // m_textMapsSize
            // 
            this.m_textMapsSize.Location = new System.Drawing.Point(12, 354);
            this.m_textMapsSize.Name = "m_textMapsSize";
            this.m_textMapsSize.Size = new System.Drawing.Size(100, 20);
            this.m_textMapsSize.TabIndex = 24;
            this.m_textMapsSize.Text = "20";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(114, 352);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(110, 23);
            this.button10.TabIndex = 23;
            this.button10.Text = "Set Map Size";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.m_textRho);
            this.groupBox1.Controls.Add(this.m_textEmin);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 48);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RL-CD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Rho";
            // 
            // m_textRho
            // 
            this.m_textRho.Location = new System.Drawing.Point(145, 19);
            this.m_textRho.Name = "m_textRho";
            this.m_textRho.Size = new System.Drawing.Size(62, 20);
            this.m_textRho.TabIndex = 2;
            this.m_textRho.Text = "0,1";
            // 
            // m_textEmin
            // 
            this.m_textEmin.Location = new System.Drawing.Point(49, 20);
            this.m_textEmin.Name = "m_textEmin";
            this.m_textEmin.Size = new System.Drawing.Size(58, 20);
            this.m_textEmin.TabIndex = 1;
            this.m_textEmin.Text = "-0,4";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Emin";
            // 
            // m_checkDetectCS
            // 
            this.m_checkDetectCS.AutoSize = true;
            this.m_checkDetectCS.Location = new System.Drawing.Point(12, 153);
            this.m_checkDetectCS.Name = "m_checkDetectCS";
            this.m_checkDetectCS.Size = new System.Drawing.Size(173, 17);
            this.m_checkDetectCS.TabIndex = 21;
            this.m_checkDetectCS.Text = "Detect context changes on run";
            this.m_checkDetectCS.UseVisualStyleBackColor = true;
            this.m_checkDetectCS.CheckedChanged += new System.EventHandler(this.m_checkDetectCS_CheckedChanged);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(5, 231);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(107, 23);
            this.button9.TabIndex = 20;
            this.button9.Text = "Option Details";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(86, 323);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(61, 23);
            this.button8.TabIndex = 19;
            this.button8.Text = "Stop Edit";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // m_buttonL
            // 
            this.m_buttonL.Location = new System.Drawing.Point(26, 300);
            this.m_buttonL.Name = "m_buttonL";
            this.m_buttonL.Size = new System.Drawing.Size(25, 23);
            this.m_buttonL.TabIndex = 18;
            this.m_buttonL.Text = "L";
            this.m_buttonL.UseVisualStyleBackColor = true;
            this.m_buttonL.Click += new System.EventHandler(this.m_buttonL_Click);
            // 
            // m_buttonNone
            // 
            this.m_buttonNone.Location = new System.Drawing.Point(26, 278);
            this.m_buttonNone.Name = "m_buttonNone";
            this.m_buttonNone.Size = new System.Drawing.Size(25, 23);
            this.m_buttonNone.TabIndex = 18;
            this.m_buttonNone.Text = "o";
            this.m_buttonNone.UseVisualStyleBackColor = true;
            this.m_buttonNone.Click += new System.EventHandler(this.m_buttonNone_Click);
            // 
            // m_buttonUp
            // 
            this.m_buttonUp.Location = new System.Drawing.Point(26, 258);
            this.m_buttonUp.Name = "m_buttonUp";
            this.m_buttonUp.Size = new System.Drawing.Size(49, 21);
            this.m_buttonUp.TabIndex = 18;
            this.m_buttonUp.Text = "^";
            this.m_buttonUp.UseVisualStyleBackColor = true;
            this.m_buttonUp.Click += new System.EventHandler(this.m_buttonUp_Click);
            // 
            // m_buttonActSwitch
            // 
            this.m_buttonActSwitch.Location = new System.Drawing.Point(5, 124);
            this.m_buttonActSwitch.Name = "m_buttonActSwitch";
            this.m_buttonActSwitch.Size = new System.Drawing.Size(107, 23);
            this.m_buttonActSwitch.TabIndex = 17;
            this.m_buttonActSwitch.Text = "Switch Actions";
            this.m_buttonActSwitch.UseVisualStyleBackColor = true;
            this.m_buttonActSwitch.Click += new System.EventHandler(this.m_buttonActSwitch_Click);
            // 
            // m_buttonSave
            // 
            this.m_buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_buttonSave.Location = new System.Drawing.Point(161, 468);
            this.m_buttonSave.Name = "m_buttonSave";
            this.m_buttonSave.Size = new System.Drawing.Size(75, 23);
            this.m_buttonSave.TabIndex = 16;
            this.m_buttonSave.Text = "Save Map";
            this.m_buttonSave.UseVisualStyleBackColor = true;
            this.m_buttonSave.Click += new System.EventHandler(this.m_buttonSave_Click);
            // 
            // m_buttonLoad
            // 
            this.m_buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_buttonLoad.Location = new System.Drawing.Point(83, 468);
            this.m_buttonLoad.Name = "m_buttonLoad";
            this.m_buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.m_buttonLoad.TabIndex = 16;
            this.m_buttonLoad.Text = "Load Map";
            this.m_buttonLoad.UseVisualStyleBackColor = true;
            this.m_buttonLoad.Click += new System.EventHandler(this.m_buttonLoad_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(113, 412);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(83, 497);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 14;
            this.button7.Text = "Load List";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(161, 497);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 14;
            this.button6.Text = "Save List";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // m_buttonAddGrid
            // 
            this.m_buttonAddGrid.Location = new System.Drawing.Point(6, 497);
            this.m_buttonAddGrid.Name = "m_buttonAddGrid";
            this.m_buttonAddGrid.Size = new System.Drawing.Size(75, 23);
            this.m_buttonAddGrid.TabIndex = 13;
            this.m_buttonAddGrid.Text = "Add";
            this.m_buttonAddGrid.UseVisualStyleBackColor = true;
            this.m_buttonAddGrid.Click += new System.EventHandler(this.m_buttonAddGrid_Click);
            // 
            // m_labelLoc
            // 
            this.m_labelLoc.AutoSize = true;
            this.m_labelLoc.Location = new System.Drawing.Point(119, 236);
            this.m_labelLoc.Name = "m_labelLoc";
            this.m_labelLoc.Size = new System.Drawing.Size(51, 13);
            this.m_labelLoc.TabIndex = 12;
            this.m_labelLoc.Text = "Location:";
            // 
            // m_checkDisableOpt
            // 
            this.m_checkDisableOpt.AutoSize = true;
            this.m_checkDisableOpt.Location = new System.Drawing.Point(19, 66);
            this.m_checkDisableOpt.Name = "m_checkDisableOpt";
            this.m_checkDisableOpt.Size = new System.Drawing.Size(100, 17);
            this.m_checkDisableOpt.TabIndex = 11;
            this.m_checkDisableOpt.Text = "Disable Options";
            this.m_checkDisableOpt.UseVisualStyleBackColor = true;
            this.m_checkDisableOpt.CheckedChanged += new System.EventHandler(this.m_checkDisableOpt_CheckedChanged);
            // 
            // m_buttonRun
            // 
            this.m_buttonRun.Location = new System.Drawing.Point(117, 124);
            this.m_buttonRun.Name = "m_buttonRun";
            this.m_buttonRun.Size = new System.Drawing.Size(107, 23);
            this.m_buttonRun.TabIndex = 10;
            this.m_buttonRun.Text = "Run";
            this.m_buttonRun.UseVisualStyleBackColor = true;
            this.m_buttonRun.Click += new System.EventHandler(this.m_buttonRun_Click);
            // 
            // m_buttonSetGoalLoc
            // 
            this.m_buttonSetGoalLoc.Location = new System.Drawing.Point(152, 323);
            this.m_buttonSetGoalLoc.Name = "m_buttonSetGoalLoc";
            this.m_buttonSetGoalLoc.Size = new System.Drawing.Size(75, 23);
            this.m_buttonSetGoalLoc.TabIndex = 9;
            this.m_buttonSetGoalLoc.Text = "Set Goal Location";
            this.m_buttonSetGoalLoc.UseVisualStyleBackColor = true;
            this.m_buttonSetGoalLoc.Click += new System.EventHandler(this.m_buttonSetGoalLoc_Click);
            // 
            // m_buttonSetAgentLoc
            // 
            this.m_buttonSetAgentLoc.Location = new System.Drawing.Point(152, 294);
            this.m_buttonSetAgentLoc.Name = "m_buttonSetAgentLoc";
            this.m_buttonSetAgentLoc.Size = new System.Drawing.Size(75, 23);
            this.m_buttonSetAgentLoc.TabIndex = 9;
            this.m_buttonSetAgentLoc.Text = "Set Agent Location";
            this.m_buttonSetAgentLoc.UseVisualStyleBackColor = true;
            this.m_buttonSetAgentLoc.Click += new System.EventHandler(this.m_buttonSetAgentLoc_Click);
            // 
            // m_buttonAddBlock
            // 
            this.m_buttonAddBlock.Location = new System.Drawing.Point(152, 265);
            this.m_buttonAddBlock.Name = "m_buttonAddBlock";
            this.m_buttonAddBlock.Size = new System.Drawing.Size(75, 23);
            this.m_buttonAddBlock.TabIndex = 9;
            this.m_buttonAddBlock.Text = "Edit Blocks";
            this.m_buttonAddBlock.UseVisualStyleBackColor = true;
            this.m_buttonAddBlock.Click += new System.EventHandler(this.m_buttonAddBlock_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(11, 37);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(108, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "One Episode";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 410);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Chart";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_buttonLeft
            // 
            this.m_buttonLeft.Location = new System.Drawing.Point(6, 278);
            this.m_buttonLeft.Name = "m_buttonLeft";
            this.m_buttonLeft.Size = new System.Drawing.Size(21, 45);
            this.m_buttonLeft.TabIndex = 18;
            this.m_buttonLeft.Text = "<";
            this.m_buttonLeft.UseVisualStyleBackColor = true;
            this.m_buttonLeft.Click += new System.EventHandler(this.m_buttonLeft_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_gridWorld);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_graphicModel);
            this.splitContainer1.Size = new System.Drawing.Size(643, 452);
            this.splitContainer1.SplitterDistance = 321;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_graphicModel
            // 
            this.m_graphicModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_graphicModel.Location = new System.Drawing.Point(0, 0);
            this.m_graphicModel.Name = "m_graphicModel";
            this.m_graphicModel.Size = new System.Drawing.Size(318, 452);
            this.m_graphicModel.TabIndex = 6;
            this.m_graphicModel.TabStop = false;
            this.m_graphicModel.MouseEnter += new System.EventHandler(this.m_graphicModel_MouseEnter);
            this.m_graphicModel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_graphicModel_MouseMove);
            // 
            // m_gridWorld
            // 
            this.m_gridWorld.CheeseLocation = new System.Drawing.Point(9, 9);
            this.m_gridWorld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridWorld.GridList = this.gridList1;
            this.m_gridWorld.GridSize = 10;
            this.m_gridWorld.Location = new System.Drawing.Point(0, 0);
            this.m_gridWorld.MouseLocation = new System.Drawing.Point(0, 0);
            this.m_gridWorld.Name = "m_gridWorld";
            this.m_gridWorld.Pointer = new System.Drawing.Point(0, 0);
            this.m_gridWorld.ShowArrows = true;
            this.m_gridWorld.Simulate = false;
            this.m_gridWorld.Size = new System.Drawing.Size(321, 452);
            this.m_gridWorld.State = 0;
            this.m_gridWorld.TabIndex = 2;
            this.m_gridWorld.StepPassed += new OneStepTakenDelegate(this.m_gridWorld_StepPassed);
            this.m_gridWorld.Load += new System.EventHandler(this.m_gridWorld_Load);
            this.m_gridWorld.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_gridWorld_MouseClick);
            this.m_gridWorld.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_gridWorld_MouseDown);
            this.m_gridWorld.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_gridWorld_MouseMove);
            this.m_gridWorld.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_gridWorld_MouseUp);
            // 
            // gridList1
            // 
            this.gridList1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridList1.Location = new System.Drawing.Point(0, 452);
            this.gridList1.Name = "gridList1";
            this.gridList1.SelectedIndex = -1;
            this.gridList1.Size = new System.Drawing.Size(643, 83);
            this.gridList1.TabIndex = 7;
            this.gridList1.SelectionMade += new Selection(this.gridList1_SelectionMade);
            this.gridList1.Load += new System.EventHandler(this.gridList1_Load);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 535);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.gridList1);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "HRL on NS Env";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_graphicModel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridWorld m_gridWorld;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button m_buttonLearn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox m_graphicModel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button m_buttonAddBlock;
        private System.Windows.Forms.Button m_buttonRun;
        private System.Windows.Forms.CheckBox m_checkDisableOpt;
        private System.Windows.Forms.Label m_labelLoc;
        private GridList gridList1;
        private System.Windows.Forms.Button m_buttonAddGrid;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button m_buttonLoad;
        private System.Windows.Forms.Button m_buttonActSwitch;
        private System.Windows.Forms.Button m_buttonLeft;
        private System.Windows.Forms.Button m_buttonNone;
        private System.Windows.Forms.Button m_buttonDown;
        private System.Windows.Forms.Button m_buttonUp;
        private System.Windows.Forms.Button m_buttonRight;
        private System.Windows.Forms.Button m_buttonSave;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button m_buttonSetGoalLoc;
        private System.Windows.Forms.Button m_buttonSetAgentLoc;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_checkDetectCS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_textRho;
        private System.Windows.Forms.TextBox m_textEmin;
        private System.Windows.Forms.TextBox m_textMapsSize;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button m_buttonInv;
        private System.Windows.Forms.Button m_buttonStop;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label m_labelInfo;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_txtstopepisode;
        private System.Windows.Forms.Button m_buttonR;
        private System.Windows.Forms.Button m_buttonL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button m_buttonBatch;
    }
}

