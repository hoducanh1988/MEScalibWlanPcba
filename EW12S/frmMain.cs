using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QC.QSPRSchedulerWrapper;
using QC.QTMInterface;
using System.IO;
using System.IO.Ports;
using VNPT.DisplayText;
using System.Threading;
using System.Management;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UtilityPack;

namespace EW12S {

    public partial class frmMain : Form {

        #region Private members

        class testFrequencyInfo {
            public testFrequencyInfo() {
                Frequency = "";
                Antenna = "";
                averagePowers = new List<double>();
            }

            public string Frequency { get; set; }
            public string Antenna { get; set; }
            public List<double> averagePowers { get; set; }
        }

        class goldenFrequencyInfo {
            public goldenFrequencyInfo() {
                Frequency = "";
                powerAnten1 = 0;
                powerAnten2 = 0;
            }

            public string Frequency { get; set; }
            public double powerAnten1 { get; set; }
            public double powerAnten2 { get; set; }
        }

        class powerDifferenceInfo {

            public powerDifferenceInfo() {
                Frequency = "";
                powerDifferenceAnten1 = 0;
                powerDifferenceAnten2 = 0;
            }

            public string resultAnten1 { get; set; }
            public string resultAnten2 { get; set; }
            public string resultTotal { get; set; }

            public string Frequency { get; set; }
            double _pw_dif1;
            public double powerDifferenceAnten1 {
                get { return _pw_dif1; }
                set {
                    _pw_dif1 = value;
                    resultAnten1 = Math.Abs(value) <= powertolerance ? "PASS" : "FAIL";
                    if (resultAnten2 != null)
                        resultTotal = (resultAnten1.Equals("PASS") && resultAnten2.Equals("PASS")) ? "PASS" : "FAIL";
                }
            }
            double _pw_dif2;
            public double powerDifferenceAnten2 {
                get { return _pw_dif2; }
                set {
                    _pw_dif2 = value;
                    resultAnten2 = Math.Abs(value) <= powertolerance ? "PASS" : "FAIL";
                    if (resultAnten1 != null)
                        resultTotal = (resultAnten1.Equals("PASS") && resultAnten2.Equals("PASS")) ? "PASS" : "FAIL";
                }
            }

        }

        class pathLossFrequencyInfo {

            public pathLossFrequencyInfo() {
                bhName = "";
                Frequency = "";
                lossValue = 0;
                lineNumber = 0;
            }

            public string bhName { get; set; }
            public string Frequency { get; set; }
            public double lossValue { get; set; }
            public int lineNumber { get; set; }
        }


        QSPRScheduler _scheduler;
        QSPRTestTree _testTree;
        bool closeAfterRun = false;

        string g_logstart = "fast-classifier: starting up";
        string g_mac2G_onboard = null;
        string g_mac5G_onboard = null;
        string g_SN = null;
        string g_macEth0 = null;
        string AddMac_Value_tam = string.Empty;
        volatile string g_HienthidataCOM = string.Empty;
        volatile string g_serial = string.Empty;

        string g_MainConfigFilePath = @"ew12s_Config\Main-Config.cfg";
        string PathXTTFile2G = "";
        string PathXTTFile5G = "";

        volatile string g_readFromCOM1 = string.Empty;
        string IP_Address = null;
        FrmGetMACaddress frmMAC = new FrmGetMACaddress();
        int timeboot = 150;
        int timeping = 30;
        int timestartruntesttree = 10;
        int timebootcomplete = 47;
        int timewaitrespone = 3;
        static double powertolerance = 1;
        static double maxpowersuyhao = 30;
        static double maxproduct = 3000;
        Stopwatch cvStopwatch = null;
        Stopwatch totalStopwatch = null;
        int TreeTestStatus = 0;

        volatile bool isCalib2G = true;
        volatile bool isCalib5G = true;

        #endregion

        #region Constructor

