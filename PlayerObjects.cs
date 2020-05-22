using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class PlayerObjects
    {
        //Pierre denchentement Blanc
        public static bool UseBlancEnchantStone (Player p1)
        {
            int x = 0;
            while (true)
            {
                Console.WriteLine("You have {0} stones, how many do you want to use ? \n(Write \"gb\" to go back)", p1.EnchantStone);
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "gb")
                {
                    return false;
                }
                try
                {
                    x = Convert.ToInt32(rep);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Not correct.");
                    continue;
                }
                if (x > p1.EnchantStone || x <= 0)
                {
                    Console.WriteLine("Not correct number.");
                    continue;
                }
                break;
            }
            while (true)
            {
                if (p1.weapon1 != null && p1.weapon2 == null)
                {
                    Console.WriteLine("You have a {0}. You want to use the stone on this weapons ? (if yes write \"1\") \n(Write \"gb\" to go back)", p1.weapon1.Name);
                }
                else if (p1.weapon2 != null && p1.weapon1 == null)
                {
                    Console.WriteLine("You have a {0}. You want to use the stone on this weapons ? (if yes write \"1\") \n(Write \"gb\" to go back)", p1.weapon2.Name);
                }
                else if ( p1.weapon1 != null && p1.weapon2 != null)
                {
                    Console.WriteLine("You have a {0} and a {1}. On whiche one would you use the stone ? (1 or 2)\n(Write \"gb\" to go back)", p1.weapon1.Name, p1.weapon2.Name);
                }
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "gb")
                {
                    return false;
                }
                else if (rep == "1")
                {
                    int repInt = Convert.ToInt32(rep);
                    bool aa = ChooseBetweenMagicAndClassic(p1, repInt, 15*x);
                    if (!aa)
                    {
                        continue;
                    }
                    break;
                }
                else if (rep == "2")
                {
                    int repInt = Convert.ToInt32(rep);
                }
            }
            p1.EnchantStone -= x;
            return true;
        }

        //Pierre denchentement Bleu
        public static bool UseBleuEnchantStone(Player p1)
        {
            int x = 0;
            while (true)
            {
                if (p1.weapon1 != null && p1.weapon2 == null)
                {
                    Console.WriteLine("You have a {0}. You want to use the stone on this weapons ? (if yes write \"1\") \n(Write \"gb\" to go back)", p1.weapon1.Name);
                }
                else if (p1.weapon2 != null && p1.weapon1 == null)
                {
                    Console.WriteLine("You have a {0}. You want to use the stone on this weapons ? (if yes write \"1\") \n(Write \"gb\" to go back)", p1.weapon2.Name);
                }
                else if (p1.weapon1 != null && p1.weapon2 != null)
                {
                    Console.WriteLine("You have a {0} and a {1}. On whiche one would you use the stone ? (1 or 2)\n(Write \"gb\" to go back)", p1.weapon1.Name, p1.weapon2.Name);
                }
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "gb")
                {
                    return false;
                }
                try
                {
                    x = Convert.ToInt32(rep);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Not correct.");
                    continue;
                }
                if (x > p1.EnchantStone || x <= 0)
                {
                    Console.WriteLine("Not correct number.");
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.WriteLine("You have a {0} and a {1}. On whiche one would you use the stone ? (1 or 2)\n(Write \"gb\" to go back)", p1.weapon1.Name, p1.weapon2.Name);
                Console.Write(" --> ");
                string rep = Console.ReadLine();
                if (rep == "gb")
                {
                    return false;
                }
                else if (rep == "1")
                {
                    int repInt = Convert.ToInt32(rep);
                    bool aa = ChooseBetweenMagicAndClassic(p1, repInt, 30 * x);
                    if (!aa)
                    {
                        continue;
                    }
                    break;
                }
                else if (rep == "2")
                {
                    int repInt = Convert.ToInt32(rep);
                }
            }
            p1.EnchantStone -= x;
            return true;
        }



        public static bool ChooseBetweenMagicAndClassic(Player p1, int repInt, int y)
        {
            while(true)
            {
                Console.WriteLine("You have {0} Classic Attack and {1} Magic Attack. What type do you want to up ? (ca - ma)\n(Write \"gb\" to go back)", p1.Damage, p1.MagicDmg);
                Console.Write(" --> ");
                string rep2 = Console.ReadLine();
                if (rep2 == "gb")
                {
                    return false;
                }
                else if ( rep2 == "ca")
                {
                    AddClassicToWeapon(p1, repInt, y);
                    break;
                }
                else if (rep2 == "ma")
                {
                    AddMagicToWeapon(p1, repInt, y);
                    break;
                }
            }
            return true;
        }


        public static void AddClassicToWeapon(Player p1, int repInt, int y)
        {
            if (repInt == 1)
            {
                p1.weapon1.ClassicDmg += y;
                Player.ChangeDamage(p1);
                Console.WriteLine("You add {0} Damage to your {1}.", y, p1.weapon1.Name);
            }
            else
            {
                p1.weapon2.ClassicDmg += y;
                Player.ChangeDamage(p1);
                Console.WriteLine("You add {0} Damage to your {1}.", y, p1.weapon2.Name);
            }
        }

        public static void AddMagicToWeapon(Player p1, int repInt, int y)
        {
            if (repInt == 1)
            {
                p1.weapon1.MagicDmg += y;
                Player.ChangeDamage(p1);
                Console.WriteLine("You add {0} Magic Damage to your {1}.", y, p1.weapon1.Name);
            }
            else
            {
                p1.weapon2.MagicDmg+= y;
                Player.ChangeDamage(p1);
                Console.WriteLine("You add {0} Magic Damage to your {1}.", y, p1.weapon2.Name);
            }
        }
    }
}
