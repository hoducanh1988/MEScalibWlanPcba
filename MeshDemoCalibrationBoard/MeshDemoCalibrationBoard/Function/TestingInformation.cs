using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public class TestingInformation : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public TestingInformation() {
            Init();
        }

        public void Init() {
            SystemLog = "";
            logInstrument = "";
            Board1 = new ItemInfo() { Name = "Calibration Board 1", portTransmitter = "3", portReceiver = "1", Status = "--" };
            Board2 = new ItemInfo() { Name = "Calibration Board 2", portTransmitter = "3", portReceiver = "1", Status = "--" };
            Antenna11 = new ItemInfo() { Name = "Antenna 1 + Board 1", portTransmitter = "3", portReceiver = "1", Status = "--" };
            Antenna21 = new ItemInfo() { Name = "Antenna 2 + Board 1", portTransmitter = "3", portReceiver = "1", Status = "--" };
            Antenna12 = new ItemInfo() { Name = "Antenna 1 + Board 2", portTransmitter = "3", portReceiver = "1", Status = "--" };
            Antenna22 = new ItemInfo() { Name = "Antenna 2 + Board 2", portTransmitter = "3", portReceiver = "1", Status = "--" };
        }


        string _system_log;
        public string SystemLog {
            get { return _system_log; }
            set {
                _system_log = value;
                OnPropertyChanged(nameof(SystemLog));
            }
        }
        string _log_instrument;
        public string logInstrument {
            get { return _log_instrument; }
            set {
                _log_instrument = value;
                OnPropertyChanged(nameof(logInstrument));
            }
        }
        ItemInfo _board1;
        public ItemInfo Board1 {
            get { return _board1; }
            set {
                _board1 = value;
                OnPropertyChanged(nameof(Board1));
            }
        }
        ItemInfo _board2;
        public ItemInfo Board2 {
            get { return _board2; }
            set {
                _board2 = value;
                OnPropertyChanged(nameof(Board2));
            }
        }
        ItemInfo _antenna11;
        public ItemInfo Antenna11 {
            get { return _antenna11; }
            set {
                _antenna11 = value;
                OnPropertyChanged(nameof(Antenna11));
            }
        }
        ItemInfo _antenna21;
        public ItemInfo Antenna21 {
            get { return _antenna21; }
            set {
                _antenna21 = value;
                OnPropertyChanged(nameof(Antenna21));
            }
        }
        ItemInfo _antenna12;
        public ItemInfo Antenna12 {
            get { return _antenna12; }
            set {
                _antenna12 = value;
                OnPropertyChanged(nameof(Antenna12));
            }
        }
        ItemInfo _antenna22;
        public ItemInfo Antenna22 {
            get { return _antenna22; }
            set {
                _antenna22 = value;
                OnPropertyChanged(nameof(Antenna22));
            }
        }


    }
}
