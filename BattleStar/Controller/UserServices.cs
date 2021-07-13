using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleStar.Data;
using BattleStar.WorkModules;
using BattleStar.View;

namespace BattleStar.Controller
{
    class UserServices
    {
        BattleStart startBattle = new BattleStart();
        Registration reg = new Registration();
        BattleshipDataManagement bdm = new BattleshipDataManagement();
        UserDataManagement udm = new UserDataManagement();
        private void UserFuncMenu(bool flag)
        {
            Console.WriteLine("Choose an option.");
            Console.WriteLine("1. Start a battle.");
            Console.WriteLine("2. Create battleship.");
            Console.WriteLine("3. Show battleships.");
            Console.WriteLine("4. Edit battleships.");
            Console.WriteLine("5. Delete battleship.");
            Console.WriteLine("6. Edit my profile.");
            Console.WriteLine("7. Show Battles");
            Console.WriteLine("8. Logout.");
            if (flag == false)
            {
            }
            else
            {
                Console.WriteLine("9. Show users.");
                Console.WriteLine("10. Edit users.");
                Console.WriteLine("11. Edit user's battleship.");
            }
        }
        private bool checkIfUsed(string name)
        {
            bool flag = false;
            List<BattleShips> b = new List<BattleShips>();
            b = bdm.ReadBattleShips();
            if(b == null)
            {
                flag = false;
            }
            else
            {
                for(int i = 0; i < b.Count; i++)
                {
                    if(string.Equals(name, b[i].name))
                    {
                        Console.WriteLine("You have already have a ship with this name.");
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }
        private int checkPo(int n)
        {
            bool f = false;
            do
            {
                Console.WriteLine("Enter value between 0 and 100");
                n = int.Parse(Console.ReadLine());
                if (n > 0 && n <= 100)
                {
                    f = true;
                }
                else
                {
                    Console.Write("Error. The value should be within the range.");
                    f = false;
                }
            }
            while (f == false);
            return n;
        }
        private BattleShips CBSProcess(int id)
        {
            string info1 = "";
            do
            {
                Console.WriteLine("Enter name for battleship.");
                info1 = Console.ReadLine();
            }
            while (checkIfUsed(info1) == true);
            bool flag1 = false;
            string info2 = "";
            while (flag1 == false)
            {
                Console.WriteLine("Enter battleship type.");
                Console.WriteLine("Capital");
                Console.WriteLine("Destroyer");
                Console.WriteLine("Cruiser");
                Console.WriteLine("Freighter");
                Console.WriteLine("Assault");
                info2 = Console.ReadLine();
                switch (info2)
                {
                    case "Capital": flag1 = true; break;
                    case "Destroyer":flag1 = true; break;
                    case "Cruiser": flag1 = true; break;
                    case "Freighter": flag1 = true; break;
                    case "Assault":  flag1 = true; break;
                    default: Console.WriteLine("Incorect input. Choose one of the battleship types."); flag1 = false; break;
                }
            }
            int ap = 0;
            var aaa = bdm.GetCount();
            int dataid = -1;
            if(aaa == 0)
            {
                dataid = 1;
            }
            else
            {
                dataid = aaa + 1;
            }
            Console.WriteLine("Enter Attack points.");
            ap = checkPo(ap);
            Console.WriteLine("Enter Defence points.");
            int dp = 0;
            dp = checkPo(dp);
            Console.WriteLine("Enter Health points");
            int hp = 0;
            hp = checkPo(hp);
            BattleShips b = new BattleShips
            {
                id = dataid,
                uid = id,
                name = info1,
                type = info2,
                attack = ap,
                defence = dp,
                health = hp,
                date = DateTime.Now
            };
            return b;
        }
        private void CreateBS(int id)
        {
            bool ex = false;
            while(ex == false)
            {
                Console.WriteLine("Choose an option please.");
                Console.WriteLine("1. Create Battleship.");
                Console.WriteLine("2. <-- Back ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        BattleShips b = new BattleShips();
                        b = CBSProcess(id);
                        bdm.CreateBattleship(b);
                        break;
                    case "2": ex = true; break;
                    default: Console.WriteLine("Incorect input, try again..."); break;
                }
            }
        }
        private int repeating(int usrInputId)
        {
            List<Player> lP = udm.ReadUsers();
            bool flagS = false;
            do
            {
                udm.ShowUsers1();
                Console.WriteLine();
                Console.WriteLine("Enter user id");
                usrInputId = int.Parse(Console.ReadLine());
                foreach (Player el in lP)
                {
                    if (el.id == usrInputId)
                    {
                        flagS = true;
                        break;
                    }
                    else
                    {
                        flagS = false;
                    }
                }
            }
            while (flagS == false);
            return usrInputId;
        }

        public void UserFunc(bool flag, int id)
        {
            Console.Clear();
          
            bool isExit = false;
            while(isExit == false)
            {
                UserFuncMenu(flag);
                Console.WriteLine("Choose an option. ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        //if (bdm.DoesUserHaveBattleShip(id) == false)
                        //{
                        //    Console.WriteLine("You should first create battleship.");
                        //} //checks if user have created battleship
                        //if(bdm.DoesUserHaveBattleShip(id) == true)
                        //{
                            startBattle.StartBattle(id);
                        //}
                        break;
                    case "2":
                        CreateBS(id);
                        break;
                    case "3":
                        //show
                        bdm.ShowUsersBattleships(id);
                        break;
                    case "4":
                        //edit battleship data
                        bdm.EditBattleshipData(id);
                        break;
                    case "5":
                        //delete battleship
                        //bdm.DeleteBattleship(id); //doesn't work properly :(
                        break;
                    case "6":
                        string newname = "";
                        do
                        {
                            Console.WriteLine("Enter new name.");
                            newname = Console.ReadLine();
                        }
                        while (reg.check(newname) == true);
                        Console.WriteLine("Enter new password please:");
                        string pass = Console.ReadLine();
                        udm.EditUser(id, newname, pass);
                        break;
                    case "7":
                        BattleResultManagement brm = new BattleResultManagement();
                        BattleCourceManagement bcm = new BattleCourceManagement();
                        brm.ShowBattles1(id);
                        Console.WriteLine("Enter id of the battle you wish to see");
                        List<int> BattleIds = brm.UserBattlesIds(id);
                        int inputId = 0;
                        bool correctId = false;
                        do
                        {
                            inputId = int.Parse(Console.ReadLine());
                            foreach (int el in BattleIds)
                            {
                                if(el == inputId)
                                {
                                    correctId = true;
                                }
                            }
                        }
                        while (correctId == false);
                        string choice = "";
                        bool choiceres = false;
                        do
                        {
                            Console.WriteLine("Do you want to see the full or shortened info? \n Type full for all the information or short only for the result");
                            choice = Console.ReadLine();
                            Console.Clear();
                            switch (choice)
                            {
                                case "full": brm.ShowResultsById(inputId); Console.WriteLine(); bcm.ReadBattleById(inputId); Console.WriteLine(); choiceres = true; break;
                                case "short": brm.ShowResultsById(inputId); Console.WriteLine(); choiceres = true; break;
                                default: Console.WriteLine("Incorect option"); break;
                            }
                        }
                        while (choiceres == false);
      
                        break;
                    case "8":
                        isExit = true;
                        Console.Clear();
                        break;
             
                    default: Console.WriteLine("Incorect input."); break;
                }
                if(flag == true)
                {
                    switch (input)
                    {
                        case "9":
                            udm.ShowUsersToAdmin();
                            break;
                        case "10":
                            int usrInputId = 0;
                            usrInputId = repeating(usrInputId);
                            string newname1 = "";
                            do
                            {
                                Console.WriteLine("Enter new name.");
                                newname1 = Console.ReadLine();
                            }
                            while (reg.check(newname1) == true);
                            Console.WriteLine("Enter new password please:");
                            string pass1 = Console.ReadLine();
                            string newRole = Console.ReadLine();
                            udm.EditUserAsAdmin(usrInputId, newname1, pass1, newRole);
                            break;
                        case "11":
                            int usrInputId1 = 0;
                            usrInputId1 = repeating(usrInputId1);
                            bdm.EditBattleshipData(usrInputId1);
                            break;
                        default: Console.WriteLine("Incorect input."); break;
                    }
                }
            }
            BattleStar.Program.StartMenu();
        }
    }
    
}
