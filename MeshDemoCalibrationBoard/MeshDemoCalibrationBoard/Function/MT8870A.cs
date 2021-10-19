using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NationalInstruments.VisaNS;

namespace MeshDemoCalibrationBoard.Function {
    public class MT8870A : IInstrument {

        private MessageBasedSession mbSession;

        public MT8870A(string gpib_port, string power_transmit, string receive_port, string transmission_port) : base(gpib_port, power_transmit, receive_port, transmission_port) {
            bool r = false;
            try {
                mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(gpib_port);

                r = this.Write("*CLS\n"); if (!r) goto END;
                r = this.Write("*RST\n"); if (!r) goto END;
                r = this.Write(":SYST:LANG SCPI\n"); if (!r) goto END;
                r = this.Write(":INST:SEL SRW\n"); if (!r) goto END;
                r = this.Write(":CONF:SRW:SEGM:APP CW\n"); if (!r) goto END;
                r = this.Write(":SOUR:GPRF:GEN:MODE NORMAL\n"); if (!r) goto END;
                r = this.Write(":SOUR:GPRF:GEN:RFS:LEV " + power_transmit + "\n"); if (!r) goto END;
                r = this.Write(":ROUT:PORT:CONN:DIR " + receive_port + "," + transmission_port + "\n"); if (!r) goto END;
                r = this.Write(":SOUR:GPRF:GEN:BBM CW\n"); if (!r) goto END;
                r = this.Write(":SOUR:GPRF:GEN:ARB:NOIS:STAT OFF\n"); if (!r) goto END;
                isConnected = true;
            }
            catch (Exception ex) {
                isConnected = false;
                myGlobal.myTesting.logInstrument += ex.ToString() + "\n";
            }

        END:
            isConnected = r;
        }


        //----------------------Cau hinh Phat--------------------------------------------//
        public override bool config_HT20_TxTest_Transmitter(string frequency) {
            if (!isConnected) return false;
            bool r = false;
            r = this.Write(":SOUR:GPRF:GEN:RFS:FREQ " + frequency + "000000HZ\n"); if (!r) return false;
            r = this.Write(":SOUR:GPRF:GEN:STAT ON\n");
            return r;
        }

        public override string config_HT20_RxTest_Receiver(string frequency) {
            string data = "";
            try {
                this.Write(":CONF:SRW:FREQ " + frequency + "000000HZ\n");      // Thiết lập tần số thu
                this.Write(":CONF:SRW:ALEV:TIME 0.005\n");
                this.Write(":INIT:SRW:ALEV\n"); //Thiết lập Power lever là auto level
                Thread.Sleep(100);
                data = this.Query(":FETC:SRW:SUMM:CW:POW? 1\n");
                this.Write(":SOUR:GPRF:GEN:STAT OFF\n"); //Lệnh OFF công suất
                return data;
            }
            catch {
                return data;
            }
        }

        public override bool Write(string cmd) {
            int count = 0;
        RE:
            count++;
            mbSession.Write(cmd);
            myGlobal.myTesting.logInstrument += cmd;

            mbSession.Write(":SYSTem:ERRor?\n");
            string data = this.Read();
            bool r = data.ToLower().Contains("no error");
            if (!r) {
                if (count < 3) goto RE;
            }
            return r;
        }

        public override string Query (string cmd) {
            int count = 0;
            bool r = false;
        RE:
            count++;
            string data = mbSession.Query(cmd);
            myGlobal.myTesting.logInstrument += cmd;
            r = string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data);
            if (r) {
                if (count < 3) goto RE;
            }

            myGlobal.myTesting.logInstrument += data + "\n";
            return data;
        }

        public override string Read() {
            string data = mbSession.ReadString();
            myGlobal.myTesting.logInstrument += data;
            return data;
        }

        public override void Dispose() {
            mbSession.Dispose();
        }


    }
}
