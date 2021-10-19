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

namespace EW12CG {
    public partial class frmMain : Form {

        private void _scheduler_OnDebugMessage(string strWin, string strText, int traceLevel, bool NoEndOfLine) {
            UpdateStatusWindow(strWin + ": " + strText);
        }

        private void _scheduler_OnTestMessage(int messageType, QSPRTestMessage messageData) {
            switch ((TestMsgTypes)messageType) {
                case TestMsgTypes.ON_UNIT_START: {
                        string sn = messageData.GetValue(TestMsgItemNames.SN);
                        string testCount = messageData.GetValue(TestMsgItemNames.TEST_COUNT);
                        UpdateStatusWindow("Tree contains " + testCount + " tests and was started with serial number: " + sn);
                        break;
                    }
                case TestMsgTypes.ON_UNIT_END: {
                        string errorCode = messageData.GetValue(TestMsgItemNames.ERROR_CODE);
                        string testResult = messageData.GetValue(TestMsgItemNames.TESTRESULT);
                        UpdateStatusWindow("Tree finished with result: " + testResult + " and error code: " + errorCode);
                        cvStopwatch.Stop();
                        UpdateRtbAll("Thời gian calib và verify : " + (cvStopwatch.ElapsedMilliseconds / 1000).ToString() + "s\n");
                        UpdateUI(false);

                        if (closeAfterRun == true) {
                            if (this.InvokeRequired) {
                                this.BeginInvoke(new MethodInvoker(delegate { this.Close(); }));
                            }
                            else {
                                this.Close();
                            }
                        }
                        break;
                    }
                case TestMsgTypes.ON_TEST_START: {
                        string testName = messageData.GetValue(TestMsgItemNames.TESTNAME);
                        string startTime = messageData.GetValue(TestMsgItemNames.START_TIME);
                        UpdateStatusWindow("Test started: " + testName + " at: " + startTime);
                        break;
                    }
                case TestMsgTypes.ON_TEST_RESULT: {
                        string testName = messageData.GetValue(TestMsgItemNames.TESTNAME);
                        string testResult = messageData.GetValue(TestMsgItemNames.TESTRESULT);
                        string loopInfo = messageData.GetValue(TestMsgItemNames.LOOP_DETAILS);
                        UpdateStatusWindow("Test finished: " + testName + " with result: " + testResult + " LOOP_DETAILS=" + loopInfo ?? "");

                        string[] msgItemNames = messageData.GetItemNames();

                        foreach (string itemName in msgItemNames) {
                            // if the item is an output parameter
                            if (itemName.StartsWith(TestMsgItemNames.OUTPUT_PARAM_PREFIX)) {
                                string outParamValue = messageData.GetValue(itemName);
                            }
                        }
                        break;
                    }
            }
        }

        private void _testTree_RunTreeDone(bool treePassed) {
            UpdateUI(false);
        }

    }
}
