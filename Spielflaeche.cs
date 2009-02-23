using System;

namespace vampi {
    public class Spielflaeche {
        private Spielfeld[,] sfeld_2dim;
        private int groesse;
        public int Groesse {
            get { return sfeld_2dim.GetLength(0) * sfeld_2dim.GetLength(1); }
        }

        public Spielflaeche(int groesse) {
            this.groesse = groesse;
            sfeld_2dim = new Spielfeld[groesse, groesse];
            for (int i = 0; i < groesse; i++) {
                for (int j = 0; j < groesse; j++) {
                    sfeld_2dim[i, j] = new Spielfeld(new Position(i+1, j+1), this);
                }
            }
        }

        public Spielflaeche(int groesse, int initBedeckung, int initVerhaeltnis) : this(groesse) {
            initSpielfeld(initBedeckung, initVerhaeltnis);
        }

        private void initSpielfeld(int initBedeckung, int initVerhaeltnis) {
            // INFO: Andere Idee: Spielfeld linear befüllen, danach alle Felder durchgehen
            //       und das aktuelle Feld jeweils mit einem zufälligen Feld tauschen
            int feldX, feldY;
            int bedeckung = this.groesse * this.groesse * initBedeckung / 100;  // groesse^2 * Prozent
            int vampire   = (int)Math.Round((double)bedeckung / (initVerhaeltnis+1));   // Felder / (Verhaeltnis+1)

            for (int i = bedeckung; i > 0; i--) {
                // find an empty, random field
                do {
                    feldX = Program.random.Next(0, this.groesse);
                    feldY = Program.random.Next(0, this.groesse);
                } while (this.sfeld_2dim[feldX, feldY].Sfigur != null);

                // first set all vampires then set inhabitants
                if (vampire > 0) {
                    new Vampir(this.sfeld_2dim[feldX, feldY]);
                    vampire--;
                } else {
                    new Einwohner(this.sfeld_2dim[feldX, feldY]);
                }
            }
        }

        public Spielfeld getSpielfeld(Position pos) {
            if (pos.x > this.groesse)
                pos.x -= this.groesse;  // should be 1
            if (pos.x < 1)
                pos.x += this.groesse;  // should be this.groesse

            if (pos.y > this.groesse || pos.y < 1) {
                // y-direction doesn't wrap
                return null;
            }

            return sfeld_2dim[pos.x-1, pos.y-1];
        }

        public int countTypeOccurrences(int typ) {
            int result = 0;
            for (int i = 0; i < this.groesse; i++) {
                for (int j = 0; j < this.groesse; j++) {
                    if (this.sfeld_2dim[i, j].Sfigur != null && this.sfeld_2dim[i, j].Sfigur.Typ == typ)
                        result++;
                }
            }
            return result;
        }

        public void simulateStep() {
            for (int i = 0; i < this.groesse; i++) {
                for (int j = 0; j < this.groesse; j++) {
                    if (this.sfeld_2dim[i, j].Sfigur != null)
                        this.sfeld_2dim[i, j].Sfigur.runStep();
                }
            }
        }
    }
}
