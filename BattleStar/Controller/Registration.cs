using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleStar.Data;
using BattleStar.WorkModules;
using BattleStar;

namespace BattleStar.Controller
{
    class Registration
    {
        private UserDataManagement udm;
        public Registration()
        {
            udm = new UserDataManagement();
        }
        public bool check(string user)
        {
            bool ret = false;
            var b = udm.ReadUsers();
            if(b == null)
            {
                ret = false;
            }
            else
            {
                for(int i = 1; i < b.Count; i++)
                {
                    if(string.Equals(user, b[i].username))
                    {
                        Console.WriteLine("This name is already taken. Please Login or enter another.");
                        ret = true; break;
                    }
                    else
                    {
                        ret = false;
                    }
                }
            }
            return ret;
        }
        

        public void Register()
        {
            Console.WriteLine("Type 1 to Register, or type 2 to return to mainMenu.");
            bool flag = false;
            string input = " ";
            do
            {
                input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        string info1 = "";
                        do
                        {
                            Console.WriteLine("Enter your username in order to register please.");
                            info1 = Console.ReadLine();
                        }
                        while (check(info1) == true);
                        Console.WriteLine("Enter your password please.");
                        string info2 = Console.ReadLine();
                        bool roleflag = false;
                        string role = "";
                        do
                        {
                            Console.WriteLine("Enter role please: User or Administrator");
                            role = Console.ReadLine();
                            switch (role)
                            {
                                case "User": roleflag = true; break;
                                case "Administrator": roleflag = true; break;
                                default: Console.Write("Incorect input."); break;
                            }
                        }
                        while (roleflag == false);
                  

                        var a = udm.GetTableCount();
                        int iid = -1;

                        if (a == 0)
                        {
                            iid = 1;
                        }
                        else
                        {
                            iid = a + 1;
                        }

                        Player p = new Player()
                        {
                            id = iid,
                            username = info1,
                            password = info2,
                            role = role,
                            createdate = DateTime.Now
                        };

                        udm.WriteUser(p);
                        break;
                    case "2": flag = true; break;
                    default: Console.Write("Incorect input. You shuld choose option 1 or opition 2."); break;
                }
               
            }
            while (flag == false);
            BattleStar.Program.StartMenu();
        }
    }
}
