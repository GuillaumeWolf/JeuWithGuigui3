using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Weapon
    {
        #region public Porpreties
        //Name
        public string Name;

        //Caractéristique Bool
        public bool ClassicDamageBool;
        public bool MagicDamageBool;
        public bool CritBool;
        public bool VolDevieBool;
        public bool FireBool;
        public bool PoisonBool;

        //Dégats et Crit
        public int ClassicDmg;
        public int MagicDmg;
        public int Crit;

        #endregion

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
            int ChanceLépéecalice = Convert.ToInt32(Lépéecalice.ChanceOfLooting * 10) + ChanceLeechSword;
            int ChanceMagicSword = Convert.ToInt32(MagicSword.ChanceOfLooting * 10) + ChanceLépéecalice;
            int ChanceCritSword = Convert.ToInt32(CritSword.ChanceOfLooting * 10) + ChanceMagicSword;
            int ChanceMagicWand = Convert.ToInt32(MagicWand.ChanceOfLooting * 10) + ChanceCritSword;
            int ChanceSword = Convert.ToInt32(Sword.ChanceOfLooting * 10) + ChanceMagicWand;
            int ChanceDague = Convert.ToInt32(Dague.ChanceOfLooting * 10) + ChanceSword;
            int y = RandomInt(ChanceDague);

            //Console.WriteLine("ChanceDague : {0}", ChanceDague);
            if (m1 != null)
            {
                ChanceMagicWand += m1.ChanceOfLoot * 10;
                ChanceCritSword += m1.ChanceOfLoot * 10;
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
            else if (y < ChanceLépéecalice)
            {
                Weapon w1 = new Lépéecalice();
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
            //Name
            Name = "Magic Wand";

            //Caractéristique Bool
            ClassicDamageBool = false;
            MagicDamageBool = true;
            CritBool = false;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 0;
            MagicDmg = 30;
            Crit = 0;
        }

    }
    class Sword : Weapon
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.SwordLoot;

        public Sword()
        {
            //Name
            Name = "Sword";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = false;
            CritBool = true;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 15;
            MagicDmg = 0;
            Crit = 5;
        }
    }
    class Dague : Weapon
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.DagueLoot;
        public Dague()
        {
            //Name
            Name = "Dague";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = false;
            CritBool = true;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 10;
            MagicDmg = 0;
            Crit = 1;
        }
    }
    class CritSword : Weapon    //épée Critique
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.CritSwordLoot;     

        public CritSword()
        {
            //Name
            Name = "Crit Sword";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = false;
            CritBool = true;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 30;
            MagicDmg = 0;
            Crit = 30;
        }

    }
    class MagicSword : Weapon    //épée magique
    {
        static public double ChanceOfLooting = ChanceOfLootingWeapon.MagicSwordLoot;     

        public MagicSword()
        {
            //Name
            Name = "Magic Sword";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = true;
            CritBool = false;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 40;
            MagicDmg = 40;
            Crit = 0;
        }

    }


    class Lépéecalice : Weapon
    {
        public static double ChanceOfLooting = ChanceOfLootingWeapon.lépéecalice;
        public Lépéecalice()
        {
            //Name
            Name = "L'épée Calice";

            //Caractéristique Bool
            ClassicDamageBool = false;
            MagicDamageBool = true;
            CritBool = false;
            VolDevieBool = false;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 0;
            MagicDmg = 50;
            Crit = 0;
        }
    }

    class LeechSword : Weapon            //épée vol de vie
    {
        public static double ChanceOfLooting = ChanceOfLootingWeapon.LeechSwordLoot;    
        public LeechSword()
        {            
            //Name
            Name = "LeechSword";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = false;
            CritBool = false;
            VolDevieBool = true;
            FireBool = false;
            PoisonBool = false;

            //Dégats et Crit
            ClassicDmg = 40;
            MagicDmg = 0;
            Crit = 0;
        }
    }
    class LegendarySword : Weapon
    {
        public static double ChanceOfLooting = ChanceOfLootingWeapon.LegendaryLoot;
        public LegendarySword()
        {            
            //Name
            Name = "LegendarySword";

            //Caractéristique Bool
            ClassicDamageBool = true;
            MagicDamageBool = false;
            CritBool = true;
            VolDevieBool = false;
            FireBool = true;
            PoisonBool = true;

            //Dégats et Crit
            ClassicDmg = 70;
            MagicDmg = 0;
            Crit = 10;

        }
    }



    class ChanceOfLootingWeapon
    {
        public static double LegendaryLoot = 0.5;
        public static double LeechSwordLoot = 4.5;
        public static double lépéecalice = 5;
        public static double MagicSwordLoot = 10;
        public static double CritSwordLoot = 10;
        public static double MagicWandLoot = 20;
        public static double SwordLoot = 20;
        public static double DagueLoot = 30;

    }

}
