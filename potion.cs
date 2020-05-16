using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Potion
    {
        //Fonction potion
        public static bool UsePotions(Player p1)
        {
            int kas = 0;
            Console.Write("What kind of potion do you want to take? ");
            if (p1.potions > 0 && p1.HP != p1.BaseHP)
            {
                Console.Write("(heal");
                if (p1.PotionMaxHP > 0)
                {
                    Console.Write(" or maxHP)");
                    kas = 2;
                }
                else
                {
                    Console.Write(")");
                    kas = 1;
                }
            }
            else if (p1.PotionMaxHP > 0)
            {
                Console.Write("(maxHP)");
                kas = 3;
            }
            Console.WriteLine("\nWrite \"gb\" to go back to commande.");

            if (kas == 0)
            {
                Console.WriteLine("You have zero potion.");
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

            //Premiere fonction
            string typePotion = ChoosePotions(kas);

            if (typePotion == "go back")
            {
                return false;
            }

            //Deuxieme fonction
            int numPotion = ChooseNumPotions(typePotion,p1);
            if (numPotion == 0)
            {
                return false;
            }

            //Troisième fonction
            ConsomePotions(typePotion, numPotion,p1);
            return true;
        }

        private static string ChoosePotions(int kas)
        {
            while (true)
            {
                Console.Write("--> ");
                string ChoosePotions = Console.ReadLine();
                if (kas == 1 && (ChoosePotions == "heal" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else if (kas == 2 && (ChoosePotions == "heal" || ChoosePotions == "maxHP" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else if (kas == 3 && (ChoosePotions == "maxHP" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else
                {
                    Console.Write("Choose a correct value.");
                    if (kas == 1)
                    {
                        Console.Write(" (heal or go back");
                    }
                    else if (kas == 2)
                    {
                        Console.Write(" (heal or maxHP or go back)");
                    }
                    else if (kas == 3)
                    {
                        Console.Write(" (maxHP or go back)");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static int ChooseNumPotions(string ChoosePotions, Player p1)
        {
            while (true)
            {
                int NumberOfPotionsMax = 0;
                Console.Write("How many potions do you want to take ? ");
                if (ChoosePotions == "heal")
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
                else if (ChoosePotions == "maxHP")
                {
                    NumberOfPotionsMax = p1.PotionMaxHP;
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

        private static void ConsomePotions(string ChoosePotions, int usedPotion, Player p1)
        {
            for (int i = 0; i < usedPotion; i++)
            {
                if (ChoosePotions == "heal")
                { p1.potions--; Heal(30, p1); }
                else if (ChoosePotions == "maxHP")
                { p1.PotionMaxHP--; UpHP(30, p1); }
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
    }
}
