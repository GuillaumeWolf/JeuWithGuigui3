using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Weapon
    {
        public int Dmg;
        public int Crit;
        public int MagicDamage;
        public bool Magic;
        public bool VoldeVie;
        public bool FireDamage;
        public bool PoisonDamage;

        public string Name { get; set; }

        public static void DeleteWeapon(Player p1, int position)
        {
            if (position == 1)
            {
                p1.weapon1 = null;
            }
            else if (position == 2)
            {
                p1.weapon2 = null;
            }
        }

        static public void AddWeapon(Player p1, Weapon w1, Monster m1)
        {
            if (p1.weapon1 == null)
            {
                ADDWeaponToPlayer(p1, w1, 1, m1);
                return;
            }
            else if (p1.weapon2 == null)
            {
                ADDWeaponToPlayer(p1, w1, 2, m1);
                return;
            }
            else
            {
                ChangeWeapon(p1, w1, m1);
                Player.ChangeDamage(p1);
            }
        }

        static public void ADDWeaponToPlayer(Player p1, Weapon w1, int x, Monster m1)
        {
            if (x == 1)
            {
                p1.weapon1 = w1;
            }
            else if (x == 2)
            {
                p1.weapon2 = w1;
            }
            Player.ChangeDamage(p1);
            Console.WriteLine("You got a {0}.", w1.Name);
        }

        static private void ChangeWeapon(Player p1, Weapon w1, Monster m1)
        {
            while (true)
            {
                Console.WriteLine("You found a {0} but you already have a {1} and a {2}. Would you change ? (yes or no)", w1.Name, p1.weapon1.Name, p1.weapon2.Name);
                Console.Write("--> ");
                string rep = Console.ReadLine();
                if (rep == "no")
                {
                    return;
                }
                else if (rep == "yes")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Write \"yes\" or \"no\".");
                }
            }

            while (true)
            {
                Console.WriteLine("Which one do you want to replace ? (1 or 2)");
                Console.WriteLine("Tap \"no\" if you don't want the weapon.");
                Console.Write("--> ");
                string rep2 = Console.ReadLine();
                if (rep2 == "no")
                {
                    return;
                }
                else if (rep2 == "1")
                {
                    DeleteWeapon(p1, 1);
                    ADDWeaponToPlayer(p1, w1, 1, m1);
                    break;
                }
                else if (rep2 == "2")
                {
                    DeleteWeapon(p1, 2);
                    ADDWeaponToPlayer(p1, w1, 2, m1);
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct value (1 or 2). \nWrite \"no\" if you don't want the weapon.");
                }
            }
        }


        static public void CreateRandomWeapon(Player p1, Monster m1)
        {
            int ChanceLegendarySword = Convert.ToInt32(LegendarySword.ChanceOfLooting * 10);
            int ChanceLeechSword = Convert.ToInt32(LeechSword.ChanceOfLooting * 10) + ChanceLegendarySword;
            int ChanceMagicSword = Convert.ToInt32(MagicSword.ChanceOfLooting * 10) + ChanceLeechSword;
            int ChanceCritSword = Convert.ToInt32(CritSword.ChanceOfLooting * 10) + ChanceMagicSword;
            int ChanceMagicWand = Convert.ToInt32(MagicWand.ChanceOfLooting * 10) + ChanceCritSword;
            int ChanceSword = Convert.ToInt32(Sword.ChanceOfLooting * 10) + ChanceMagicWand;
            int ChanceDague = Convert.ToInt32(Dague.ChanceOfLooting * 10) + ChanceSword;
            int y = RandomInt(ChanceDague);

            //Console.WriteLine("ChanceDague : {0}", ChanceDague);
            if (m1 != null)
            {
                ChanceMagicWand += m1.ChanceOfLoot * 10;
                ChanceSword += m1.ChanceOfLoot * 10;
                ChanceDague += m1.ChanceOfLoot * 10;
                ChanceLeechSword += m1.ChanceOfLoot * 10;
                ChanceMagicSword += m1.ChanceOfLoot * 10;
            }
            //Console.WriteLine("                                                    ChanceLegendarySword: {0}. ChanceLeechSword: {1}. ChanceMagicSword: {2}. ChanceMagicWand: {3}. ChanceSword: {4}. ChanceDague: {5}. x: {6}.",  ChanceLegendarySword, ChanceLeechSword, ChanceMagicSword, ChanceMagicWand, ChanceSword, ChanceDague, y);
            if (y < ChanceLegendarySword)
            {
                Weapon w1 = new LegendarySword();
                AddWeapon(p1, w1, m1);
                return;
            }
            else if (y < ChanceLeechSword)
            {
                Weapon w1 = new LeechSword();
                AddWeapon(p1, w1, m1);
                return;
            }
            else if (y < ChanceMagicSword)
            {
                Weapon w1 = new MagicSword();
                AddWeapon(p1, w1, m1);
                return;
            }
            else if (y < ChanceMagicWand)
            {
                Weapon w1 = new MagicWand();
                AddWeapon(p1, w1, m1);
                return;
            }
            else if (y < ChanceSword)
            {
                Weapon w1 = new Sword();
                AddWeapon(p1, w1, m1);
                return;
            }
            else if (y < ChanceDague)
            {
                Weapon w1 = new Dague();
                AddWeapon(p1, w1, m1);
                return;
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






    class MagicWand : Weapon
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.MagicSwordLoot;
        public MagicWand()
        {
            MagicDamage = 30;
            Name = "Magic Wand";
            Magic = true;
            ChanceOfLooting /= 2;
        }

    }
    class Sword : Weapon
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.SwordLoot;

        public Sword()
        {
            Dmg = 15;
            Name = "Sword";
            Crit = 5;
            Magic = false;
            ChanceOfLooting /= 2;
        }
    }
    class Dague : Weapon
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.DagueLoot;
        public Dague()
        {
            Dmg = 10;
            Name = "Dague";
            Crit = 1;
            Magic = false;
            ChanceOfLooting /= 2;
        }
    }
    class CritSword : Weapon    //épée Critique
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.CritSwordLoot;     

        public CritSword()
        {
            Dmg = 30;
            Crit = 20;
            MagicDamage = 0;
            Name = "Crit Sword";
            Magic = false;
            ChanceOfLooting /= 2;
        }

    }
    class MagicSword : Weapon    //épée magique
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.MagicSwordLoot;     

        public MagicSword()
        {
            Dmg = 30;
            MagicDamage = 30;
            Name = "Magic Sword";
            Magic = true;
            ChanceOfLooting /= 2;
        }

    }
    class LeechSword : Weapon            //épée vol de vie
    {
        public static double ChanceOfLooting = ChanceOfLootingWeapon.LeechSwordLoot;    
        public LeechSword()
        {
            Dmg = 30;
            Name = "LeechSword";
            Magic = false;
            VoldeVie = true;
            ChanceOfLooting /= 2;
        }
    }
    class LegendarySword : Weapon
    {
        public static double ChanceOfLooting = ChanceOfLootingWeapon.LegendaryLoot;
        public LegendarySword()
        {
            Dmg = 70;
            Name = "LegendarySword";
            Crit = 10;
            Magic = false;
            FireDamage = true;
            PoisonDamage = true;
            ChanceOfLooting /= 2;
            LegendarySword.ChanceOfLooting = 0;
        }
    }



    class ChanceOfLootingWeapon
    {
        public static double LegendaryLoot = 0.5;
        public static double LeechSwordLoot = 4.5;
        public static double MagicSwordLoot = 10;
        public static double CritSwordLoot = 10;
        public static double MagicWandLoot = 20;
        public static double SwordLoot = 25;
        public static double DagueLoot = 30;

    }

}
