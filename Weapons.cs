using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Weapon
    {
        public int Dmg;
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
            int y = RandomInt(1001);
            int ChanceLegendarySword = Convert.ToInt32(LegendarySword.ChanceOfLooting * 10);
            int ChanceLeechSword = Convert.ToInt32(LeechSword.ChanceOfLooting * 10) + ChanceLegendarySword;
            int ChanceMagicSword = MagicSword.ChanceOfLooting * 10 + ChanceLeechSword;
            int ChanceMagicWand = MagicWand.ChanceOfLooting * 10 + ChanceMagicSword;
            int ChanceSword = Sword.ChanceOfLooting * 10 + ChanceMagicWand;
            int ChanceDague = Dague.ChanceOfLooting * 10 + ChanceSword;

            if (m1 != null)
            {
                ChanceMagicWand += m1.ChanceOfLoot * 10;
                ChanceSword += m1.ChanceOfLoot * 10;
                ChanceDague += m1.ChanceOfLoot * 10;
                ChanceLeechSword += m1.ChanceOfLoot * 10;
                ChanceMagicSword += m1.ChanceOfLoot * 10;
                ChanceLegendarySword += m1.ChanceOfLoot * 10;
            }
            //Console.WriteLine("y: {0}, LegendarySword: {1}. ChanceLeechSword: {3}. ChanceTrap: {2}. ChanceMonster: {4}.", y, LegendarySword, ChanceLeechSword, ChanceNothing, ChanceMonster);
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
        static public int ChanceOfLooting = 20;
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
        static public int ChanceOfLooting = 25;

        public Sword()
        {
            Dmg = 25;
            Name = "Sword";
            Magic = false;
            ChanceOfLooting /= 2;
        }
    }
    class Dague : Weapon
    {
        static public int ChanceOfLooting = 30;
        public Dague()
        {
            Dmg = 15;
            Name = "Dague";
            Magic = false;
            ChanceOfLooting /= 2;
        }
    }
    class MagicSword : Weapon    //épée magique
    {
        static public int ChanceOfLooting = 15;     // a modifier

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
        public static double ChanceOfLooting = 9.5;     // a modifier
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
        public static double ChanceOfLooting = 0.5;
        public LegendarySword()
        {
            Dmg = 60;
            Name = "LegendarySword";
            Magic = false;
            FireDamage = true;
            PoisonDamage = true;
            ChanceOfLooting /= 2;
        }
    }
}
