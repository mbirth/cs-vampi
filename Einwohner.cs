using System;

namespace vampi
{
    class Einwohner : Spielfigur {
        public const int F_SEX = 10;
        public const int SEX_MALE = 1;
        public const int SEX_FEMALE = 2;
        
        public const int F_INFECTED = 11;
        
        private static int count = 0;
        public static int Count {
            get { return Einwohner.count; }
        }
        public bool Infected {
            get { return (this.props[F_INFECTED] != 0); }
        }
        public string name;

        public Einwohner(Spielfeld sfeld)
            : base(sfeld) {
            this.props[F_TYPE] = TYPE_HUMAN;
            this.props[F_MAXAGE] = Settings.humanMaxAge;
            this.props[F_AGE] = Program.random.Next(0, Settings.humanMaxInitAge);
            Einwohner.count++;
            this.props[F_SEX] = (Program.random.Next(0, 100) < 85)?SEX_MALE:SEX_FEMALE;
            if (this.props[F_SEX] == SEX_MALE) {
                this.name = Settings.namesMale[Program.random.Next(0, Settings.namesMale.GetLength(0))];
            } else {
                this.name = Settings.namesFemale[Program.random.Next(0, Settings.namesFemale.GetLength(0))];
            }
            // Console.WriteLine(this.name+" is born! (" + ((this.ismale)?"m":"f") + ")");
        }

        public void infect() {
            if (this.Infected)
                return;
            // Console.WriteLine(this.name+" got infected!");
            this.props[F_INFECTED] = 1;
            if (this.props[F_AGE] < this.props[F_MAXAGE] - Settings.humanInfectedMaxAge) this.props[F_AGE] = this.props[F_MAXAGE] - Settings.humanInfectedMaxAge;
        }

        public override void die() {
            Einwohner.count--;
            if (this.Infected) {
                int rvalue = Program.random.Next(0, 100);
                if (rvalue <= Settings.humanVampireConversionPercent) {
                    new Vampir(this.sfeld);
                    return;
                }
            }
            base.die();
        }

        public override void runStep() {
            base.runStep();
            this.tryMate();
        }
        
        protected void tryMate() {
            if (this.Infected && !Settings.humanInfectedCanReproduceWithNormal) return;

            // search for constraints (empty field, partner to mate > 10 yrs)
            Spielfeld birthplace = null;
            bool mateFound = false;
            for (int i = 1; i <= 8; i++) {
                Spielfeld neighbor = this.sfeld.getNachbarfeld(i);
                if (neighbor != null && neighbor.Sfigur != null) {
                    if (neighbor.Sfigur.props[F_TYPE] == TYPE_HUMAN) {
                        if (neighbor.Sfigur.Age >= Settings.humanLegalSexAge && (Settings.humanNormalCanReproduceWithInfected || !((Einwohner)neighbor.Sfigur).Infected)) {
                            if (neighbor.Sfigur.props[F_SEX] != this.props[F_SEX]) {
                                mateFound = true;
                            }
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
