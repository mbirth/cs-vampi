using System;
using System.Drawing;

namespace vampi {
    public abstract class Settings {
        public const int size = 120;
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
        public static Color[] guiColorHumanFemale = {Color.FromArgb(60, 60, 0), Color.Yellow};
        public static Color[] guiColorHumanMale = {Color.FromArgb(0, 60, 0), Color.LimeGreen};
        public static Color[] guiColorHumanInfected = {Color.FromArgb(60, 0, 60), Color.DarkMagenta};
        public static Color[] guiColorVampire = {Color.FromArgb(60, 0, 0), Color.Red};
        public static Color guiColorEmpty = Color.Wheat;
        
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
        
        public static string[] namesMale = {
            "Aaron", "Achim", "Adalger", "Adam", "Adelar", "Adrian", "Aginulf", "Agomar", "Alain", "Alarich", "Alban", "Albero", "Albert", "Alderich", "Aldo", "Alexander", "Alger", "Ali", "Allen", "Amon", "Andrew", "Antoine", "Arnaud", "Arnie", "Art", "Arwed", "Arve", "Aquarius",
            "Benny", "Beowulf", "Billy", "Bobby", "Bela", "Binti", "Biko", "Bastian", "Brian", "Boris", "Bero", "Bert", "Bertil", "Bernard", "Berenger", "Benoit", "Bernd", "Baldur", "Baptist", "Baldwin", "Basil", "Björn", "Bohumil", "Brad", "Burkhard", "Brix",
            "Carl", "Carlo", "Cedric", "Claude", "Claus", "Cletus", "Cestos", "Colbert", "Coleman", "Cole", "Chris", "Cajetan", "Candid", "Cassian", "Castor", "Cecil", "Clark", "Cliff", "Clive", "Clemens", "Conrad", "Cosmas", "Curt", "Cyrus",
            "Damian", "Dan", "Darwin", "Dave", "David", "Denis", "Derek", "Dilbert", "Dionys", "Don", "Duras", "Diablo", "Domenic", "Dorian", "Dale", "Didier", "Diego", "Dieter", "Dietrich", "Dietwald", "Dino", "Dirk", "Dismas", "Dominik", "Donald", "Doug", "Drawo", "Duncan",
            "Edmond", "Eliah", "Enki", "Eric", "Etienne", "Emmerich", "Einstein", "Encke", "Earl", "Eberhard", "Ecki", "Eddi", "Edmond", "Einar", "Elias", "Elmar", "Elmo", "Emile", "Ephraim", "Erik", "Erasmus", "Ernst", "Erwin", "Esra", "Esteban", "Eugen", "Ezzo",
            "Fabrice", "Felix", "Fjodor", "Francis", "Frederic", "Fritz", "Fredegar", "Furtwängler", "Florin", "Frido", "Fabian", "Falko", "Farold", "Fides", "Flodoard", "Florian", "Franz", "Fridolin", "Friedrich", "Frowin", "Fritz",
            "Gaston", "Gene", "Georges", "Gerard", "Gert", "Gilbert", "Gilles", "Gino", "Guy", "Guilio", "Ghiberti", "Gismo", "Guntbert", "Gerrik", "Ged", "Gordy",
            "Heiko", "Henner", "Henri", "Hubert", "Hask", "Herluf", "Hilbert", "Han Solo", "Harrison", "Herodot", "Hinrich", "Halfred",
            "Ian", "Igor", "Ibert", "Immanuel", "Indy", "Ingres", "Ingram", "Ingvar", "Ignatius", "Irving", "Ivan", "Ivar", "Isbert", "Ismael",
            "Jacob", "Jake", "Jamie", "Jan", "Jason", "Jay", "Jean", "Jeannot", "Jeff", "Jeffrey", "Jeremy", "Jerome", "Jimmy", "Joel", "Jon", "Jost", "Jules", "Julien", "Jervis", "Johann", "Jodokus",
            "Kolya", "Kor", "Kras", "Kurn", "Kelkad", "Kenny", "Kant", "Korbinian", "Kai", "Kalle", "Karl", "Kaspar", "Kevin", "Klaas", " Klaus", "Konrad", "Kurd", "Kurt",
            "Larry", "Laurent", "Lennart", "Leon", "Leonard", "Lionel", "Louis", "Luc", "Lucien", "Linné", "Lohengrin", "Leif", "Leslie", "Lewis", "Lorenz", "Loriot",
            "Malcolm", "Marc", "Marcel", "Marco", "Martial", "Marvin", "Maurice", "Maxime", "Michel", "Mike", "Miles", "Momo", "Mordok", "Morten", "Manuzai", "Milio", "Meelo", "Maurice", "Machiavelli", "Micha",
            "Nicolas", "Nives", "Noel", "Norbert", "Nathan", "Nahum", "Nithard", "Nick", "Nivard", "Nando",
            "Odo", "Olivier", "Odin", "Oliver", "Orion", "Orlando", "Ojoro", "Orki", "Oruio", "Otker", "Otthermann",
            "Pascal", "Paul", "Pete", "Peter", "Philip", "Pierre", "Pelle", "Pilo", "Pindo", "Pisto", "Poulo",
            "Quark", "Quasimodo", "Quirin", "Quintus", "Quantus", "Questo", "Quasto", "Quentin", "Quentino",
            "Raoul", "Remi", "Rom", "Romeo", "Romain", "Ronny", "Rudy", "Ruprecht", "Raimo", "Rudenz", "Reinold", "Rolando", "Rowland", "Rudolf", "Rodolopho", "Ray", "Raymond",
            "Sammy", "Scotty", "Severin", "Simon", "Sisko", "Spock", "Steve", "Stuart", "Sven", "Sylvain", "Serge", "Sheyel", "Stephen", "Soren", "Sordes", "Strubbel",
            "Tassilo", "Theo", "Thibaut", "Tilman", "Titi", "Toby", "Tom", "Tristan", "Thomas", "Tyus", "Telly", "Thilo",
            "Urban", "Urs", "Udo",
            "Vince", "Vincenzo", "Vincent", "Valentin", "Vaclaw", "Victor", "Vivien", "Volker", "Volkhart", "Volkmar", "Vitus", "Vittorio",
            "Willi", "Wolfgang", "Worf", "Wallenstein", "Walter", "Welf", "Wennemar", "Werther", "Wigand", "Winfried", "Wolf", "Wotan",
            "Xenophanes", "Xenopoulos", "Xerxes", "Xaver", "Xavier",
            "Yannick", "Yvan", "Yves", "Yvon", "Yfri", "Yorick", "Yorck", "Yul", "Yvo",
            "Zenji", "Zorg", "Zeltan", "Zacharias", "Zoltan", "Zyprian"            
        };
        public static string[] namesFemale = {
            "Adalie", "Adda", "Adeline", "Adelita", "Agnes", "Agneta", "Alexis", "Alice", "Aline", "Allison", "Almud", "Amanda", "Amelia", "Amy", "Andrea", "Anemone", "Angelica", "Anita", "Anja", "Annabel", "Anne", "Annette", "Anouk", "Antonia", "Ariana", "Aude", "Audrey", "Auguste", "Aurora", "Ayla", "Alouette", "Alyssa", "April", "Arlene", "Askama", "Auriga",
            "Babette", "Baltrun", "Barbara", "Belannah", "Belinda", " Bernadette", "Bertha", "Betty", "Bibi", "Biggi", "Birte", "Blanche", "Britta", "Beekje", "Bilba", "Bonnie", "Beate", "Bovista",
            "Camille", "Carine", "Carla", "Carmen", "Carola", "Caroline", "Cathy", "Celia", "Chantal", "Charlene", "Chloe", "Christina", "Cindy", "Claire", "Clemence", "Coco", "Colette", "Connie", "Corinne", "Cora", "Cynthia", "Carmina", "Columba",
            "Dalia", "Dana", "Dani", "Danielle", "Darla", "Delphine", "Denise", "Desiree", "Dette", "Dina", "Doris", "Dunja", "Djalao", "Drawida", "Debra", "Dyonisia", "Domenica", "Dora", "Dalila",
            "Edita", "Edith", "Eliane", "Elisa", "Elli", "Elsa", "Emilie", "Emma", "Erika", "Esmeralda", "Estelle", "Ethel", "Eve", "Euklida", "Eva Maria", "Elina", "Emmanuelle", "Erdtrud", "Emmi", "Ester", "Evita",
            "Fafa", "Fanette", "Fatima", "Felicie", "Fiona", "Flora", "Florence", "Franzi", "Frauke", "Friederike", "Fia", "Frizzi", "Florina", "Fenjala", "Froane", "Fabienne", "Francine",
            "Gerda", "Gesine", "Ginger", "Grausi", "Grenda", "Grilka", "Griselda", "Gundela", "Gwen", "Gerlis", "Gaia", "Gallia", "Grenouille", "Gea", "Gabrielle", "Gisela",
            "Hannah", "Hannelore", "Helena", "Helga", "Heloise", "Henna", "Henrike", "Habima", "Haifa", "Halma", "Hero", "Huberta", "Hilaria", "Helke", "Henrietta", "Hidda",
            "Ines", "Ingeborg", "Irene", "Iris", "Irma", "Isabelle", "Isolde", "Ilma", "Imperia", "Inka", "Irina", "Imura", "Inken", "Inka", "Ilka", "Ishtar",
            "Janet", "Janina", "Jeanne", "Jeannie", "Jessica", "Jill", "Joanna", "Josette", "Julia", "Julianne", "Juliette", "Jellina", "Jovana", "Juliana", "June", "Jennifer", "Jordana",
            "Kassandra", "Katja", "Kelly", "Kes", "Kim", "Kira", "Kordia", "Kendra", "Killy", "Kissy", "Kate", "Kathryn", "Karin", "Karla", "Kulka",
            "Lara", "Laura", "Lea", "Leila", "Lili", "Linda", "Lis", "Lucia", "Lursa", "Lydie", "Lale", "Lisbeth", "Larissa", "Lelia", "Lidia", "Lora", "Loretta",
            "Madeleine", "Mafalda", "Maia", "Makeba", "Mandy", "Manon", "Manuela", "Mara", "Marcelle", "Margo", "Marie", "Martine", "Mary", "Maryse", "Mathilda", "Meg", "Melusine", "Merita", "Micaela", "Michelle", "Minerva", "Minette", "Miriam", "Monika", "Marketta", "Molina", "Miranda", "Marthe", "Mariana", "Melissa", "Mona", "Mila", "Mizzi", "Molly", "Morgane",
            "Nadege", "Nadine", "Naomi", "Nathalie", "Nina", "Noelle", "Norina", "Nilly", "Nadia", "Norma", "Nuptia", "Nadura",
            "Odette", "Oona", "Oceana", "Odessa", "Ombra", "Ophra", "Ophelia",
            "Paloma", "Pascale", "Pauline", "Pam", "Phila", "Paola", "Pelagia", "Punta", "Paula", "Perdita", "Pella", "Petra", "Panja", "Pasta", "Petula",
            "Quantana", "Quentina", "Quirina", "Quanta", "Quesera", "Quarka", "Questa", "Quilla",
            "Rachel", "Rebecca", "Robyn", "Roselyne", "Ruth", "Rahel", "Ricarda", "Raya", "Rea", "Renata", "Rasta",
            "Sabine", "Sally", "Samantha", "Sandra", "Sandy", "Sarah", "Sarana", "Sieglinde", "Sina", "Solange", "Sophie", "Stacy", "Sibylle", "Sylvie", "Salina", "Seltar", "Setha", "Suleika", "Sunna", "Sasa", "Silvana", "Sonja", "Sinja", "Siska", "Sirta", "Sujin",
            "Tabita", "Tamara", "Tanja", "Terri", "Tiffany", "Tilly", "Tina", "Tonja", "Troi", "Taina", "Tassila", "Thornia", "Tildy", "Tanita",
            "Undine", "Ursula", "Ulla",
            "Vekma", "Veronique", "Viviane", "Valentina", "Vilma", "Valentine", "Valerie", "Vanessa", "Vivien",
            "Wanda", "Wendy", "Wilma", "Wally", "Willa", "Wismut", "Wulfila", "Wilgund", "Wilhelmina", "Wilrun", "Winny", "Wisgard", "Wiska", "Witta",
            "Xenia", "Xanthia", "Xana", "Xanthippe", "Xandra",
            "Yolande", "Yvette", "Yerba", "Yvonne", 
            "Zoe", "Zenja", "Zancara", "Zarah"
        };
        
    }
}
