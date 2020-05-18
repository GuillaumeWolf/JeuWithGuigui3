using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Money
    {
        public static void ChangeMoney(Player p1, int x)
        {
            p1.money -= x;
            if (p1.money < 0)
            {
                p1.money = 0;
            }
            Console.WriteLine("You have {0} gold.", x);
        }
    }
}
