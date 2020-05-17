using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Armor
    {
        //Charactéristique 
        public string Name { get; set; }
        public int ClassicResistance;
        public int MagicResistance;
        

        public static void DeleteArmor(Player p1)
        {
            p1.armor = null;
        }

        static public void AddArmor(Player p1, Armor a1)
        {
            if (p1.armor == null)
            {
                p1.armor = a1;
                Console.WriteLine("You got a {0}.", a1.Name);

            }
            else
            {
                ChangeArmor(p1, a1);
                Player.ChangeDamage(p1);
            }
        }

        static public void ChangeArmor(Player p1, Armor a1)
        {
            while(true)
            {
                Console.WriteLine("You found a {0} but you already have a {1}. Would you change ? (yes or no)", a1.Name, p1.armor.Name);
                Console.Write("--> ");
                string rep = Console.ReadLine();
                if (rep == "no")
                {
                    return;
                }
                else if (rep == "yes")
                {
                    DeleteArmor(p1);
                    AddArmor(p1, a1);
                    break;
                }
                else
                {
                    Console.WriteLine("Write \"yes\" or \"no\".");
                }
            }       
        }

        static public void CreateRandomArmor(Player p1, Monster m1)
        {
            int ChanceBig = SmallArmor.ChanceOfLooting;
            int ChanceMedium = MediumArmor.ChanceOfLooting + ChanceBig;
            int ChanceSmall = BigArmor.ChanceOfLooting + ChanceMedium;



            int y = RandomInt(SmallArmor.ChanceOfLooting + MediumArmor.ChanceOfLooting + BigArmor.ChanceOfLooting);

            if (m1 != null)
            {
                ChanceBig += m1.ChanceOfLoot;
                ChanceMedium += m1.ChanceOfLoot;
                ChanceSmall += m1.ChanceOfLoot;

            }

            if (y < ChanceBig)
            {
                Armor a1 = new BigArmor();
                AddArmor(p1, a1);
                return;
            }
            else if (y < ChanceMedium)
            {
                Armor a1 = new MediumArmor();
                AddArmor(p1, a1);
                return;
            }
            else if (y < ChanceSmall)
            {
                Armor a1 = new SmallArmor();
                AddArmor(p1, a1);
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









    class SmallArmor : Armor
    {
        static public int ChanceOfLooting = 60;

        public SmallArmor()
        {
            Name = "Small Armor";
            ClassicResistance = 5;
            MagicResistance = 0;
        }
    
    }

    class MediumArmor : Armor
    {
        static public int ChanceOfLooting = 30;

        public MediumArmor()
        {
            Name = "Medium Armor";
            ClassicResistance = 10;
            MagicResistance = 10;
        }
    }

    class BigArmor : Armor
    {
        static public int ChanceOfLooting = 10;

        public BigArmor()
        {
            Name = "Big Armor";
            ClassicResistance = 20;
            MagicResistance = 20;
        }
    }
}
