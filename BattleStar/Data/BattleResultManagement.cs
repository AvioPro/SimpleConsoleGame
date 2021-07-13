using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BattleStar.WorkModules;

namespace BattleStar.Data
{
    class BattleResultManagement
    {
        const string conStr = "Data Source=Desktop-fb8br93;Initial Catalog=BattleStar;Integrated Security=True";
        List<BattleResult> botData = new List<BattleResult>();
        SqlConnection con = new SqlConnection(conStr);

        public List<BattleResult> ReadBattleResults()
        {
            List<BattleResult> br = new List<BattleResult>();
            con.Open();
            string q = "SELECT [id], [UserId], [Winner], [Loser], [CreateDate] from BattleResult";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleResult b = new BattleResult()
                            {
                                id = reader.GetInt32(0),
                                uid = reader.GetInt32(1),
                                winner = reader.GetString(2),
                                loser = reader.GetString(3),
                                date = reader.GetDateTime(4)
                            };
                            br.Add(b);
                        }
                    }
                    else
                    {
                        br = null;
                    }
                }
            }
            return br;
        }
        public void WriteBattleResult(BattleResult b)
        {
            con.Open();
            string q = " insert into BattleResult([id], [UserId], [Winner], [Loser], [CreateDate])values(@id, @UserId, @Winner, @Loser, @CreateDate)";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                com.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = b.id;
                com.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = b.uid;
                com.Parameters.Add("@Winner", System.Data.SqlDbType.VarChar).Value = b.winner;
                com.Parameters.Add("@Loser", System.Data.SqlDbType.VarChar).Value = b.loser;
                com.Parameters.Add("@CreateDate", System.Data.SqlDbType.DateTime).Value = b.date;
                object result = com.ExecuteNonQuery();
            }
            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }
        public int getCount()
        {
            int num = 0;
            con.Open();
            string q = "SELECT [id] FROM BattleResult";
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
        public void ShowBattles1(int userid)
        {
            con.Open();
            string q = "SELECT [id], [UserId], [Winner], [Loser], [CreateDate] from BattleResult where UserId = "+userid+"";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleResult b = new BattleResult()
                            {
                                id = reader.GetInt32(0),
                                uid = reader.GetInt32(1),
                                winner = reader.GetString(2),
                                loser = reader.GetString(3),
                                date = reader.GetDateTime(4)
                            };
                            b.ShowBattleResult1();
                        }
                    }
                }
            }
        }
        public List<int> UserBattlesIds(int userid)
        {
            List<int> ids = new List<int>();
            con.Open();
            string q = "SELECT [id], [UserId], [Winner], [Loser], [CreateDate] from BattleResult where UserId = " + userid + "";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleResult b = new BattleResult()
                            {
                                id = reader.GetInt32(0),
                                uid = reader.GetInt32(1),
                                winner = reader.GetString(2),
                                loser = reader.GetString(3),
                                date = reader.GetDateTime(4)
                            };
                            ids.Add(b.id);
                        }
                    }
                }
            }
            return ids;
        }
        public void ShowResultsById(int battleid)
        {
            con.Open();
            string q = "SELECT [id], [UserId], [Winner], [Loser], [CreateDate] from BattleResult where id = " + battleid + "";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleResult b = new BattleResult()
                            {
                                id = reader.GetInt32(0),
                                uid = reader.GetInt32(1),
                                winner = reader.GetString(2),
                                loser = reader.GetString(3),
                                date = reader.GetDateTime(4)
                            };
                            b.ShowBattleResult();
                        }
                    }
                }
            }
        }
    }
}
