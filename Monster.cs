using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    abstract class Monster
    {
        //crée des dégats magique pour les monstres
        public int MaxLife; 
        public int Vie; //Fight
        public int Dmg; //Fight
        public bool FireDmg;
        public bool PoisonDmg;
        public bool MagicResistance;
        public bool inRage;
        public int ChanceOfLoot;
        public int Numpotion;
        public bool Usepower;
        public string Name { get; set; }

        public void DieNot(Monster m1)
        {
            if (m1.Name == "Gobelin" && Vie <= 0 && !m1.Usepower)
            {
                Vie = 1;
                m1.Usepower = true;
            }
        }

        public void Attak(Player p1)
        {
            int finalDmg = Dmg;
            if (inRage)
            { 
                finalDmg *= 2;
            }
            if (p1.armor != null)
            {
                finalDmg -= p1.armor.ClassicResistance;
                if(finalDmg < 0)
                {
                    finalDmg = 0;
                }
            }
            if (PoisonDmg)
            {
                int poisonDmg = 5 * Fight_Organizer.tour;
                finalDmg += poisonDmg;
                Console.Write("You take {0} damage from poisonning. ", poisonDmg);
            }
            if (FireDmg && p1.RaceOfPlayer.Name != "Cracheur de feu")
            {
                finalDmg += 15;
                Console.Write("You take 15 damage from fire. ");
            }
            //Inflige les degats
            p1.HP -= finalDmg;

            if (p1.HP < 0)
            {
                p1.HP = 0;
            }
            Console.WriteLine("You lost {0} HP. You are {1} HP left.", finalDmg, p1.HP);
        }
        
        public bool CheckDie(Monster m1)
        {
            bool isDead = false;
            if (Vie <= 0)
            {
                if (m1.Name == "Zombie")
                {
                    Zombie.ReviveZ(m1);
                }
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
            int basilic = Basilic.ChanceOfSpawn + (Game.RoomCount - 1) * 1;
            int dragon = Dragon.ChanceOfSpawn + basilic + (Game.RoomCount - 1) * 1;
            int ghost = Ghost.ChanceOfSpawn + dragon + (Game.RoomCount - 1) * 3;  //what pk y a un 3 ici?
            int zombie = Zombie.ChanceOfSpawn + ghost + (Game.RoomCount - 1) * 1;
            int gobelin = Gobelin.ChanceOfSpawn + ghost + (Game.RoomCount - 1) * 1;
            int slime = Slime.ChanceOfSpawn + gobelin + (Game.RoomCount - 1) * 1;
            int x = RandomInt(101);

            //Console.WriteLine("                                                    Basilic: {0}. Dragon: {1}. Ghost: {2}. Gobelin: {3}. Slime: {4}. x: {5}", basilic, dragon, ghost, gobelin, slime, x);

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
            else if (x < zombie)
            {
                m1 = new Zombie();
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
            if (m1 == null)
            {
                Console.WriteLine("Probleme de proba dans Monster().CreatrandomMonster()");
                m1 = new Slime();
            }
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
        public static int ChanceOfSpawn = 29;
        public Slime()
        {
            Name = "Slime";
            MaxLife = 20;
            Vie = MaxLife;
            Dmg = 5;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 0;
            Numpotion = 1;
            MagicResistance = true;
        }
    }
    class Gobelin : Monster
    {
        public static int ChanceOfSpawn = 29;
        public Gobelin()
        {
            Name = "Gobelin";
            MaxLife = 15;
            Vie = MaxLife;
            Dmg = 30;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 0;
            Numpotion = 2;
            MagicResistance = false;
            Usepower = false;
        }
    }
    class Ghost : Monster
    {
        public static int ChanceOfSpawn = 20;
        public Ghost()
        {
            Name = "Ghost";
            MaxLife = 50;
            Vie = MaxLife;
            Dmg = 30;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 4;
            Numpotion = 3;
            MagicResistance = false;
        }
    }
    class Zombie : Monster
    {
        public static int ChanceOfSpawn = 20;
        public Zombie()
        {
            Name = "Zombie";
            MaxLife = 30;
            Vie = MaxLife;
            Dmg = 10;
            FireDmg = false;
            PoisonDmg = true;
            ChanceOfLoot = 4;
            Numpotion = 3;
            MagicResistance = false;
        }
        public static void ReviveZ(Monster m1)
        {
            int x = RandomInt(2);
            if (x == 2)
            {
                m1.Vie = m1.MaxLife;
            }
        }
    }
    class Dragon : Monster
    {
        public static int ChanceOfSpawn = 1;
        public Dragon()
        {
            Name = "Dragon";
            MaxLife = 300;
            Vie = MaxLife;
            Dmg = 40;
            FireDmg = true;
            PoisonDmg = false;
            ChanceOfLoot = 30;
            Numpotion = 5;
            MagicResistance = true;
        }
    }
    class Basilic : Monster
    {
        public static int ChanceOfSpawn = 1;
        public Basilic()
        {
            Name = "Basilic";
            MaxLife = 250;
            Vie = MaxLife;
            Dmg = 25;
            FireDmg = false;
            PoisonDmg = true;
            ChanceOfLoot = 30;
            Numpotion = 5;
            MagicResistance = false;
        }
    }
    class GolemOfArmagedon : Monster
    {
        public static int ChanceOfSpawn = 0;

        public GolemOfArmagedon()
        {
            Name = "Golem of Armagedon";
            MaxLife = 1000;
            Vie = MaxLife;
            Dmg = 100;
            FireDmg = false;
            PoisonDmg = false;
            ChanceOfLoot = 100;
            MagicResistance = true;
            inRage = false;


        }
        public static void GolemRage(Monster m1)
        {
            m1.inRage = true;
            if (m1.inRage)
            {
                m1.FireDmg = true;
                m1.PoisonDmg = true;
            }
        }

    }
}
