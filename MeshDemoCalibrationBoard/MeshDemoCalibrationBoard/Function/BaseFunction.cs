using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshDemoCalibrationBoard.Function {
    public class BaseFunction {

        public static string getFrequencyFromPathLoss(string fi) {
            if (File.Exists(fi) == false) return null;

            string[] data = File.ReadAllLines(fi);

            int start_num = 0, end_num = 0, i = 0;
            bool r = false;
            foreach (var s in data) {
                if (s.ToLower().Contains("<datalist>")) start_num = i;
                if (s.ToLower().Contains("</datalist>")) { end_num = i; r = true; }
                i++;
                if (r) break;
            }
            if (start_num == 0 && end_num == 0) return null;

            //get frequency
            string freq = "";
            for (int k = start_num; k < end_num; k++) {
                string s = data[k].ToLower();
                string frq = "";
                if (s.Contains("<frequency>")) {
                    frq = s.Replace("<frequency>", "").Replace("</frequency>", "").Trim();
                    freq += frq + ",";
                }
            }
            freq = freq.Substring(0, freq.Length - 1);
            Console.WriteLine(freq);

            return freq;
        }

    }
}
