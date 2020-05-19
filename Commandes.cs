using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace JeuWithGuigui3
{
    class Commandes
    {
        public static string Commande(Player p1, Monster m1)
        {
            while (true)
            {
                Console.Write("\nWhat do you want to do ? ");
                WritePoss(p1);
                Console.Write("--> ");
                string PlayerCommande = Console.ReadLine();

                //DIFFERENTE COMMANDE   
                
                //Exit
                if (PlayerCommande == "ex")
                {
                    Console.WriteLine("Goodbye");
                    return "ex";
                }

                //Player stats
                else if (PlayerCommande == "ps")
                {
                    Console.WriteLine("Name: {0}. Race: {1}. Classe : {6}. HP: {2}/{3}.\nClassic Damage: {4}. Magic Damage : {5}. ", p1.name, p1.RaceOfPlayer.Name, p1.HP, p1.BaseHP, p1.Damage, p1.MagicDmg, p1.COfP.ClassName);
                }

                //Monster stats
                else if (PlayerCommande == "ms" && p1.InFight)
                {
                    ShowMonsterStats(m1);
                }

                //Potions
                else if (PlayerCommande == "po")
                {
                    Console.WriteLine("Potions: {0}. MaxHP+ Potions: {1}. Puissance Potions: {2}.\nYou have {3} gold.", p1.potions, p1.PotionMaxHP, p1.PuissancePotions, p1.money);

                }

                //weapons
                else if (PlayerCommande == "we")
                {
                    ShowWeapons(p1);
                }

                //armor
                else if (PlayerCommande == "ar" )
                {
                    if (p1.armor != null)
                    {
                        Console.WriteLine("Armor: {0}. Classic Resistance {1}. Magic Resistance: {2}.", p1.armor.Name, p1.armor.ClassicResistance, p1.armor.MagicResistance);
                    }
                    else
                    {
                        Console.WriteLine("You don't have armor.");
                    }
                }

                //Use potions
                else if (PlayerCommande == "up")
                {
                    bool usedPotions = Potion.UsePotions(p1);
                    if (usedPotions && p1.InFight)
                    {
                        return "";
                    }

                }

                //Enter room
                else if (PlayerCommande == "er" && !p1.InFight)
                {
                    Game.RoomCount++;
                    Console.Write("\n(room {0}). ", Game.RoomCount);
                    Room.EnterRoom(p1);
                    return "";
                }

                //Escape Room
                else if(PlayerCommande == "es" && p1.InFight && !p1.FightBoos)
                {
                    Console.WriteLine("You escape the monster.");
                    Game.RoomCount -= 1;
                    Console.WriteLine("\n(room {0})", Game.RoomCount);
                    return PlayerCommande;
                }

                //Attack
                else if (PlayerCommande == "a" && p1.InFight)
                {
                    p1.Attak(m1, p1);
                    return "";
                }

                //Cheat Commande
                else if (PlayerCommande == "lee")
                {
                    LeechSword lee1 = new LeechSword();
                    Weapon.AddWeapon(p1, lee1, null);
                }

                //Autre
                else
                {
                    Console.WriteLine("Choose a correct commande.");
                }
            }
        }


        //Possibilité 1: On laisse le joueur utiliser les potions qu'il veut
        //Possibilité 2: On le remet full hp
        public static void SpecialBossCommande(Player p1)
        {
            while (true)
            {
                /*
                Console.WriteLine("\nYou can use consomables before fighting the boss. What do you want to do ? (use potion: up - continue: c)");
                Console.Write("--> ");
                string PlayerCommande = Console.ReadLine();
                if (PlayerCommande == "c")
                {
                    Console.WriteLine("Are you sure to continue ?");
                    Console.Write("--> ");
                    string rep = Console.ReadLine();
                    if (rep == "yes")
                    {
                        return;
                    }

                }
                else if (PlayerCommande == "up")
                {
                    Potion.UsePotions(p1);
                }
                else
                {
                    Console.WriteLine("Choose a correct commande.");
                }
                */
                break;
            }
            
            int x = 2;
        }



        public static void WritePoss(Player p1)
        {
            Console.Write("(");
            if (!p1.InFight)
            {
                Console.Write("enter room: er");
            }
            if (p1.InFight)
            {
                Console.Write("attack: a");
            }
            if (p1.InFight && !p1.FightBoos)
            {
                Console.Write(" - escape: es");
            }
            if (true)
            {
                Console.Write(" - player stats: ps");
            }
            if (p1.InFight)
            {
                Console.Write(" - monster stats: ms");
            }
            if (true)
            {
                Console.Write(" - weapons: we");
            }
            if (true)
            {
                Console.Write(" - armor: ar");
            }
            if (true)
            {
                Console.Write(" - potions: po");
            }
            if (true)
            {
                Console.Write(" - use potions: up");
            }
            if (true)
            {
                Console.Write(" - exit: ex");
            }
            Console.WriteLine(")");
        }

        public static void ShowWeapons(Player p1)
        {
            if (p1.weapon1 == null && p1.weapon2 == null)
            {
                Console.WriteLine("You don't have any weapon.");
            }
            else if (p1.weapon1 == null && p1.weapon2 != null)
            {
                Console.Write("Weapon 2: {0}. Damage: {1}. Magic Damage : {2}. ", p1.weapon2.Name, p1.weapon2.Dmg, p1.weapon2.MagicDamage);
                if (p1.weapon2.PoisonDamage) { Console.Write("Poison Damage. "); }
                if (p1.weapon2.FireDamage) { Console.Write("Fire Damage. "); }
                Console.WriteLine();
            }
            else if (p1.weapon1 != null && p1.weapon2 == null)
            {
                Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}. ", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                Console.WriteLine();
            }
            else
            {
                Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}. ", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                Console.WriteLine();

                Console.Write("Weapon 2: {0}. Damage: {1}. Magic Damage : {2}. ", p1.weapon2.Name, p1.weapon2.Dmg, p1.weapon2.MagicDamage);
                if (p1.weapon2.PoisonDamage) { Console.Write("Poison Damage. "); }
                if (p1.weapon2.FireDamage) { Console.Write("Fire Damage. "); }
                Console.WriteLine();
            }
        }

        public static void ShowMonsterStats(Monster m1)
        {
            Console.Write("name: {0}. HP: {1}/{2}. Damage: {3}. ", m1.Name, m1.Vie, m1.MaxLife, m1.Dmg);
            if (m1.MagicResistance)
            {
                Console.Write("Magic resistance. ");
            }
            if (m1.FireDmg)
            {
                Console.Write("Fire damage. ");
            }
            if (m1.PoisonDmg)
            {
                Console.Write("Poison damage. ");
            }
            Console.WriteLine();
        }

    }
}
