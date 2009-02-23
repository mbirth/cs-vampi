using System;

namespace vampi
{
    public class Spielfeld {
        private Position pos;
        private Spielflaeche sflaeche;
        private Spielfigur sfigur = null;
        private int[] mx = {-1, 0, 1, 1, 1, 0, -1, -1};
        private int[] my = {-1, -1, -1, 0, 1, 1, 1, 0};

        public Spielfigur Sfigur {
            get { return this.sfigur; }
            set { this.sfigur = value; }
        }

        public Position Pos {
            get { return pos; }
        }

        public Spielfeld(Position pos, Spielflaeche sflaeche) {
            this.pos = pos;
            this.sflaeche = sflaeche;
        }

        public Spielfeld getNachbarfeld(int lage) {
            return this.sflaeche.getSpielfeld(this.getNachbarpos(lage));
        }

        private Position getNachbarpos(int lage) {
            Position nachbarpos = new Position(this.pos);
            nachbarpos.x += mx[lage - 1];
            nachbarpos.y += my[lage - 1];
            return nachbarpos;
        }

    }
}
