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
        /// Commande normal fight/autre ligne 18*
        /// 
        /// --> commande du marchand lignes 310*
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
                
                //Use stones
                else if ( PlayerCommande == "us" && !p1.InFight && p1.EnchantStone > 0 && (p1.weapon1 != null || p1.weapon2 != null))
                {
                    bool usedStone = PlayerObjects.UseBlancEnchantStone(p1);
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
                    p1.EnchantStone += 10000;
                    p1.money = 100000;
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
                else if (PlayerCommande == "q" && p1.InFight)
                { m1.Vie = 0; return ""; }
                //else if(PlayerCommande == )


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
            if (!p1.InFight && p1.EnchantStone > 0 && (p1.weapon1 != null || p1.weapon2 != null))
            {
                Console.Write(" - use stones: us");
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
        #region public static propreties
        //Class object on shop or not
        public static int ShopPotions = 0;
        public static int ShopArmor = 0;
        public static int ShopWeapon = 0;
        public static int ShopSpecial = 0;

        //Object dans le shop on non
        //potions 0-2
        public static bool hppotion = false;
        public static bool mhppotion = false;
        public static bool pppotion = false;
            //armures 3-5
        public static bool smallarmor = false;
        public static bool mediumarmor = false;
        public static bool bigarmor = false;
            //armes 6-12
        public static bool dague = false;
        public static bool sword = false;
        public static bool magicwand = false;
        public static bool magicsword = false;
        public static bool lépéecalice= false; 
        public static bool leechsword = false;
        public static bool critsword = false;
        //Spécial
        public static bool pierredanchentement = false;
        public static bool pierredanchentement2 = false;

        //Très Special 
        public static bool legendarysword = false;


        //Arrays
        public static bool[] boolobject = { hppotion, mhppotion, pppotion, smallarmor, mediumarmor, bigarmor, dague, sword, magicwand, magicsword, lépéecalice, leechsword, critsword, pierredanchentement, pierredanchentement2 };
        public static int[] intobject = new int[boolobject.Count()];
        #endregion


        public static void Commande(Player p1)
        {
            //Nom
            string[] Names = { "Alfred Bateater", "Johny la palourde", "Ibrimovic the giant", "Igor alias \"Sucabliet\"", "JOJO alias ORAORAORAORAAAAAAA!!!!!!", "Canarticho the duck", "Mandalou the Manificient" };
            int x = RandomInt(Names.Count());
            string name = Names[x];
            Console.WriteLine("Hello, I'm {0}. I am a marchand. Here are the objects I propose. ", name);

            //Objet dans le shop
            MakeMarchandObject(p1); //Crée les idfférents objets qui vont apparaitre
            refreshPrice(p1);
            refreshArray();
            TriObject();
            ProposeObjects(p1); //Montre les articles
            //Achat des articles
            bool lool = false;
            while (true)
            {
                if (lool )
                {
                    Console.WriteLine("Would you want to buy something else ? (yes or no)");
                }
                else
                {
                    Console.WriteLine("Would you want to buy something ? (yes or no)");
                }
                Console.Write("--> ");
                string rep1 = Console.ReadLine();
                
                //Très très moche:
                if (rep1 ==  "no")
                {
                    if (legendarysword && p1.money > 200)
                    {
                        Console.WriteLine("Wait ! I have maybe something that will interest you. Want to check ? (yes or no)");
                        while (true)
                        {
                            Console.Write(" --> ");
                            string rep3 = Console.ReadLine();
                            if (rep3 == "yes")
                            {
                                while (true)
                                {
                                    Console.WriteLine("Would you like to buy this beautiful Legendary Sword for all your money ? (yes or no)");
                                    while (true)
                                    {
                                        Console.Write(" --> ");
                                        string rep4 = Console.ReadLine();
                                        if (rep4 == "yes")
                                        {
                                            p1.money = 0;
                                            Weapon.AddWeapon(p1, new LegendarySword(), null);
                                            break; ;
                                        }
                                        else if (rep4 == "no")
                                        {
                                            Console.WriteLine("WAT ? Go away !");
                                            return;
                                        }
                                        else
                                        {
                                            Console.WriteLine("yes or no ??");
                                            continue;
                                        }
                                    }
                                    break;
                                }
                                break;
                            }
                            else if (rep3 == "no")
                            {
                                Console.WriteLine("Ok. You wast a good weapon...");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("yes or no ??");
                                continue;
                            }
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ok, I hope to see you again. Goodbye.");
                        break;
                    }
                    
                }
                else if (rep1 == "yes")
                {
                    while (true)
                    {
                        Console.WriteLine("Which article do you want ? (enter the number)");
                        Console.Write(" -->");
                        string rep2 = Console.ReadLine();
                        int sortie = CheckRep2(rep2);
                        if (sortie == 0)
                        {
                            continue;
                        }
                        else
                        {
                            int indexx = Array.IndexOf(intobject, sortie);
                            BuyObject(indexx, p1);
                            Console.WriteLine("\n");
                            break;
                        }
                    }
                    refreshPrice(p1);
                    ProposeObjects(p1);
                    lool = true;
                    //Shop vide:
                    if (!intobject.Contains(1))
                    {
                        Console.WriteLine("Hei, you buy all my stuf... I hope that I never see you again...");
                        break;
                    }
                    continue;
                }
                else
                {
                    Console.Write("I didn't understand your request...");
                    lool = false;
                    continue;
                }

                break;

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
            int y = RandomInt(100);
            int numpotionexpos = 0;
            if (y < 100)
            {
                hppotion = true;
                numpotionexpos++;
                ShopPotions++;
            }
            if (y < 50)
            {
                pppotion = true;
                numpotionexpos++;
                ShopPotions++;
            }
            if (y < 100 && numpotionexpos < 2)
            {
                mhppotion = true;
                numpotionexpos++;
                ShopPotions++;
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
                    ShopArmor++;
                }
                else
                {
                    smallarmor = true;
                    ShopArmor++;
                }

            }
            //Millieu de la Game
            else if (x <= 21)
            {
                if (p1.armor != null && p1.armor.Name == "Medium Armor")
                {
                    bigarmor = true;
                    ShopArmor++;
                }
                else
                {
                    mediumarmor = true;
                    ShopArmor++;
                }
            }
            //Fin de Game
            else if (x <= 31)
            {
                bigarmor = true;
                ShopArmor++;
            }

            int z = RandomInt(1000);

            //Weapon
            if (x <= 11)
            {
                dague = true;
                ShopWeapon++;
                sword = true;
                ShopWeapon++;
                magicwand = true;
                ShopWeapon++;
                if (z < 1)
                {
                    legendarysword = true;
                    
                }

            }
            //Millieu de la Game
            else if (x <= 21)
            {
                magicsword = true;
                ShopWeapon++;
                if (z < 10)
                {
                    legendarysword = true;
                    
                }
            }
            //Fin de Game
            else if (x <= 31)
            {
                critsword = true;
                ShopWeapon++;
                lépéecalice = true;
                ShopWeapon++;
                leechsword = true;
                ShopWeapon++;
                if (z < 100)
                {
                    legendarysword = true;
                    
                }
            }

            //Special
            if (x <= 11)
            {
                pierredanchentement = true;
                ShopSpecial++;
            }
            else if (x <= 31)
            {
                pierredanchentement2 = true;
                ShopSpecial++;
            }

        }

        public static void ProposeObjects (Player p1)
        {
            //Potions
            if (ShopPotions > 0)
            {
                Console.WriteLine("Potions:");
            }
            if (hppotion)
            {
                int number = Game.RoomCount / 2 + 1;
                int cost = 10;
                if (number * cost > p1.money)
                {
                    hppotion = false;
                    ShopPotions--;
                }
                else
                {
                    Console.WriteLine("Article {0}: {1} Heal Potions: {2} gold ({3} gold/unit) ", intobject[0], number, number * cost, cost);
                }
            }
            if (mhppotion)
            {
                int number = Game.RoomCount / 5 + 1;
                int cost = 15;
                if (number * cost > p1.money)
                {
                    mhppotion = false;
                    ShopPotions--;
                }
                else
                {
                    Console.WriteLine("Article {0}: {1} MaxHP Potions: {2} gold ({3} gold/unit)", intobject[1], number, number * cost, cost);
                }
            }
            if (pppotion)
            {
                int number = Game.RoomCount / 4 + 1;
                int cost = 15;
                if (number * cost > p1.money)
                {
                    pppotion = false;
                    ShopPotions--;
                }
                else
                {
                    Console.WriteLine("Article {0}: {1} MaxHP Potions: {2} gold ({3} gold/unit)", intobject[2], number, number * cost, cost);
                }
            }
            Console.WriteLine();
            //Console.WriteLine("ShopPotion: {0}.", ShopPotions);

            //Armor
            if (ShopArmor > 0)
            {
                Console.WriteLine("Armor:");
            }
            if (smallarmor)
            {
                int cost = 30;
                if (cost > p1.money)
                {
                    smallarmor = false;
                    ShopArmor--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Small Armor: {1} gold ", intobject[3], cost);
                }
            }
            if (mediumarmor)
            {
                int cost = 100;
                if (cost > p1.money)
                {
                    mediumarmor = false;
                    ShopArmor--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Medium Armor: {1} gold ", intobject[4], cost);
                }
            }
            if (bigarmor)
            {
                int cost = 300;
                if (cost > p1.money)
                {
                    bigarmor = false;
                    ShopArmor--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Big Armor: {1} gold ", intobject[5], cost);
                }
            }
            Console.WriteLine();
            //Console.WriteLine("ShopArmor: {0}.", ShopArmor);

            //Weapons
            if (ShopWeapon > 0)
            {
                Console.WriteLine("Weapons:");
            }
            if (dague)
            {
                int cost = 30;
                if (cost > p1.money)
                {
                    dague = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Dague: {1} gold ", intobject[6], cost);
                }
            }
            if (sword)
            {
                int cost = 100;
                if (cost > p1.money)
                {
                    sword = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Sword: {1} gold ", intobject[7], cost);
                }
            }
            if (magicwand)
            {
                int cost = 120;
                if (cost > p1.money)
                {
                    magicwand = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Magic Wand: {1} gold ", intobject[8], cost);
                }
            }
            if (magicsword)
            {
                int cost = 250;
                if (cost > p1.money)
                {
                    magicsword = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine("Article {0}: Magic Sword: {1} gold ", intobject[9], cost);
                }
            }
            if (lépéecalice)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    lépéecalice = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine("Article {0}: L'épée Calice: {1} gold ", intobject[10], cost);
                }
            }
            if (leechsword)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    leechsword = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine(" Article {0}: Leech sword: {1} gold ", intobject[11], cost);
                }
            }
            if (critsword)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    critsword = false;
                    ShopWeapon--;
                }
                else
                {
                    Console.WriteLine(" Article {0}: Crit Sword: {1} gold ", intobject[12], cost);
                }
            }
            Console.WriteLine();
            //Console.WriteLine("ShopWeapon: {0}.", ShopWeapon);

            //Special
            if(ShopSpecial > 0)
            {
                Console.WriteLine("Spécial:");
            }
            if (pierredanchentement)
            {
                int cost = 200;
                int number = Game.RoomCount / 10 + 1;
                if (cost > p1.money)
                {
                    pierredanchentement = false;
                    ShopSpecial--;
                }
                else
                {
                    Console.WriteLine("Article {0}: {1} enchantment stone: {2} gold ({3} gold/unit) (One stone give + 15 damages to a weapon.)", intobject[13], number, cost*number, cost);
                }
            }
            if(pierredanchentement2)
            {
                int cost = 200;
                int number = Game.RoomCount / 10 + 1;
                if (cost > p1.money)
                {
                    pierredanchentement = false;
                    ShopSpecial--;
                }
                else
                {
                    Console.WriteLine("Article {0}: {1} enchantment stone blue: {2} gold ({3} gold/unit) (One stone give + 30 damages to a weapon.)", intobject[14], number, cost * number, cost);
                }
            }
            Console.WriteLine();

        }

        public static void TriObject()
        {
            int x = 0;
            Console.Write("Array: ");
            for (int i = 0; i < boolobject.Count(); i++)
            {
                if (boolobject[i])
                {
                    x++;
                }
                intobject[i] = x;
                Console.Write(x + " ");
            }
            Console.WriteLine();
        }

        public static void refreshArray()
        {
            boolobject[0] = hppotion;
            boolobject[1] = mhppotion;
            boolobject[2] = pppotion;
            boolobject[3] = smallarmor;
            boolobject[4] = mediumarmor;
            boolobject[5] = bigarmor;
            boolobject[6] = dague;
            boolobject[7] = sword;
            boolobject[8] = magicwand;
            boolobject[9] = lépéecalice;
            boolobject[10] = magicsword;
            boolobject[11] = leechsword;
            boolobject[12] = critsword;
            boolobject[13] = pierredanchentement;
            boolobject[14] = pierredanchentement2;
        }

        public static void refreshBool()
        {
            hppotion = boolobject[0] ;
            mhppotion = boolobject[1] ;
            pppotion = boolobject[2] ;
            smallarmor = boolobject[3] ;
            mediumarmor = boolobject[4] ;
            bigarmor = boolobject[5] ;
            dague = boolobject[6];
            sword = boolobject[7] ;
            magicwand = boolobject[8] ;
            lépéecalice = boolobject[9] ;
            magicsword = boolobject[10] ;
            leechsword = boolobject[11] ;
            critsword = boolobject[12] ;
            pierredanchentement = boolobject[13];
            pierredanchentement2 = boolobject[14];
        }

        public static void refreshPrice (Player p1)
        {
            if (hppotion)
            {
                int number = Game.RoomCount / 2 + 1;
                int cost = 10;
                if (number * cost > p1.money)
                {
                    hppotion = false;
                    ShopPotions--;
                }
            }
            if (mhppotion)
            {
                int number = Game.RoomCount / 5 + 1;
                int cost = 15;
                if (number * cost > p1.money)
                {
                    mhppotion = false;
                    ShopPotions--;
                }

            }
            if (pppotion)
            {
                int number = Game.RoomCount / 4 + 1;
                int cost = 15;
                if (number * cost > p1.money)
                {
                    pppotion = false;
                    ShopPotions--;
                }

            }
            if (smallarmor)
            {
                int cost = 30;
                if (cost > p1.money)
                {
                    smallarmor = false;
                    ShopArmor--;
                }
            }
            if (mediumarmor)
            {
                int cost = 100;
                if (cost > p1.money)
                {
                    mediumarmor = false;
                    ShopArmor--;
                }
            }
            if (bigarmor)
            {
                int cost = 300;
                if (cost > p1.money)
                {
                    bigarmor = false;
                    ShopArmor--;
                }
            }
            if (dague)
            {
                int cost = 30;
                if (cost > p1.money)
                {
                    dague = false;
                    ShopWeapon--;
                }
            }
            if (sword)
            {
                int cost = 100;
                if (cost > p1.money)
                {
                    sword = false;
                    ShopWeapon--;
                }
            }
            if (magicwand)
            {
                int cost = 120;
                if (cost > p1.money)
                {
                    magicwand = false;
                    ShopWeapon--;
                }
            }
            if (magicsword)
            {
                int cost = 250;
                if (cost > p1.money)
                {
                    magicsword = false;
                    ShopWeapon--;
                }
            }
            if (lépéecalice)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    lépéecalice = false;
                    ShopWeapon--;
                }
            }
            if (leechsword)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    leechsword = false;
                    ShopWeapon--;
                }
            }
            if (critsword)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    critsword = false;
                    ShopWeapon--;
                }
            }
            if (pierredanchentement)
            {
                int cost = 200;
                if (cost > p1.money)
                {
                    pierredanchentement = false;
                    ShopSpecial--;
                }
            }
            if (pierredanchentement2)
            {
                int cost = 400;
                if (cost > p1.money)
                {
                    pierredanchentement2 = false;
                    ShopSpecial--;
                }
            }
            refreshArray();
            TriObject();
        }

        public static int CheckRep2 (string rep)
        {
            int IntRep = 0;
            try
            {
                IntRep = Convert.ToInt32(rep);
            }
            catch(Exception e)
            {
                Console.WriteLine("Enter a number.");
                return 0;
            }
            if (IntRep < 0)
            {
                Console.WriteLine("Negativ number... Are you kidding ?");
                return 0;
            }
            else if (!intobject.Contains(IntRep))
            {
                Console.WriteLine("I don't have as much article.");
                return 0;
            }
            else
            {
                return IntRep;
            }

        }

        public static void BuyObject(int indexx, Player p1)
        {
            boolobject[indexx] = false;
            refreshBool();

            if (indexx == 0)
            {
                int number = Game.RoomCount / 2 + 1;
                int cost = 10;
                p1.money -= number * cost;
                Potion.GetPotions(number, p1);
                ShopPotions--;
            }
            if (indexx == 1)
            {
                int number = Game.RoomCount / 5 + 1;
                int cost = 15;
                p1.money -= number * cost;
                Potion.GetMaxHPPotions(number, p1);
                ShopPotions--;
            }
            if (indexx == 2)
            {
                int number = Game.RoomCount / 4 + 1;
                int cost = 15;
                p1.money -= number * cost;
                Potion.GetPuissancePotions(number, p1);
                ShopPotions--;
            }

            if (indexx == 3)
            {
                int cost = 30;
                p1.money -= cost;
                Armor.AddArmor(p1, new SmallArmor());
                ShopArmor--;
            }
            if (indexx == 4)
            {
                int cost = 100;
                p1.money -= cost;
                Armor.AddArmor(p1, new MediumArmor());
                ShopArmor--;
            }
            if (indexx == 5)
            {
                int cost = 300;
                p1.money -= cost;
                Armor.AddArmor(p1, new BigArmor());
                ShopArmor--;
            }

            if (indexx == 6)
            {
                int cost = 30;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new Dague(), null);
                ShopWeapon--;
            }
            if (indexx == 7)
            {
                int cost = 100;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new Sword(), null);
                ShopWeapon--;
            }
            if (indexx == 8)
            {
                int cost = 120;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new MagicWand(), null);
                ShopWeapon--;
            }
            if (indexx == 9)
            {
                int cost = 250;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new MagicSword(), null);
                ShopWeapon--;
            }
            if (indexx == 10)
            {
                int cost = 400;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new Lépéecalice(), null);
                ShopWeapon--;
            }
            if (indexx == 11)
            {
                int cost = 400;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new LeechSword(), null);
                ShopWeapon--;
            }
            if (indexx == 12)
            {
                int cost = 400;
                p1.money -= cost;
                Weapon.AddWeapon(p1, new CritSword(), null);
                ShopWeapon--;
            }

            if (indexx == 13)
            {
                int cost = 200;
                int number = Game.RoomCount / 10; 
                p1.money -= cost*number;
                p1.EnchantStone += number;
                ShopSpecial--;
            }
            if (indexx == 14)
            {
                int cost = 400;
                int number = Game.RoomCount / 10;
                p1.money -= cost * number;
                p1.EnchantStone2 += number;
                ShopSpecial--;
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
