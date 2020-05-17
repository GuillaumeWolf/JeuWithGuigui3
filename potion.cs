using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuWithGuigui3
{
    class Potion
    {
        //Fonction potion
        public static bool UsePotions(Player p1)
        {
            //Premiere fonction
            string[] poss = writePossibilities(p1);
            if (poss == null)
            {
                Console.WriteLine("You cannot use potion now potion.");
                while (true)
                {
                    Console.Write("--> ");
                    string rep1 = Console.ReadLine();
                    if (rep1 == "gb")
                    {
                        return false;
                    }
                }
            }
            //Deuxieme fonction
            string typePotion = ChoosePotions(poss, p1);

            if (typePotion == "gb")
            {
                return false;
            }

            //Troisieme fonction
            int numPotion = ChooseNumPotions(typePotion,p1);
            if (numPotion == 0)
            {
                return false;
            }

            //Quatrieme fonction
            ConsomePotions(typePotion, numPotion,p1);
            return true;
        }

        //Premiere fonction
        private static string[] writePossibilities(Player p1)
        {
            string[] poss = new string[3];
            if (p1.potions > 0 && p1.HP != p1.BaseHP)
            { poss[0] = "hp"; }
            if (p1.PotionMaxHP > 0)
            { poss[1] = "mhp"; }
            if (p1.PuissancePotions > 0 && p1.InFight)
            { poss[2] = "pp"; }

            Console.Write("What kind of potion do you want to take? ");
            if (p1.potions > 0 && p1.HP != p1.BaseHP)
            {
                Console.Write("(heal (hp)");
                if (p1.InFight && p1.PuissancePotions > 0)
                {
                    Console.Write(" - Puissance Potion (pp)");
                }
                if (p1.PotionMaxHP > 0)
                {
                    Console.Write(" - maxHP (mhp))");
                }
                else
                {
                    Console.Write(")");
                }
            }
            else if (p1.PotionMaxHP > 0)
            {
                Console.Write("(maxHP (mhp)");
                if (p1.InFight && p1.PuissancePotions > 0)
                {
                    Console.Write(" - Puissance Potion (pp)");
                }
                else
                {
                    Console.Write(")");
                }
            }
            else if (p1.PuissancePotions > 0 && p1.InFight)
            {
                Console.Write("(PuissancePotion (pp))");
            }

            Console.WriteLine("\nWrite \"gb\" to go back to commande.");

            return poss;
        }


        //Deuxieme fonction
        private static string ChoosePotions(string[] poss, Player p1)
        {
            while (true)
            {
                Console.Write("--> ");
                string ChoosePotions = Console.ReadLine();
                if (poss.Contains(ChoosePotions))
                {
                    return ChoosePotions;
                }
                else
                {
                    writePossibilities(p1);
                }
            }
        }

        //Troisieme fonction
        private static int ChooseNumPotions(string ChoosePotions, Player p1)
        {
            while (true)
            {
                int NumberOfPotionsMax = 0;
                Console.Write("How many potions do you want to take ? ");
                if (ChoosePotions == "hp")
                {
                    NumberOfPotionsMax = p1.potions;
                    int maxPotions = (p1.BaseHP - p1.HP) / 30;
                    if ((p1.BaseHP - p1.HP) % 30 != 0)
                    {
                        maxPotions++;
                    }
                    if (maxPotions < NumberOfPotionsMax)
                    {
                        NumberOfPotionsMax = maxPotions;
                    }

                }
                else if (ChoosePotions == "mhp")
                {
                    NumberOfPotionsMax = p1.PotionMaxHP;
                }
                else if (ChoosePotions == "pp")
                {
                    NumberOfPotionsMax = p1.PuissancePotions;
                }

                Console.WriteLine("(max {0})\nWrite \"back\" to go back to commande. ", NumberOfPotionsMax);
                Console.Write("--> ");

                string Rep = Console.ReadLine();
                if (Rep == "back")
                {
                    return 0;
                }

                int usedPotion;
                try
                {
                    usedPotion = Convert.ToInt32(Rep);
                }
                catch (Exception)
                {
                    Console.WriteLine("Choose a number");
                    continue;
                }
                if (usedPotion <= 0 || usedPotion > NumberOfPotionsMax)
                {
                    Console.WriteLine("The choosen number isn't correct.");
                    continue;
                }
                else
                {
                    return usedPotion;
                }

            }
        }


        //Quatrieme fonction
        private static void ConsomePotions(string ChoosePotions, int usedPotion, Player p1)
        {
            string type = "";
            if (ChoosePotions == "pp")
            {
                while (true)
                {
                    Console.WriteLine("Which type do you want to up ? (ma or ca)");
                    Console.Write("--> ");
                    string rep = Console.ReadLine();
                    if (rep == "ma" || rep == "ca")
                    { type = rep; break; }
                    else
                    { Console.WriteLine("Choose a correct value."); }
                }
            }
            for (int i = 0; i < usedPotion; i++)
            {
                if (ChoosePotions == "hp")
                { p1.potions--; Heal(30, p1); }
                else if (ChoosePotions == "mhp")
                { p1.PotionMaxHP--; UpHP(30, p1); }
                else if (ChoosePotions == "pp")
                { p1.PuissancePotions--; UpDamage(20, type, p1); }
            }
            if (ChoosePotions == "heal")
            { Console.WriteLine("You used {0} heal postions.", usedPotion); }
            else if (ChoosePotions == "maxHP")
            { Console.WriteLine("You used {0} MaxHP potions.", usedPotion); }
        }



        //Modifie les stats
        public static void Heal(int x, Player p1)
        {
            if (p1.HP + x > p1.BaseHP)
            {
                Console.WriteLine("You heal {0} HP. You are full HP.", p1.BaseHP - p1.HP);
                p1.HP = p1.BaseHP;
            }

            else
            {
                p1.HP += x;
                Console.WriteLine("You heal {0} HP.", x);
            }
        }

        public static void UpHP(int x, Player p1)
        {
            p1.BaseHP += x;
        }

        public static void UpDamage(int x, string type, Player p1)
        {
            
            if (type == "ca")
            {
                p1.Damage += x;
            }
            else if (type == "ma")
            {
                p1.MagicDmg += x;
            }

        }

        //recevoir les Potions
        public static void GetPotions(int x, Player p1)
        {
            p1.potions += x;
            Console.WriteLine("You find {0} potions. You have {1} potions.", x, p1.potions);
        }
        public static void GetMaxHPPotions(int x, Player p1)
        {
            p1.PotionMaxHP += x;
            Console.WriteLine("You find {0} MaxHP+ potions. You have {1} MaxHP+ potions.", x, p1.PotionMaxHP);
        }
        public static void GetPuissancePotions(int x, Player p1)
        {
            p1.PuissancePotions += x;
            Console.WriteLine("You find {0} Puissance Potions. You have {1} Puissance Potions.", x, p1.PuissancePotions);
        }
    }
}