        public frmMain(string[] args) {
            InitializeComponent();

            progressBar1.Visible = false;
            buttonRunTree.Enabled = false;
            buttonRunLastFolder.Enabled = false;
            buttonStop.Enabled = false;

            try {
                _scheduler = new QSPRScheduler();
                _scheduler.OnDebugMessage += new QSPRScheduler.OnDebugMessageEventHandler(_scheduler_OnDebugMessage);
                _scheduler.OnTestMessage += new QSPRScheduler.OnTestMessageEventHandler(_scheduler_OnTestMessage);
                _scheduler.LoadWorkspaceConfig(@"C:\Qualcomm\QSPR\QSPRConfigurations\Workspace.config");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

        }


        #endregion

        private void UpdateUI(bool treeRunning) {
            if (this.InvokeRequired) {
                this.BeginInvoke(new MethodInvoker(delegate { UpdateUI(treeRunning); }));
                return;
            }

            buttonRunLastFolder.Enabled = !treeRunning;
            buttonOpenTree.Enabled = !treeRunning;
            buttonRunTree.Enabled = !treeRunning;
            buttonStop.Enabled = treeRunning;
            progressBar1.Visible = treeRunning;
            //lblResult.Text = "NONE";
            //lblResult.ForeColor = Color.Yellow;
        }

        private void showResult(string result) {
            //lblResult.Text = result;
            VNPT_TextDisplay.SetText(lblResult, result);
            if (result == "PASS") {
                lblResult.ForeColor = Color.Blue;
            }
            else {
                lblResult.ForeColor = Color.Red;
            }
        }

        private void UpdateStatusWindow(string msg) {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { UpdateStatusWindow(msg); }));
                return;
            }
            if (msg.Contains("Failed")) {
                richTextBoxStatus.SelectionColor = Color.Red;
                UpdateRtbAll(msg);

            }
            else {
                richTextBoxStatus.SelectionColor = Color.White;
            }

            if (msg.Contains("Tree finished with result:")) {
                if (msg.Contains("Tree finished with result: Passed")) {
                    TreeTestStatus = 1;
                    MsgBox.Show("PASSED", "Test Result", 500);
                }
                else {
                    TreeTestStatus = 2;
                    MsgBox.Show("FAILED", "Test Result", 500);
                }
            }

