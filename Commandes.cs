using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                else if (PlayerCommande == "ar")
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
                else if (PlayerCommande == "es" && p1.InFight && !p1.FightBoos)
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
                    Potion.GetMaxHPPotions(20, p1);
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


    }




    class CommandeMarchand
    { 
        //Object dans le shop on non
        public static bool hppotion = false;
        public static bool mhppotion = false;
        public static bool pppotion = false;
        public static bool smallarmor = false;
        public static bool mediumarmor = false;
        public static bool bigarmor = false;
        public static bool dague = false;
        public static bool sword = false;
        public static bool magicwand = false;
        public static bool magicsword = false;
        public static bool leechsword = false;
        public static bool critsword = false;

        //Arrays
        public static bool[] boolobject = { hppotion, mhppotion, pppotion, smallarmor, mediumarmor, bigarmor, dague, sword, magicwand, magicsword, leechsword, critsword };
        public static int[] intobject = new int[boolobject.Count()];

        public static void Commande(Player p1)
        {
            //Nom
            string[] Names = { "Alfred de Batman", "Johny la palourde", "Ibrimovic the giant", "Igor alias \"Sucabliet\"", "JOJO from the waves power", "Canarticho the duck", "Mandalou the manificient" };
            int x = RandomInt(Names.Count());
            string name = Names[x];
            Console.WriteLine("Hello, I'm {0} . I am a marchand. Here are the objects I propose. ", name);

            //Objet dans le shop
            MakeMarchandObject(p1); //Crée les idfférents objets qui vont apparaitre
            Triobject(); // Fait une array pour les numero d'article
            ProposeObjects(); //Montre les articles
            Console.WriteLine("Would you want to buy something ?");
            bool lool = false;
            while (true)
            {
                if (lool )
                {
                    Console.WriteLine("Would you want to buy something else ?");
                }
                Console.Write("--> ");
                string rep = Console.ReadLine();

                if (rep ==  "no")
                {
                    Console.WriteLine("Ok, I hope to see you again. Goodbye.");
                    break;
                }
                else if (rep == "yes")
                {

                    lool = true;
                }
                else
                {
                    Console.WriteLine("What do you want ? I didn't understand your request...");
                    lool = false;
                }



            }
            hppotion = false;
            mhppotion = false;
            pppotion = false;
            smallarmor = false;
            mediumarmor = false;
            bigarmor = false;
            dague = false;
            sword = false;
            magicwand = false;
            magicsword = false;
            leechsword = false;
            critsword = false;

    }


        public static void MakeMarchandObject(Player p1)
        {
            //Potion
            int Potions = RandomInt(100);
            int numpotionexpos = 0;
            if(Potions < 50)
            {
                hppotion = true;
            }
            if (Potions < 80)
            {
                pppotion = true;
            }
            if (Potions < 100 && numpotionexpos < 2)
            {
                mhppotion = true; 
            }
            Console.WriteLine();


            //Armor
            //Début de game
            int x = Game.RoomCount;
            if (x <= 11)
            {
                if (p1.armor != null && p1.armor.Name == "Small Armor")
                {
                    mediumarmor = true;
                }
                else
                {
                    smallarmor = true;
                }
            }
            //Millieu de la Game
            else if (x <= 21)
            {
                if (p1.armor != null && p1.armor.Name == "Medium Armor")
                {
                    bigarmor = true;
                }
                else 
                {
                    mediumarmor = true;
                }
            }
            //Fin de Game
            else if (x <= 31)
            {
                bigarmor = true;
            }

        }

        public static void Triobject()
        {
            int x = 1;
            for (int i = 0; i < boolobject.Count(); i++)
            {
                if (boolobject[i])
                {
                    intobject[i] = x;
                    x++;
                }
            }
        }


        public static void ProposeObjects ()
        {
            //Potions
            if (true)
            {
                Console.WriteLine("Potions:");
            }
            if (hppotion)
            {
                int number = Game.RoomCount / 2+1;
                int cost = 10;
                Console.WriteLine(" Article {0}: {1} Heal Potions: {2} gold ({3} gold/unit) ", intobject[0], number, number * cost, cost);
            }
            if (mhppotion)
            {
                int number = Game.RoomCount / 5 + 1;
                int cost = 15;
                Console.WriteLine(" Article {0}: {1} MaxHP Potions: {2} gold ({3} gold/unit)", intobject[1], Game.RoomCount / 5, Game.RoomCount / 5 * cost, cost);
            }
            if (pppotion)
            {
                int number = Game.RoomCount / 4 + 1;
                int cost = 15;  
                Console.WriteLine(" Article {0}: {1} MaxHP Potions: {2} gold ({3} gold/unit)", intobject[2], Game.RoomCount / 5, Game.RoomCount / 5 * cost, cost);
            }
            Console.WriteLine();

            //Armor
            if (true)
            {
                Console.WriteLine("Armor:");
            }
            if (smallarmor)
            {
                int cost = 30;
                Console.WriteLine(" Article {0}: Small Armor: {1} gold ", intobject[3], cost);
            }
            if (mediumarmor)
            {
                int cost = 100;
                Console.WriteLine(" Article {0}: Medium Armor: {1} gold ", intobject[4], cost);
            }
            if (bigarmor)
            {
                int cost = 300;
                Console.WriteLine(" Article {0}: Big Armor: {1} gold ", intobject[5], cost);
            }
            Console.WriteLine();

            //Weapons
            if (true)
            {
                Console.WriteLine("Weapons:");
            }
            if (dague)
            {
                int cost = 30;
                Console.WriteLine(" Article {0}: Dague: {1} gold ", intobject[6], cost);
            }
            if (sword)
            {
                int cost = 100;
                Console.WriteLine(" Article {0}: Sword: {1} gold ", intobject[7], cost);
            }
            if (magicwand)
            {
                int cost = 120;
                Console.WriteLine(" Article {0}: Magic Wand: {1} gold ", intobject[8], cost);
            }
            if (magicsword)
            {
                int cost = 250;
                Console.WriteLine(" Article {0}: Magic Sword: {1} gold ", intobject[9], cost);
            }
            if (leechsword)
            {
                int cost = 400;
                Console.WriteLine(" Article {0}: Leech sword: {1} gold ", intobject[10], cost);
            }
            if (critsword)
            {
                int cost = 400;
                Console.WriteLine(" Article {0}: Crit Sword: {1} gold ", intobject[11], cost);
            }
           
            Console.WriteLine();



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
