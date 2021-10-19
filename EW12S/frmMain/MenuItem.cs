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

        private void settingToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Opacity = 0.3;
            FrmSetting FrmSetting = new FrmSetting();
            FrmSetting.ShowDialog();
            this.Opacity = 1.0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }


        private void qSPRToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(myGlobal.qsprDirectory);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void detailToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(myGlobal.systemDirectory);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uartToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(myGlobal.uartDirectory);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void goldenToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                Process.Start(myGlobal.goldenDirectory);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
