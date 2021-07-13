using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleStar.Data;

namespace BattleStar.Controller
{
    class Login
    {
        private UserDataManagement udm;
        public Login()
        {
            udm = new UserDataManagement();
        }
        private bool checkUserRole(string user)
        {
            bool re = false;
            int id = -1;
            var c = udm.ReadUsers();
            if (c == null)
            {
                Console.WriteLine("There is no data or the database is empty.");
            }
            else
            {
                for (int j = 1; j < c.Count; j++)
                {
                    if (string.Equals(user, c[j].username))
                    {
                        id = j;
                        break;
                    }
                }
                if (string.Equals("Administrator", c[id].role))
                {
                    re = true;
                    Console.WriteLine("You are administrator.");
                }
                else
                {
                    re = false;
                    Console.WriteLine("You are user.");
                }
            }
            return re;
        }
        private bool checkUserPass(string user, string pass)
        {
            bool ret;
            bool usr = false;
            bool pas = false;
            int count = udm.GetTableCount();
            var b = udm.ReadUsers();
            if (b == null)
            {
                usr = false;
                pas = false;
                Console.WriteLine("There is no data for this user or the database is empty.");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (string.Equals(user, b[i].username) && string.Equals(pass, b[i].password.ToString()))
                    {
                        usr = true; 
                        pas = true;
                        i = count - 1;
                        //break;
                    }
                    else 
                    {
                        usr = false;
                        pas = false;
                        //Console.WriteLine("There is no such username in the databas or your password is wrong.");
                        //break;
                    }
                }
            }
            if(usr == true && pas == true)
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
            return ret;
        }
        public void LogIn()
        {
            Console.WriteLine("Type 1 to Login, or type 2 to return to mainMenu.");
            bool flag = false;
            string input = " ";
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter your username please.");
                        string info1 = Console.ReadLine();
                        Console.WriteLine("Enter your password please.");
                        string info2 = Console.ReadLine();
                        bool res = checkUserPass(info1, info2);
                        if(res == true)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("Welcome back " + info1);
                            bool rol = checkUserRole(info1);
                            UserServices userF = new UserServices();
                            int idUsr = udm.GetId(info1, info2);
                            userF.UserFunc(rol, idUsr);
                        }
                        else
                        {
                            Console.Write("Login failed. Press any key.");
                        }
                        break;
                    case "2": flag = true; break;
                    default:
                        Console.WriteLine("Incorect input. Type 2 to return to main menu");
                        break;
                }

            }
            while (flag == false);
            BattleStar.Program.StartMenu();
        }

    }
}
