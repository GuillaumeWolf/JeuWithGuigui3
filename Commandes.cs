using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace JeuWithGuigui3
{
    class Commandes
    {
        /// <summary>
        /// Commande normal fight/autre
        /// 
        /// --> commande du marchand lignes 280*
        /// </summary>

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
                if (PlayerCommande == "exit")
                {
                    Console.WriteLine("Goodbye");
                    return "ex";
                }

                //Player stats
                else if (PlayerCommande == "ps")
                {
                    Console.WriteLine("Name: {0}. Race: {1}. Classe : {2}. HP: {3}/{4}.\nClassic Damage: {5}. Magic Damage : {6}. Crit: {7}. ", p1.name, p1.RaceOfPlayer.Name, p1.COfP.ClassName, p1.HP, p1.BaseHP, p1.Damage, p1.MagicDmg, p1.ChanceCrit);
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
                    if (p1.money - m1.POCost < 0)
                    {
                        Console.WriteLine("You dont have enough gold.");
                        continue;
                    }
                    while (true)
                    {
                        Console.WriteLine("If you want to escape, it will cost you {0} gold. if you want to pay wirte \"yes\". (Sold : {1})", m1.POCost, p1.money - m1.POCost);
                        Console.Write("--> ");
                        string rep = Console.ReadLine();
                        if (rep == "yes")
                        {
                            p1.money -= m1.POCost;
                            Console.WriteLine("You escape the monster.");
                            Game.RoomCount -= 1;
                            Console.WriteLine("\n(room {0})", Game.RoomCount);
                            return PlayerCommande;
                        }
                        else
                        {
                            Console.WriteLine("Choose again.");
                            break;
                        }
                    }
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
                    Player.ChangeDamage(p1);
                }
                else if (PlayerCommande == "addpo")
                {
                    Potion.GetMaxHPPotions(20,p1);
                    Potion.GetPotions(20, p1);
                    Potion.GetPuissancePotions(20, p1);
                }
                else if (PlayerCommande == "crit")
                {
                    CritSword crit1 = new CritSword();
                    Weapon.AddWeapon(p1, crit1, null);
                    Player.ChangeDamage(p1);
                }
                else if (PlayerCommande == "maxhp")
                {
                    p1.BaseHP += 100;
                    p1.HP += 100;
                }


                //Autre
                else
                {
                    Console.WriteLine("Choose a correct commande.");
                }
            }
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
                Console.Write(" - exit");
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
                Console.Write("Weapon: {0}. ", p1.weapon2.Name);
                if (p1.weapon2.ClassicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon2.ClassicDmg); }
                if (p1.weapon2.MagicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon2.MagicDmg); }
                if (p1.weapon2.CritBool) { Console.Write("Crit: {0}. ", p1.weapon2.Crit); }
                if (p1.weapon2.PoisonBool) { Console.Write("Poison Damage. "); }
                if (p1.weapon2.FireBool) { Console.Write("Fire Damage. "); }
                Console.WriteLine();
            }
            else if (p1.weapon1 != null && p1.weapon2 == null)
            {
                Console.Write("Weapon: {0}. ", p1.weapon1.Name);
                if (p1.weapon1.ClassicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon1.ClassicDmg); }
                if (p1.weapon1.MagicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon1.MagicDmg); }
                if (p1.weapon1.CritBool) { Console.Write("Crit: {0}. ", p1.weapon1.Crit); }
                if (p1.weapon1.PoisonBool) { Console.Write("Poison Damage. "); }
                if (p1.weapon1.FireBool) { Console.Write("Fire Damage. "); }
                Console.WriteLine();
            }
            else
            {
                Console.Write("Weapon 1: {0}. ", p1.weapon1.Name);
                if (p1.weapon1.ClassicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon1.ClassicDmg); }
                if (p1.weapon1.MagicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon1.MagicDmg); }
                if (p1.weapon1.CritBool) { Console.Write("Crit: {0}. ", p1.weapon1.Crit); }
                if (p1.weapon1.PoisonBool) { Console.Write("Poison Damage. "); }
                if (p1.weapon1.FireBool) { Console.Write("Fire Damage. "); }
                Console.WriteLine();

                Console.Write("Weapon 2: {0}. ", p1.weapon2.Name);
                if (p1.weapon2.ClassicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon2.ClassicDmg); }
                if (p1.weapon2.MagicDamageBool) { Console.Write("Damage: {0}. ", p1.weapon2.MagicDmg); }
                if (p1.weapon2.CritBool) { Console.Write("Crit: {0}. ", p1.weapon2.Crit); }
                if (p1.weapon2.PoisonBool) { Console.Write("Poison Damage. "); }
                if (p1.weapon2.FireBool) { Console.Write("Fire Damage. "); }
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




        /// <summary>
        /// Commande sécial pour le marchand
        /// </summary>


        public static void CommandeMarchand(Player p1)
        {
            string[] Names = { "Alfred de Batman", "Johny la palourde", "Ibrimovic the giant", "Igor alias \"Sucabliet\"", "JOJO from the waves power", "Canarticho the duck", "Mandalou the manificient" };
            int x = RandomInt(Names.Count());
            string name = Names[x];
            Console.WriteLine("Hello, I'm {0} . I am a marchand. Here are the objects I propose. ", name);

            while (true)
            {
                WritePossMarchand(p1);
                Console.WriteLine("Would You want to buy something ?");
                Console.Write("--> ");
                string rep = Console.ReadLine();

                if (rep ==  "no")
                {
                    Console.WriteLine("Ok, I hope to see you again. Goodbye.");
                    break;
                }



            }
        }


        public static void WritePossMarchand(Player p1)
        {
            //Potions 100 % mais laquelle ? 
            int Potions = RandomInt(100);
            if(Potions < 50)
            {
                int HPCost = 10;
                Console.Write("{0} Heal Potions: {1} gold ({2} gold/unit) - ", Game.RoomCount / 2, Game.RoomCount/2 * HPCost, HPCost);
            }
            else if (Potions < 30)
            {
                int PPcost = 15;
                Console.Write("{0} Puissance Potions: {1} gold ({3} gold/unit) - ", Game.RoomCount / 4, Game.RoomCount / 4 * PPcost, PPcost);
            }
            else if (Potions < 20)
            {
                int MHPCost = 20;
                Console.Write("{0} MaxHP Potions: {1} gold ({3} gold/unit) - ", Game.RoomCount / 5, Game.RoomCount / 5 * MHPCost, MHPCost);
            }
            Console.WriteLine();


            //armor
            int ArmorWeapon = RandomInt(100);
            if (ArmorWeapon < 60)
            {
                //Fin de game propose Leechswors, Magic Sword, Crit Sword
                if (Game.RoomCount > 20)
                {
                    int LeeMagCrit = RandomInt(3);
                    switch(LeeMagCrit)
                    {
                        case 0:
                            Console.Write("Leech Sword: {0} gold.", 400);
                            break;
                    }
                }
            }
            else if (ArmorWeapon < 100)
            {

            }
        }












        static public int RandomInt(int Max)
        {
            int randomNum;
            var rand = new Random();
            randomNum = rand.Next(Max);
            return randomNum;
        }

    }
}
