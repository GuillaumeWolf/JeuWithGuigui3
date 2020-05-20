﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Loot
    {
        static public int ChanceOfGettingWeapon = 40;
        static public int ChanceOfGettingArmor = 25;
        static public int ChanceOfGettingMaxHpPotion = 10;
        static public int ChanceOfGettingPuissancePotions = 10;
        static public int ChanceOfGetting2Potion = 20;
        static public int nothing = 1;


        static public void GetRandomLoot(Player p1, Monster m1)
        {
            if (p1.RaceOfPlayer.Name == "Minautore")
            {
                ChanceOfGettingArmor = -1;
            }
            int ChanceWeapon = ChanceOfGettingWeapon;
            if(p1.weapon1 != null && p1.weapon2 != null )
            {ChanceWeapon -= 20;}
            int ChanceArmor = ChanceOfGettingArmor + ChanceWeapon;
            if (p1.armor != null)
            { ChanceArmor -= 15; }
            int ChanceMaxhpPotion = ChanceOfGettingMaxHpPotion + ChanceArmor;
            int ChancePuissancePotions = ChanceOfGettingPuissancePotions + ChanceMaxhpPotion;
            int Chance3Potion = ChanceOfGetting2Potion + ChancePuissancePotions;
            int ChanceNothing = nothing + Chance3Potion;

            int total = ChanceNothing;
            int x = RandomInt(total);

            if (m1 != null)
            {
                if (p1.COfP.ClassName == "Thief")
                { p1.COfP.ClassCapacity(p1, m1); }

                ChanceWeapon += m1.ChanceOfLoot;
                ChanceArmor += m1.ChanceOfLoot;
                ChanceMaxhpPotion += m1.ChanceOfLoot;
                ChancePuissancePotions += m1.ChanceOfLoot;
                Chance3Potion += m1.ChanceOfLoot;

                if (p1.RaceOfPlayer.Name == "Minautore")
                {
                    ChanceArmor = 0;
                }
            }

            //Console.WriteLine("                                                    ChanceWeapon: {0}. ChanceArmor: {1}. ChanceMaxhpPotion: {2}. ChancePuissancePotions: {3}. Chance2Potion: {4}. ChanceNothing; {5}.  x: {6}", ChanceWeapon, ChanceArmor, ChanceMaxhpPotion, ChancePuissancePotions, Chance3Potion, ChanceNothing, x);

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
                Potion.GetPuissancePotions(2, p1);
                return;
            }
            else if (x < Chance3Potion)
            {
                Potion.GetPotions(3, p1);
                return;
            }
            else if (x < ChanceNothing)
            {
                Console.WriteLine("You receive nothing.");
                return;
            }

            Console.WriteLine("Pas normal (Loot.GetLoot())");
        }

        public static void LootPotion(Player p1, int x)
        {
            int y = RandomInt(30);
            //Console.WriteLine("y: {0}",y);
            if (y < 15)
            {
                Potion.GetPotions(x, p1);
            }
            else if (y < 20)
            {
                Potion.GetMaxHPPotions(x-1, p1);
            }
            else if (y < 30)
            {
                Potion.GetPuissancePotions(x, p1);
            }
        }

        public static void LootMoney(Player p1, int x)
        {
            p1.money += x;
            Console.WriteLine("You got {0} gold, you have {1} gold.", x, p1.money);
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

