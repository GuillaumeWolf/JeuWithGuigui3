using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace JeuWithGuigui3
{
    class ClassPlayer
    {
        public string ClassName { get; set; }
        

    }
    
    class Mage : ClassPlayer
    {
        public static double MultiplicatorMagicDamage = 1.25;

        public Mage(Player p1)
        {
            ClassName = "Mage";
        }

        public int AddMagicDmg(double MagicDamage)
        {
            MagicDamage = 1.25;
        }
    }
    class Warrior : ClassPlayer
    {
        public Warrior(Player p1)
        {
            ClassName = "Warrior";
            p1.Damage *= 1.1;
        }
    }
}
