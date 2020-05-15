using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace JeuWithGuigui3

/*
à faire: 
- crée un perso #
- crée l'event "entré dans la salle" #
- crée le monstre (possède vie + dmg)  #
- crée le combat #
- crée le loot  ~ (manque l'aléatoire)#
- crée des armes ~ (crée plus d'arme)
- crée une protection 
- créé un compteur de salle #
*/
{
    class Game
    {
        static public int RoomCount = 0;
        static public int tour = 0;

        static void Main(string[] args)
        {
            Player p1 = Player.CreatePlayer();
            Commande(p1);
        }



        static public void Commande(Player p1)
        {
            while (true)
            {
                Console.WriteLine("What do you want to do ? (enter room: er - stats: s - objects: o - weapons: w - use potion: up - exit: exit)");
                Console.Write("--> ");
                string PlayerCommande = Console.ReadLine();


                if (PlayerCommande == "e")
                {
                    Console.WriteLine("Goodbye");
                    return;
                }


                else if (PlayerCommande == "s")
                {
                    Console.WriteLine("Name: {0}. Race: {1}. HP: {2}/{3}. Classic Damage: {4}. Magic Damage : {5}. ", p1.name, p1.RaceOfPlayer.Name, p1.HP, p1.BaseHP, p1.Damage, p1.MagicDmg);
                }


                else if (PlayerCommande == "o")
                {
                    Console.Write("Potions: {0}. ", p1.potions);
                    Console.WriteLine("MaxHP+ Potions: {0}.", p1.PotionMaxHP);
                }


                else if (PlayerCommande == "w")
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
                        Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                        if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                        if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();

                        Console.Write("Weapon 2: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon2.Name, p1.weapon2.Dmg, p1.weapon2.MagicDamage);
                        if (p1.weapon2.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon2.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();
                    }
                }


                else if (PlayerCommande == "up")
                {
                    bool usedPotions = p1.UsePotions();
                    if (!usedPotions)
                    {
                        Commande(p1);
                        return;
                    }

                }


                else if (PlayerCommande == "er")
                {
                    RoomCount++;
                    Console.WriteLine();
                    Console.Write("(room {0}). ", RoomCount);
                    Room.EnterRoom(p1);
                    return;
                }


                else
                {
                    Console.WriteLine("Choose a correct commande.");
                }
            }
        }



        public static bool CommandeFight(Player p1, Monster m1)
        {
            while (true)
            {
                Console.WriteLine("What do you want to do ? (stats: s - objects: o - weapons: w - attack: a - monster stats: ms - use potion: up - exit: e)");
                Console.Write("--> ");
                string PlayerCommande = Console.ReadLine();


                if (PlayerCommande == "e")
                {
                    Console.WriteLine("Goodbye");
                    return true;
                }


                else if (PlayerCommande == "s")
                {

                    Console.Write("Name: {0}. HP: {1}/{2}. Classic Damage: {3}. Magic Damage : {4}. ", p1.name, p1.HP, p1.BaseHP, p1.Damage, p1.MagicDmg);
                    if (m1.PoisonDmg)
                    {
                        Console.Write("You are Poisoned. ");
                    }
                    if (m1.FireDmg)
                    {
                        Console.Write("Your are in Fire. ");
                    }
                    Console.WriteLine();
                }


                else if (PlayerCommande == "o")
                {
                    Console.WriteLine("Potions: {0}. MaxHP+ Potions: {1}.", p1.potions, p1.PotionMaxHP);
                }

                else if (PlayerCommande == "w")
                {
                    if (p1.weapon1 == null && p1.weapon2 == null)
                    {
                        Console.WriteLine("You don't have any weapon.");
                    }
                    else if (p1.weapon1 == null && p1.weapon2 != null)
                    {
                        Console.Write("Weapon 2: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon2.Name, p1.weapon2.Dmg, p1.weapon2.MagicDamage);
                        if (p1.weapon2.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon2.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();
                    }
                    else if (p1.weapon1 != null && p1.weapon2 == null)
                    {
                        Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                        if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("Weapon 1: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon1.Name, p1.weapon1.Dmg, p1.weapon1.MagicDamage);
                        if (p1.weapon1.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon1.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();

                        Console.Write("Weapon 2: {0}. Damage: {1}. Magic Damage : {2}", p1.weapon2.Name, p1.weapon2.Dmg, p1.weapon2.MagicDamage);
                        if (p1.weapon2.PoisonDamage) { Console.Write("Poison Damage. "); }
                        if (p1.weapon2.FireDamage) { Console.Write("Fire Damage. "); }
                        Console.WriteLine();
                    }
                }


                else if (PlayerCommande == "a")
                {
                    p1.Attak(m1);
                    return false;
                }


                else if (PlayerCommande == "ms")
                {
                    Console.Write("HP: {0}. Damage: {1}.", m1.Vie, m1.Dmg);
                    if (m1.MagicResistance)
                    {
                        Console.WriteLine("Magic resistance. ");
                    }
                    if (m1.FireDmg)
                    {
                        Console.WriteLine("Fire damage. ");
                    }
                    if (m1.PoisonDmg)
                    {
                        Console.WriteLine("Poison damage. ");
                    }
                    Console.WriteLine();
                    if (p1.Poison)
                    {
                        Console.Write("He is Poisoned. ");
                    }
                    if (p1.Fire)
                    {
                        Console.Write("He is on Fire. ");
                    }
                    Console.WriteLine();
                }


                else if (PlayerCommande == "up")
                {
                    bool usedPotions = p1.UsePotions();
                    if (usedPotions)
                    {
                        return false;
                    }
                }

                else
                {
                    Console.WriteLine("Choose a correct commande.");
                }
            }
        }

        static public void GetLoot(Player p1, Monster m1)
        {
            int x = RandomInt(101);
            int ChanceWeapon = 40;
            int ChanceMaxhpPotion = 20 + ChanceWeapon;
            int Chance2Potions = 25 + ChanceMaxhpPotion;
            int ChancePotion = 15 + Chance2Potions;

            if (m1 != null)
            {
                ChanceWeapon += m1.ChanceOfLoot;
                Chance2Potions += m1.ChanceOfLoot;
                ChancePotion += m1.ChanceOfLoot;
                ChanceMaxhpPotion += m1.ChanceOfLoot;
            }

            if (x < ChanceWeapon)
            {
                Weapon.CreateRandomWeapon(p1, m1);
                return;
            }
            else if (x < ChanceMaxhpPotion)
            {
                p1.GetMaxHPPotions(1);
                return;
            }
            else if (x < Chance2Potions)
            {
                p1.GetPotions(2);
                return;
            }
            else if (x < ChancePotion)
            {
                p1.GetPotions(1);
                return;
            }

            Console.WriteLine("You receive nothing.");
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



