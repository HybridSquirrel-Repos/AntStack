/*
 * Robin:
 * Onödiga using statements. De gör inget rent prestandamässigt att de finns med här,
 * men det är snyggare om man tar bort de! Den som är värd att tänka på är
 * System.Text. Du använder den i din kod, men du använder där hela sökvägen
 * t.ex. System.Text.StringBuilder = new etc... Detta leder till att ditt
 * using statement inte används. Du hade kunnat ta bort System.Text innan du 
 * skapar din stringbuilder och ha kvar using här för att göra den delen av koden
 * lite mer lättläst.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobertEkorre_Myrstack
{
    class Program
    {
        Command c = new Command();
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome To The Ant Nest \nType \"Help\" or \"-h\" to see Commands and what they do\n");
            bool a = true;

            /*
             * Robin:
             * Antar att singleAnt skapades för att testa saker?
             */
            Ant singleAnt = new Ant(null, 0);
            Program p = new Program();

            /*
             * Robin:
             * Intressant val att lägga loopen i Main funktionen.
             */
            while (a)
            {
                Console.Write(">$ ");
                p.Run();

            }
        }

        private void Run()
        {
            string input;
            input = Console.ReadLine();

            string command = input.ToUpper();
            c.CommandChecker(command);

        }
    }

    class Ant
    {
        public int antLegs;
        public string antName;
        public int creationNum;

        public Ant(string antName, int antLegs)
        {
            this.antName = antName;
            this.antLegs = antLegs;

        }  
    }

    /*
     * Robin:
     * Den här klassen känns lite överflödig då den endast har en variabel och ingen 
     * funktionalitet. Det känns som att du var påväg att skapa en klass som sköter
     * arbetet mot listan, men slutade halvvägs och istället lät Command klassen sköta 
     * allt arbete.
     */
    class ListManager
    {
        public List<Ant> AntList = new List<Ant>();

    }

    /*
     * Robin:
     * Klassen heter Command, men vad jag kan se så gör den mycket mer än att hantera 
     * kommandon.
     */
    class Command
    {
        ListManager LM = new ListManager();
        public int createLegsInput = 0;
        public string createNameInput;
        public string input;
        public int creationNum = 0;

        public void CommandChecker(string input)
        {
            this.input = input;

            if (input == "LS" || input == "LIST")
            {
                List();

            }
            else if (input == "CT" || input == "CREATE")
            {
                Create();

            }
            else if (input == "RM" || input == "REMOVE")
            {
                Remove();

            }
            else if (input == "-E" || input == "EXIT")
            {
                Exit();

            }
            else if (input == "-H" || input == "HELP")
            {
                Help();

            }
            else if (input == "-F" || input == "FIND")
            {
                Find();

            }
            /*
             * Robin:
             * ???
             */
            else if (input == "")
            {

            }
            else if (input == "CL" || input == "CLEAR")
            {
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Invalide Command, Enter \"Help\" or \"-h\" for more information \n");

            }
        }

        public void List()
        {
            int id = 0;
            Console.WriteLine("Ant count: " + LM.AntList.Count + "\n");
            foreach (Ant anti in LM.AntList)
            {
                id++;
                Console.WriteLine("ID: " + id + "\tCreation num: " + anti.creationNum + "\tName: " + anti.antName + "\tLegs: " + anti.antLegs);

            }
            Console.WriteLine("\n");

        }

        public bool nameAccepted = false;
        public bool legsAccepted = false;

        /*
         * Robin:
         * Jag är fundersam över variabeln createContinue. Vad jag kan se så fyller den 
         * ingen direkt funktion i detta sammanhang. Den kan dock fylla en estetisk
         * fuktion och göra koden mer läsbar, men även det är jag fundersam på om den gör
         * med tanke på att du inte använder en motsvarande variabel i den första while-
         * loopen. Värt att fundera på rent stilistiskt.
         */
        public void Create() // Checks input with MAXNAMELENGHT and then checks with the ant list if both criterias are acived it continues
        {
            bool createContinue = true;
            string tempStringToInt;

            const int MAXNAMELENGTH = 11;
            const int MAXLEGS = 7;
            const int MINLEGS = -1;
            int createLength;
                       
            while (true)
            {
                Console.WriteLine("Enter a ant name: \n");
                bool duplicate = false;
                createNameInput = Console.ReadLine();
                createLength = createNameInput.Length;
                createNameInput = createNameInput.Replace(" ", "");

                if (MAXNAMELENGTH <= createLength)
                {
                    Console.WriteLine("The ant won't remember the name plebb\n");
                    createNameInput = null;
                    continue;

                }
                else
                {
                    if (String.IsNullOrEmpty(createNameInput))
                    {
                        Console.WriteLine("Invaild, retry!");
                        continue;
                    }
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(createNameInput);
                    /*
                     * Robin:
                     * Behöver inte använda System. nedan eller System.Text ovan. Se kommentaren längst upp
                     * för mer info.
                     */
                    if (System.Char.IsLower(sb[0]) == true)
                    {
                        sb[0] = System.Char.ToUpper(sb[0]);
                        createNameInput = sb.ToString();

                    }
                }

                foreach (Ant anti in LM.AntList)
                {
                    if (anti.antName == createNameInput)
                    {
                        Console.WriteLine("The ant cant be twins plebb\n");
                        createNameInput = null;
                        duplicate = true;

                    }
                }

                if (duplicate)
                {
                    continue;
                }

                Console.WriteLine("Accepted\n");
                nameAccepted = true;
                break;

            }

            createContinue = true;

            Console.WriteLine("Enter amount of legs:\n");
            while (createContinue)                       // check input if it can convert to num and then checks with MAXLEGS and MINLEGS if achived it creates the ant
            {
                tempStringToInt = Console.ReadLine();

                if (!int.TryParse(tempStringToInt, out createLegsInput))
                {
                    Console.WriteLine("That is not a number\n");

                }
                else if (createLegsInput >= MAXLEGS)
                {
                    Console.WriteLine("Ant dont have more legs then 6 plebb\n");
                    createLegsInput = 0;

                }
                else if (createLegsInput <= MINLEGS)
                {
                    Console.WriteLine("Ant cant have minus legs\n");
                    createLegsInput = 0;

                }
                else
                {
                    Console.WriteLine("Accepted\n");
                    createContinue = false;
                    legsAccepted = true;

                }

                if (legsAccepted == true && nameAccepted == true)
                {
                    creationNum++;
                    Ant singleAnt = new Ant(createNameInput, createLegsInput);
                    singleAnt.creationNum = creationNum;
                    LM.AntList.Add(singleAnt);
                    legsAccepted = false;
                    nameAccepted = false;

                }
            }
        }

        public void Remove()
        {
            List();
            Console.WriteLine("If the count is zero please press enter to exit and then create and ant");
            Console.WriteLine("Enter a ID: ");

            /*
             * Robin:
             * Vad står rm för? Enda stället vad jag kan se att du använt en förkortning.
             */
            string rmInput;
            int rmint;
            rmInput = Console.ReadLine();
            
            if (!int.TryParse(rmInput, out rmint))
            {
                Console.WriteLine("\nThat is not a number\n");

            }
            rmint = rmint - 1;
            try
            {
                LM.AntList.RemoveAt(rmint);
                Console.WriteLine("\nSuccess");

            }
            catch
            {
                Console.WriteLine("Did not work try again later");

            }
        }

        public void Exit()
        {
            Environment.Exit(0);

        }

        public void Help()
        {
            Console.WriteLine("ct - create : Creates an ant \nls - list : Show all the ants and how many \nrm - remove : Removes ants");
            Console.WriteLine("cl - clear : Clears the terminal \n-f - find : Finds an ant \n-h - help : Gives you help");
            Console.WriteLine("-e - exit : self-explanatory");

        }

        /*
         * Robin:
         * Snygg metod! Det går inte att lägga till 2 myror med samma namn vad jag kan se,
         * däremot så går det att lägga till en myra med en siffra som namn. Detta leder till 
         * att man kan hamna i situationer där vissa myror inte går att nå. Detta är dock ett
         * väldigt litet problem.
         */
        public void Find()// trys to convert input to int if not it will search by name
        {
            Console.WriteLine("Please enter a name or number of legs to find an ant \nIf it does not show anything there arnt any ants with the name or legs\n");

            string findInput = Console.ReadLine();
            string findInputUpper = findInput.ToUpper();
            int checklegs;

            if (!int.TryParse(findInputUpper, out checklegs))
            {
                foreach (Ant anti in LM.AntList)
                {
                    if (findInputUpper == anti.antName.ToUpper())
                    {
                        Console.WriteLine("\tCreation num: " + anti.creationNum + "\tName: " + anti.antName + "\tLegs: " + anti.antLegs + "\n");

                    }
                }
            }
            else
            {
                foreach (Ant anti in LM.AntList) // if it can convert input it will search by int
                {
                    if (checklegs == anti.antLegs)
                    {
                        Console.WriteLine("\tCreation num: " + anti.creationNum + "\tName: " + anti.antName + "\tLegs: " + anti.antLegs);

                    }
                }
            }
        }
    }
}


/*
 * Robin:
 * Snyggt jobbat! Jag hittar inga större problem, så de flesta kommentarer som jag skrivit 
 * är detaljer och stilistiska åsikter. Gör med de som du vill.
 * 
 * Du har en tydlig och konsekvent kodningsstil, med bra beskrivande namngivning. Jag kan
 * rekommendera till framtiden att du funderar över vad som borde vara klass och inte 
 * och vad du vill att klasserna ska göra. Jag skulle även titta över vad som behöver 
 * vara en variabel och inte. 
 * 
 * Jag kan rekommendera extra läsning kring att deklarera variabler inline om det är något 
 * som intresserar dig!
 * 
 * Det har varit väldigt kul att se dig arbeta med uppgiften på lektionerna. Det märks att du
 * har en del förkunskap kring programmering när jag hör dig prata om de problem som uppstår,
 * och det märks i din kod att du funderar mycket och försöker lösa problem självständigt.
 * 
 * Fortsätt så här!
 */