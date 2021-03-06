using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace EW12CG {
    public partial class FrmSetting : Form {
        string[] ports = SerialPort.GetPortNames();
        string g_MainConfigFilePath = @"EW12CG_Config\Main-Config.cfg";
        public FrmSetting() {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            try {
                StreamWriter StreamW = new StreamWriter(g_MainConfigFilePath);

                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Log-Start:");
                StreamW.WriteLine(tb_logkhoidong.Text + ",");
                StreamW.WriteLine("Thiet Lap Comport:");
                StreamW.WriteLine(cbb_comport.Text + ", ");
                StreamW.WriteLine("Thiet Lap IP:");
                StreamW.WriteLine(tb_IPAddress.Text + ", ");
                StreamW.WriteLine("Thiet Mac Header:");
                StreamW.WriteLine(tb_MacHeader.Text + ", ");
                StreamW.WriteLine("Thiet Lap Time:");
                StreamW.WriteLine(tb_khoidongtimeout.Text + ", " + tb_timeping.Text + ", " + tb_timestart.Text + ", " + tb_khoidong.Text + ", " + tb_waitrespone.Text);
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("XttPath:");
                StreamW.WriteLine(rtbTreeFileName2G.Text + ", " + rtbAttFileName2G.Text + ", " + rtbTreeFileName5G.Text + ", " + rtbAttFileName5G.Text + ", " + rtbPathlossFileName.Text);
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Power:");
                StreamW.WriteLine(tb_dolechgolden.Text + ", " + tb_maxpowersuyhao.Text + ", " + tb_maxproduct.Text);
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Enable:");
                StreamW.WriteLine(ckEnable2G.Checked.ToString() + ", " + ckEnable5G.Checked.ToString());
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Golden verify:");
                StreamW.WriteLine(tb_Golden.Text + ",");
                StreamW.Close();
                this.Close();

                DialogResult DR = MessageBox.Show("Cấu hình vừa thay đổi. Bạn có muốn Restart chương trình?", "Thông báo", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == DR) {
                    myGlobal.CallBackApplication();
                }
                if (DialogResult.No == DR) {
                    this.Close();
                }
            }
            catch (Exception Ex) {
                MessageBox.Show("Xảy Ra Lỗi Khi Lưu Cấu Hình." + Ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void FrmSetting_Load(object sender, EventArgs e) {
            //*****************************************
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
                        tb_logkhoidong.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("XttPath:")) {
                        rtbTreeFileName2G.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim());
                        rtbAttFileName2G.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim());
                        rtbTreeFileName5G.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[2].Trim());
                        rtbAttFileName5G.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[3].Trim());
                        rtbPathlossFileName.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[4].Trim());
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Power:")) {
                        tb_dolechgolden.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                        tb_maxpowersuyhao.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim();
                        tb_maxproduct.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[2].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap Comport:")) {
                        cbb_comport.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap IP:")) {
                        tb_IPAddress.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Mac Header:")) {
                        tb_MacHeader.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Thiet Lap Time:")) {
                        tb_khoidongtimeout.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                        tb_timeping.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim();
                        tb_timestart.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[2].Trim();
                        tb_khoidong.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[3].Trim();
                        tb_waitrespone.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[4].Trim();
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Enable:")) {
                        ckEnable2G.Checked = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim().ToLower().Equals("true") ? true : false;
                        ckEnable5G.Checked = readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim().ToLower().Equals("true") ? true : false;
                    }
                    if (readConfigFilePath.Split('\n')[i].Contains("Golden verify:")) {
                        tb_Golden.Text = readConfigFilePath.Split('\n')[i + 1].Split(',')[0].Trim();
                    }
                }
            }
            catch {

            }
        }

        private void buttonOpenTree_Click(object sender, EventArgs e) {
            Button btn = sender as Button;
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            switch (btn.Name.ToString().ToLower()) {
                case "buttonopentree2g": {
                        dlg.Title = "Select calib test tree 2G";
                        dlg.Filter = "Calib 2G | *Calibration*2G*.xtt";

                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            rtbTreeFileName2G.Clear();
                            rtbTreeFileName2G.AppendText(dlg.FileName);
                        }
                        break;
                    }
                case "buttonopenatt2g": {
                        dlg.Title = "Select attenuation test tree 2G";
                        dlg.Filter = "Attenuation 2G | *Attenuation*2G*.xtt";

                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            rtbAttFileName2G.Clear();
                            rtbAttFileName2G.AppendText(dlg.FileName);
                        }
                        break;
                    }
                case "buttonopentree5g": {
                        dlg.Title = "Select calib test tree 5G";
                        dlg.Filter = "Calib 5G | *Calibration*5G*.xtt";

                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            rtbTreeFileName5G.Clear();
                            rtbTreeFileName5G.AppendText(dlg.FileName);
                        }
                        break;
                    }
                case "buttonopenatt5g": {
                        dlg.Title = "Select attenuation test tree 5G";
                        dlg.Filter = "Attenuation 5G | *Attenuation*5G*.xtt";

                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            rtbAttFileName5G.Clear();
                            rtbAttFileName5G.AppendText(dlg.FileName);
                        }
                        break;
                    }
                case "buttonopenpathloss": {
                        dlg.Title = "Select pathloss xml file";
                        dlg.Filter = "Pathloss | *.xml";

                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                            rtbPathlossFileName.Clear();
                            rtbPathlossFileName.AppendText(dlg.FileName);
                        }
                        break;
                    }
            }
        }

        private void cbb_comport_MouseClick(object sender, MouseEventArgs e) {
            cbb_comport.Items.Clear();
            cbb_comport.Items.AddRange(ports);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox box = sender as CheckBox;
            box.Font = box.Checked ? new Font(box.Font, FontStyle.Bold) : new Font(box.Font, FontStyle.Regular);
            box.BackColor = box.Checked ? Color.Yellow : Color.Transparent;
        }

        private void ckEnable_CheckedChanged(object sender, EventArgs e) {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked == true) cb.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            else cb.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        }
    }

}
