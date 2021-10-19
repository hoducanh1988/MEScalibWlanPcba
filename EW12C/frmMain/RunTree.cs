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
            File.WriteAllText("ew12s_remain.dll", lblProduct.Text);

            //01. Khởi tạo hiển thị control
            _initTestingView();

            VNPT_TextDisplay.SetText(lblResult, "Waiting...");

            //02. Mở cổng COM IMAP
            if (!_openSerialPort()) {
                UpdateRtbAll(string.Format("\r\n[FAIL] Không mở được cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll(string.Format("\r\n[PASS] mở cổng COM của Board {0}!\r\n\r\n", lbl_comport.Text));

            //03. Chờ IMAP PCB Board khởi động hoàn thành
            st.Reset(); st.Restart();
            r = _waitBootingComplete();
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
            r = _loginIMAP();
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
            r = _setEthernetIP_NoPing();
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
            r = _waitPingToImap();
            st.Stop();
            UpdateRtbAll(string.Format("\r\nThời gian ping IMAP: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

            if (!r) {
                UpdateRtbAll("[FAIL] Ping IMAP vượt quá thời gian timeout!\r\n\r\n");
                VNPT_TextDisplay.SetText(lblResult, "FAIL");
                goto END;
            }
            UpdateRtbAll("[PASS] Ping IMAP!\r\n\r\n");

            //08. Chuyển IMAP sang mode calib 5G
            if (isCalib5G) {
                st.Reset(); st.Restart();
                r = _switchCalibMode5G();
                st.Stop();
                UpdateRtbAll(string.Format("\r\nThời gian chuyển mode calib 5G: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

                if (!r) {
                    UpdateRtbAll("[FAIL] Chuyển mode calib 5G vượt quá thời gian timeout!\r\n\r\n");
                    VNPT_TextDisplay.SetText(lblResult, "FAIL");
                    goto END;
                }
                UpdateRtbAll("[PASS] Chuyển mode calib 5G!\r\n\r\n");
            }


            //09. Calib và verify Board 5G
            if (isCalib5G) {
                lbl_Calib5G.Text = "Waiting...";
                var tt = new Stopwatch();
                tt.Start();
                r = _calibAndVerify(is2G: false);
                tt.Stop();
                lbl_Calib5GTime.Text = $"{tt.ElapsedMilliseconds} ms";
                lbl_Calib5G.Text = r ? "Passed" : "Failed";

                if (!r) {
                    UpdateRtbAll("\r\n[FAIL] Calib và verify tín hiệu WIFI!\r\n\r\n");
                    VNPT_TextDisplay.SetText(lblResult, "FAIL");
                    goto END;
                }
                UpdateRtbAll("\r\n[Finished] Calib và verify tín hiệu WIFI!\r\n\r\n");
            }


            //10. Chuyển IMAP sang mode calib 2G
            if (isCalib2G) {
                st.Reset(); st.Restart();
                r = _switchCalibMode2G();
                st.Stop();
                UpdateRtbAll(string.Format("\r\nThời gian chuyển mode calib 2G: {0} sec\r\n", st.ElapsedMilliseconds / 1000));

                if (!r) {
                    UpdateRtbAll("[FAIL] Chuyển mode calib 2G vượt quá thời gian timeout!\r\n\r\n");
                    VNPT_TextDisplay.SetText(lblResult, "FAIL");
                    goto END;
                }
                UpdateRtbAll("[PASS] Chuyển mode calib 2G!\r\n\r\n");
            }


            //11. Calib và verify Board 2G
            if (isCalib2G) {
                lbl_Calib2G.Text = "Waiting...";
                var tv = new Stopwatch();
                tv.Start();
                r = _calibAndVerify(is2G: true);
                tv.Stop();
                lbl_Calib2GTime.Text = $"{tv.ElapsedMilliseconds} ms";
                lbl_Calib2G.Text = r ? "Passed" : "Failed";
                if (!r) {
                    UpdateRtbAll("\r\n[FAIL] Calib và verify tín hiệu WIFI!\r\n\r\n");
                    VNPT_TextDisplay.SetText(lblResult, "FAIL");
                    goto END;
                }
                UpdateRtbAll("\r\n[Finished] Calib và verify tín hiệu WIFI!\r\n\r\n");
            }

            goto END;

        END:
            Thread.Sleep(500);
            serialPort1.Close();
            btn_disconnect.Visible = false;
            btnStart.Enabled = true;

            VNPT_TextDisplay.SetText(lb_statusconnect, "Disconnected");
            showResult(r ? "PASS" : "FAIL");
            totalStopwatch.Stop();
            UpdateRtbAll("\r\n------------------------------------------------\r\n");
            UpdateRtbAll(string.Format("Tổng thời gian: {0} sec\r\n", totalStopwatch.ElapsedMilliseconds / 1000));
            if (r) {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "", "PASS"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "", "PASS"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "", "PASS"); //log uart
            }
            else {
                WriteLog.WriteToTxtfileRunAll(richTextBoxStatus, myGlobal.qsprDirectory, g_SN, "", "FAIL"); //log calib
                WriteLog.WriteToTxtfileRunAll(rtbRunTestAll, myGlobal.systemDirectory, g_SN, "", "FAIL"); //log detail
                WriteLog.WriteToTxtfileRunAll(rtbCOMPort, myGlobal.uartDirectory, g_SN, "", "FAIL"); //log uart
            }

            //cảnh báo mạch đã calib tần số // 13.02.2020
            string qspr_Str = richTextBoxStatus.Text;
            if (qspr_Str.Contains("Cal values already present in OTP !")) {
                MessageBox.Show("Mạch đã có dữ liệu calib tần số trong OTP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return r;
        }


        #region sub-Function

        private void _initTestingView() {
            btnStart.Enabled = false;
            //btn_disconnect.Visible = true;
            VNPT_TextDisplay.SetText(lb_statusconnect, "Connected");
            VNPT_TextDisplay.ClearText(richTextBoxStatus);
            lblResult.Text = "";
            lbl_MacEthernet.Text = "-";
            VNPT_TextDisplay.ClearText(rtbRunTestAll);
            VNPT_TextDisplay.ClearText(rtbCOMPort);
            g_readFromCOM1 = "";
            g_HienthidataCOM = "";
        }

        private bool _openSerialPort() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_OpenCom.Text = "Waiting...";

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
            lbl_OpenComTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_OpenCom.Text = ret ? "Passed" : "Failed";
            return ret;
        }

        private bool _waitBootingComplete() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_WaitBoot.Text = "Waiting...";

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
            lbl_WaitBootTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_WaitBoot.Text = r ? "Passed" : "Failed";
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

        private bool _loginIMAP() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_Login.Text = "Waiting...";

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
            lbl_LoginTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_Login.Text = r ? "Passed" : "Failed";
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

        private bool _setEthernetIP_NoPing() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_ChangeIP.Text = "Waiting...";

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
            lbl_ChangeIPTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_ChangeIP.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _waitPingToImap() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_PingNewIP.Text = "Waiting...";

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
            lbl_PingNewIPTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_PingNewIP.Text = r ? "Passed" : "Failed";
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

        private bool _switchCalibMode5G() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_SwitchMode5G.Text = "Waiting...";

            bool r = false;
            try {
                UpdateRtbAll(">>> Thực hiện thiết lập IMAP về chế độ calib 5G\n");

                UpdateRtbAll("Send Command: cd /lib/firmware/QCA9888/hw.2 \n");
                bool ret = _sendwait_command("", 3, "root@VNPT:/#", "root@VNPT:~#");
                if (!ret) goto END;
                ret = _sendwait_command("cd /lib/firmware/QCA9888/hw.2", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2");
                if (!ret) goto END;
                UpdateRtbAll("Pass\n");

                UpdateRtbAll("[OK] Di chuyển tới thư mục chứa file BoardData thành công \n");
                UpdateRtbAll("Gửi Command thực thiện copy file BoardData 5G\n");
                ret = _sendwait_command("cp boardData_2_0_QCA9888_5G_Y9582.bin boarddata_0.bin", 3, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                ret = _sendwait_command("sync", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                ret = _sendwait_command("wifi down", 10, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;

                UpdateRtbAll("Thực hiện gửi lệnh turn on QCMBR trong IMAP \n");
                UpdateRtbAll("Gửi command: /etc/init.d/qcmbr restart\n");
                ret = _sendwait_command("/etc/init.d/qcmbr restart", 30, "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                r = ret;
            }
            catch { r = false; }

        END:
            st.Stop();
            lbl_SwitchMode5GTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_SwitchMode5G.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _switchCalibMode2G() {
            Stopwatch st = new Stopwatch();
            st.Start();
            lbl_SwitchMode2G.Text = "Waiting...";

            bool r = false;
            try {
                UpdateRtbAll(">>> Thực hiện thiết lập IMAP về chế độ calib 2G\n");
                UpdateRtbAll("Send Command: /etc/init.d/art start \n");
                bool ret = _sendwait_command("", 3, "root@VNPT:/#", "root@VNPT:~#", "root@VNPT:/lib/firmware/QCA9888/hw.2#");
                if (!ret) goto END;
                ret = _sendwait_command("cd", 3, "root@VNPT:/#", "root@VNPT:~#");
                if (!ret) goto END;
                ret = _sendwait_command("/etc/init.d/art start", 30, "sa_init_module");
                r = ret;
            }
            catch { r = false; }

        END:
            st.Stop();
            lbl_SwitchMode2GTime.Text = $"{st.ElapsedMilliseconds} ms";
            lbl_SwitchMode2G.Text = r ? "Passed" : "Failed";
            return r;
        }

        private bool _calibAndVerify(bool is2G) {
            try {
                TreeTestStatus = 0;

                string xttFileName = is2G ? PathXTTFile2G : PathXTTFile5G;
                _testTree = _scheduler.OpenXTT(xttFileName);
                _testTree.RunTreeDone += new QSPRTestTree.RunTreeDoneEventHandler(_testTree_RunTreeDone);

                if (_testTree != null) {
                    //ClearStatusWindow();
                    if (g_macEth0.Length == 0) g_macEth0 = getMacFromDut();
                    lbl_MacEthernet.Text = g_macEth0;
                    g_SN = "IMAP_" + g_macEth0;
                    if (g_macEth0 == "") return false;
                    try {
                        AddMac_Value_tam = g_macEth0.Substring(6, 6);
                        g_mac2G_onboard = g_macEth0.Substring(0, 6) + GetMAC2G(AddMac_Value_tam);
                        g_mac5G_onboard = g_macEth0.Substring(0, 6) + GetMAC5G(AddMac_Value_tam);
                    }
                    catch { return false; }

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
