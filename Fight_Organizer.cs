using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JeuWithGuigui3
{
    class Fight_Organizer
    {
        static public int tour = 0;
        static public string Fight(Player p1, Monster m1)
        {
            p1.InFight = true;
            tour = 1;
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Turn {0}.", tour);

                //Tour du joueur
                string sortie = Commandes.Commande(p1, m1);

                if (sortie == "ex")
                {
                    return sortie;
                }
                else if (sortie == "es")
                {
                    return sortie;
                }
                //Check si le monstre est mort
                bool MonsterisDead = m1.CheckDie(m1);

                if (MonsterisDead == true)
                {
                    Console.WriteLine("You win the fight. ");
                    EndOfFight(p1, m1);
                    if (m1.Name == "Golem of Armagedon")
                    {
                        return "BoosDead";
                    }
                    return "Alive";
                }

                //tour du monstre 
                Console.Write("The {0} attack you ! ", m1.Name);
                m1.Attak(p1);
                
                //check si le joueur est mort
                bool playerisDead = p1.CheckDie();
                if (playerisDead)
                {
                    Console.WriteLine("You lost...");
                    return "Dead";
                }
                //Fin du tour
                tour++;
            }
        }


        static public void EndOfFight(Player p1, Monster m1)
        {
            tour = 0;
            p1.InFight = false;
            p1.PuissancePotionsused = 0;
            Player.ChangeDamage(p1);
            if (m1.Name == "Golem of Armagedon" && m1.Vie < 500)
            {
                GolemOfArmagedon.GolemRage(m1);
            }
        }

    }
}
