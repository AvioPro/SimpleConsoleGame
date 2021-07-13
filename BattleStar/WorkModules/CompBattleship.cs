using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleStar.WorkModules
{
    class CompBattleship
    {
        public int id;
        public int UserId;
        public string name;
        public string type;
        public int ap;
        public int dp;
        public int hp;
        public DateTime date;
        public void ShowCompData()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Bot id: {0}, \n User's id for which it was created {1}, \n Bot name: {2}, \nBot Battleship type: {3}, \nAttack points: {4}, \nDefence Points: {5}, \n Health points{6}, \n Date Created {7}",
                id, UserId, name, type, ap, dp, hp, date);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
