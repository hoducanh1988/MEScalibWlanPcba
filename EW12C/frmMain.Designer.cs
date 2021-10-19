namespace EW12C {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.buttonOpenTree = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTestTree2G = new System.Windows.Forms.Label();
            this.buttonRunTree = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonRunLastFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonClearText = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panel_DebugView = new System.Windows.Forms.Panel();
            this.btnTinhSuyHao = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rtbCOMPort = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbRunTestAll = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_disconnect = new System.Windows.Forms.Button();
            this.checkBox_ViewDebug = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qSPRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lbl_comport = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lb_statusconnect = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblTestTree5G = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbl_MacEthernet = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_Calib2G = new System.Windows.Forms.Label();
            this.lbl_SwitchMode2G = new System.Windows.Forms.Label();
            this.lbl_Calib5G = new System.Windows.Forms.Label();
            this.lbl_SwitchMode5G = new System.Windows.Forms.Label();
            this.lbl_PingNewIP = new System.Windows.Forms.Label();
            this.lbl_ChangeIP = new System.Windows.Forms.Label();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.lbl_Calib2GTime = new System.Windows.Forms.Label();
            this.lbl_SwitchMode2GTime = new System.Windows.Forms.Label();
            this.lbl_Calib5GTime = new System.Windows.Forms.Label();
            this.lbl_SwitchMode5GTime = new System.Windows.Forms.Label();
            this.lbl_PingNewIPTime = new System.Windows.Forms.Label();
            this.lbl_ChangeIPTime = new System.Windows.Forms.Label();
            this.lbl_LoginTime = new System.Windows.Forms.Label();
            this.lbl_WaitBootTime = new System.Windows.Forms.Label();
            this.lbl_OpenComTime = new System.Windows.Forms.Label();
            this.lbl_WaitBoot = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl_OpenCom = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.panel_DebugView.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenTree
            // 
            this.buttonOpenTree.Location = new System.Drawing.Point(267, 494);
            this.buttonOpenTree.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpenTree.Name = "buttonOpenTree";
            this.buttonOpenTree.Size = new System.Drawing.Size(89, 28);
            this.buttonOpenTree.TabIndex = 0;
            this.buttonOpenTree.Text = "Open Tree";
            this.buttonOpenTree.UseVisualStyleBackColor = true;
            this.buttonOpenTree.Visible = false;
            this.buttonOpenTree.Click += new System.EventHandler(this.buttonOpenTree_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Testtree 2G:";
            // 
            // lblTestTree2G
            // 
            this.lblTestTree2G.AutoSize = true;
            this.lblTestTree2G.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestTree2G.ForeColor = System.Drawing.Color.Blue;
            this.lblTestTree2G.Location = new System.Drawing.Point(95, 127);
            this.lblTestTree2G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestTree2G.Name = "lblTestTree2G";
            this.lblTestTree2G.Size = new System.Drawing.Size(18, 17);
            this.lblTestTree2G.TabIndex = 2;
            this.lblTestTree2G.Text = "--";
            this.lblTestTree2G.MouseHover += new System.EventHandler(this.lblTestTree2G_MouseHover);
            // 
            // buttonRunTree
            // 
            this.buttonRunTree.Location = new System.Drawing.Point(569, 495);
            this.buttonRunTree.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRunTree.Name = "buttonRunTree";
            this.buttonRunTree.Size = new System.Drawing.Size(85, 28);
            this.buttonRunTree.TabIndex = 3;
            this.buttonRunTree.Text = "Run Tree";
            this.buttonRunTree.UseVisualStyleBackColor = true;
            this.buttonRunTree.Visible = false;
            this.buttonRunTree.Click += new System.EventHandler(this.buttonRunTree_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(475, 494);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(87, 28);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "Stop Tree";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonRunLastFolder
            // 
            this.buttonRunLastFolder.Location = new System.Drawing.Point(139, 495);
            this.buttonRunLastFolder.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRunLastFolder.Name = "buttonRunLastFolder";
            this.buttonRunLastFolder.Size = new System.Drawing.Size(120, 28);
            this.buttonRunLastFolder.TabIndex = 5;
            this.buttonRunLastFolder.Text = "Run Last Folder";
            this.buttonRunLastFolder.UseVisualStyleBackColor = true;
            this.buttonRunLastFolder.Visible = false;
            this.buttonRunLastFolder.Click += new System.EventHandler(this.buttonRunLastFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 241);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "LOG QSPR:";
            // 
            // buttonClearText
            // 
            this.buttonClearText.Location = new System.Drawing.Point(365, 494);
            this.buttonClearText.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClearText.Name = "buttonClearText";
            this.buttonClearText.Size = new System.Drawing.Size(103, 28);
            this.buttonClearText.TabIndex = 8;
            this.buttonClearText.Text = "Clear Text";
            this.buttonClearText.UseVisualStyleBackColor = true;
            this.buttonClearText.Visible = false;
            this.buttonClearText.Click += new System.EventHandler(this.buttonClearText_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 498);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "LOG UART:";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            // 
            // panel_DebugView
            // 
            this.panel_DebugView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_DebugView.Controls.Add(this.btnTinhSuyHao);
            this.panel_DebugView.Controls.Add(this.panel4);
            this.panel_DebugView.Controls.Add(this.panel3);
            this.panel_DebugView.Controls.Add(this.panel2);
            this.panel_DebugView.Controls.Add(this.label4);
            this.panel_DebugView.Controls.Add(this.label11);
            this.panel_DebugView.Controls.Add(this.label2);
            this.panel_DebugView.Controls.Add(this.buttonClearText);
            this.panel_DebugView.Controls.Add(this.buttonRunTree);
            this.panel_DebugView.Controls.Add(this.buttonStop);
            this.panel_DebugView.Controls.Add(this.buttonOpenTree);
            this.panel_DebugView.Controls.Add(this.btn_disconnect);
            this.panel_DebugView.Controls.Add(this.buttonRunLastFolder);
            this.panel_DebugView.Controls.Add(this.checkBox_ViewDebug);
            this.panel_DebugView.Location = new System.Drawing.Point(793, 123);
            this.panel_DebugView.Margin = new System.Windows.Forms.Padding(4);
            this.panel_DebugView.Name = "panel_DebugView";
            this.panel_DebugView.Size = new System.Drawing.Size(669, 648);
            this.panel_DebugView.TabIndex = 17;
            // 
            // btnTinhSuyHao
            // 
            this.btnTinhSuyHao.Location = new System.Drawing.Point(447, 4);
            this.btnTinhSuyHao.Name = "btnTinhSuyHao";
            this.btnTinhSuyHao.Size = new System.Drawing.Size(207, 30);
            this.btnTinhSuyHao.TabIndex = 90;
            this.btnTinhSuyHao.Text = "Tính suy hao trạm calib";
            this.btnTinhSuyHao.UseVisualStyleBackColor = true;
            this.btnTinhSuyHao.Click += new System.EventHandler(this.btnTinhSuyHao_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rtbCOMPort);
            this.panel4.Location = new System.Drawing.Point(17, 526);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(634, 110);
            this.panel4.TabIndex = 19;
            // 
            // rtbCOMPort
            // 
            this.rtbCOMPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbCOMPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCOMPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCOMPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCOMPort.ForeColor = System.Drawing.Color.White;
            this.rtbCOMPort.Location = new System.Drawing.Point(0, 0);
            this.rtbCOMPort.Margin = new System.Windows.Forms.Padding(4);
            this.rtbCOMPort.Name = "rtbCOMPort";
            this.rtbCOMPort.Size = new System.Drawing.Size(632, 108);
            this.rtbCOMPort.TabIndex = 19;
            this.rtbCOMPort.Text = "";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.richTextBoxStatus);
            this.panel3.Location = new System.Drawing.Point(16, 266);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(635, 222);
            this.panel3.TabIndex = 17;
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.richTextBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxStatus.ForeColor = System.Drawing.Color.White;
            this.richTextBoxStatus.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.ReadOnly = true;
            this.richTextBoxStatus.Size = new System.Drawing.Size(633, 220);
            this.richTextBoxStatus.TabIndex = 7;
            this.richTextBoxStatus.Text = "";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rtbRunTestAll);
            this.panel2.Location = new System.Drawing.Point(16, 38);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(638, 187);
            this.panel2.TabIndex = 89;
            // 
            // rtbRunTestAll
            // 
            this.rtbRunTestAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtbRunTestAll.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRunTestAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRunTestAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRunTestAll.ForeColor = System.Drawing.Color.White;
            this.rtbRunTestAll.Location = new System.Drawing.Point(0, 0);
            this.rtbRunTestAll.Margin = new System.Windows.Forms.Padding(4);
            this.rtbRunTestAll.Name = "rtbRunTestAll";
            this.rtbRunTestAll.Size = new System.Drawing.Size(636, 185);
            this.rtbRunTestAll.TabIndex = 48;
            this.rtbRunTestAll.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 10);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "LOG SYSTEM:";
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(139, 236);
            this.btn_disconnect.Margin = new System.Windows.Forms.Padding(4);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(120, 27);
            this.btn_disconnect.TabIndex = 78;
            this.btn_disconnect.Text = "Disconnected";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Visible = false;
            // 
            // checkBox_ViewDebug
            // 
            this.checkBox_ViewDebug.AutoSize = true;
            this.checkBox_ViewDebug.Checked = true;
            this.checkBox_ViewDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ViewDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_ViewDebug.Location = new System.Drawing.Point(316, 236);
            this.checkBox_ViewDebug.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_ViewDebug.Name = "checkBox_ViewDebug";
            this.checkBox_ViewDebug.Size = new System.Drawing.Size(108, 22);
            this.checkBox_ViewDebug.TabIndex = 79;
            this.checkBox_ViewDebug.Text = "View Debug";
            this.checkBox_ViewDebug.UseVisualStyleBackColor = true;
            this.checkBox_ViewDebug.Visible = false;
            this.checkBox_ViewDebug.CheckedChanged += new System.EventHandler(this.checkBox_ViewDebug_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.logToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1483, 28);
            this.menuStrip1.TabIndex = 58;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(128, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(131, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qSPRToolStripMenuItem,
            this.detailToolStripMenuItem,
            this.uartToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // qSPRToolStripMenuItem
            // 
            this.qSPRToolStripMenuItem.Name = "qSPRToolStripMenuItem";
            this.qSPRToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.qSPRToolStripMenuItem.Text = "QSPR";
            this.qSPRToolStripMenuItem.Click += new System.EventHandler(this.qSPRToolStripMenuItem_Click);
            // 
            // detailToolStripMenuItem
            // 
            this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
            this.detailToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.detailToolStripMenuItem.Text = "Detail";
            this.detailToolStripMenuItem.Click += new System.EventHandler(this.detailToolStripMenuItem_Click);
            // 
            // uartToolStripMenuItem
            // 
            this.uartToolStripMenuItem.Name = "uartToolStripMenuItem";
            this.uartToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.uartToolStripMenuItem.Text = "Uart";
            this.uartToolStripMenuItem.Click += new System.EventHandler(this.uartToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(8, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(304, 39);
            this.label9.TabIndex = 64;
            this.label9.Text = "Product :  EW12C";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 79);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(428, 29);
            this.label10.TabIndex = 65;
            this.label10.Text = "Station:   Calib Wlan Pcba (2G && 5G)";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.progressBar1);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.lblProduct);
            this.panel5.Controls.Add(this.lbl_comport);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Controls.Add(this.lb_statusconnect);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Location = new System.Drawing.Point(16, 180);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(769, 593);
            this.panel5.TabIndex = 66;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(17, 566);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(705, 12);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(491, 15);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 18);
            this.label8.TabIndex = 88;
            this.label8.Text = "CÒN LẠI:";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(587, 11);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(24, 25);
            this.lblProduct.TabIndex = 87;
            this.lblProduct.Text = "0";
            // 
            // lbl_comport
            // 
            this.lbl_comport.AutoSize = true;
            this.lbl_comport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_comport.ForeColor = System.Drawing.Color.Black;
            this.lbl_comport.Location = new System.Drawing.Point(120, 10);
            this.lbl_comport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_comport.Name = "lbl_comport";
            this.lbl_comport.Size = new System.Drawing.Size(73, 25);
            this.lbl_comport.TabIndex = 81;
            this.lbl_comport.Text = "NONE";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStart.Location = new System.Drawing.Point(17, 473);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(285, 86);
            this.btnStart.TabIndex = 75;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lb_statusconnect
            // 
            this.lb_statusconnect.AutoSize = true;
            this.lb_statusconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_statusconnect.Location = new System.Drawing.Point(251, 14);
            this.lb_statusconnect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_statusconnect.Name = "lb_statusconnect";
            this.lb_statusconnect.Size = new System.Drawing.Size(118, 18);
            this.lb_statusconnect.TabIndex = 74;
            this.lb_statusconnect.Text = "COMPort Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(13, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 18);
            this.label3.TabIndex = 72;
            this.label3.Text = "COM PORT:";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(16, 114);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1446, 1);
            this.panel6.TabIndex = 67;
            // 
            // lblTestTree5G
            // 
            this.lblTestTree5G.AutoSize = true;
            this.lblTestTree5G.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestTree5G.ForeColor = System.Drawing.Color.Blue;
            this.lblTestTree5G.Location = new System.Drawing.Point(95, 151);
            this.lblTestTree5G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestTree5G.Name = "lblTestTree5G";
            this.lblTestTree5G.Size = new System.Drawing.Size(18, 17);
            this.lblTestTree5G.TabIndex = 2;
            this.lblTestTree5G.Text = "--";
            this.lblTestTree5G.MouseHover += new System.EventHandler(this.lblTestTree5G_MouseHover);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 151);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Testtree 5G:";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lbl_MacEthernet);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label20);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Controls.Add(this.lbl_Calib2G);
            this.panel7.Controls.Add(this.lbl_SwitchMode2G);
            this.panel7.Controls.Add(this.lbl_Calib5G);
            this.panel7.Controls.Add(this.lbl_SwitchMode5G);
            this.panel7.Controls.Add(this.lbl_PingNewIP);
            this.panel7.Controls.Add(this.lbl_ChangeIP);
            this.panel7.Controls.Add(this.lbl_Login);
            this.panel7.Controls.Add(this.lbl_Calib2GTime);
            this.panel7.Controls.Add(this.lbl_SwitchMode2GTime);
            this.panel7.Controls.Add(this.lbl_Calib5GTime);
            this.panel7.Controls.Add(this.lbl_SwitchMode5GTime);
            this.panel7.Controls.Add(this.lbl_PingNewIPTime);
            this.panel7.Controls.Add(this.lbl_ChangeIPTime);
            this.panel7.Controls.Add(this.lbl_LoginTime);
            this.panel7.Controls.Add(this.lbl_WaitBootTime);
            this.panel7.Controls.Add(this.lbl_OpenComTime);
            this.panel7.Controls.Add(this.lbl_WaitBoot);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.lbl_OpenCom);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.lblResult);
            this.panel7.Location = new System.Drawing.Point(17, 48);
            this.panel7.Margin = new System.Windows.Forms.Padding(4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(730, 418);
            this.panel7.TabIndex = 92;
            // 
            // lbl_MacEthernet
            // 
            this.lbl_MacEthernet.AutoSize = true;
            this.lbl_MacEthernet.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MacEthernet.Location = new System.Drawing.Point(180, 17);
            this.lbl_MacEthernet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_MacEthernet.Name = "lbl_MacEthernet";
            this.lbl_MacEthernet.Size = new System.Drawing.Size(19, 26);
            this.lbl_MacEthernet.TabIndex = 80;
            this.lbl_MacEthernet.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 26);
            this.label5.TabIndex = 80;
            this.label5.Text = "Mac Ethernet:";
            // 
            // label20
            // 
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(16, 270);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(281, 27);
            this.label20.TabIndex = 79;
            this.label20.Text = "Calib wlan 2G";
            // 
            // label18
            // 
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(16, 218);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(281, 27);
            this.label18.TabIndex = 79;
            this.label18.Text = "Calib wlan 5G";
            // 
            // label19
            // 
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(16, 244);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(281, 27);
            this.label19.TabIndex = 78;
            this.label19.Text = "Switch mesh to mode calib 2G";
            // 
            // label17
            // 
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(16, 192);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(281, 27);
            this.label17.TabIndex = 78;
            this.label17.Text = "Switch mesh to mode calib 5G";
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(16, 166);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(281, 27);
            this.label16.TabIndex = 78;
            this.label16.Text = "Ping to new ip";
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(16, 140);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(281, 27);
            this.label15.TabIndex = 78;
            this.label15.Text = "Change ip address";
            // 
            // label14
            // 
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(16, 114);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(281, 27);
            this.label14.TabIndex = 78;
            this.label14.Text = "Login to mesh";
            // 
            // lbl_Calib2G
            // 
            this.lbl_Calib2G.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Calib2G.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Calib2G.Location = new System.Drawing.Point(296, 270);
            this.lbl_Calib2G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Calib2G.Name = "lbl_Calib2G";
            this.lbl_Calib2G.Size = new System.Drawing.Size(222, 27);
            this.lbl_Calib2G.TabIndex = 78;
            this.lbl_Calib2G.Text = "--";
            this.lbl_Calib2G.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SwitchMode2G
            // 
            this.lbl_SwitchMode2G.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SwitchMode2G.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SwitchMode2G.Location = new System.Drawing.Point(296, 244);
            this.lbl_SwitchMode2G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SwitchMode2G.Name = "lbl_SwitchMode2G";
            this.lbl_SwitchMode2G.Size = new System.Drawing.Size(222, 27);
            this.lbl_SwitchMode2G.TabIndex = 78;
            this.lbl_SwitchMode2G.Text = "--";
            this.lbl_SwitchMode2G.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Calib5G
            // 
            this.lbl_Calib5G.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Calib5G.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Calib5G.Location = new System.Drawing.Point(296, 218);
            this.lbl_Calib5G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Calib5G.Name = "lbl_Calib5G";
            this.lbl_Calib5G.Size = new System.Drawing.Size(222, 27);
            this.lbl_Calib5G.TabIndex = 78;
            this.lbl_Calib5G.Text = "--";
            this.lbl_Calib5G.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SwitchMode5G
            // 
            this.lbl_SwitchMode5G.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SwitchMode5G.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SwitchMode5G.Location = new System.Drawing.Point(296, 192);
            this.lbl_SwitchMode5G.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SwitchMode5G.Name = "lbl_SwitchMode5G";
            this.lbl_SwitchMode5G.Size = new System.Drawing.Size(222, 27);
            this.lbl_SwitchMode5G.TabIndex = 78;
            this.lbl_SwitchMode5G.Text = "--";
            this.lbl_SwitchMode5G.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_PingNewIP
            // 
            this.lbl_PingNewIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PingNewIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PingNewIP.Location = new System.Drawing.Point(296, 166);
            this.lbl_PingNewIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PingNewIP.Name = "lbl_PingNewIP";
            this.lbl_PingNewIP.Size = new System.Drawing.Size(222, 27);
            this.lbl_PingNewIP.TabIndex = 78;
            this.lbl_PingNewIP.Text = "--";
            this.lbl_PingNewIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_ChangeIP
            // 
            this.lbl_ChangeIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ChangeIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ChangeIP.Location = new System.Drawing.Point(296, 140);
            this.lbl_ChangeIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ChangeIP.Name = "lbl_ChangeIP";
            this.lbl_ChangeIP.Size = new System.Drawing.Size(222, 27);
            this.lbl_ChangeIP.TabIndex = 78;
            this.lbl_ChangeIP.Text = "--";
            this.lbl_ChangeIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Login
            // 
            this.lbl_Login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Login.Location = new System.Drawing.Point(296, 114);
            this.lbl_Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(222, 27);
            this.lbl_Login.TabIndex = 78;
            this.lbl_Login.Text = "--";
            this.lbl_Login.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Calib2GTime
            // 
            this.lbl_Calib2GTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Calib2GTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Calib2GTime.Location = new System.Drawing.Point(517, 270);
            this.lbl_Calib2GTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Calib2GTime.Name = "lbl_Calib2GTime";
            this.lbl_Calib2GTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_Calib2GTime.TabIndex = 78;
            this.lbl_Calib2GTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SwitchMode2GTime
            // 
            this.lbl_SwitchMode2GTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SwitchMode2GTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SwitchMode2GTime.Location = new System.Drawing.Point(517, 244);
            this.lbl_SwitchMode2GTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SwitchMode2GTime.Name = "lbl_SwitchMode2GTime";
            this.lbl_SwitchMode2GTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_SwitchMode2GTime.TabIndex = 78;
            this.lbl_SwitchMode2GTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Calib5GTime
            // 
            this.lbl_Calib5GTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Calib5GTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Calib5GTime.Location = new System.Drawing.Point(517, 218);
            this.lbl_Calib5GTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Calib5GTime.Name = "lbl_Calib5GTime";
            this.lbl_Calib5GTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_Calib5GTime.TabIndex = 78;
            this.lbl_Calib5GTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SwitchMode5GTime
            // 
            this.lbl_SwitchMode5GTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SwitchMode5GTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SwitchMode5GTime.Location = new System.Drawing.Point(517, 192);
            this.lbl_SwitchMode5GTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SwitchMode5GTime.Name = "lbl_SwitchMode5GTime";
            this.lbl_SwitchMode5GTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_SwitchMode5GTime.TabIndex = 78;
            this.lbl_SwitchMode5GTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_PingNewIPTime
            // 
            this.lbl_PingNewIPTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PingNewIPTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PingNewIPTime.Location = new System.Drawing.Point(517, 166);
            this.lbl_PingNewIPTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PingNewIPTime.Name = "lbl_PingNewIPTime";
            this.lbl_PingNewIPTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_PingNewIPTime.TabIndex = 78;
            this.lbl_PingNewIPTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_ChangeIPTime
            // 
            this.lbl_ChangeIPTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ChangeIPTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ChangeIPTime.Location = new System.Drawing.Point(517, 140);
            this.lbl_ChangeIPTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ChangeIPTime.Name = "lbl_ChangeIPTime";
            this.lbl_ChangeIPTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_ChangeIPTime.TabIndex = 78;
            this.lbl_ChangeIPTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_LoginTime
            // 
            this.lbl_LoginTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_LoginTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LoginTime.Location = new System.Drawing.Point(517, 114);
            this.lbl_LoginTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_LoginTime.Name = "lbl_LoginTime";
            this.lbl_LoginTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_LoginTime.TabIndex = 78;
            this.lbl_LoginTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WaitBootTime
            // 
            this.lbl_WaitBootTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_WaitBootTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WaitBootTime.Location = new System.Drawing.Point(517, 89);
            this.lbl_WaitBootTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WaitBootTime.Name = "lbl_WaitBootTime";
            this.lbl_WaitBootTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_WaitBootTime.TabIndex = 78;
            this.lbl_WaitBootTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_OpenComTime
            // 
            this.lbl_OpenComTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_OpenComTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OpenComTime.Location = new System.Drawing.Point(517, 63);
            this.lbl_OpenComTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_OpenComTime.Name = "lbl_OpenComTime";
            this.lbl_OpenComTime.Size = new System.Drawing.Size(162, 27);
            this.lbl_OpenComTime.TabIndex = 78;
            this.lbl_OpenComTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WaitBoot
            // 
            this.lbl_WaitBoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_WaitBoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WaitBoot.Location = new System.Drawing.Point(296, 89);
            this.lbl_WaitBoot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_WaitBoot.Name = "lbl_WaitBoot";
            this.lbl_WaitBoot.Size = new System.Drawing.Size(222, 27);
            this.lbl_WaitBoot.TabIndex = 78;
            this.lbl_WaitBoot.Text = "--";
            this.lbl_WaitBoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(16, 89);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(281, 27);
            this.label21.TabIndex = 78;
            this.label21.Text = "Wait mesh boot completed";
            // 
            // lbl_OpenCom
            // 
            this.lbl_OpenCom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_OpenCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_OpenCom.Location = new System.Drawing.Point(296, 63);
            this.lbl_OpenCom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_OpenCom.Name = "lbl_OpenCom";
            this.lbl_OpenCom.Size = new System.Drawing.Size(222, 27);
            this.lbl_OpenCom.TabIndex = 78;
            this.lbl_OpenCom.Text = "--";
            this.lbl_OpenCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(16, 63);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(281, 27);
            this.label13.TabIndex = 78;
            this.label13.Text = "Open comport";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 308);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 26);
            this.label6.TabIndex = 77;
            this.label6.Text = "total result:";
            // 
            // lblResult
            // 
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Orange;
            this.lblResult.Location = new System.Drawing.Point(3, 336);
            this.lblResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(681, 75);
            this.lblResult.TabIndex = 76;
            this.lblResult.Text = "NONE";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1483, 788);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel_DebugView);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblTestTree5G);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTestTree2G);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calibration & DVT EW12C Ver 1.0.0.0 28/09/2020 10:10";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel_DebugView.ResumeLayout(false);
            this.panel_DebugView.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTestTree2G;
        private System.Windows.Forms.Button buttonRunTree;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonRunLastFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClearText;
        private System.Windows.Forms.Label label4;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Panel panel_DebugView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolStripMenuItem qSPRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uartToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox rtbCOMPort;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbRunTestAll;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lbl_comport;
        private System.Windows.Forms.CheckBox checkBox_ViewDebug;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btn_disconnect;
        private System.Windows.Forms.Label lb_statusconnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblTestTree5G;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnTinhSuyHao;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbl_MacEthernet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_Calib2G;
        private System.Windows.Forms.Label lbl_SwitchMode2G;
        private System.Windows.Forms.Label lbl_Calib5G;
        private System.Windows.Forms.Label lbl_SwitchMode5G;
        private System.Windows.Forms.Label lbl_PingNewIP;
        private System.Windows.Forms.Label lbl_ChangeIP;
        private System.Windows.Forms.Label lbl_Login;
        private System.Windows.Forms.Label lbl_Calib2GTime;
        private System.Windows.Forms.Label lbl_SwitchMode2GTime;
        private System.Windows.Forms.Label lbl_Calib5GTime;
        private System.Windows.Forms.Label lbl_SwitchMode5GTime;
        private System.Windows.Forms.Label lbl_PingNewIPTime;
        private System.Windows.Forms.Label lbl_ChangeIPTime;
        private System.Windows.Forms.Label lbl_LoginTime;
        private System.Windows.Forms.Label lbl_WaitBootTime;
        private System.Windows.Forms.Label lbl_OpenComTime;
        private System.Windows.Forms.Label lbl_WaitBoot;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbl_OpenCom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblResult;
    }
}

