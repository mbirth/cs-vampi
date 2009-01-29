using System;

namespace vampi
{
    class Einwohner : Spielfigur {

        private static int count = 0;
        public static int Count {
            get { return Einwohner.count; }
        }
        private bool infected = false;
        public bool Infected {
            get { return this.infected; }
        }
        public static int legalSexAge = Settings.humanLegalSexAge;
        public static int vampireConversionRate = Settings.humanVampireConversionPercent;

        public Einwohner(Spielfeld sfeld)
            : base(sfeld) {
            this.typ = Typliste.EINWOHNER;
            this.maxAge = Settings.humanMaxAge;
            this.age = Program.random.Next(0, Settings.humanMaxInitAge);
            Einwohner.count++;
        }

        public void infect() {
            if (this.infected)
                return;
            this.infected = true;
            if (this.age < this.maxAge - Settings.humanInfectedMaxAge) this.age = this.maxAge - Settings.humanInfectedMaxAge;
        }

        public override void die() {
            Einwohner.count--;
            if (this.infected) {
                int rvalue = Program.random.Next(0, 100);
                if (rvalue <= Einwohner.vampireConversionRate) {
                    new Vampir(this.sfeld);
                    return;
                }
            }
            base.die();
        }

        public override void runStep() {
            base.runStep();

            if (this.infected && !Settings.humanInfectedCanReproduceWithNormal) return;

            // search for constraints (empty field, partner to mate > 10 yrs)
            Spielfeld birthplace = null;
            bool mateFound = false;
            for (int i = 1; i <= 8; i++) {
                Spielfeld neighbor = this.sfeld.getNachbarfeld(i);
                if (neighbor != null && neighbor.Sfigur != null) {
                    if (neighbor.Sfigur.Typ == Typliste.EINWOHNER) {
                        if (neighbor.Sfigur.Age >= Einwohner.legalSexAge && (Settings.humanNormalCanReproduceWithInfected || !((Einwohner)neighbor.Sfigur).Infected)) {
                            mateFound = true;
                        }
                    }
                } else if (neighbor != null && neighbor.Sfigur == null) {
                    birthplace = neighbor;
                }
            }
            // reproduce!
            if (mateFound && birthplace != null) {
                new Einwohner(birthplace);
            }

        }
    }
}//namespace
