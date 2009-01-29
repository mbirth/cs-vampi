using System;

namespace vampi
{
    public abstract class Spielfigur {
        protected Spielfeld sfeld;
        protected int typ;
        protected int maxAge = 80;
        protected int age = -1;
        public int Age {
            get { return this.age; }
        }

        public int Typ {
            get { return this.typ; }
        }

        public Spielfigur(Spielfeld sfeld) {
            sfeld.Sfigur = this;
            this.sfeld = sfeld;
        }

        public virtual void runStep() {
            if (this.age != -1) {
                this.age++;
                if (this.age >= this.maxAge) {
                    this.die();
                }
            }
        }

        public virtual void die() {
            this.sfeld.Sfigur = null;
        }
    }

}