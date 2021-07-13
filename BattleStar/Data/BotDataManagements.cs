using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BattleStar.WorkModules;

namespace BattleStar.Data
{
    class BotDataManagements
    {
        const string conStr = "Data Source=Desktop-fb8br93;Initial Catalog=BattleStar;Integrated Security=True";
        List<CompBattleship> botData = new List<CompBattleship>();
        SqlConnection con = new SqlConnection(conStr);

        public List<CompBattleship> ReadBots()
        {
            con.Open();
            string q = "SELECT[id], [UserId], [BotName], [BotType],[AttackPoints],[DefencePoints],[HealthPoints], [DateCreated] FROM Bots";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            CompBattleship b = new CompBattleship()
                            {
                                id = reader.GetInt32(0),
                                UserId = reader.GetInt32(1),
                                name = reader.GetString(2),
                                type = reader.GetString(3),
                                ap = reader.GetInt32(4),
                                dp = reader.GetInt32(5),
                                hp = reader.GetInt32(6),
                                date = reader.GetDateTime(7)
                            };
                            botData.Add(b);
                        }
                    }
                    else
                    {
                        botData = null;
                    }
                }
                return botData;
            }
        }
        public void AddBot(CompBattleship b)
        {
            con.Open();
            string q = " insert into Bots([id], [UserId], [BotName], [BotType],[AttackPoints],[DefencePoints],[HealthPoints], [DateCreated])values(@id, @UserId, @BotName, @BotType, @AtackPoints, @DefencePoints, @HealthPoints,@DateCreated)";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                com.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = b.id;
                com.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = b.UserId;
                com.Parameters.Add("@BotName", System.Data.SqlDbType.NVarChar).Value = b.name;
                com.Parameters.Add("@BotType", System.Data.SqlDbType.NVarChar).Value = b.type;
                com.Parameters.Add("@AtackPoints", System.Data.SqlDbType.Int).Value = b.ap;
                com.Parameters.Add("@DefencePoints", System.Data.SqlDbType.Int).Value = b.dp;
                com.Parameters.Add("@HealthPoints", System.Data.SqlDbType.Int).Value = b.hp;
                com.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime).Value = b.date;
                object result = com.ExecuteNonQuery();
            }
            Console.WriteLine("Connection Successful!");
            Console.WriteLine("Press any key.");
        }

        public int GetCounts()
        {
            int num = 0;
            con.Open();
            string q = "SELECT [id] FROM Bots";
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
    }
}
