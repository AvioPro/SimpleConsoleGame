using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BattleStar.WorkModules
{
    public class BattleResult
    {
        public int id;
        public int uid;
        public string winner;
        public string loser;
        public DateTime date;

        public void ShowBattleResult()
        {
            Console.WriteLine("Battle id: {0}, \n User's id: {1}, \n Winner: {2}, \n Loser: {3}, \n Date created: {4}", id, uid, winner, loser, date);
        }
        public void ShowBattleResult1()
        {
            Console.WriteLine("Battle id: {0}, \n User's id: {1},  Date created: {2}", id, uid, date);
        }
    }

}
