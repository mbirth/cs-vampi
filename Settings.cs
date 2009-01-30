using System;
using System.Drawing;

namespace vampi {
    public abstract class Settings {
        public const int size = 500;
        public const int coveragePercent = 77;
        public const int vampireRatio = 3;
        public const int drawEveryNthStep = 1;

        public const ConsoleColor colorHuman = ConsoleColor.Green;
        public const ConsoleColor colorHumanInfected = ConsoleColor.DarkMagenta;
        public const ConsoleColor colorVampire = ConsoleColor.Red;
        public const ConsoleColor colorEmpty = ConsoleColor.Gray;
        
/*        public static Color guiColorHuman = Color.LimeGreen;
        public static Color guiColorHumanInfected = Color.DarkMagenta;
        public static Color guiColorVampire = Color.Red;
*/        public static Color[] guiColorHuman = {Color.FromArgb(0, 60, 0), Color.LimeGreen};
        public static Color[] guiColorHumanInfected = {Color.FromArgb(60, 0, 60), Color.DarkMagenta};
        public static Color[] guiColorVampire = {Color.FromArgb(60, 0, 0), Color.Red};
        public static Color guiColorEmpty = Color.Silver;
        
        public static Font guiFont = new Font("sans-serif", 8);
        public static Brush guiFontBrush = Brushes.Black;

        public const int humanLegalSexAge = 10;
        public const int humanVampireConversionPercent = 5;
        public const int humanMaxInitAge = 10;
        public const int humanMaxAge = 80;
        public const int humanInfectedMaxAge = 20;
        public const bool humanInfectedCanReproduceWithNormal = false;
        public const bool humanNormalCanReproduceWithInfected = false;

        public const int vampireInitialFill = 100;
        public const int vampireMaxInitAge = 60;
        public const int vampireMaxAge = 500;
        public const int vampireDecreaseFillPerStep = 3;
        public const int vampireIncreaseFillPerBite = 1;
        public const bool vampireInfectOnlyOneHuman = false;
        public const bool vampireInfectOneOrMoreHumans = false;

    }
}
