using System;
using BattleStar.Data;
using BattleStar.Controller;
using BattleStar.WorkModules;

namespace BattleStar
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDataManagement udm = new UserDataManagement();
            StartMenu();
            bool isExit = false;
            while(isExit == false)
            {
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Login l = new Login();
                        l.LogIn();
                        break;
                    case "2":
                        Registration r = new Registration();
                        r.Register();
                        break;
                    case "3":
                        udm.ShowUsers1();
                        break;
                    case "4": Console.WriteLine("The battleships between which you can choose are: \n Capital \n Destroyer \n Cruiser \n Freighter \n Assault");
                        Console.WriteLine("Press any key...");
                        break;
                    case "5": udm.InfoFileRead(@"C:\Users\nanu\source\repos\BattleStar\BattleStar\Data\HelpInfo.txt.txt"); break;
                    case "6":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Invalid command");
                        StartMenu();
                        break;
                }
                Console.WriteLine();

            }
            Console.ReadKey();
        }
        public static void StartMenu()
        {
            Console.WriteLine("Choose an option (enter option number).");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Users");
            Console.WriteLine("4. BattleShips");
            Console.WriteLine("5. Help? - Info (!) ");
            Console.WriteLine("6. Logout");
        }
    }
}
