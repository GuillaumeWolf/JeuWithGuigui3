﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JeuWithGuigui3
{
    abstract class Race
    {
        public string Name { get; protected set; }
        public string[] Propreties;
        public override string ToString()
        {

            return Name + "\n  - " + string.Join("\n  - ", Propreties) ;
        }

        public static string GetInfos()
        {
            Player p2 = new Player("wejhfvw");
            Elf elf1 = new Elf(p2);
            Dwarf dwarf1 = new Dwarf(p2);
            Cracheurdefeu cracheur1 = new Cracheurdefeu(p2);

            return "\n" + elf1.ToString() + "\n\n" + dwarf1.ToString() + "\n\n" + cracheur1.ToString() + "\n\n"; 
        }
    }

    class Elf : Race
    {
        static private string proprties1 = "+10 classic damage";
        static private string proprties2 = "+10 magic damage";
        static private string proprties3 = "Start the game with a dague";

        public Elf(Player p1)
        {
            Name = "Elf";
            Propreties = new string[] { proprties1, proprties2, proprties3};
            p1.BaseDamage += 5;
            p1.BaseMagicDmg += 5;
            p1.weapon1 = new Dague();
            p1.ChangeDamage();
        }

    }
    class Dwarf : Race
    {
        static private string proprties1 = "+30 HP";
        static private string proprties2 = "Start the game with a Armor";

        public Dwarf(Player p1)
        {
            Name = "Dwarf";
            Propreties = new string[] { proprties1, proprties2};

            p1.HP += 15;
            p1.BaseHP += 15;
            p1.armor= new MediumArmor();
            p1.ChangeDamage();
        }

    }
    class Cracheurdefeu : Race
    {
        static private string proprties1 = "+5 classic damage";
        static private string proprties2 = "Has a permanent fire effect";

        public Cracheurdefeu(Player p1)
        {
            Name = "Cracheur de feu";
            Propreties = new string[] { proprties1, proprties2};

            p1.BaseDamage += 5;
            p1.Fire = true;
            p1.ChangeDamage();
        }

    }

    class Minautore : Race
    {
        static private string proprties1 = "+20 classic damage";
        static private string proprties2 = "+10 HP";
        static private string proprtie3 = "Can't have Armor.";

        public Minautore(Player p1)
        {
            Name = "Minautore";
            Propreties = new string[] { proprties1, proprties2, proprtie3 };

            p1.BaseDamage += 20;
            p1.BaseHP += 10;
            p1.HP += 10;
            p1.ChangeDamage();
        }

    }

}
