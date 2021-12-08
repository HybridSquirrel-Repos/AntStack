using System;
using System.Collections.Generic;
using System.Linq;

namespace AntStackV2
{
    class Program
    {
        List<Ant> antNest = new();
        static void Main()
        {
            Program program = new Program();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome To The Ant Nest \nType \"help\" to see Commands and what they do\n");

            while (true)
            {
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "help":
                        Console.WriteLine("create : Creates an ant \nlist : Show all the ants and how many \nremove : Removes ants");
                        Console.WriteLine("clear : Clears the terminal \nfind : Finds an ant \nhelp : Gives you help");
                        Console.WriteLine("exit : self-explanatory");
                        break;
                    case "remove":
                        program.Remove();
                        break;
                    case "exit":
                        return;
                    case "find":
                        Console.WriteLine("Enter a name to find or exit to leave");
                        while (true)
                        {
                            string findName = Console.ReadLine().ToLower();
                            if (findName == "exit") { break; }
                            Ant ant = program.Find(findName, false);
                            if (ant != null)
                            {
                                Console.WriteLine(ant.GetName() + ", " + ant.GetLegs() + ", " + ant.GetID());
                            }
                            else { Console.WriteLine("Can't find the ant!"); }
                        }
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "create":
                        program.Create();
                        break;
                    case "list":
                        foreach (var ant in program.antNest)
                        {
                            Console.WriteLine(ant.GetName() + ", " + ant.GetLegs() + ", " + ant.GetID());
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }
        
        void Remove()
        {
            Console.WriteLine("Enter ant id to remove it from ant nest: ");
            if (Int32.TryParse(Console.ReadLine(), out var id))
            {
                try { antNest.RemoveAt(id); }
                catch { Console.WriteLine("Could not remove"); }
            }
        }

        Ant Find(string antName, bool callCreation) //Debugging is needed 
        {
            foreach (var ant in antNest)
            {
                if (antName == ant.GetName())
                {
                    if(callCreation){ Console.WriteLine("Can't have duplicate names!"); }
                    return ant;
                }
            }
            return null;
        }

        void Create()
        {
            bool init = false;
            var singleAnt = new Ant();
            while (true)
            {
                if (!init)
                {
                    Console.WriteLine("Enter the ant's name:");
                    string nameInput = Console.ReadLine().ToLower();
                    bool isValid = string.IsNullOrEmpty(nameInput) || nameInput.Any(char.IsDigit);
                    if (isValid || null == Find(nameInput, true))
                    { 
                        singleAnt.SetName(nameInput);
                        init = true;
                    }
                }
                else
                {
                    Console.WriteLine("Enter legs amount:");
                    if (Int32.TryParse(Console.ReadLine(), out var legs) && legs > 0)
                    {
                        singleAnt.SetLegs(legs);
                        antNest.Add(singleAnt);
                        break;
                    }
                    Console.WriteLine("Ant can't have zero or minus legs!");
                }
            }
        }
    }
}