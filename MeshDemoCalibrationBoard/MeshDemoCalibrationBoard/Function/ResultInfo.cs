using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public class ResultInfo : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }


        public ResultInfo() {
            Freq = "";
            Board1 = "";
            Board2 = "";
            at1Board1 = "";
            at2Board1 = "";
            at1Board2 = "";
            at2Board2 = "";

            at1Dif = "";
            at2Dif = "";
            at1Att = "";
            at2Att = "";
        }

        string _freq;
        public string Freq {
            get { return _freq; }
            set {
                _freq = value;
                OnPropertyChanged(nameof(Freq));
            }
        }
        string _calibration_board1;
        public string Board1 {
            get { return _calibration_board1; }
            set {
                _calibration_board1 = value;
                OnPropertyChanged(nameof(Board1));
            }
        }
        string _calibration_board2;
        public string Board2 {
            get { return _calibration_board2; }
            set {
                _calibration_board2 = value;
                OnPropertyChanged(nameof(Board2));
            }
        }
        string _antenna1_board1;
        public string at1Board1 {
            get { return _antenna1_board1; }
            set {
                _antenna1_board1 = value;
                OnPropertyChanged(nameof(at1Board1));
            }
        }
        string _antenna2_board1;
        public string at2Board1 {
            get { return _antenna2_board1; }
            set {
                _antenna2_board1 = value;
                OnPropertyChanged(nameof(at2Board1));
            }
        }
        string _antenna1_board2;
        public string at1Board2 {
            get { return _antenna1_board2; }
            set {
                _antenna1_board2 = value;
                OnPropertyChanged(nameof(at1Board2));
            }
        }
        string _antenna2_board2;
        public string at2Board2 {
            get { return _antenna2_board2; }
            set {
                _antenna2_board2 = value;
                OnPropertyChanged(nameof(at2Board2));
            }
        }
        string _antenna1_dif;
        public string at1Dif {
            get { return _antenna1_dif; }
            set {
                _antenna1_dif = value;
                OnPropertyChanged(nameof(at1Dif));
            }
        }
        string _antenna2_dif;
        public string at2Dif {
            get { return _antenna2_dif; }
            set {
                _antenna2_dif = value;
                OnPropertyChanged(nameof(at2Dif));
            }
        }
        string _antenna1_att;
        public string at1Att {
            get { return _antenna1_att; }
            set {
                _antenna1_att = value;
                OnPropertyChanged(nameof(at1Att));
            }
        }
        string _antenna2_att;
        public string at2Att {
            get { return _antenna2_att; }
            set {
                _antenna2_att = value;
                OnPropertyChanged(nameof(at2Att));
            }
        }
    }
}
