using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    abstract class Monster
    {
        public int Vie; //Fight
        public int Dmg; //Fight
        public bool FireDmg;
        public bool PoisonDmg;
        public bool MagicResistance;
        public int ChanceOfLoot;
        public string Name { get; set; }
        
        public void Attak(Player p1)
        {
            p1.HP -= Dmg;

            if (p1.HP < 0)
            {
                p1.HP = 0;
            }
            else
            {
                Console.WriteLine("You lost {0} HP. You are {1} HP left.", Dmg, p1.HP);
            }
        }
        
        public bool CheckDie()
        {
            bool isDead = false;
            if (Vie <= 0)
            {
                isDead = Die();
            }
            return isDead;
        }
        private bool Die()
        {
            Console.Write("The monster is dead. ");
            return true;
        }
        //Appelée avant dans Room
        static public Monster CreatRandomMonster(Player p1)
        {
            int x = RandomInt(101);
            int basilic = Basilic.ChanceOfSpawn + (Game.RoomCount - 1) * 1;
            int dragon = Dragon.ChanceOfSpawn + basilic;
            int ghost = Ghost.ChanceOfSpawn + dragon;
            int gobelin = Gobelin.ChanceOfSpawn + ghost;
            int slime = Slime.ChanceOfSpawn + gobelin;
            //Console.WriteLine("Basilic: {0}. Dragon: {1}. Ghost: {2}. Gobelin: {3}. Slime: {4}. x: {5}", basilic, dragon, ghost, gobelin, slime, x);
            Monster m1 = null;
            if (x < basilic)
            {
                m1 = new Basilic();
                return m1;
            }
            else if (x < dragon)
            {
                m1 = new Dragon();
                return m1;
            }
            else if (x < ghost)
            {
                m1 = new Ghost();
                return m1;
            }
            else if (x < gobelin)
            {
                m1 = new Gobelin();
                return m1;
            }
            else if (x < slime)
            {
                m1 = new Slime();
                return m1;
            }
            Console.Write("The room is empty. ");
            Game.GetLoot(p1, m1);
            return m1;
        }
        static public int RandomInt(int Max)
        {
            int randomNum;
            var rand = new Random();
            randomNum = rand.Next(Max);
            return randomNum;
        }
    }



    class Slime : Monster
    {
        public static int ChanceOfSpawn = 39;
        public Slime()
        {
            Name = "Slime";
            Vie = 20;
            Dmg = 5;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 5;
            MagicResistance = true;
        }
    }
    class Gobelin : Monster
    {
        public static int ChanceOfSpawn = 34;
        public Gobelin()
        {
            Name = "Gobelin";
            Vie = 15;
            Dmg = 10;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 5;
            MagicResistance = false;
        }
    }
    class Ghost : Monster
    {
        public static int ChanceOfSpawn = 25;
        public Ghost()
        {
            Name = "Ghost";
            Vie = 50;
            Dmg = 30;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 10;
            MagicResistance = false;
        }
    }
    class Dragon : Monster
    {
        public static int ChanceOfSpawn = 1;

        public Dragon()
        {
            Name = "Dragon";
            Vie = 300;
            Dmg = 40;
            FireDmg = true;
            PoisonDmg = false;
            ChanceOfLoot = 30;
            MagicResistance = true;
        }
    }
    class Basilic : Monster
    {
        public static int ChanceOfSpawn = 1;
        public Basilic()
        {
            Name = "Basilic";
            Vie = 250;
            Dmg = 25;
            FireDmg = false;
            PoisonDmg = true;
            ChanceOfLoot = 30;
            MagicResistance = false;
        }
    }
}
