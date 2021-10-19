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

namespace EW12C {
    public partial class FrmSetting : Form {
        string[] ports = SerialPort.GetPortNames();
        string g_MainConfigFilePath = @"EW12C_Config\Main-Config.cfg";
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
                StreamW.WriteLine(rtbTreeFileName2G.Text + ", " + rtbTreeFileName5G.Text);
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Power:");
                StreamW.WriteLine(tb_dolechgolden.Text + ", " + tb_maxpowersuyhao.Text + ", " + tb_maxproduct.Text);
                StreamW.WriteLine("*********************************************");
                StreamW.WriteLine("Enable:");
                StreamW.WriteLine(ckEnable2G.Checked.ToString() + ", " + ckEnable5G.Checked.ToString());
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
                        rtbTreeFileName5G.AppendText(readConfigFilePath.Split('\n')[i + 1].Split(',')[1].Trim());
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
                }
            }
            catch {

            }
        }

        private void buttonOpenTree_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select a test tree (.xtt) to open";
            dlg.Filter = "Test Tree Files | *.xtt";
            dlg.DefaultExt = "xtt";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                rtbTreeFileName2G.Clear();
                rtbTreeFileName2G.AppendText(dlg.FileName);
            }
        }

        private void cbb_comport_MouseClick(object sender, MouseEventArgs e) {
            cbb_comport.Items.Clear();
            cbb_comport.Items.AddRange(ports);
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog dlgg = new OpenFileDialog();
            dlgg.Title = "Select a test tree (.xtt) to open";
            dlgg.Filter = "Test Tree Files | *.xtt";
            dlgg.DefaultExt = "xtt";
            if (dlgg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                rtbTreeFileName5G.Clear();
                rtbTreeFileName5G.AppendText(dlgg.FileName);
            }
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