            richTextBoxStatus.AppendText(msg + Environment.NewLine);
            richTextBoxStatus.ScrollToCaret();
            richTextBoxStatus.Refresh();

        }

        private void ChangeIPAfterTest() {
            Thread.Sleep(500);
            //serialPort1.Close();
            btn_disconnect.Visible = false;
            btnStart.Enabled = true;
            VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
        }

        private void ClearStatusWindow() {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { ClearStatusWindow(); }));
                return;
            }

            richTextBoxStatus.Clear();
        }

        List<string> ComPortNames(String VID, String PID) {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames()) {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames()) {
                    if (_rx.Match(s).Success) {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames()) {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;

            string indata = sp.ReadExisting();
            g_HienthidataCOM += indata;
            g_readFromCOM1 += indata;
            g_serial += indata;

        }

        private void Form1_Load(object sender, EventArgs e) {
            Thread t = new Thread(new ThreadStart(() => {
            REP:
                VNPT_TextDisplay.SetText(rtbCOMPort, g_HienthidataCOM);
                g_HienthidataCOM = "";
                Thread.Sleep(500);
                goto REP;
            }));
            t.IsBackground = true;
            t.Start();

            btn_disconnect.Visible = false;

            Read_Config_File();
            //RunbuttonTestTree();

            lblProduct.Text = File.ReadAllText("ew12s_remain.dll");
            try {
                string xtt_2g = PathXTTFile2G;
                string xtt_5g = PathXTTFile5G;

                if (xtt_2g.Contains("\\")) {
                    string[] buffer = xtt_2g.Split('\\');
                    lblTestTree2G.Text = "... " + buffer[buffer.Length - 1];
                }
                if (xtt_5g.Contains("\\")) {
                    string[] buffer = xtt_5g.Split('\\');
                    lblTestTree5G.Text = "... " + buffer[buffer.Length - 1];
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }

        }

        public void Read_Config_File() {
            string readConfigFilePath = string.Empty;
            if (File.Exists(g_MainConfigFilePath)) {
                StreamReader StreamR = File.OpenText(g_MainConfigFilePath);
                readConfigFilePath = StreamR.ReadToEnd();
                StreamR.Close();
            }
            //------------------------------
            try {
                for (int i = 0; i < readConfigFilePath.Split('\n').Length; i++) {
                    if (readConfigFilePath.Split('\n')[i].Contains("Log-Start:")) {
                        g_logstart = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("XttPath:")) {
                        PathXTTFile2G = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                        PathXTTFile5G = readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Power:")) {
                        powertolerance = double.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim());
                        maxpowersuyhao = double.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim());
                        maxproduct = int.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[2].Trim());
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap Comport:")) {
                        lbl_comport.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap IP:")) {
                        IP_Address = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Mac Header:")) {
                        myGlobal.macHeader = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap Time:")) {
                        timeboot = Int16.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim());
                        timeping = Int16.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim());
                        timestartruntesttree = 1000 * Int16.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[2].Trim());
                        timebootcomplete = Int16.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[3].Trim());
                        timewaitrespone = Int16.Parse(readConfigFilePath.Split('\n')[i + 1].Split(',')[4].Trim());
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Enable:")) {
                        isCalib2G = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim().ToLower().Equals("true") ? true : false;
                        isCalib5G = readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim().ToLower().Equals("true") ? true : false;
                    }
                }
            }
            catch {
                MessageBox.Show("Lỗi Khi Load File Cấu Hình.");
            }
        }

        private void UpdateDebugCOM(string msg) {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { UpdateDebugCOM(msg); }));
                return;
            }

            rtbCOMPort.AppendText(msg + Environment.NewLine);
            rtbCOMPort.ScrollToCaret();
            rtbCOMPort.Refresh();
        }

        private void UpdateRtbAll(string msg) {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { UpdateRtbAll(msg); }));
                return;
            }
            rtbRunTestAll.AppendText(msg);
            rtbRunTestAll.ScrollToCaret();
            rtbRunTestAll.Refresh();
        }

        private void Runbutton() {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { Runbutton(); }));
                return;
            }
            buttonRunTree.PerformClick();
        }

        private void RunbuttonTestTree() {
            if (this.InvokeRequired == true) {
                this.BeginInvoke(new MethodInvoker(delegate { RunbuttonTestTree(); }));
                return;
            }
            buttonOpenTree.PerformClick();
            buttonOpenTree.Visible = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            try {
                _scheduler.Close();
            }
            catch { }
        }

        private bool CheckForm(Form form) {
            foreach (Form f in Application.OpenForms)
                if (form == f)
                    return true;

            return false;
        }

        private void btn_disconnect_Click(object sender, EventArgs e) {
            if (serialPort1.IsOpen) {
                serialPort1.Close();
                btn_disconnect.Visible = false;
                btnStart.Enabled = true;
                VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
            }

        }

        private void checkBox_ViewDebug_CheckedChanged(object sender, EventArgs e) {
            if (checkBox_ViewDebug.Checked) {
                this.panel_DebugView.Visible = true; // Hiện cửa sổ Debug bên phải
                this.panel_DebugView.Enabled = true; // Hiện cửa sổ Debug bên phải
            }
            else {
                this.panel_DebugView.Visible = false;  // Ẩn cửa sổ Debug bên phải
                this.panel_DebugView.Enabled = false;  // Ẩn cửa sổ Debug bên phải
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            string p_name = "EW12S";
            if (UtilityPack.IO.WindowProcess.isProcessRunning(p_name)) UtilityPack.IO.WindowProcess.killAllProcessByName(p_name);
        }

        private void btnStart_Click(object sender, EventArgs e) {
            g_mac2G_onboard = "";
            g_mac5G_onboard = "";
            g_macEth0 = "";
            g_SN = "";
            ClearStatusWindow();

            int c = 0;
            bool x = int.TryParse(lblProduct.Text, out c);
            if (x == false || c <= 0) {
                MessageBox.Show("Trạm calib cần phải đo lại giá trị suy hao.", "Cảnh báo đo suy hao trạm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            reset_Result();

            ThreadStart tRun = delegate {
                calibWlan();
            };
            Thread t10 = new Thread(tRun);
            t10.IsBackground = true;
            t10.Start();
        }

        private void lblTestTree2G_MouseHover(object sender, EventArgs e) {
            string xtt_2g = PathXTTFile2G;
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(this.lblTestTree2G, xtt_2g);
        }

        private void lblTestTree5G_MouseHover(object sender, EventArgs e) {
            string xtt_5g = PathXTTFile5G;
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(this.lblTestTree5G, xtt_5g);
        }

        private void reset_Result() {
            lbl_OpenCom.Text = "--"; lbl_OpenComTime.Text = "";
            lbl_WaitBoot.Text = "--"; lbl_WaitBootTime.Text = "";
            lbl_Login.Text = "--"; lbl_LoginTime.Text = "";
            lbl_ChangeIP.Text = "--"; lbl_ChangeIPTime.Text = "";
            lbl_PingNewIP.Text = "--"; lbl_PingNewIPTime.Text = "";
            lbl_SwitchMode2G.Text = "--"; lbl_SwitchMode2GTime.Text = "";
            lbl_Calib2G.Text = "--"; lbl_Calib2GTime.Text = "";
            lbl_SwitchMode5G.Text = "--"; lbl_SwitchMode5GTime.Text = "";
            lbl_Calib5G.Text = "--"; lbl_Calib5GTime.Text = "";
            lblResult.Text = "--";
        }

        private void btnTinhSuyHao_Click(object sender, EventArgs e) {
            bool r = UtilityPack.IO.WindowProcess.isProcessRunning("RFAttenuation");
            if (r) UtilityPack.IO.WindowProcess.killAllProcessByName("RFAttenuation");
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "attenuation.dll", $"EW12S_remain.dll,{maxproduct}");

            UtilityPack.IO.WindowProcess.callBackProcess(AppDomain.CurrentDomain.BaseDirectory + "RFAttenuation.exe");
            Application.Exit();
        }
    }
}
