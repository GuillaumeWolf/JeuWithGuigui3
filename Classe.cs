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
        public Mage()
        {
            ClassName = "Mage";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            if (ClassName == "Mage")
            {
                p1.MagicDmg *= 1.25;
            }
        }

    }
    class Warrior : ClassPlayer
    {
        public Warrior()
        {
            ClassName = "Warrior";
        }
        public override void ClassCapacity(Player p1, Monster m1)
        {
            if (ClassName == "Warrior")
            {
                p1.Damage *= 1.25;
            }
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
            if (ClassName == "Thief")
            {
                m1.ChanceOfLoot *= 2;
            }
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
            if (ClassName == "Druide")
            {
                Console.WriteLine("Do you want to turn yourself into a Monster? (yes or no)");
                string reptransfo;
                while (true)
                {
                    reptransfo = Console.ReadLine();
                    if (reptransfo == "yes")
                    {
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
}
