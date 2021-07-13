using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BattleStar.WorkModules;

namespace BattleStar.Data
{
    class BattleCourceManagement
    {
        const string conStr = "Data Source=Desktop-fb8br93;Initial Catalog=BattleStar;Integrated Security=True";
        List<BattleCourceClass> botData = new List<BattleCourceClass>();
        SqlConnection con = new SqlConnection(conStr);

        public List<BattleCourceClass> ReadBattleCourceInfo()
        {
            List<BattleCourceClass> bccm = new List<BattleCourceClass>();
            con.Open();
            string q = "Select [id], [BattleId], [Round], [Player1State], [Player2State], [DiceResult], [DateCreated] from BattleCource";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleCourceClass bcc = new BattleCourceClass()
                            {
                                id = reader.GetInt32(0),
                                battleid = reader.GetInt32(1),
                                round = reader.GetInt32(2),
                                pl1state = reader.GetString(3),
                                pl2state = reader.GetString(4),
                                diceresult = reader.GetInt32(5),
                                date = reader.GetDateTime(6)
                            };
                            bccm.Add(bcc);
                        }
                    }
                    else
                    {
                        bccm = null;
                    }
                }
            }
            return bccm;
        }
        public void WriteBCInfo(BattleCourceClass bcc)
        {
            con.Open();
            string q = " insert into BattleCource([id], [BattleId], [Round], [Player1State], [Player2State], [DiceResult], [DateCreated])values(@id, @BattleId, @Round, @Player1State, @Player2State, @DiceResult, @DateCreated)";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                com.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = bcc.id;
                com.Parameters.Add("@BattleId", System.Data.SqlDbType.Int).Value = bcc.battleid;
                com.Parameters.Add("@Round", System.Data.SqlDbType.Int).Value = bcc.round;
                com.Parameters.Add("@Player1State", System.Data.SqlDbType.VarChar).Value = bcc.pl1state;
                com.Parameters.Add("@Player2State", System.Data.SqlDbType.VarChar).Value = bcc.pl2state;
                com.Parameters.Add("@DiceResult", System.Data.SqlDbType.Int).Value = bcc.diceresult;
                com.Parameters.Add("@DateCreated", System.Data.SqlDbType.DateTime).Value = bcc.date;
                object result = com.ExecuteNonQuery();
            }
            con.Close();
        }

        public void ReadBattleById(int battleid)
        {
            con.Open();
            string q = "Select [id], [BattleId], [Round], [Player1State], [Player2State], [DiceResult], [DateCreated] from BattleCource where BattleId = "+battleid+" ";
            using (SqlCommand com = new SqlCommand(q, con))
            {
                using (SqlDataReader reader = com.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BattleCourceClass bcc = new BattleCourceClass()
                            {
                                id = reader.GetInt32(0),
                                battleid = reader.GetInt32(1),
                                round = reader.GetInt32(2),
                                pl1state = reader.GetString(3),
                                pl2state = reader.GetString(4),
                                diceresult = reader.GetInt32(5),
                                date = reader.GetDateTime(6)
                            };
                            bcc.ShowBattleCourceInfo();
                        }
                    }
                }
            }
        }

        public int GetCount()
        {
            int num = 0;
            con.Open();
            string q = "SELECT [id] FROM BattleCource";
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
