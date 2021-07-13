using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleStar.WorkModules
{
    public class BattleCourceClass
    {
        public int id;
        public int battleid;
        public int round;
        public string pl1state;
        public string pl2state;
        public int diceresult;
        public DateTime date;

        public void ShowBattleCourceInfo()
        {
            Console.WriteLine("Battle id: {0}, Round :{1}, \n Player 1 state: {2}, \n Player 2 state {3}, \n Dice Result: {4} \n Date Created: {5}",
                battleid, round, pl1state, pl2state, diceresult, date);
        }
    }
}
