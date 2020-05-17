using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace JeuWithGuigui3

/*
à faire: 

    Obligatoirement :


    Nouveau (idée):
        - créer un systeme d'argent(Guillaume -> confirmation de lidée par leander ??)  (enleve juste les poitn d'interogation si tu es daccord)
        - room pour acheter des armes(Guillaume -> confirmation de lidée par leander ??)  (enleve juste les poitn d'interogation si tu es daccord)



*/
{
    class Game
    {
        //Attribut du jeu
        static public int RoomCount = 0;
        static public Monster m1 = null;

        static void Main()
        {
            Player p1 = Player.CreatePlayer();
            Commande(p1);
        }



        static public void Commande(Player p1)
        {
            while (true)
            {
                Console.WriteLine("\nWhat do you want to do ? (enter room: er - stats: s - objects: o - weapons: w - use potion: up - exit: e)");
                Console.Write("--> ");
                string PlayerCommande = Console.ReadLine();


                if (PlayerCommande == "e")
                {
                    Console.WriteLine("Goodbye");
                    return;
                }

                else if (PlayerCommande == "ggg")
                { Weapon.CreateRandomWeapon(p1, null); }
                else if (PlayerCommande == "s")
                {
                    Console.WriteLine("Name: {0}. Race: {1}. Classe : {6}. HP: {2}/{3}.\nClassic Damage: {4}. Magic Damage : {5}. ", p1.name, p1.RaceOfPlayer.Name, p1.HP, p1.BaseHP, p1.Damage, p1.MagicDmg, p1.COfP.ClassName);
                }


                else if (PlayerCommande == "o")
                {
                    Console.Write("Potions: {0}. ", p1.potions);
                    Console.Write("MaxHP+ Potions: {0}. ", p1.PotionMaxHP);
                    Console.WriteLine("Puissance Potions: {0}.", p1.PuissancePotions);
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
                    bool usedPotions = Potion.UsePotions(p1);
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
                    Room.EnterRoom(p1, m1);
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
                Console.WriteLine("\nWhat do you want to do ? (stats: s - objects: o - weapons: w - attack: a - monster stats: ms - use potion: up - exit: e)");
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
                    if (p1.RaceOfPlayer.Name == "Cracheur de feu")
                    { Console.Write("You can fire the enemy."); }
                    if (m1.PoisonDmg)
                    {
                        Console.Write("You are Poisoned. ");
                    }
                    if (m1.FireDmg && p1.RaceOfPlayer.Name != "Cracheur de feu")
                    {
                        Console.Write("Your are in Fire. ");
                    }
                    Console.WriteLine();
                }


                else if (PlayerCommande == "o")
                {
                    Console.WriteLine("Potions: {0}. MaxHP+ Potions: {1}. Puissance Potions: {2}.", p1.potions, p1.PotionMaxHP, p1.PuissancePotions);
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
                    bool usedPotions = Potion.UsePotions(p1);
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
    }
}




