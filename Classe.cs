using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JeuWithGuigui3
{
    class ClassPlayer
    {
        public string ClassName { get; set; }
        public virtual void ClassCapacity(Player p1, Monster m1)
        {
            Console.Write("You have no capacity");
        }
    } 
    
    class Mage : ClassPlayer
    {
        public static double MagicUp = 25;
        public Mage()
        {
            ClassName = "Mage";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            p1.MagicDmg *= (100 + MagicUp)/100 ;
        }

    }
    class Warrior : ClassPlayer
    {
        public static double ClassicUp = 25;
        public Warrior()
        {
            ClassName = "Warrior";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            p1.Damage *= (100 + ClassicUp) / 100;
        }
    }
    class Thief : ClassPlayer
    {
        public Thief()
        {
            ClassName = "Thief";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            m1.ChanceOfLoot += 30;
        }
    }
    class Druide : ClassPlayer
    {
        public Druide()
        {
            ClassName = "Druide";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            Console.WriteLine("Do you want to turn yourself into a Monster? (yes or no)");
            string reptransfo;
            while (true)
            {
                reptransfo = Console.ReadLine();
                if (reptransfo == "yes")
                {
                    p1.changableName = p1.name + " the " + m1.Name;
                    p1.BaseHP = m1.Vie;
                    p1.HP = m1.Vie;
                    p1.BaseDamage = m1.Dmg;
                    if (m1.PoisonDmg)
                    {
                        p1.Poison = true;
                    }
                    if (m1.FireDmg)
                    {
                        p1.Fire = true;
                    }
                    break;
                }
                if (reptransfo == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct answer");
                }
            }
            
        }
    }
}
