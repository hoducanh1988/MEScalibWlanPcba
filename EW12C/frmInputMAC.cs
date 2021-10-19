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


namespace EW12C {
    public partial class FrmGetMACaddress : Form {
        public string getMAC = string.Empty;
        public bool validMAC = false;

        public FrmGetMACaddress() {
            InitializeComponent();
        }

        private void FrmGetMACaddress_Load(object sender, EventArgs e) {
            tbMac.Text = "";
            getMAC = "";
            tbMac.Focus();
        }

        private void CheckMac() {
            bool flagCheckMacAddress = false;

            getMAC = tbMac.Text.ToUpper();

            for (int i = 0; i < getMAC.Length; i++) {
                if ((getMAC[i] > 'f') || ((getMAC[i] > 'F') && (getMAC[i] < 'a'))) {
                    flagCheckMacAddress = true;
                    break;
                }
                else flagCheckMacAddress = false;
            }
            if (getMAC.Length != 12) {
                MessageBox.Show("Địa Chỉ MAC Phải Có 12 Kí Tự. Hãy Thử Lại!", "Thử Lại!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMac.Text = "";
                getMAC = "";
                tbMac.Focus();
            }
            else if (flagCheckMacAddress) {
                MessageBox.Show("Địa Chỉ MAC Không Hợp Lệ. Hãy Thử Lại!", "Thử Lại!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMac.Text = "";
                getMAC = "";
                tbMac.Focus();
            }
            else {
                string MacListFilePath = @"MACs.txt";
                if (File.Exists(MacListFilePath) == false) {
                    StreamWriter StreamW = new StreamWriter(MacListFilePath);
                    StreamW.Close();
                }
                string[] lines = System.IO.File.ReadAllLines(@"MACs.txt");

                //using a foreach loop.               
                ///*Tam thoi bo phan nay de Cong nhan ho test thu -- Khi nao chay that thi uncomment
                foreach (string line in lines) {
                    if (getMAC == line) {
                        MessageBox.Show("Địa Chỉ MAC Đã Tồn Tại Trong Danh Sách!", "Thử Lại!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbMac.Text = "";
                        getMAC = "";
                        tbMac.Focus();
                        break;
                    }
                }
                //save MAC address               
                if (getMAC != "") {
                    this.validMAC = true;
                    this.Close();
                }

            }
        }

        private void tbMac_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 13) {
                if (tbMac.Text.Length == 12 && myGlobal.macHeader.ToUpper().Contains(tbMac.Text.Substring(0, 6).ToUpper())) {
                    CheckMac();
                }
                else { }
            }
        }


    }
}
