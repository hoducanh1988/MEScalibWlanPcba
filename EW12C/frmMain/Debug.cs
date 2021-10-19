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

namespace EW12C {
    public partial class frmMain : Form {

        private void buttonOpenTree_Click(object sender, EventArgs e) {

        }

        private void buttonRunTree_Click(object sender, EventArgs e) {
            g_mac2G_onboard = "";
            g_mac5G_onboard = "";
            g_macEth0 = "";
            g_SN = "";

            //if (g_QCA9561 == true) // Nếu test IAP
            //{
            //    if (_testTree != null) {
            //        ClearStatusWindow();
            //        g_macEth0 = getMAC("Lấy địa chỉ MAC Eth0 cho Board");
            //        g_SN = "IMAP_" + g_macEth0;
            //        if (g_macEth0 == "") {

            //        }
            //        else {
            //            try {
            //                AddMac_Value_tam = g_macEth0.Substring(6, 6);
            //                g_mac2G_onboard = g_macEth0.Substring(0, 6) + GetMAC2G(AddMac_Value_tam);
            //            }
            //            catch (Exception) {
            //                throw;
            //            }
            //        }

            //        cvStopwatch = new Stopwatch();
            //        cvStopwatch.Start();
            //        _scheduler.SetGlobalVariable("SN", g_macEth0);
            //        _scheduler.SetGlobalVariable("mac", g_mac2G_onboard);
            //        _testTree.RunTree();
            //        UpdateUI(true);
            //    }

            //}

            //if (g_QCA9886 == true || g_QCA9886_xpa == true) // Nếu test card
            //{
            //    if (_testTree != null) {
            //        ClearStatusWindow();
            //        g_macEth0 = getMAC("Lấy địa chỉ MAC Eth0 cho Board");
            //        g_SN = "IMAP_" + g_macEth0;
            //        if (g_macEth0 == "") {

            //        }
            //        else {
            //            try {
            //                AddMac_Value_tam = g_macEth0.Substring(6, 6);
            //                g_mac5G_onboard = g_macEth0.Substring(0, 6) + GetMAC5G(AddMac_Value_tam);
            //            }
            //            catch (Exception) {
            //                throw;
            //            }
            //        }

            //        cvStopwatch = new Stopwatch();
            //        cvStopwatch.Start();
            //        _scheduler.SetGlobalVariable("SN", g_macEth0);
            //        _scheduler.SetGlobalVariable("5gMAC", g_mac5G_onboard);
            //        _testTree.RunTree();
            //        UpdateUI(true);
            //    }            
            //}
        }

        private void buttonClearText_Click(object sender, EventArgs e) {
            ClearStatusWindow();
        }

        private void buttonRunLastFolder_Click(object sender, EventArgs e) {
            string[] subFolders;
            int folderCount = 0;

            // get all the folders under the Root
            _testTree.GetAllFolderTests("", out subFolders, out folderCount);

            // run the last folder under the root
            if (folderCount > 0)
                _testTree.RunTest(subFolders[folderCount - 1]);
        }

        private void buttonStop_Click(object sender, EventArgs e) {
            _testTree.StopTest();
        }

    }
}
