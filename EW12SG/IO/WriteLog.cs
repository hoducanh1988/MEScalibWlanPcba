using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace EW12SG {
    class WriteLog {

        public static void WriteToTxtfileRunAll(RichTextBox rtb, string dir_path, string mac, string action, string result) {
            string TextFromRTB = string.Empty;
            if (Directory.Exists(dir_path) == false) { Directory.CreateDirectory(dir_path); Thread.Sleep(100); }

            string FileName = string.Format("{0}\\EW12SG_{1}_{2}_{3}_{4}.txt", dir_path, mac, DateTime.Now.ToString("HHmmss"), action, result);
            try {
                StreamWriter StreamW = new StreamWriter(FileName, true, Encoding.Unicode);
                TextFromRTB += rtb.Text;
                StreamW.Write(TextFromRTB);
                TextFromRTB = string.Empty;
                StreamW.Close();
            }
            catch (Exception Ex) {
                MessageBox.Show("Lỗi xảy ra trong quá trình lưu Log kiểm tra" + Ex.ToString());
            }
        }
    }
}
