using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace vampi {
    class Program {
        private static int anz_sim = 100000;
        private static int anz_sim_done = 0;
        public static int AnzSimDone {
            get { return Program.anz_sim_done; }
        }
        public static Random random = new Random();
        public static Spielflaeche sflaeche = new Spielflaeche(Settings.size, Settings.coveragePercent, Settings.vampireRatio);

        static void Main(string[] args) {
            Output output = new Output_GUI();

            for (anz_sim_done=0; anz_sim_done < anz_sim; anz_sim_done++) {
                if (anz_sim_done % Settings.drawEveryNthStep == 0) {
                    output.doOutput();
                }
                if (output.requestAbort) break;
                sflaeche.simulateStep();
            }
        }//Main
    }//class
}//namespace
