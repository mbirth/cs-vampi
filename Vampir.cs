using System;

namespace vampi
{
    class Vampir : Spielfigur {

        private static int count = 0;
        public static int Count {
          get { return Vampir.count; }
        }
        private int filler = Settings.vampireInitialFill;
        
        public Vampir(Spielfeld sfeld)
            : base(sfeld) {
            this.typ = Typliste.VAMPIR;
            this.maxAge = Settings.vampireMaxAge;
            this.age = Program.random.Next(0, Settings.vampireMaxInitAge);
            Vampir.count++;
        }

        public override void die() {
            base.die();
            Vampir.count--;   
        }

        public override void runStep() {
            base.runStep();
            this.filler -= Settings.vampireDecreaseFillPerStep;
            if (this.filler <= 0) {
                this.die();
                return;
            }

            // count humans around me
            int humans = 0;
            for (int i = 1; i <= 8; i++) {
                Spielfeld neighbor = this.sfeld.getNachbarfeld(i);
                if (neighbor != null && neighbor.Sfigur != null) {
                    if (neighbor.Sfigur.Typ == Typliste.EINWOHNER) {
                        if (Settings.vampireInfectOnlyOneHuman) {
                            humans++;
                        } else {
                            ((Einwohner)neighbor.Sfigur).infect();
                            this.filler += Settings.vampireIncreaseFillPerBite;
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
                if (neighbor != null && neighbor.Sfigur != null && neighbor.Sfigur.Typ == Typliste.EINWOHNER) {
                    if (infect == 0) {
                        ((Einwohner)neighbor.Sfigur).infect();
                        this.filler += Settings.vampireIncreaseFillPerBite;
                        if (!Settings.vampireInfectOneOrMoreHumans) break;
                    } else {
                        infect--;
                    }
                }
            }
        } // runStep()
    }

}