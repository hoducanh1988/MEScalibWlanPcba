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

namespace EW30SX {
    public partial class frmMain : Form {

        private bool calibWlan() {
            Control.CheckForIllegalCrossThreadCalls = false;
            totalStopwatch = new Stopwatch();
            totalStopwatch.Reset(); totalStopwatch.Start();
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;

            //update product
            int x = int.Parse(lblProduct.Text);
            lblProduct.Text = (x - 1).ToString();
            File.WriteAllText("EW30SX_remain.dll", lblProduct.Text);

            //01. Khởi tạo hiển thị control
            _initTestingView(lblResult, lbl_MacEthernet);

            VNPT_TextDisplay.SetText(lblResult, "Waiting...");

            //02. Mở cổng COM IMAP
            if (!_openSerialPort(lbl_OpenCom, lbl_OpenComTime)) {
                UpdateRtbAll(string.Format("\r\n[FAIL] Không mở được cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("\r\n[PASS] mở cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));

            //03. Chờ IMAP PCB Board khởi động hoàn thành
            st.Reset(); st.Restart();
            r = _waitBootingComplete(lbl_WaitBoot, lbl_WaitBootTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian Board khởi động: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Board khởi động vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Board khởi động!\r\n\r\n");

            //04. login IMAP
            st.Reset(); st.Restart();
            r = _loginIMAP(lbl_Login, lbl_LoginTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian login IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Login IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Login IMAP!\r\n\r\n");


            //06. Thiết lập địa chỉ cổng LAN cho IMAP
            st.Reset(); st.Restart();
            r = _setEthernetIP_NoPing(lbl_ChangeIP, lbl_ChangeIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian đổi IP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Đổi địa chỉ IP ethernet vượt quá timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Đổi địa chỉ IP Ethernet!\r\n\r\n");


            //07. Ping IMAP
            st.Reset(); st.Restart();
            r = _waitPingToImap(lbl_PingNewIP, lbl_PingNewIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian ping IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Ping IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Ping IMAP!\r\n\r\n");


            //08. Chuyển IMAP sang mode calib
            st.Reset(); st.Restart();
            r = _switchCalibMode(lbl_SwitchMode5G, lbl_SwitchMode5GTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian chuyển mode calib 5G: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Chuyển mode calib 5G vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Chuyển mode calib 5G!\r\n\r\n");


            //09. Calib và verify Board
            lbl_Calib5G.Text = "Waiting...";
            var tt = new Stopwatch();
            tt.Start();
            r = _calibAndVerify();
            tt.Stop();
            lbl_Calib5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
            lbl_Calib5G.Text = r ? "Passed" : "Failed";

            if (!r) {
                UpdateRtbAll("\r\n[FAIL] Calib và verify tín hiệu WIFI!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                //goto END;
            }
            UpdateRtbAll("\r\n[Finished] Calib và verify tín hiệu WIFI!\r\n\r\n");

            goto END;

        END:
            Thread.Sleep(500);
            serialPort1.Close();
            btn_disconnect.Visible = false;
            btnStart.Enabled = true;

            VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
            showResult(r ? "PASS" : "FAIL", lblResult);
            totalStopwatch.Stop();
            UpdateRtbAll("\r\n------------------------------------------------\r\n");
            UpdateRtbAll(string.Format("Tổng thời gian: {0} sec\r\n", totalStopwatch.ElapsedMilliseconds / 1000));
            if (r) {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "calib", "PASS"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "calib", "PASS"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "calib", "PASS"); //log uart
            }
            else {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "calib", "FAIL"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "calib", "FAIL"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "calib", "FAIL"); //log uart
            }

            //cảnh báo mạch đã calib tần số // 13.02.2020
            string qspr_Str = richTextBoxStatus.Text;
            if (qspr_Str.Contains("Cal values already present in OTP !")) {
                MessageBox.Show("Mạch đã có dữ liệu calib tần số trong OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }

        private bool measureAtt() {
            Control.CheckForIllegalCrossThreadCalls = false;
            totalStopwatch = new Stopwatch();
            totalStopwatch.Reset(); totalStopwatch.Start();
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;

            //reset giá trị golden
            lbl_mac_att.Text = "";
            File.WriteAllText("EW30SX_golden.dll", "");
            File.WriteAllText("EW30SX_verify.dll", "");
            this.Invoke(new Action(() => {
                this.panel10.Controls.Clear();
            }));


            //01. Khởi tạo hiển thị control - ok
            _initTestingView(lblaResult, lbl_aMacEthernet);
            VNPT_TextDisplay.SetText(lblaResult, "Waiting...");

            //02. Mở cổng COM IMAP - ok
            if (!_openSerialPort(lbl_aOpenCom, lbl_aOpenComTime)) {
                UpdateRtbAll(string.Format("\r\n[FAIL] Không mở được cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("\r\n[PASS] mở cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));

            //03. Chờ IMAP PCB Board khởi động hoàn thành
            st.Reset(); st.Restart();
            r = _waitBootingComplete(lbl_aWaitBoot, lbl_aWaitBootTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian Board khởi động: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Board khởi động vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Board khởi động!\r\n\r\n");

            //04. login IMAP
            st.Reset(); st.Restart();
            r = _loginIMAP(lbl_aLogin, lbl_aLoginTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian login IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Login IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Login IMAP!\r\n\r\n");


            //06. Thiết lập địa chỉ cổng LAN cho IMAP
            st.Reset(); st.Restart();
            r = _setEthernetIP_NoPing(lbl_aChangeIP, lbl_aChangeIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian đổi IP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Đổi địa chỉ IP ethernet vượt quá timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Đổi địa chỉ IP Ethernet!\r\n\r\n");


            //07. Ping IMAP
            st.Reset(); st.Restart();
            r = _waitPingToImap(lbl_aPingNewIP, lbl_aPingNewIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian ping IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Ping IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Ping IMAP!\r\n\r\n");


            //08. Chuyển IMAP sang mode calib
            st.Reset(); st.Restart();
            r = _switchCalibMode(lbl_aSwitchMode5G, lbl_aSwitchMode5GTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian chuyển mode calib 5G: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Chuyển mode calib 5G vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Chuyển mode calib 5G!\r\n\r\n");


            //09. measure and calculate attenuation 5G
            lbl_Att5G.Text = "Waiting...";
            var tt = new Stopwatch();
            tt.Start();

            //run testtree đo suy hao 5G
            VNPT_TextDisplay.ClearText(richTextBoxStatus);
            r = _calibAndVerify();
            //if (!r) {
            //    UpdateRtbAll("\r\n[FAIL] run testtree đo suy hao!\r\n\r\n");
            //    VNPT_TextDisplay.SetText(lblaResult, "FAIL");
            //    //goto END;
            //}
            UpdateRtbAll("\r\n[Finished] run testtree đo suy hao!\r\n\r\n");

            //get golden standard
            r = _getGoldenStandardValues(g_macEth0);
            if (!r) {
                UpdateRtbAll("[FAIL] Mạch golden không đúng!\n\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                lbl_Att5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
                lbl_Att5G.Text = r ? "Passed" : "Failed";
                goto END;
            }
            UpdateRtbAll(string.Format("{0}{1}{2}\n", "Frequency(MHz)".PadRight(20, ' '), "pw_Anten1(dbm)".PadRight(20, ' '), "pw_Anten2(dbm)".PadRight(20, ' ')));
            foreach (var item in goldenStandardValues) {
                UpdateRtbAll(string.Format("{0}{1}{2}\n", item.Frequency.PadRight(30, ' '), item.powerAnten1.ToString().PadRight(30, ' '), item.powerAnten2.ToString().PadRight(30, ' ')));
            }
            UpdateRtbAll("[PASS] Đọc dữ liệu tiêu chuẩn của mạch golden!\n\n");

            //Extract tree log to struct
            r = _getGoldenTestResults();
            if (!r) {
                UpdateRtbAll("[FAIL] Dữ liệu test mạch golden sai định dạng!\n\n");
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                lbl_Att5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
                lbl_Att5G.Text = r ? "Passed" : "Failed";
                goto END;
            }
            UpdateRtbAll(string.Format("{0}{1}{2}\n", "Frequency(MHz)".PadRight(20, ' '), "Antenna".PadRight(20, ' '), "Average_Power(dbm)".PadRight(20, ' ')));
            foreach (var item in goldenTestResults) {
                UpdateRtbAll(string.Format("{0}{1}{2}\n", item.Frequency.PadRight(30, ' '), item.Antenna.ToString().PadRight(30, ' '), Math.Round(item.averagePowers.Average(), 4).ToString().PadRight(30, ' ')));
            }
            UpdateRtbAll("[PASS] Đọc dữ liệu test của mạch golden!\n\n");

            //Tính độ lệch công suất mạch golden
            r = _getGoldenPowerDefferences();
            if (!r) {
                UpdateRtbAll(string.Format("[FAIL] Không tính được độ lệch công suất!\n\n"));
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                lbl_Att5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
                lbl_Att5G.Text = r ? "Passed" : "Failed";
                goto END;
            }
            UpdateRtbAll(string.Format("\nAnten1(dBm): min={0}, max={1}\n",
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten1).ToList().Min(),
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten1).ToList().Max()
                         ));
            UpdateRtbAll(string.Format("Anten2(dBm): min={0}, max={1}\n\n",
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten2).ToList().Min(),
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten2).ToList().Max()
                         ));
            UpdateRtbAll(string.Format("{0}{1}{2}{3}\n", "Frequency".PadRight(15, ' '), "Diff_At1(dbm)".PadRight(15, ' '), "Diff_At2(dbm)".PadRight(15, ' '), "Result".PadRight(15, ' ')));
            foreach (var item in powerDifferenceValues) {
                UpdateRtbAll(string.Format("{0}{1}{2}{3}\n", item.Frequency.PadRight(30, ' '), item.powerDifferenceAnten1.ToString().PadRight(30, ' '), item.powerDifferenceAnten2.ToString().PadRight(30, ' '), item.resultTotal));
            }
            UpdateRtbAll("[PASS] Tính độ lệch công suất mạch golden!\n\n");


            //update file pathloss
            r = _updateFilePathLoss();
            if (!r) {
                UpdateRtbAll(string.Format("[FAIL] Update file pathloss {0} thất bại!\n\n", PathXMLSuyHao));
                VNPT_TextDisplay.SetText(lblaResult, "FAIL");
                lbl_Att5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
                lbl_Att5G.Text = r ? "Passed" : "Failed";
                goto END;
            }
            UpdateRtbAll(string.Format("[PASS] Update file pathloss {0} đã thành công!\n\n", PathXMLSuyHao));

            tt.Stop();
            lbl_Att5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
            lbl_Att5G.Text = r ? "Passed" : "Failed";

            System.Diagnostics.Process.Start(PathXMLSuyHao);
            goto END;

        END:
            Thread.Sleep(500);
            serialPort1.Close();
            btn_disconnect.Visible = false;
            btnStart.Enabled = true;

            VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
            showResult(r ? "PASS" : "FAIL", lblaResult);
            totalStopwatch.Stop();
            UpdateRtbAll("\r\n------------------------------------------------\r\n");
            UpdateRtbAll(string.Format("Tổng thời gian: {0} sec\r\n", totalStopwatch.ElapsedMilliseconds / 1000));
            if (r) {
                lbl_mac_att.Text = g_macEth0;
                File.WriteAllText("EW30SX_golden.dll", g_macEth0);
                lblProduct.Text = maxproduct.ToString();
                File.WriteAllText("EW30SX_remain.dll", lblProduct.Text);
                File.WriteAllText("EW30SX_verify.dll", "");
                this.Invoke(new Action(() => {
                    this.panel10.Controls.Clear();
                }));

                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "att", "PASS"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "att", "PASS"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "att", "PASS"); //log uart
            }
            else {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "att", "FAIL"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "att", "FAIL"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "att", "FAIL"); //log uart
            }

            //cảnh báo mạch đã calib tần số // 13.02.2020
            string qspr_Str = richTextBoxStatus.Text;
            if (qspr_Str.Contains("Cal values already present in OTP !")) {
                MessageBox.Show("Mạch đã có dữ liệu calib tần số trong OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }

        private bool verifyAtt() {
            Control.CheckForIllegalCrossThreadCalls = false;
            totalStopwatch = new Stopwatch();
            totalStopwatch.Reset(); totalStopwatch.Start();
            Stopwatch st = new Stopwatch();
            st.Start();
            bool r = false;

            //01. Khởi tạo hiển thị control - ok
            _initTestingView(lblvResult, lbl_vMacEthernet);

            VNPT_TextDisplay.SetText(lblvResult, "Waiting...");

            //02. Mở cổng COM IMAP - ok
            if (!_openSerialPort(lbl_vOpenCom, lbl_vOpenComTime)) {
                UpdateRtbAll(string.Format("\r\n[FAIL] Không mở được cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("\r\n[PASS] mở cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));

            //03. Chờ IMAP PCB Board khởi động hoàn thành
            st.Reset(); st.Restart();
            r = _waitBootingComplete(lbl_vWaitBoot, lbl_vWaitBootTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian Board khởi động: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Board khởi động vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Board khởi động!\r\n\r\n");

            //04. login IMAP
            st.Reset(); st.Restart();
            r = _loginIMAP(lbl_vLogin, lbl_vLoginTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian login IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Login IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Login IMAP!\r\n\r\n");


            //06. Thiết lập địa chỉ cổng LAN cho IMAP
            st.Reset(); st.Restart();
            r = _setEthernetIP_NoPing(lbl_vChangeIP, lbl_vChangeIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian đổi IP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Đổi địa chỉ IP ethernet vượt quá timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Đổi địa chỉ IP Ethernet!\r\n\r\n");


            //07. Ping IMAP
            st.Reset(); st.Restart();
            r = _waitPingToImap(lbl_vPingNewIP, lbl_vPingNewIPTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian ping IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Ping IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Ping IMAP!\r\n\r\n");


            //08. Chuyển IMAP sang mode calib 5G
            st.Reset(); st.Restart();
            r = _switchCalibMode(lbl_vSwitchMode5G, lbl_vSwitchMode5GTime);
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian chuyển mode calib 5G: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Chuyển mode calib 5G vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Chuyển mode calib 5G!\r\n\r\n");


            //09. measure and calculate attenuation 5G
            lbl_Verify5G.Text = "Waiting...";
            var tt = new Stopwatch();
            tt.Start();

            //run testtree verify attenuation
            r = _calibAndVerify();
            if (!r) {
                UpdateRtbAll("\r\n[FAIL] run testtree verify attenuation!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                //goto END;
            }
            UpdateRtbAll("\r\n[Finished] run testtree verify attenuation!\r\n\r\n");

            //Get golden standard
            r = _getGoldenStandardValues(g_macEth0);
            if (!r) {
                UpdateRtbAll("[FAIL] Mạch golden không đúng!\n\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("{0}{1}{2}\n", "Frequency(MHz)".PadRight(20, ' '), "pw_Anten1(dbm)".PadRight(20, ' '), "pw_Anten2(dbm)".PadRight(20, ' ')));
            foreach (var item in goldenStandardValues) {
                UpdateRtbAll(string.Format("{0}{1}{2}\n", item.Frequency.PadRight(30, ' '), item.powerAnten1.ToString().PadRight(30, ' '), item.powerAnten2.ToString().PadRight(30, ' ')));
            }
            UpdateRtbAll("[PASS] Đọc dữ liệu tiêu chuẩn của mạch golden!\n\n");

            //Extract tree log to struct
            r = _getGoldenTestResults();
            if (!r) {
                UpdateRtbAll("[FAIL] Dữ liệu test mạch golden sai định dạng!\n\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("{0}{1}{2}\n", "Frequency(MHz)".PadRight(20, ' '), "Antenna".PadRight(20, ' '), "Average_Power(dbm)".PadRight(20, ' ')));
            foreach (var item in goldenTestResults) {
                UpdateRtbAll(string.Format("{0}{1}{2}\n", item.Frequency.PadRight(30, ' '), item.Antenna.ToString().PadRight(30, ' '), Math.Round(item.averagePowers.Average(), 4).ToString().PadRight(30, ' ')));
            }
            UpdateRtbAll("[PASS] Đọc dữ liệu test của mạch golden!\n\n");

            //Tính độ lệch công suất mạch golden
            r = _getGoldenPowerDefferences();
            if (!r) {
                UpdateRtbAll("[FAIL] Không thể tính độ lệch công suất của mạch golden!\n\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("\nAnten1(dBm): min={0}, max={1}\n",
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten1).ToList().Min(),
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten1).ToList().Max()
                         ));
            UpdateRtbAll(string.Format("Anten2(dBm): min={0}, max={1}\n\n",
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten2).ToList().Min(),
                         powerDifferenceValues.Select(x => x.powerDifferenceAnten2).ToList().Max()
                         ));
            UpdateRtbAll(string.Format("{0}{1}{2}{3}\n", "Frequency".PadRight(15, ' '), "Diff_At1(dbm)".PadRight(15, ' '), "Diff_At2(dbm)".PadRight(15, ' '), "Result".PadRight(15, ' ')));
            foreach (var item in powerDifferenceValues) {
                UpdateRtbAll(string.Format("{0}{1}{2}{3}\n", item.Frequency.PadRight(30, ' '), item.powerDifferenceAnten1.ToString().PadRight(30, ' '), item.powerDifferenceAnten2.ToString().PadRight(30, ' '), item.resultTotal));
            }
            UpdateRtbAll("[PASS] Tính độ lệch công suất mạch golden!\n\n");

            //Phán định kết quả
            r = _judgeGolden();
            if (!r) {
                UpdateRtbAll("[FAIL] Suy hao trạm không đạt. Vui lòng thiết lập lại giá trị file pathloss!\n\n");
                VNPT_TextDisplay.SetText(lblvResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Suy hao trạm đạt tiêu chuẩn!\n\n");
            tt.Stop();
            lbl_Verify5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
            lbl_Verify5G.Text = r ? "Passed" : "Failed";

            goto END;

        END:
            Thread.Sleep(500);
            serialPort1.Close();
            btn_disconnect.Visible = false;
            btnStart.Enabled = true;

            VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
            showResult(r ? "PASS" : "FAIL", lblvResult);
            totalStopwatch.Stop();
            UpdateRtbAll("\r\n------------------------------------------------\r\n");
            UpdateRtbAll(string.Format("Tổng thời gian: {0} sec\r\n", totalStopwatch.ElapsedMilliseconds / 1000));
            if (r) {
                //update label to panel
                //!!!!!!!!!!!!!!!!!!!!!!!!
                Label l = new Label();
                l.Text = g_macEth0;
                l.ForeColor = Color.Black;
                l.Font = new Font(l.Font, FontStyle.Bold);


                bool z = false;
                foreach (var c in panel10.Controls) {
                    if (c is Label) {
                        Label la = (Label)c;
                        z = la.Text.Equals(g_macEth0);
                        if (z) break;
                    }
                }
                if (z == false) {
                    this.Invoke(new Action(() => {
                        if (panel10.Controls.Count < 2) {
                            l.Location = new Point(panel10.Controls.Count * 100, 2);
                        }
                        else {
                            l.Location = new Point((panel10.Controls.Count - 2) * 100, 22);
                        }
                        l.Size = new Size(100, 20);
                        panel10.Controls.Add(l);
                        File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "EW30SX_verify.dll", new string[] { l.Text });
                    }));
                }
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "veri", "PASS"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "veri", "PASS"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "veri", "PASS"); //log uart
            }
            else {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "veri", "FAIL"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "veri", "FAIL"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "veri", "FAIL"); //log uart
            }

            //cảnh báo mạch đã calib tần số // 13.02.2020
            string qspr_Str = richTextBoxStatus.Text;
            if (qspr_Str.Contains("Cal values already present in OTP !")) {
                MessageBox.Show("Mạch đã có dữ liệu calib tần số trong OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }

        #region sub-Function-calib

        //ok
        private void _initTestingView(Label lb_result, Label lb_mac) {
            btnStart.Enabled = false;
            //btn_disconnect.Visible = true;
            VNPT_TextDisplay.SetText(lb_statusconnect, "Connected");
            VNPT_TextDisplay.ClearText(richTextBoxStatus);
            lb_result.Text = "";
            lb_mac.Text = "-";
            VNPT_TextDisplay.ClearText(rtbRunTestAll);
            VNPT_TextDisplay.ClearText(rtbCOMPort);
            g_readFromCOM1 = "";
            g_HienthidataCOM = "";
        }

        //ok
        private bool _openSerialPort(Label lb_open_com, Label lb_open_com_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_open_com.Text = "Waiting...";

            bool ret = false;
            try {
                UpdateRtbAll(">>> Đang chờ mở cổng COM\n");
                UpdateRtbAll("Waitting.");

                serialPort1.PortName = lbl_comport.Text;
                serialPort1.Open();
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                ret = serialPort1.IsOpen;
            }
            catch {
                ret = false;
            }

            st.Stop();
            lb_open_com_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_open_com.Text = ret ? "Passed" : "Failed";
            return ret;
        }

        //ok
        private bool _waitBootingComplete(Label lb_wait_boot, Label lb_wait_boot_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_wait_boot.Text = "Waiting...";

            bool r = false;
            try {
                int count = 0, d = 0, len = 0, rep_len = 0;
                int wait_no_response = timewaitrespone;
                int time_boot_complete = timebootcomplete;
                int delay_time = 500;

                UpdateRtbAll(">>> Đang chờ Board khởi động\n");
                UpdateRtbAll("Waitting.");
            REP:
                count++;
                UpdateRtbAll(string.Format("..{0}", count));

                if (g_readFromCOM1.Replace("\r", "").Replace("\n", "").Trim().Length == 0) {
                    d++;
                    if (d < (wait_no_response * 1000) / delay_time) {
                        Thread.Sleep(delay_time);
                        goto REP;
                    }
                    else {
                        serialPort1.WriteLine(""); Thread.Sleep(300);
                        if (g_readFromCOM1.Contains("root@VNPT:/#") || g_readFromCOM1.Contains("root@VNPT:~#")) { r = true; }
                        if (g_readFromCOM1.Contains("root@VNPT:/lib/firmware/QCA9888/hw.2#")) {
                            serialPort1.WriteLine("cd");
                            Thread.Sleep(300);
                            r = true;
                        }
                    }

                    if ((count < (timeboot * 1000) / delay_time) && r == false) {
                        d = 0;
                        Thread.Sleep(delay_time);
                        goto REP;
                    }
                }
                else {
                    d = 0;
                    if (g_readFromCOM1.Contains(g_logstart) == false) {
                        if (count < (timeboot * 1000) / delay_time) {
                            if (len == g_readFromCOM1.Length) {
                                rep_len++;
                                if (rep_len >= (wait_no_response * 1000) / delay_time) {
                                    string[] buffer = g_readFromCOM1.Split('\n');
                                    int max = buffer.Length - 1;
                                    string data = "";
                                    double db = double.MinValue;
                                    bool zzz = false;

                                    for (int i = max; i >= 0; i--) {
                                        if (string.IsNullOrEmpty(buffer[i]) == false && string.IsNullOrWhiteSpace(buffer[i]) == false) {
                                            if (buffer[i].Contains("[") && buffer[i].Contains("]")) {
                                                data = buffer[i];
                                                data = data.Split(']')[0].Split('[')[1].Trim();
                                                zzz = double.TryParse(data, out db);
                                                if (zzz) break;
                                            }
                                        }
                                    }

                                    if (zzz) {
                                        if (db >= (time_boot_complete * 1000) / delay_time) {
                                            serialPort1.WriteLine(""); Thread.Sleep(300);
                                            if (g_readFromCOM1.Contains("root@VNPT:/#") || g_readFromCOM1.Contains("root@VNPT:~#")) { r = true; }
                                        }
                                        else {
                                            rep_len = 0;
                                            Thread.Sleep(delay_time);
                                            goto REP;
                                        }
                                    }
                                    else {
                                        rep_len = 0;
                                        Thread.Sleep(delay_time);
                                        goto REP;
                                    }
                                }
                                else {
                                    Thread.Sleep(delay_time);
                                    goto REP;
                                }
                            }
                            else {
                                len = g_readFromCOM1.Length;
                                rep_len = 0;
                                Thread.Sleep(delay_time);
                                goto REP;
                            }
                        }
                    }
                    else r = true;
                }
            }
            catch (Exception ex) {
                UpdateRtbAll(string.Format("{0}\r\n", ex.ToString()));
                r = false;
            }

            st.Stop();
            lb_wait_boot_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_wait_boot.Text = r ? "Passed" : "Failed";
            return r;

        }

        private bool pingNetwork(string ip) {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 60;

            try {
                PingReply reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch {
                return false;
            }
        }

        //ok
        private bool _loginIMAP(Label lb_login, Label lb_login_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_login.Text = "Waiting...";

            bool r = false;
            try {
                int count = 0;
                UpdateRtbAll(">>> Đang chờ login IMAP\n");
                UpdateRtbAll("Waitting.");
            REP:
                count++;
                g_readFromCOM1 = "";
                serialPort1.WriteLine("");
                Thread.Sleep(300);
                r = g_readFromCOM1.Contains("root@VNPT:/#") || g_readFromCOM1.Contains("root@VNPT:~#");
                if (!r) {
                    if (count < 10) {
                        Thread.Sleep(300);
                        goto REP;
                    }
                }
            }
            catch {
                r = false;
            }

            st.Stop();
            lb_login_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_login.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _setEthernetIP() {
            try {
                serialPort1.WriteLine("");
                Thread.Sleep(100);
                serialPort1.WriteLine("ifconfig br-lan " + IP_Address);
                Thread.Sleep(100);
                serialPort1.WriteLine("");
                Thread.Sleep(100);
                serialPort1.WriteLine("netmask 255.255.255.0 up");
                Thread.Sleep(100);

                bool r = false;
                int count = 0;
                UpdateRtbAll(">>> Thực hiện thiết lập địa chỉ cổng LAN cho IMAP\n");
                UpdateRtbAll("Waitting.");
            REP:
                count++;
                UpdateRtbAll(string.Format("..{0}", count));
                r = pingNetwork(IP_Address);
                if (!r) {
                    if (count < timeping) {
                        Thread.Sleep(1000);
                        goto REP;
                    }
                }
                return r;
            }
            catch (Exception ex) {
                UpdateRtbAll(string.Format("{0}\r\n", ex.ToString()));
                return false;
            }
        }

        //ok
        private bool _setEthernetIP_NoPing(Label lb_change_ip, Label lb_change_ip_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_change_ip.Text = "Waiting...";

            bool r = false;
            try {
                serialPort1.WriteLine("");
                Thread.Sleep(100);
                serialPort1.WriteLine("ifconfig br-lan " + IP_Address);
                Thread.Sleep(100);
                serialPort1.WriteLine("");
                Thread.Sleep(100);
                serialPort1.WriteLine("netmask 255.255.255.0 up");
                Thread.Sleep(100);
                r = true;
            }
            catch {
                r = false;
            }

            st.Stop();
            lb_change_ip_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_change_ip.Text = r ? "Passed" : "Failed";
            return r;
        }

        //ok
        private bool _waitPingToImap(Label lb_ping, Label lb_ping_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_ping.Text = "Waiting...";

            bool r = false;
            int delay_time = 100;
            try {
                int count = 0;
                UpdateRtbAll(">>> Thực hiện ping địa chỉ cổng LAN cho IMAP\n");
                UpdateRtbAll("Waitting.");
            REP:
                count++;
                r = pingNetwork(IP_Address);
                if (!r) {
                    if (count < (timeping * 1000) / delay_time) {
                        Thread.Sleep(delay_time);
                        goto REP;
                    }
                }
            }
            catch {
                r = false;
            }

            st.Stop();
            lb_ping_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_ping.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _sendwait_command(string cmd, int timeout_sec, params string[] end_text) {
            serialPort1.WriteLine("");
            Thread.Sleep(100);
            serialPort1.WriteLine(cmd);

            int count = 0;
            int delay_value = 100;
            int max_count = (timeout_sec * 1000) / delay_value;
            g_serial = "";
            bool r = false;
        RE:
            count++;
            Thread.Sleep(delay_value);
            foreach (var txt in end_text) {
                r = g_serial.ToLower().Contains(txt.ToLower());
                if (r) break;
            }

            if (!r) {
                if (count < max_count) {
                    goto RE;
                }
            }
            return r;
        }

        //ok
        private bool _switchCalibMode(Label lb_switch_5g, Label lb_switch_5g_time) {
            Stopwatch st = new Stopwatch();
            st.Start();
            lb_switch_5g.Text = "Waiting...";

            bool r = false;
            try {
                UpdateRtbAll(">>> Thực hiện thiết lập IMAP về chế độ calib 5G\n");

                UpdateRtbAll("Send Command: cd tmp/ \n");
                bool ret = _sendwait_command("cd tmp/", 3, "root@VNPT:/tmp#");
                if (!ret) goto END;
                UpdateRtbAll("Send Command: cp wifi1.caldata wifi0.caldata \n");
                ret = _sendwait_command("cp wifi1.caldata wifi0.caldata", 3, "root@VNPT:/tmp#");
                if (!ret) goto END;
                UpdateRtbAll("Send Command: cd .. \n");
                ret = _sendwait_command("cd ..", 3, "root@VNPT:/#");
                if (!ret) goto END;
                UpdateRtbAll("Send Command: cd /lib/firmware/QCA9888/hw.2 \n");
                ret = _sendwait_command("cd lib/firmware/QCA9888/hw.2", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2");
                if (!ret) goto END;
                UpdateRtbAll("Send Command: rm boarddata_0.bin \n");
                ret = _sendwait_command("rm boarddata_0.bin", 3, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                UpdateRtbAll("Pass\n");

                UpdateRtbAll("[OK] Di chuyển tới thư mục chứa file BoardData thành công \n");
                UpdateRtbAll("Gửi Command thực thiện copy file BoardData 5G\n");
                ret = _sendwait_command("cp boardData_2_0_QCA9888_5G_YA105.bin boarddata_0.bin", 3, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                ret = _sendwait_command("sync", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                ret = _sendwait_command("wifi down", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;

                UpdateRtbAll("Thực hiện gửi lệnh turn on QCMBR trong IMAP \n");
                UpdateRtbAll("Gửi command: /etc/init.d/qcmbr start\n");
                ret = _sendwait_command("/etc/init.d/qcmbr start", 30, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                r = ret;
            }
            catch { r = false; }

        END:
            st.Stop();
            lb_switch_5g_time.Text = $"{st.ElapsedMilliseconds} ms";
            lb_switch_5g.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _calibAndVerify() {
            try {
                TreeTestStatus = 0;
                string xttFileName = g_mode == 0 ? PathXTTFile : PathATTFile;
                _testTree = _scheduler.OpenXTT(xttFileName);
                _testTree.RunTreeDone += new QSPRTestTree.RunTreeDoneEventHandler(_testTree_RunTreeDone);

                if (_testTree != null) {
                    if (g_macEth0.Length == 0) g_macEth0 = getMacFromDut();
                    switch (g_mode) {
                        case 0: { lbl_MacEthernet.Text = g_macEth0; break; }
                        case 1: { lbl_aMacEthernet.Text = g_macEth0; break; }
                        case 2: { lbl_vMacEthernet.Text = g_macEth0; break; }
                    }

                    g_SN = "IMAP_" + g_macEth0;
                    if (g_macEth0 == "") return false;
                    try {
                        AddMac_Value_tam = g_macEth0.Substring(6, 6);
                        g_mac2G_onboard = g_macEth0.Substring(0, 6) + GetMAC2G(AddMac_Value_tam);
                        g_mac5G_onboard = g_macEth0.Substring(0, 6) + GetMAC5G(AddMac_Value_tam);
                    }
                    catch { return false; }


                    //check golden
                    string dir = AppDomain.CurrentDomain.BaseDirectory;
                    string golden_file = string.Format("EW30SX_Golden\\GOLDEN_{0}.txt", g_macEth0.ToUpper());
                    string golden_file_full_name = Path.Combine(dir, golden_file);

                    if (g_mode == 0) {
                        if (File.Exists(golden_file_full_name)) {
                            UpdateRtbAll(string.Format("Không thể calib golden sample {0}.", g_macEth0));
                            return false;
                        }
                    }
                    else {
                        if (!File.Exists(golden_file_full_name)) {
                            UpdateRtbAll(string.Format("MAC {0} không phải là golden sample.", g_macEth0));
                            return false;
                        }
                    }

                    UpdateRtbAll($"... Mac Ethernet : {g_macEth0}\n");
                    UpdateRtbAll($"... Serial : {g_SN}\n");
                    UpdateRtbAll($"... Mac Wlan 2G : {g_mac2G_onboard}\n");
                    UpdateRtbAll($"... Mac Wlan 5G : {g_mac5G_onboard}\n");

                    cvStopwatch = new Stopwatch();
                    cvStopwatch.Start();
                    _scheduler.SetGlobalVariable("SN", g_macEth0);
                    _scheduler.SetGlobalVariable("mac", g_mac2G_onboard);
                    _scheduler.SetGlobalVariable("5gMAC", g_mac5G_onboard);

                    _testTree.RunTree();
                    UpdateUI(true);
                }
                else return false;

                int count = 0;
                bool r = false;
                UpdateRtbAll(">>> Thực hiện calib và verify tín hiệu WIFI của IMAP\n");
                UpdateRtbAll("Waitting.");
            RE:
                count++;
                UpdateRtbAll(string.Format("..{0}", count));
                r = TreeTestStatus != 0;
                if (!r) {
                    Thread.Sleep(1000);
                    goto RE;
                }

                return TreeTestStatus == 1;
            }
            catch (Exception ex) {
                UpdateRtbAll(string.Format("{0}\r\n", ex.ToString()));
                return false;
            }
        }

        private string getMAC(string frmText) {
            string tempMac = string.Empty;
            FrmGetMACaddress frm = new FrmGetMACaddress();
            //Lay gia tri MAC
            frm.Text = frmText;
            frm.ShowDialog();

            while (CheckForm(frm)) {
            }
            Thread.Sleep(200);
            if (frm.validMAC)
                tempMac = frm.getMAC;
            else
                tempMac = "";

            return tempMac;
        }

        private string getMacFromDut() {
            int count = 0;
        ReGET_MAC:
            count++;
            g_serial = "";
            serialPort1.WriteLine("cat /sys/class/net/eth0/address");
            Thread.Sleep(300);
            string data = g_serial;
            if (data == null || data == "" || data == string.Empty) data = "null";
            if (data == "null") {
                if (count < 3) goto ReGET_MAC;
                else return "";
            }
            data = data.ToLower();
            data = data.Replace("cat /sys/class/net/eth0/address", "").Replace("\r", "").Replace("\n", "").Replace(":", "").Replace("root@vnpt:~#", "");
            data = data.Substring(0, 12).ToUpper();
            bool r = Regex.IsMatch(data, "^[0-9,A-F]{12}$", RegexOptions.IgnoreCase);
            if (!r) {
                if (count < 3) goto ReGET_MAC;
                else return "";
            }
            return data;
        }

        private string GetMAC2G(string mac) {
            string hexMAC = "FAIL";
            try {
                int num = Int32.Parse(mac, System.Globalization.NumberStyles.HexNumber);
                num = num + 1;
                hexMAC = num.ToString("X").Trim();
                int hexMAC_len = hexMAC.Length;
                if (hexMAC_len < 6) {
                    for (int i = 0; i < 6 - hexMAC_len; i++) {
                        hexMAC = "0" + hexMAC;
                    }
                }
                else
                    if (hexMAC_len == 7) {
                    hexMAC = hexMAC.Substring(0, 6);
                }
            }
            catch { }

            return hexMAC;
        }

        private string GetMAC5G(string mac) {
            string hexMAC = "FAIL";
            try {
                int num = Int32.Parse(mac, System.Globalization.NumberStyles.HexNumber);
                num = num + 2;
                hexMAC = num.ToString("X").Trim();
                int hexMAC_len = hexMAC.Length;
                if (hexMAC_len < 6) {
                    for (int i = 0; i < 6 - hexMAC_len; i++) {
                        hexMAC = "0" + hexMAC;
                    }
                }
                else
                    if (hexMAC_len == 7) {
                    hexMAC = hexMAC.Substring(0, 6);
                }
            }
            catch { }

            return hexMAC;
        }

        private bool pingToBoard(RichTextBox rtb, string hostAddress) {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            // Use the default Ttl value which is 128, 
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 60;
            try {
                PingReply reply = pingSender.Send(hostAddress, timeout, buffer, options);
                if (reply.Status == IPStatus.Success) {
                    return true;
                }
                else {
                    UpdateRtbAll("[Ping_NoReply] Chưa Ping được tới Board. \n");
                    return false;
                }
            }
            catch (System.Exception ex) {
                UpdateRtbAll("[Ping_Error] Xảy ra lỗi khi Ping. Chưa Ping được Board.\n");
                return false;
            }
        }

        #endregion

        #region Suy-Hao

        bool _getGoldenStandardValues(string mac_ethernet) {
            UpdateRtbAll(">>> Thực hiện đọc dữ liệu tiêu chuẩn của mạch golden\n");

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            //string golden_file = string.Format("Golden\\GOLDEN_{0}.csv", mac_ethernet.ToUpper());
            string golden_file = string.Format("EW30SX_Golden\\GOLDEN_{0}.txt", mac_ethernet.ToUpper());
            string golden_file_full_name = Path.Combine(dir, golden_file);
            UpdateRtbAll(golden_file + "\n");
            UpdateRtbAll("Waitting.\n");

            if (!File.Exists(golden_file_full_name)) return false;

            string golden_content = File.ReadAllText(golden_file_full_name);
            if (golden_content == null || golden_content.Length == 0) return false;

            string[] buffer = golden_content.Split('\n');
            goldenStandardValues = new List<goldenFrequencyInfo>();
            int count = 0;
            int max_count = buffer.Length - 1;

        RE:
            count++;
            string data_line = buffer[count];
            if (data_line != null && data_line.Length > 0) {
                string[] bff = data_line.Split(',');
                goldenFrequencyInfo item = new goldenFrequencyInfo();
                item.Frequency = bff[0];
                item.powerAnten1 = double.Parse(bff[1]);
                item.powerAnten2 = double.Parse(bff[2]);
                goldenStandardValues.Add(item);
            }
            if (count < max_count) goto RE;
            return goldenStandardValues.Count > 0;

        }

        bool _getGoldenTestResults() {
            try {
                UpdateRtbAll(">>> Thực hiện đọc dữ liệu test của mạch golden\n");
                UpdateRtbAll("Waitting.\n");
                string log_content = richTextBoxStatus.Text;

                if (log_content == null || log_content.Length == 0) return false;
                string[] buffer = log_content.Split('\n');

                goldenTestResults = new List<testFrequencyInfo>();
                int count = 0;
                int max_count = buffer.Length - 1;
                string result_channel = "";
                const string STRING_START = "Test started: WlanTxVerifyPowerTest at:";
                const string STRING_END = "Test finished: WlanTxVerifyPowerTest with result:";
                bool add_flag = false;

            RE:
                string data_line = buffer[count];
                if (data_line.ToLower().Contains(STRING_START.ToLower())) { add_flag = true; }
                if (add_flag == true) {
                    result_channel += data_line + "\n";
                }
                if (data_line.ToLower().Contains(STRING_END.ToLower())) {
                    goldenTestResults.Add(_getWlanVerifyResultItem(result_channel));
                    result_channel = "";
                    add_flag = false;
                }
                count++;
                if (count < max_count) goto RE;

                return goldenTestResults.Count > 0;
            }
            catch {
                return false;
            }
        }


        testFrequencyInfo _getWlanVerifyResultItem(string result_channel) {
            testFrequencyInfo itemResult = new testFrequencyInfo();
            string[] buffer = result_channel.Split('\n');
            foreach (var data_line in buffer) {
                //get average power
                if (data_line.ToLower().Contains("Average Power:".ToLower())) {
                    string s = data_line.ToLower();
                    string[] bff = s.Split(new string[] { "average power:" }, StringSplitOptions.None);
                    string pw_str = bff[1].Split(new string[] { "dbm" }, StringSplitOptions.None)[0].Trim();
                    double pw_double;
                    bool r = double.TryParse(pw_str, out pw_double);
                    if (r) itemResult.averagePowers.Add(pw_double);
                }
                //get frequency && antenna
                if (data_line.ToLower().Contains("channel") && data_line.ToLower().Contains("wlan_chain")) {
                    string s = data_line.ToLower();
                    int idx = s.IndexOf("channel");
                    string freq = s.Substring(idx + 9, 4);
                    idx = s.IndexOf("wlan_chain");
                    string anten = s.Substring(idx + 11, 1);
                    itemResult.Antenna = anten;
                    itemResult.Frequency = freq;
                }
            }
            return itemResult;
        }

        double _getWlanAveragePower(string freq, string anten, List<testFrequencyInfo> wlanTestResults) {
            try {
                var items = wlanTestResults.Where(x => x.Frequency.Equals(freq) && x.Antenna.Equals(anten)).Select(x => x.averagePowers);
                List<double> pw_doubles = new List<double>();
                foreach (var item in items) pw_doubles.Add(item.Average());
                return Math.Round(pw_doubles.Average(), 4);
            }
            catch {
                return double.MaxValue;
            }
        }

        bool _getGoldenPowerDefferences() {
            UpdateRtbAll(">>> Thực hiện tính độ lệch công suất của mạch golden\n");
            UpdateRtbAll("Waitting.\n");
            if (goldenStandardValues == null || goldenStandardValues.Count == 0) return false;
            if (goldenTestResults == null || goldenTestResults.Count == 0) return false;

            powerDifferenceValues = new List<powerDifferenceInfo>();
            foreach (var item in goldenStandardValues) {
                double pw_at1 = _getWlanAveragePower(item.Frequency, "1", goldenTestResults);
                double pw_at2 = _getWlanAveragePower(item.Frequency, "2", goldenTestResults);
                if (pw_at1 != double.MaxValue && pw_at2 != double.MaxValue) {
                    powerDifferenceInfo power_diff = new powerDifferenceInfo() { Frequency = item.Frequency, powerDifferenceAnten1 = Math.Round(item.powerAnten1 - pw_at1, 4), powerDifferenceAnten2 = Math.Round(item.powerAnten2 - pw_at2, 4) };
                    powerDifferenceValues.Add(power_diff);
                }
            }

            return powerDifferenceValues.Count > 0;
        }

        bool _judgeGolden() {
            UpdateRtbAll(">>> Phán định suy hao trạm calib\n");
            UpdateRtbAll("Waitting.\n");
            if (powerDifferenceValues == null || powerDifferenceValues.Count == 0) return false;
            bool r = true;
            foreach (var item in powerDifferenceValues) {
                if (!item.resultTotal.Equals("PASS")) { r = false; break; }
            }
            return r;
        }

        bool _updateFilePathLoss() {
            try {
                UpdateRtbAll(">>> Update file pathloss\n");
                UpdateRtbAll("Waitting.\n");

                pathlossInfos = _getPathLossInfoFromFile(PathXMLSuyHao);
                if (pathlossInfos == null || pathlossInfos.Count == 0) return false;

                foreach (var item in powerDifferenceValues) {
                    _updateLossInfo("bh0_lp", item.Frequency, item.powerDifferenceAnten1, pathlossInfos);
                    _updateLossInfo("bh1_lp", item.Frequency, item.powerDifferenceAnten2, pathlossInfos);
                }

                _savePathLossFile(PathXMLSuyHao, pathlossInfos);
                return true;
            }
            catch { return false; }
        }

        List<pathLossFrequencyInfo> _getPathLossInfoFromFile(string file_path_loss) {
            if (System.IO.File.Exists(file_path_loss) == false) return null;
            string[] lines = File.ReadAllLines(file_path_loss);

            List<pathLossFrequencyInfo> lossInfos = new List<pathLossFrequencyInfo>();
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i].ToLower();
                if (line.Contains("<frequency>")) {
                    pathLossFrequencyInfo lossinfo = new pathLossFrequencyInfo();
                    lossinfo.lineNumber = i;
                    lossinfo.Frequency = line.Replace("<frequency>", "").Replace("</frequency>", "").Trim();
                    lossinfo.lossValue = double.Parse(lines[i + 1].ToLower().Replace("<value>", "").Replace("</value>", "").Trim());

                    for (int k = i; k >= 0; k--) {
                        if (lines[k].ToLower().Contains("<pathname>")) {
                            lossinfo.bhName = lines[k].ToLower().Replace("<pathname>", "").Replace("</pathname>", "").Trim();
                            break;
                        }
                    }
                    lossInfos.Add(lossinfo);
                }
            }
            return lossInfos;
        }

        bool _savePathLossFile(string file_path_loss, List<pathLossFrequencyInfo> pathlossInfo) {
            if (System.IO.File.Exists(file_path_loss) == false) return false;
            string[] buffer = File.ReadAllLines(file_path_loss);

            foreach (var item in pathlossInfo) {
                //        <Value>8.0299</Value>
                string s = buffer[item.lineNumber + 1].ToLower();
                string[] bff = s.Split(new string[] { "value" }, StringSplitOptions.None);
                buffer[item.lineNumber + 1] = string.Format("{0}Value>{1}</Value{2}", bff[0], item.lossValue, bff[2]);
            }

            File.WriteAllLines(file_path_loss, buffer);
            //System.Diagnostics.Process.Start(file_path_loss);

            return true;
        }

        bool _updateLossInfo(string bh_name, string center_frequency, double diff_value, List<pathLossFrequencyInfo> pathlossInfo) {
            //update center frequency
            double center_loss_value = 0;
            foreach (var item in pathlossInfo) {
                if (item.Frequency.Equals(center_frequency) && item.bhName.Equals(bh_name)) {
                    item.lossValue = item.lossValue + diff_value;
                    center_loss_value = item.lossValue;
                    break;
                }
            }
            if (center_loss_value == 0) return true;

            //update side frequency
            string lower_freq = (int.Parse(center_frequency) - 10).ToString();
            string upper_freq = (int.Parse(center_frequency) + 10).ToString();
            bool r1 = false;
            bool r2 = false;

            foreach (var item in pathlossInfo) {
                if (item.Frequency.Equals(lower_freq) && item.bhName.Equals(bh_name)) {
                    item.lossValue = center_loss_value;
                    r1 = true;
                }
                if (item.Frequency.Equals(upper_freq) && item.bhName.Equals(bh_name)) {
                    item.lossValue = center_loss_value;
                    r2 = true;
                }
                if (r1 == true && r2 == true) break;
            }

            return true;
        }

        #endregion

        #region MessageBox

        public class MsgBox {
            [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll")]
            static extern bool EndDialog(IntPtr hDlg, int nResult);

            [DllImport("user32.dll")]
            static extern int MessageBoxTimeout(IntPtr hwnd, string txt, string caption,
                int wtype, int wlange, int dwtimeout);

            const int WM_CLOSE = 0x10;

            public static int Show(string text, string caption, int milliseconds, MsgBoxStyle style) {
                return MessageBoxTimeout(IntPtr.Zero, text, caption, (int)style, 0, milliseconds);
            }

            public static int Show(string text, string caption, int milliseconds, int style) {
                return MessageBoxTimeout(IntPtr.Zero, text, caption, style, 0, milliseconds);
            }
            public static int Show(string text, string caption, int milliseconds) {
                return MessageBoxTimeout(IntPtr.Zero, text, caption, 0, 0, milliseconds);
            }
        }

        public enum MsgBoxStyle {
            OK = 0, OKCancel = 1, AbortRetryIgnore = 2, YesNoCancel = 3, YesNo = 4,
            RetryCancel = 5, CancelRetryContinue = 6,

            RedCritical_OK = 16, RedCritical_OKCancel = 17, RedCritical_AbortRetryIgnore = 18,
            RedCritical_YesNoCancel = 19, RedCritical_YesNo = 20,
            RedCritical_RetryCancel = 21, RedCritical_CancelRetryContinue = 22,

            BlueQuestion_OK = 32, BlueQuestion_OKCancel = 33, BlueQuestion_AbortRetryIgnore = 34,
            BlueQuestion_YesNoCancel = 35, BlueQuestion_YesNo = 36,
            BlueQuestion_RetryCancel = 37, BlueQuestion_CancelRetryContinue = 38,

            YellowAlert_OK = 48, YellowAlert_OKCancel = 49, YellowAlert_AbortRetryIgnore = 50,
            YellowAlert_YesNoCancel = 51, YellowAlert_YesNo = 52,
            YellowAlert_RetryCancel = 53, YellowAlert_CancelRetryContinue = 54,

            BlueInfo_OK = 64, BlueInfo_OKCancel = 65, BlueInfo_AbortRetryIgnore = 66,
            BlueInfo_YesNoCancel = 67, BlueInfo_YesNo = 68,
            BlueInfo_RetryCancel = 69, BlueInfo_CancelRetryContinue = 70,
        }

        #endregion

    }
}
