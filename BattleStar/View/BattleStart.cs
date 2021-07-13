using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleStar.Controller;
using BattleStar.Data;
using BattleStar.WorkModules;

namespace BattleStar.View
{
    public class BattleStart
    {
        BattleshipDataManagement bdm = new BattleshipDataManagement();
        BattleShips b = new BattleShips();
        List<BattleShips> bs = new List<BattleShips>();
        public void StartBattle(int id)
        {
            Random rand = new Random();
            bs = bdm.ReadBattleShips();
            int input = 0;
            input = bdm.repeated(id, input);
            foreach(BattleShips el in bs)
            {
                if(el.id == input)
                {
                    b = el;
                }
            }
            Console.Clear();
            b.Show();
            CompBattleship Computer = new CompBattleship();
            Computer = CompShip(b, id);
            Console.WriteLine();
            int dice = rand.Next(1, 6);
            ActualFighting(b, Computer, dice, id);
        }
        private CompBattleship CompShip(BattleShips b, int uid)
        {
            string usrbtype = b.type;
            int usrap = b.attack;
            int usrdef = b.defence;
            int usrhp = b.health;
            Random rand = new Random();
            //1 -5
            int randNum4Type = rand.Next(1, 5);
            string compType = "";
            switch (randNum4Type)
            {
                case 1: compType = "Capital"; break;
                case 2: compType = "Destroyer"; break;
                case 3: compType = "Cruiser"; break;
                case 4: compType = "Freighter"; break;
                case 5: compType = "Assault"; break;
                default: compType = "Cruiser"; break;
            }
            int num = rand.Next(65, 90);
            char ch = (char)num;
            string compName = "CompBot" + ch.ToString() + rand.Next(1, 100).ToString();
            int compAP = rand.Next((usrap - 5), (usrap + 1));
            int compDP = rand.Next((usrdef - 5), (usrdef + 5));
            int compHP = rand.Next((usrhp - 5), (usrhp + 5));
            int id = 0;
            BotDataManagements bdm = new BotDataManagements();
            List<CompBattleship> bot = new List<CompBattleship>();
            bot = bdm.ReadBots();
            int num1 = 0;
            num1 = bdm.GetCounts();
            if(bot == null)
            {
                id = 1;
            }
            else
            {
                id = num1 + 1;
            }
            CompBattleship compShip = new CompBattleship()
            {
                id = id,
                UserId = uid,
                name = compName,
                type = compType,
                ap = compAP,
                dp = compDP,
                hp = compHP,
                date = DateTime.Now
            };
            bdm.AddBot(compShip);
            compShip.ShowCompData();
            return compShip;
        }
        private int DiceResultChanges(int dice, int val)
        {
            switch (dice)
            {
                case 1: val += 1; break;
                case 2: val += (((val / 20) * 100)); break;
                case 3: val += (((val / 40) * 100)); break;
                case 4: val += (((val / 60) * 100)); break;
                case 5: val += (((val / 80) * 100)); break;
                case 6: val += val; break;
                default: break;
            }
            return val;
        }
        private void ActualFighting(BattleShips bs, CompBattleship cbs, int dice, int id)
        {
            int battlecourceid = 0;
            string winner = "";
            string loser = "";
            int battleId = 0;
            string pl1state = "";
            string pl2state = "";
            BattleResult batres;
            BattleResultManagement brm = new BattleResultManagement();
            if (brm.ReadBattleResults() == null)
            {
                battleId = 1;
            }
            else
            {
                battleId = brm.getCount() + 1;
            }
            BattleCourceClass bcc;
            BattleCourceManagement bcm = new BattleCourceManagement();
            if (bcm.ReadBattleCourceInfo() == null)
            {
                battlecourceid = 1;
            }
            else
            {
                battlecourceid = bcm.GetCount() + 1;
            }
            int Round = 0;
            int uap = 0; uap = bs.attack + DiceResultChanges(dice, bs.attack); //user attack points
            int udp = 0; udp = bs.defence;//user shield points
            int uhp = 0; uhp = bs.health;//user health points
            int eap = 0; eap = cbs.ap + DiceResultChanges(dice, cbs.ap); //enemy attack points
            int edp = 0; edp = cbs.dp;//enemy shield points
            int ehp = 0; ehp = cbs.hp;//enemy health points
            bool end = false;
            while (end == false)
            {
                if (end == true) break;
                Round++;
                if(edp > 0)
                {
                    edp -= uap;
                    if(edp < 0)
                    {
                        ehp += edp;
                    }
                }
                else
                {
                    ehp -= uap;
                }
                if (ehp < 0)
                {
                    end = true;
                }
                if (udp > 0)
                {
                    udp -= eap;
                    if(udp < 0)
                    {
                        uhp += udp;
                    }
                }
                else
                {
                    //executescalar
                    uhp -= eap;
                }
                if(uhp < 0)
                {
                    end = true;
                }
                if(uhp > ehp)
                {
                    pl1state = "Ship: "+bs.name+"Shield: "+udp+" Health: "+uhp+" ahead";
                    pl2state = "Ship: "+cbs.name+"Shield: "+edp+" Health: "+ehp+" back";
                    winner = bs.name;
                    loser = cbs.name;
                }
                else
                {
                    pl1state = "Ship: " + bs.name + "Shield: " + udp + " Health: " + uhp + " back";
                    pl2state = "Ship: " + cbs.name + "Shield: " + edp + " Health: " + ehp + " ahead";
                    loser = bs.name;
                    winner = cbs.name;
                }
                bcc = new BattleCourceClass()
                {
                    id = battlecourceid,
                    battleid = battleId,
                    round = Round,
                    pl1state = pl1state,
                    pl2state = pl2state,
                    diceresult = dice,
                    date = DateTime.Now
                };
                bcm.WriteBCInfo(bcc);
                battlecourceid = bcm.GetCount() + 1;
                Console.WriteLine("Round {0} saved. User specs are: {1} defence, {2} health, Computer specs are {3} defence, {4} health", Round, udp, uhp, edp, ehp);
            }
            batres = new BattleResult()
            {
                id = battleId,
                uid = id,
                winner = winner,
                loser = loser,
                date = DateTime.Now
            };
            brm.WriteBattleResult(batres);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("BATTLE OVER \n Winner: {0}, \n Loser: {1}", winner, loser);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }
    }
}
