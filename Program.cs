using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace vampi {
    class Program {
        private static int anz_sim = 100000;
        private static int anz_sim_done = 0;
        public static int AnzSimDone {
            get { return Program.anz_sim_done; }
        }
        public static Random random = new Random();
        public static Spielflaeche sflaeche = new Spielflaeche(Settings.size, Settings.coveragePercent, Settings.vampireRatio);
        public static double lastCalcTime = 0;
        public static double lastStatsTime = 0;

        static void Main(string[] args) {
            Output output = new Output_GUI();
            Stopwatch sw = new Stopwatch();

            for (anz_sim_done=0; anz_sim_done < anz_sim; anz_sim_done++) {
                if (anz_sim_done % Settings.drawEveryNthStep == 0) {
                    sw.Reset();
                    sw.Start();
                    output.doOutput();
                    sw.Stop();
                    Program.lastStatsTime = sw.Elapsed.TotalMilliseconds;
                }
                if (output.requestAbort) break;
                sw.Reset();
                sw.Start();
                sflaeche.simulateStep();
                sw.Stop();
                Program.lastCalcTime = sw.Elapsed.TotalMilliseconds;
            }
        }//Main
    }//class
}//namespace
