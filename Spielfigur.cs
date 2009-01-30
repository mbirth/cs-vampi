using System;
using System.Collections;

namespace vampi
{
    public abstract class Spielfigur {
        public const int F_TYPE = 0;
        public const int TYPE_HUMAN = 1;
        public const int TYPE_VAMPIRE = 2;
        
        public const int F_AGE = 1;
        public const int F_MAXAGE = 2;
        public int[] props = new int[20];

        protected Spielfeld sfeld;

        public int Age {
            get { return this.props[F_AGE]; }
        }

        public int Typ {
            get { return this.props[F_TYPE]; }
        }

        public Spielfigur(Spielfeld sfeld) {
            sfeld.Sfigur = this;
            this.sfeld = sfeld;
        }

        public virtual void runStep() {
            this.props[F_AGE]++;
            if (this.props[F_AGE] >= this.props[F_MAXAGE]) {
                this.die();
            }
        }

        public virtual void die() {
            this.sfeld.Sfigur = null;
        }
    }
}