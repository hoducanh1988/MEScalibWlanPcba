using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public class ItemInfo : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
                Properties.Settings.Default.Save();
            }
        }

        public ItemInfo() {
            Init();
        }

        public void Init() {
            Name = "";
            portTransmitter = "";
            portReceiver = "";
            Status = "--";
        }
        string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        string _port_trans;
        public string portTransmitter {
            get { return _port_trans; }
            set {
                _port_trans = value;
                OnPropertyChanged(nameof(portTransmitter));
            }
        }
        string _port_recei;
        public string portReceiver {
            get { return _port_recei; }
            set {
                _port_recei = value;
                OnPropertyChanged(nameof(portReceiver));
            }
        }
        string _status;
        public string Status {
            get { return _status; }
            set {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }



    }
}
