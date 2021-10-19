/* Created by Panda - 17/12/2015
 * Class này chuyên hiển thị giá trị text cho các đối tượng Richtexbox, Textbox, Label
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace VNPT.DisplayText {
    public class VNPT_TextDisplay //: IText_control
    {
        //-------------------------------------------------------------------   
        //-------------------------------------------------------------------
        public static void SetText(RichTextBox rtb, string text) {
            rtb.Invoke(new MethodInvoker(delegate () {
                try {
                    if (text != "") {
                        //Them hieu ung mau sac...vv cho text hien thi
                        if (text.Contains("FAIL") || text.Contains("Fail") || text.Contains("ERROR") || text.Contains("Bai Test Wifi Chưa The Thuc Hien!") || text.Contains("[RED]")) {
                            rtb.SelectionColor = Color.Red;
                            //rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                        }
                        else if (text.Contains("OK") || text.Contains("PASS") || text.Contains("Xong") || text.Contains("xong") || text.Contains("thành công") || text.Contains("Da Ping duoc") || text.Contains("[GRE]") || text.Contains("Connected")) {
                            rtb.SelectionColor = Color.Lime;
                            //rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                        }
                        else if (text.Contains("Bai test thu:") || text.Contains("Đo chuẩn") || text.Contains("Antena") || text.Contains("MAC Address=") || text.Contains("Firmware version")) {
                            //rtb.SelectionColor = Color.Black;
                            //rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                        }
                        else if (text.Contains("Checked") || text.Contains("[BLU]")) {
                            //rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                            //rtb.SelectionColor = Color.Blue;
                        }
                        else if (text.Contains("[BOL]")) {
                            rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                            //rtb.SelectionColor = Color.Black;
                        }
                        else {
                            rtb.SelectionColor = Color.White;
                        }

                        rtb.AppendText(text);
                        rtb.ScrollToCaret();
                        rtb.Refresh();
                    }
                }
                catch { }
            }));

        }

        //-------------------------------------------------------------------  
        public static void SetText(Label lb, string text) {
            lb.Invoke(new MethodInvoker(delegate () {
                if (text != "") {
                    //Them hieu ung mau sac...vv cho text hien thi
                    if (text.Contains("FAIL") || text.Contains("Fail") || text.Contains("ERROR") || text.Contains("Bai Test Wifi Chưa The Thuc Hien!") || text.Contains("[RED]") || text.Contains("Disconnected")) {
                        lb.ForeColor = Color.Red;
                    }
                    else if (text.Contains("OK") || text.Contains("PASS") || text.Contains("pass") || text.Contains("Pass") || text.Contains("[GRE]") || text.Contains("Connected")) {
                        lb.ForeColor = Color.DarkGreen;
                    }
                    else if (text.Contains("Waiting...")) {
                        lb.ForeColor = Color.Orange;
                    }
                    else if (text.Contains("Bai test thu:") || text.Contains("Đo chuẩn") || text.Contains("Antena") || text.Contains("MAC Address=") || text.Contains("Firmware version")) {
                        lb.ForeColor = Color.Black;
                    }
                    else if (text.Contains("Checked") || text.Contains("[BLU]")) {
                        //rtb.SelectionFont = new Font(rtb.SelectionFont, FontStyle.Bold);
                        lb.ForeColor = Color.Blue;
                    }
                    else if (text.Contains("[BOL]")) {
                        lb.ForeColor = Color.Black;
                    }

                    lb.Text = text;
                    lb.Refresh();
                }

            }));
        }
        //------------------------------------------------------------------- 
        //-------------------------------------------------------------------
        //Ham ghi noi dung vao 1 Textbox 
        public static void SetText(TextBox textbox, string text) {
            textbox.Invoke(new MethodInvoker(delegate () {
                textbox.Text = text;
                textbox.Refresh();
            }));
        }
        //------------------------------------------------------------------- 
        //-------------------------------------------------------------------
        // Ham xoa noi dung 1 Rich Text Box 
        public static void ClearText(RichTextBox rtb) {
            rtb.Invoke(new MethodInvoker(delegate () {
                rtb.Clear();
                rtb.Refresh();
            }));
        }
        //------------------------------------------------------------------- 
        //-------------------------------------------------------------------
        // Ham xoa noi dung 1 Text Box 
        public static void ClearText(TextBox textbox) {
            textbox.Invoke(new MethodInvoker(delegate () {
                textbox.Clear();
            }));
        }
        //------------------------------------------------------------------- 
        //-------------------------------------------------------------------  
        public static string GetText(RichTextBox rtb) {
            string textStr = "";

            rtb.Invoke(new MethodInvoker(delegate () {
                textStr = rtb.Text;
            }));

            return textStr;
        }
        //-------------------------------------------------------------------

    }
}
