using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleStar.WorkModules
{
    public class BattleShips
    {
        public int id;
        public int uid;
        public string name;
        public string type;
        public int attack;
        public int defence;
        public int health;
        public DateTime date;
        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ship id: {0}, \n User's id: {1},\n Name: {2}, \nType: {3},\n Attack points: {4},\n Defence(shield) points: {5},\n Health points: {6},\n Date created: {7}",
                id, uid, name, type, attack, defence, health, date);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
