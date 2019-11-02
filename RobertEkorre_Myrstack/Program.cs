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

            Ant singleAnt = new Ant(null, 0);
            Program p = new Program();

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

    class ListManager
    {
        public List<Ant> AntList = new List<Ant>();

    }

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
