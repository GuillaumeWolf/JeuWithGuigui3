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


        static public void GetLoot(Player p1, Monster m1)
        {
            int ChanceWeapon = ChanceOfGettingWeapon;
            int ChanceArmor = ChanceOfGettingArmor + ChanceWeapon;
            int ChanceMaxhpPotion = ChanceOfGettingMaxHpPotion + ChanceArmor;
            int ChancePuissancePotions = ChanceOfGettingPuissancePotions + ChanceMaxhpPotion;
            int Chance2Potion = ChanceOfGetting2Potion + ChancePuissancePotions;
            int ChanceNothing = nothing + Chance2Potion;

            int x = RandomInt(ChanceNothing);

            if (m1 != null)
            {
                if (p1.COfP.ClassName == "Thief")
                { p1.COfP.ClassCapacity(p1, m1);}

                if (p1.RaceOfPlayer.Name != "Minautore")
                { ChanceWeapon += m1.ChanceOfLoot; }
                ChanceArmor += m1.ChanceOfLoot;
                ChanceMaxhpPotion += m1.ChanceOfLoot;
                ChancePuissancePotions += m1.ChanceOfLoot;
                Chance2Potion += m1.ChanceOfLoot;
            }

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



        static public int RandomInt(int Max)
        {
            int randomNum;
            var rand = new Random();
            randomNum = rand.Next(Max);
            return randomNum;
        }
    }
}

