// Output_CLI.cs created with MonoDevelop at 5:34 PM 1/29/2009
// @author mbirth

using System;

namespace vampi {
    public class Output_CLI : Output {
        
        public Output_CLI() {
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.CursorVisible = false;
            Console.SetWindowSize(1, 1);
            /*
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
             */
            if (System.Environment.OSVersion.Platform.ToString() != "Unix") {
                Console.SetBufferSize(Settings.size + 2, Settings.size + 8);
                Console.SetWindowSize(Settings.size + 2, Settings.size + 8);
            }
            Console.Title = "Vampi CLI --- ©2008 Markus Birth, FA76";
            Console.Clear();
        }
        
        ~Output_CLI() {
            Console.Read();
        }
        
        public override void doOutput() {
            this.drawGameMap();
            this.drawStatistics();
        }
        
        public void testSpielfeld() {
            Position pos;
            Spielfeld nachbarfeld;

            //Schleife ueber alle Spielfelder
            for (int y = 1; y <= Settings.size; y++) {
                for (int x = 1; x <= Settings.size; x++) {
                    pos.x = x;
                    pos.y = y;

                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(' ');

                    //Schleife ueber alle Nachbarfelder eines Spielfeldes
                    for (int lage = 1; lage <= 8; lage++) {
                        nachbarfeld = Program.sflaeche.getSpielfeld(pos).getNachbarfeld(lage);

                        if (nachbarfeld != null) { //Nachbarfeld existiert
                            Console.SetCursorPosition(nachbarfeld.Pos.x, nachbarfeld.Pos.y);

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(' ');

                            Console.SetCursorPosition(nachbarfeld.Pos.x, nachbarfeld.Pos.y);

                            System.Threading.Thread.Sleep(10);

                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(' ');
                        }
                    }
                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(' ');
                }//for					 
            }//for
        }
        
        public void drawGameMap() {
            Position pos;
            Console.SetCursorPosition(0, 0);
            for (pos.y = 1; pos.y <= Settings.size; pos.y++) {
                for (pos.x = 1; pos.x <= Settings.size; pos.x++) {
                    Spielfigur sf = Program.sflaeche.getSpielfeld(pos).Sfigur;

                    if (sf == null) //Spielfeld leer
                        Console.BackgroundColor = Settings.colorEmpty;
                    else { //Spielfeld besetzt
                        switch (sf.Typ) {
                            case Typliste.EINWOHNER:
                                if (((Einwohner)sf).Infected)
                                    Console.BackgroundColor = Settings.colorHumanInfected;
                                else
                                    Console.BackgroundColor = Settings.colorHuman;
                                break;
                            case Typliste.VAMPIR:
                                Console.BackgroundColor = Settings.colorVampire;
                                break;
                        }//switch
                    }//else
                    Console.Write(' ');
                }//for
                Console.WriteLine();
            }//for
            Console.ResetColor();
        }
        
        public void drawStatistics() {
            int Ecount = Einwohner.Count; // sflaeche.countTypeOccurrences(Typliste.EINWOHNER);
            int Vcount = Vampir.Count; // sflaeche.countTypeOccurrences(Typliste.VAMPIR);
            Console.WriteLine("\n" + String.Format("Steps Done: {0:D5}  ", Program.AnzSimDone));
            Console.WriteLine(String.Format("Einwohner: {0:D} / Vampire: {1:D}  ", Ecount, Vcount));
            Console.WriteLine(String.Format("Verhältnis Vampire/Einwohner = 1/{0:N5}  ", (double)Ecount / Vcount));
            Console.WriteLine(String.Format("Bedeckung: {0:N5} %", (double)(Ecount + Vcount) / Settings.size*Settings.size * 100) + "  ");
        }
        
    }
}
