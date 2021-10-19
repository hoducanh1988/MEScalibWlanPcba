﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EW30SX {

    public class myGlobal {
        public static string macHeader = "";
        public static string dir_Path = AppDomain.CurrentDomain.BaseDirectory;
        public static string systemDirectory = string.Format("{0}Logdetail\\EW30SX\\{1}", dir_Path, DateTime.Now.ToString("yyyy-MM-dd"));
        public static string qsprDirectory = string.Format("{0}LogQSPR\\EW30SX\\{1}", dir_Path, DateTime.Now.ToString("yyyy-MM-dd"));
        public static string uartDirectory = string.Format("{0}LogUart\\EW30SX\\{1}", dir_Path, DateTime.Now.ToString("yyyy-MM-dd"));
        public static string goldenDirectory = string.Format("{0}EW30SX_Golden", dir_Path);


        public static void CallBackApplication() {
            //save file callback.txt
            string call_back_file = string.Format("{0}callback.txt", AppDomain.CurrentDomain.BaseDirectory);
            File.WriteAllText(call_back_file, string.Format("{0}EW30SX.exe", AppDomain.CurrentDomain.BaseDirectory), Encoding.Unicode);
            Process.Start("callBackApplication.exe");
            Application.Exit();
        }
    }

}
