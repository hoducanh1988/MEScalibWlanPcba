using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MeshDemoCalibrationBoard.Function;
using Microsoft.Win32;

namespace MeshDemoCalibrationBoard {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {


        class pathLossFrequencyInfo {

            public pathLossFrequencyInfo() {
                bhName = "";
                Frequency = "";
                lossValue = 0;
                lineNumber = 0;
            }

            public string bhName { get; set; }
            public string Frequency { get; set; }
            public double lossValue { get; set; }
            public int lineNumber { get; set; }
        }


        TestingInformation myTesting = myGlobal.myTesting;
        SettingInformation mySetting = myGlobal.mySetting;
        IInstrument instrument = null;
        volatile bool isScrollViewer = false;
        Dictionary<string, ItemInfo> dictTestCase = new Dictionary<string, ItemInfo>();

        public MainWindow() {
            InitializeComponent();

            this.cbb_instrtype.ItemsSource = new List<string>() { "MT8870A", "E6640A" };
            bindingData();
            addDict();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (e, s) => {
                if (isScrollViewer) {
                    this.scr_system.ScrollToEnd();
                    this.scr_instrument.ScrollToEnd();
                }
            };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string b_tag = (string)b.Tag;

            switch (b_tag) {
                case "browser_file_pathloss": {
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.Filter = "*.xml|*.xml";
                        dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                        if (dlg.ShowDialog() == true) {
                            mySetting.filePathloss = dlg.FileName;

                            myGlobal.listFrequency = new List<string>();
                            myGlobal.listResult = new List<ResultInfo>();

                            if (File.Exists(mySetting.filePathloss)) {
                                string data = BaseFunction.getFrequencyFromPathLoss(mySetting.filePathloss);
                                if (data != null) {
                                    string[] buffer = data.Split(',');
                                    foreach (var s in buffer) {
                                        if (string.IsNullOrEmpty(s) == false && string.IsNullOrWhiteSpace(s) == false) {
                                            myGlobal.listFrequency.Add(s);
                                            myGlobal.listResult.Add(new ResultInfo() { Freq = s });
                                        }
                                    }
                                }
                            }
                            bindingData();
                        }
                        break;
                    }
                case "save_path_loss": {
                        Thread t = new Thread(new ThreadStart(() => {
                            bool r = this.calculateAttenuation();
                            if (!r) {
                                MessageBox.Show($"Dữ liệu suy hao sai định dạng.", "Update File Pathloss", MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                                return;
                            }
                            r = checkAttenuationValue_2G();
                            if (!r) {
                                MessageBox.Show($"Giá trị suy hao giữa các kênh 2G bị lệch quá tiêu chuẩn {myGlobal.mySetting.tor2G} dBm.", "Update File Pathloss", MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                                return;
                            }
                            r = checkAttenuationValue_5G();
                            if (!r) {
                                MessageBox.Show($"Giá trị suy hao giữa các kênh 5G bị lệch quá tiêu chuẩn {myGlobal.mySetting.tor5G} dBm.", "Update File Pathloss", MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                                return;
                            }
                            r = this.updateFilePathLoss();
                            if (r) {
                                string f = AppDomain.CurrentDomain.BaseDirectory + "attenuation.dll";
                                if (File.Exists(f)) {
                                    string data = File.ReadAllText(f).Replace("\n", "").Replace("\r", "").Trim();
                                    string[] buffer = data.Split(',');
                                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + buffer[0], buffer[1]);
                                }
                            }
                            MessageBox.Show(r ? "Success." : "Failure.", "Update File Pathloss", MessageBoxButton.OK, r ? MessageBoxImage.Information : MessageBoxImage.Error);
                        }));
                        t.IsBackground = true;
                        t.Start();
                        break;
                    }
                case "reset": {
                        bool r = MessageBox.Show("Bạn muốn đo lại suy hao từ đầu phải không?\nYes = đồng ý, No = thoát.", "Đo lại suy hao", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                        if (r) {
                            myTesting.Init();
                            cleanDataGrid();
                            bindingData();
                        }
                        break;
                    }
                default: {
                        ItemInfo tf = null;
                        dictTestCase.TryGetValue(b_tag, out tf);
                        if (tf != null) {
                            Thread t = new Thread(new ThreadStart(() => {
                                bool r = measureAttenuation(tf);
                            }));
                            t.IsBackground = true;
                            t.Start();
                        }
                        break;
                    }
            }
        }

        private void Dg_Viewer_LostFocus(object sender, RoutedEventArgs e) {
            DataGrid dg = sender as DataGrid;
            dg.UnselectAll();
            dg.UnselectAllCells();
        }

        #region Sub function

        void bindingData() {
            this.tab_runall.DataContext = myTesting;
            this.sp_setting.DataContext = mySetting;
            this.wp_board1.DataContext = myTesting.Board1;
            this.wp_board2.DataContext = myTesting.Board2;
            this.wp_antenna11.DataContext = myTesting.Antenna11;
            this.wp_antenna21.DataContext = myTesting.Antenna21;
            this.wp_antenna12.DataContext = myTesting.Antenna12;
            this.wp_antenna22.DataContext = myTesting.Antenna22;
            this.dg_Viewer.ItemsSource = myGlobal.listResult;
            this.tab_setting.DataContext = mySetting;
        }

        void cleanDataGrid() {
            foreach (var item in myGlobal.listResult) {
                item.Board1 = "";
                item.Board2 = "";
                item.at1Board1 = "";
                item.at2Board1 = "";
                item.at1Board2 = "";
                item.at2Board2 = "";
                item.at1Dif = "";
                item.at2Dif = "";
                item.at1Att = "";
                item.at2Att = "";
            }
        }

        void addDict() {

            dictTestCase = new Dictionary<string, ItemInfo>();
            dictTestCase.Add("board1", myTesting.Board1);
            dictTestCase.Add("board2", myTesting.Board2);
            dictTestCase.Add("antenna11", myTesting.Antenna11);
            dictTestCase.Add("antenna21", myTesting.Antenna21);
            dictTestCase.Add("antenna12", myTesting.Antenna12);
            dictTestCase.Add("antenna22", myTesting.Antenna22);
        }

        bool measureAttenuation(ItemInfo tf) {
            bool ret = false;
            isScrollViewer = true;

            try {
                myTesting.SystemLog += $"\n+++ {tf.Name} ++++++++++++++++++++++++++\n";
                tf.Status = "Waiting...";
                string port_tran = mySetting.instrumentType.Equals("MT8870A") ? "PORT" + tf.portTransmitter : "RFIO" + tf.portTransmitter;
                string port_rev = mySetting.instrumentType.Equals("MT8870A") ? "PORT" + tf.portReceiver : "RFIO" + tf.portReceiver;

                myTesting.SystemLog += $"Connecting to instrument {mySetting.instrumentType} - address {mySetting.GPIB} = ";
                if (instrument == null) {
                    if (mySetting.instrumentType.Equals("MT8870A")) {
                        instrument = new MT8870A(mySetting.GPIB, mySetting.powerTransmit, port_rev, port_tran);
                    }
                }
                myTesting.SystemLog += $"{instrument.isConnected}\n";
                if (instrument.isConnected == false) goto END;

                foreach (var item in myGlobal.listFrequency) {
                    int count = 0;
                    bool r = false;
                    List<double> listR = new List<double>();

                RE:
                    count++;
                    myTesting.SystemLog += $"Frequency {item} MHz - Transmitter {port_tran} - Receiver {port_rev} - Power {mySetting.powerTransmit} dBm\n";

                    int c = 0;
                R:
                    c++;
                    instrument.config_HT20_TxTest_Transmitter(item);
                    string data = instrument.config_HT20_RxTest_Receiver(item);

                    //kiem tra dinh dang du lieu doc ve
                    r = data.Contains(",");
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }
                    string[] buffer = data.Split(',');
                    r = buffer.Length >= 3;
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }
                    string pwr_str = buffer[2];
                    double pwr_dbl = 0;
                    r = double.TryParse(pwr_str, out pwr_dbl);
                    if (r == false) {
                        if (count < 3) goto RE;
                        else goto END;
                    }


                    myTesting.SystemLog += $"... Power received[{c}]: {pwr_dbl} dBm\n";
                    listR.Add(pwr_dbl);
                    if (c < int.Parse(myGlobal.mySetting.NOM)) goto R;

                    double pw = Math.Round(listR.Average(), 2);
                    myTesting.SystemLog += $"... Power Average: {pw} dBm\n";

                    double att = Math.Round(double.Parse(mySetting.powerTransmit) - pw, 2);
                    myTesting.SystemLog += $"... Attenuation: {att} dBm\n";

                    var itr = myGlobal.listResult.Where(x => x.Freq == item).FirstOrDefault();
                    switch (tf.Name) {
                        case "Calibration Board 1": { itr.Board1 = $"{att}"; break; }
                        case "Calibration Board 2": { itr.Board2 = $"{att}"; break; }
                        case "Antenna 1 + Board 1": { itr.at1Board1 = $"{att}"; break; }
                        case "Antenna 2 + Board 1": { itr.at2Board1 = $"{att}"; break; }
                        case "Antenna 1 + Board 2": { itr.at1Board2 = $"{att}"; break; }
                        case "Antenna 2 + Board 2": { itr.at2Board2 = $"{att}"; break; }
                    }
                }

                ret = true;
            }
            catch { ret = false; }


        END:
            isScrollViewer = false;
            tf.Status = ret ? "Passed" : "Failed";
            myTesting.SystemLog += $"Kết quả : {tf.Status}\n";
            myTesting.SystemLog += $"...Disconnect instrument.\n";
            if (instrument != null && instrument.isConnected == true) {
                instrument.Dispose();
                instrument = null;
            }
            return ret;
        }

        bool calculateAttenuation() {
            try {
                double cnatt = 0;
                bool ___ = double.TryParse(mySetting.conectorAtt, out cnatt);

                foreach (var item in myGlobal.listResult) {
                    var at11 = Convert.ToDecimal(item.at1Board1) - Convert.ToDecimal(item.Board1);
                    var at12 = Convert.ToDecimal(item.at1Board2) - Convert.ToDecimal(item.Board2);
                    item.at1Dif = Math.Round(Math.Abs(at12 - at11), 2).ToString();
                    item.at1Att = Math.Round(((at11 + at12) - Convert.ToDecimal(cnatt)) / 2, 3).ToString();

                    var at21 = Convert.ToDecimal(item.at2Board1) - Convert.ToDecimal(item.Board1);
                    var at22 = Convert.ToDecimal(item.at2Board2) - Convert.ToDecimal(item.Board2);
                    item.at2Dif = Math.Round(Math.Abs(at22 - at21), 2).ToString();
                    item.at2Att = Math.Round(((at21 + at22) - Convert.ToDecimal(cnatt)) / 2, 3).ToString();

                }
                return true;
            }
            catch { return false; }
        }

        bool updateFilePathLoss() {
            try {
                string PathXMLSuyHao = mySetting.filePathloss;
                var pathlossInfos = getPathLossInfoFromFile(PathXMLSuyHao);
                if (pathlossInfos == null || pathlossInfos.Count == 0) return false;

                foreach (var item in myGlobal.listResult) {
                    updateLossInfo("bh0_lp", item.Freq, double.Parse(item.at1Att), pathlossInfos);
                    updateLossInfo("bh1_lp", item.Freq, double.Parse(item.at2Att), pathlossInfos);
                }

                savePathLossFile(PathXMLSuyHao, pathlossInfos);
                return true;
            }
            catch { return false; }
        }

        List<pathLossFrequencyInfo> getPathLossInfoFromFile(string file_path_loss) {
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

        bool savePathLossFile(string file_path_loss, List<pathLossFrequencyInfo> pathlossInfo) {
            if (System.IO.File.Exists(file_path_loss) == false) return false;
            string[] buffer = File.ReadAllLines(file_path_loss);

            foreach (var item in pathlossInfo) {
                //        <Value>8.0299</Value>
                string s = buffer[item.lineNumber + 1].ToLower();
                string[] bff = s.Split(new string[] { "value" }, StringSplitOptions.None);
                buffer[item.lineNumber + 1] = string.Format("{0}Value>{1}</Value{2}", bff[0], item.lossValue, bff[2]);
            }

            File.WriteAllLines(file_path_loss, buffer);
            System.Diagnostics.Process.Start(file_path_loss);

            return true;
        }

        bool updateLossInfo(string bh_name, string center_frequency, double loss_value, List<pathLossFrequencyInfo> pathlossInfo) {
            //update center frequency
            double center_loss_value = 0;
            foreach (var item in pathlossInfo) {
                if (item.Frequency.Equals(center_frequency) && item.bhName.Equals(bh_name)) {
                    item.lossValue = loss_value;
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

        bool checkAttenuationValue_2G() {
            List<double> list_att1 = null, list_att2 = null;
            list_att1 = new List<double>();
            list_att2 = new List<double>();

            //get list attenuation value from list result
            foreach (var item in myGlobal.listResult) {
                int freq = int.Parse(item.Freq);
                if (freq < 3000) {
                    list_att1.Add(double.Parse(item.at1Att));
                    list_att2.Add(double.Parse(item.at2Att));
                }
            }

            //check attenuation value antenna 1
            double min_at1 = list_att1.Min();
            double max_at1 = list_att1.Max();

            bool r = Math.Abs(max_at1 - min_at1) <= double.Parse(myGlobal.mySetting.tor2G);
            if (!r) return false;

            //check attenuation value antenna 2
            double min_at2 = list_att2.Min();
            double max_at2 = list_att2.Max();

            r = Math.Abs(max_at2 - min_at2) <= double.Parse(myGlobal.mySetting.tor2G);
            if (!r) return false;

            return true;
        }

        bool checkAttenuationValue_5G() {
            bool r = false;

            for (int i = 0; i < myGlobal.listResult.Count - 1; i++) {
                int freq1 = int.Parse(myGlobal.listResult[i].Freq);
                int freq2 = int.Parse(myGlobal.listResult[i + 1].Freq);

                if (Math.Abs(freq2 - freq1) <= 20) {
                    bool r1 = Math.Abs(double.Parse(myGlobal.listResult[i].at1Att) - double.Parse(myGlobal.listResult[i + 1].at1Att)) <= double.Parse(myGlobal.mySetting.tor5G);
                    bool r2 = Math.Abs(double.Parse(myGlobal.listResult[i].at2Att) - double.Parse(myGlobal.listResult[i + 1].at2Att)) <= double.Parse(myGlobal.mySetting.tor5G);
                    r = r1 && r2;
                }
                else r = true;

                if (!r) break;
            }

            return r;
        }

        #endregion


    }
}
