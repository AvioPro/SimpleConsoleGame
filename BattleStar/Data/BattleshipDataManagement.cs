using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BattleStar.WorkModules;
using BattleStar.Controller;

namespace BattleStar.Data
{

    class BattleshipDataManagement
    {
        const string conStr = "Data Source=Desktop-fb8br93;Initial Catalog=BattleStar;Integrated Security=True";
        List<BattleShips> bsl = new List<BattleShips>();
        SqlConnection con = new SqlConnection(conStr);
        public List<BattleShips> ReadBattleShips()
        {
            con.Open();
            string q = "SELECT [id], [UserId], [BattleshipName], [BattleshipType],[AttackPoints],[DefencePoints],[HealthPoints], [DateCreatedBS] FROM BattleShip";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            BattleShips b = new BattleShips()
                            {
                                id = reader.GetInt32(0),
                                uid = reader.GetInt32(1),
                                name = reader.GetString(2),
                                type = reader.GetString(3),
                                attack = reader.GetInt32(4),
                                defence = reader.GetInt32(5),
                                health = reader.GetInt32(6),
                                date = reader.GetDateTime(7)
                            };
                            bsl.Add(b);
                        }
                    }
                    else
                    {
                        bsl = null;
                    }
                }
            }
            return bsl;
        }
        public void CreateBattleship(BattleShips b)
        {
            con.Open();
            string q = "SET IDENTITY_INSERT BattleShip ON; insert into BattleShip([id], [UserId], [BattleshipName], [BattleshipType],[AttackPoints],[DefencePoints],[HealthPoints], [DateCreatedBS])values(@id, @UserId, @BattleshipName, @BattleshipType, @AtackPoints, @DefencePoints, @HealthPoints,@DateCreatedBS)";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                com.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = b.id;
                com.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = b.uid;
                com.Parameters.Add("@BattleshipName", System.Data.SqlDbType.NVarChar).Value = b.name;
                com.Parameters.Add("@BattleshipType", System.Data.SqlDbType.NVarChar).Value = b.type;
                com.Parameters.Add("@AtackPoints", System.Data.SqlDbType.Int).Value = b.attack;
                com.Parameters.Add("@DefencePoints", System.Data.SqlDbType.Int).Value = b.defence;
                com.Parameters.Add("@HealthPoints", System.Data.SqlDbType.Int).Value = b.health;
                com.Parameters.Add("@DateCreatedBS", System.Data.SqlDbType.DateTime).Value = b.date;
                object result = com.ExecuteNonQuery();
            }
            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }
        public int GetCount()
        {
            int num = 0;
            con.Open();
            string q = "SELECT [id], [UserId], [BattleshipName], [BattleshipType],[AttackPoints],[DefencePoints],[HealthPoints], [DateCreatedBS] FROM BattleShip";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            num++;
                        }
                    }
                    else
                    {
                        num = 0;
                    }
                }
            }
            return num;
        }
        public void ShowUsersBattleships(int id)
        {
            List<BattleShips> b = new List<BattleShips>();
            b = ReadBattleShips();
            foreach (BattleShips el in b)
            {
                if (el.uid == id)
                {
                    Console.WriteLine("id: {0} User id: {1} Battleship name: {2}, Battleship type {3}, AttackPoints: {4}, Defence(Shield) points {5}, Health points {6}, Date created: {7}",
                        el.id.ToString(), el.uid.ToString(), el.name, el.type, el.attack.ToString(), el.defence.ToString(), el.health.ToString(), el.date.ToString(@"yyyy MM dd"));
                }
                else if(b == null)
                {
                    Console.WriteLine("You have no battleships.");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Press any key...");
        }
        public int repeated(int id, int input) //return the id of the battleship which the user chose, from all his battleships
        {
            List<BattleShips> b = new List<BattleShips>();
            b = ReadBattleShips();
            List<int> IDs = new List<int>();
            foreach (BattleShips el in b)
            {
                if (el.uid == id)
                {
                    Console.WriteLine("id: {0} User id: {1} Battleship name: {2}, Battleship type {3}, AttackPoints: {4}, Defence(Shield) points {5}, Health points {6}, Date created: {7}",
                        el.id.ToString(), el.uid.ToString(), el.name, el.type, el.attack.ToString(), el.defence.ToString(), el.health.ToString(), el.date.ToString(@"yyyy MM dd"));
                    IDs.Add(el.id);
                }
            }
            bool flag1 = false;
            do
            {
                Console.WriteLine("Enter the id of the battleship...");
                input = int.Parse(Console.ReadLine());
                foreach (int el in IDs)
                {
                    if (el == input)
                    {
                        flag1 = true;
                        break;
                    }
                    else
                    {
                        flag1 = false;
                    }
                }
            }
            while (flag1 == false);
            return input;
        }
        public void DeleteBattleship(int id)
        {

            bool flag = false;
            int input = 0;
            while (flag == false)
            {
                input = repeated(id, input);
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Battleship WHERE id = '" + input + "'", con))
                    {
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
                Console.WriteLine("Operation Successfull.");
                flag = true; break;
            }
        }
        public void EditBattleshipData(int userid)
        {
            bool flag = false;
            int input = 0;
            while (flag == false)
            {
                input = repeated(userid, input);
                EditFuncMenu(input);
                Console.WriteLine("Operation Successfull.");
                flag = true; break;
            }
        }
        private void EditFuncMenu(int input)
        {
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
                    case "Destroyer": flag1 = true; break;
                    case "Cruiser": flag1 = true; break;
                    case "Freighter": flag1 = true; break;
                    case "Assault": flag1 = true; break;
                    default: Console.WriteLine("Incorect input. Choose one of the battleship types."); flag1 = false; break;
                }
            }
            int ap = 0;
            Console.WriteLine("Enter Attack points.");
            ap = checkPo(ap);
            Console.WriteLine("Enter Defence points.");
            int dp = 0;
            dp = checkPo(dp);
            Console.WriteLine("Enter Health points");
            int hp = 0;
            hp = checkPo(hp);
            con.Open();
            string q = "Update BattleShip set BattleshipType = '" + info2 + "', AttackPoints = '" + ap + "', DefencePoints = '" + dp + "', HealthPoints = '" + hp + "' Where id = '" + input + "'";
            using (SqlCommand com = new SqlCommand(q, con))
            {

                object result = com.ExecuteNonQuery();
            }
            Console.WriteLine("Operation Successful!");
            Console.WriteLine("Press any key.");
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

        public bool DoesUserHaveBattleShip(int iid)
        {
            bool ret = false;
            List<BattleShips> battleShips = new List<BattleShips>();
            foreach(BattleShips el in battleShips)
            {
                if (el.uid == iid)
                {
                    ret = true; break;
                }
                else
                {
                    ret = false;
                    //break;
                }
            }
            return ret;
        }
    }
}
