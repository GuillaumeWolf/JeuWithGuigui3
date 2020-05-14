using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Fight_Organizer
    {
        static public bool Fight(Player p1, Monster m1)
        {
            bool FightWon = false;
            int tour = Game.tour = 1;
            if (m1.MagicResistance)
            {
                p1.MagicDmg /= 2;
                p1.EnemyMagicRes = true;
            }
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Turn {0}.", tour);
                bool exit = Game.CommandeFight(p1, m1);
                if (exit)
                {
                    return false;
                }
                //Joueur attaque
                if (p1.Poison)
                {
                    int y = 5 * tour;
                    m1.Vie -= y;
                    Console.WriteLine("The {0} is poisonned! He has {1} HP. ", m1.Name, m1.Vie);
                }
                if (p1.Fire)
                {
                    m1.Vie -= 10;
                    Console.WriteLine("The {0} is on fire! He has {1} HP.", m1.Name, m1.Vie);
                }
                bool isDead = m1.CheckDie();

                //Check si le monstre est mort
                if (isDead == true)
                {
                    tour = 0;
                    Console.Write("You win the fight. ");
                    FightWon = true;
                    return FightWon;
                }

                //monstre attaque
                Console.Write("The {0} attack you ! ", m1.Name);
                m1.Attak(p1);
                if (m1.PoisonDmg)
                {
                    int poisonDmg = 3 * (tour);
                    p1.HP -= poisonDmg;
                    Console.WriteLine("You're poisonned! You have {0} HP.", p1.HP);
                }
                if (m1.FireDmg)
                {
                    p1.HP -= 10;
                    Console.WriteLine("You're on fire! You have {0} HP.", p1.HP);
                }
                //check si le joueur est mort
                bool playerisDead = p1.CheckDie();
                if (playerisDead)
                {
                    Console.WriteLine("You lost...");
                    return FightWon;
                }
                tour++;
            }
        }

    }
}
