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
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Turn {0}.", tour);

                //Tour du joueur
                bool exit = Game.CommandeFight(p1, m1);
                if (exit)
                {
                    return false;
                }
                //Check si le monstre est mort
                bool MonsterisDead = m1.CheckDie();

                if (MonsterisDead == true)
                {
                    Game.tour = 0;
                    Console.Write("You win the fight. ");
                    FightWon = true;
                    return FightWon;
                }

                //tour du monstre 
                Console.Write("The {0} attack you ! ", m1.Name);
                m1.Attak(p1);
                
                //check si le joueur est mort
                bool playerisDead = p1.CheckDie();
                if (playerisDead)
                {
                    Console.WriteLine("You lost...");
                    return FightWon;
                }
                //Fin du tour
                tour++;
            }
        }

    }
}
