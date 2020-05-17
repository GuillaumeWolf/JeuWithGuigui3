using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Loot
    {
        static public int ChanceOfGettingWeapon = 40;
        static public int ChanceOfGettingArmor = 25;
        static public int ChanceOfGettingMaxHpPotion = 20;
        static public int ChanceOfGettingPuissancePotions = 15;
        static public int ChanceOfGetting2Potion = 15;
        static public int nothing = 1;


        static public void GetRandomLoot(Player p1, Monster m1)
        {
            int ChanceWeapon = ChanceOfGettingWeapon;
            int ChanceArmor = ChanceOfGettingArmor + ChanceWeapon;
            if (p1.RaceOfPlayer.Name == "Minautore")
            {
                ChanceArmor = 0;
            }
            int ChanceMaxhpPotion = ChanceOfGettingMaxHpPotion + ChanceArmor;
            int ChancePuissancePotions = ChanceOfGettingPuissancePotions + ChanceMaxhpPotion;
            int Chance2Potion = ChanceOfGetting2Potion + ChancePuissancePotions;
            int ChanceNothing = nothing + Chance2Potion;

            int x = RandomInt(ChanceNothing);

            if (m1 != null)
            {
                if (p1.COfP.ClassName == "Thief")
                { p1.COfP.ClassCapacity(p1, m1);}

                ChanceWeapon += m1.ChanceOfLoot; 
                ChanceArmor += m1.ChanceOfLoot;
                ChanceMaxhpPotion += m1.ChanceOfLoot;
                ChancePuissancePotions += m1.ChanceOfLoot;
                Chance2Potion += m1.ChanceOfLoot;
                if (p1.RaceOfPlayer.Name == "Minautore")
                {
                    ChanceArmor = 0;
                }
            }
            
            //Console.WriteLine("                                                    ChanceWeapon: {5}. ChanceArmor: {0}. ChanceMaxhpPotion: {1}. ChancePuissancePotions: {2}. Chance2Potion: {3}. x: {4}", ChanceArmor, ChanceMaxhpPotion, ChancePuissancePotions, Chance2Potion, x, ChanceWeapon);
            
            if (x < ChanceWeapon)
            {
                Weapon.CreateRandomWeapon(p1, m1);
                return;
            }
            else if (x < ChanceArmor)
            {
                Armor.CreateRandomArmor(p1, m1);
                return;
            }
            else if (x < ChanceMaxhpPotion)
            {
                Potion.GetMaxHPPotions(1, p1);
                return;
            }
            else if (x < ChancePuissancePotions)
            {
                Potion.GetPuissancePotions(1, p1);
                return;
            }
            else if (x < Chance2Potion)
            {
                Potion.GetPotions(2, p1);
                return;
            }
            else if (x < ChanceNothing)
            {
                Console.WriteLine("You receive nothing.");
            }

            Console.WriteLine("Pas normal");
        }

        public static void LootPotion(Player p1, int x)
        {
            int y = RandomInt(3);
            if (y == 0)
            {
                Potion.GetPotions(x, p1);
            }
            else if ( y == 1)
            {
                Potion.GetMaxHPPotions(x, p1);
            }
            else if (y == 2)
            {
                Potion.GetPuissancePotions(x, p1);
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

