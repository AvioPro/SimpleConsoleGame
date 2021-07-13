using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using BattleStar.WorkModules;
using BattleStar.Controller;


namespace BattleStar.Data
{
    class UserDataManagement
    {
        List<Player> lsp = new List<Player>();
        const string conStr = "Data Source=Desktop-fb8br93;Initial Catalog=BattleStar;Integrated Security=True";
        SqlConnection con = new SqlConnection(conStr);
        public List<Player> ReadUsers()
        {
            con.Open();
            string q = "SELECT [id], [Username], [Password], [UserRole], [DateCreated] FROM Users";
            using(SqlCommand com = new SqlCommand(q, con))
            {
                using(SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Player p = new Player()
                            {
                                id = reader.GetInt32(0),
                                username = reader.GetString(1),
                                password = reader.GetString(2),
                                role = reader.GetString(3),
                                createdate = reader.GetDateTime(4).Date
                            };
                            lsp.Add(p);
                        }
                    }
                    else
                    {
                        lsp = null;
                    }
                }
            }
            return lsp;
        }
        public int GetTableCount()
        {
            int idnum = 0;
            con.Open();
            string q = "SELECT [id], [Username], [Password], [UserRole], [DateCreated] FROM Users";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idnum++;
                        }
                    }
                    else
                    {
                        idnum = 0;
                    }
                }
            }
            return idnum;
        }
        //writes a new user.
        public void WriteUser(Player pl)
        {
          
            con.Open();
            string q = "insert into Users([id], [Username], [Password], [UserRole], [DateCreated])values(@id, @Username, @Password, @UserRole, @DateCreated)";
            using(SqlCommand com = new SqlCommand(q, con))
            {
                com.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = pl.id;
                com.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar).Value = pl.username;
                com.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = pl.password;
                com.Parameters.Add("@UserRole", System.Data.SqlDbType.NVarChar).Value = pl.role;
                com.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime).Value = pl.createdate;
                object result = com.ExecuteNonQuery();
            }
         
            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }
        public void InfoFileRead(string filepath)
        {
            //@"C:\Users\nanu\source\repos\BattleStar\BattleStar\Data\HelpInfo.txt.txt"
            StreamReader sr = new StreamReader(filepath);
            string line;
            line = sr.ReadLine();
            while(line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }
            sr.Close();
            Console.ReadLine();
        }
        public void ShowUsers1()
        {
            var obj = ReadUsers();
            foreach (Player el in obj)
            {
                Console.WriteLine("UserID:{0}, Username: {1}, User role: {2} Date Created: {3}", el.id.ToString(), el.username.ToString(), el.role.ToString(), el.createdate.ToString(@"yyyy MM dd"));
            }
            Console.WriteLine("Press any key to continue...");
        }
        public void ShowUsersToAdmin()
        {
            var obj = ReadUsers();
            foreach (Player el in obj)
            {
                Console.WriteLine("UserID:{0}, Username: {1}, Password {2}, User role: {3} Date Created: {4}", el.id.ToString(), el.username.ToString(),el.password.ToString(), el.role.ToString(), el.createdate.ToString(@"yyyy MM dd"));
            }
            Console.WriteLine("Press any key to continue...");
        }
        public int GetId(string user, string pass)
        {
            int ret = -1;
            int count = GetTableCount();
            var b = ReadUsers();
            if (b == null)
            {
                Console.WriteLine("There is no data for this user or the database is empty.");
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (string.Equals(user, b[i].username) && string.Equals(pass, b[i].password.ToString()))
                    {
                        ret = i + 1;
                        i = count - 1;
                        //break;
                    }
                    else
                    {
                        Console.WriteLine("There is no such username in the databas or your password is wrong.");
                        //break;
                    }
                }
            }
            return ret;
        }
        public void EditUser(int id, string usernamenew, string password)
        {
            con.Open();
            string q = "Update Users set Username = '"+usernamenew+ "', Password = '" + password + "' Where id = '" + id+"'";
            using (SqlCommand com = new SqlCommand(q, con))
            {
   
                object result = com.ExecuteNonQuery();
            }

            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }
        public void EditUserAsAdmin(int id, string usernamenew, string password, string role)
        {
            con.Open();
            string q = "Update Users set Username = '" + usernamenew + "', Password = '" + password + "', UserRole = '" + role + "'Where id = '" + id + "'";
            using (SqlCommand com = new SqlCommand(q, con))
            {

                object result = com.ExecuteNonQuery();
            }

            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }
    }
   
}
