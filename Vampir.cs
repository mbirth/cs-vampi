using System;

namespace vampi
{
    class Vampir : Spielfigur {
        public const int F_FILLER = 10;
        
        private static int count = 0;
        public static int Count {
          get { return Vampir.count; }
        }
        
        public Vampir(Spielfeld sfeld)
            : base(sfeld) {
            this.props[F_TYPE] = TYPE_VAMPIRE;
            this.props[F_MAXAGE] = Settings.vampireMaxAge;
            this.props[F_AGE] = Program.random.Next(0, Settings.vampireMaxInitAge);
            this.props[F_FILLER] = Settings.vampireInitialFill;
            Vampir.count++;
        }

        public override void die() {
            base.die();
            Vampir.count--;   
        }

        public override void runStep() {
            base.runStep();
            this.props[F_FILLER] -= Settings.vampireDecreaseFillPerStep;
            if (this.props[F_FILLER] <= 0) {
                this.die();
                return;
            }
            this.tryInfect();
        } // runStep()
        
        protected void tryInfect() {
            // count humans around me
            int humans = 0;
            for (int i = 1; i <= 8; i++) {
                Spielfeld neighbor = this.sfeld.getNachbarfeld(i);
                if (neighbor != null && neighbor.Sfigur != null) {
                    if (neighbor.Sfigur.props[F_TYPE] == TYPE_HUMAN) {
                        if (Settings.vampireInfectOnlyOneHuman) {
                            humans++;
                        } else {
                            ((Einwohner)neighbor.Sfigur).infect();
                            this.props[F_FILLER] += Settings.vampireIncreaseFillPerBite;
                            // humans is still 0, so runStep() will return after this
                        }
                    }
                }
            }

            if (humans == 0)
                return;

            // randomly infect one human
            int infect = Program.random.Next(0, humans);

            for (int i = 1; i <= 8; i++) {
                Spielfeld neighbor = this.sfeld.getNachbarfeld(i);
                if (neighbor != null && neighbor.Sfigur != null && neighbor.Sfigur.props[F_TYPE] == TYPE_HUMAN) {
                    if (infect == 0) {
                        ((Einwohner)neighbor.Sfigur).infect();
                        this.props[F_FILLER] += Settings.vampireIncreaseFillPerBite;
                        if (!Settings.vampireInfectOneOrMoreHumans) break;
                    } else {
                        infect--;
                    }
                }
            }
        }
    }

}