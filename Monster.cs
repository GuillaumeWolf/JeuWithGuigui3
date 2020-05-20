using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    abstract class Monster
    {
        // - crée des dégats magique pour les monstres

        //Vie
        public int MaxLife; 
        public int Vie;
        public int baseDmg;
        public int Dmg; 
        //Capacité de feu, poison, etc...
        public bool FireDmg;
        public bool PoisonDmg;
        public bool MagicResistance;
        public bool inRage;
        // Pour les loots
        public int ChanceOfLoot;
        public int Numpotion;
        public int PODrop;
        public int POCost;
        //Capacité utilisé du monstre
        public bool Usepower;
        public string Name { get; set; }

        public void MonsterCapacity(Monster m1)
        {
            if (m1.Name == "Gobelin" && Vie <= 0 && !m1.Usepower)
            {
                m1.Vie = 1;
                m1.Usepower = true;
                Console.Write("The gobelin should has been killed but he survived with 1 HP: ");
            }
            if (m1.Name == "Zombie" && Vie <= 0 && !m1.Usepower)
            {
                m1.Vie = m1.MaxLife;
                m1.Dmg -= 10; 
                m1.Usepower = true;
                Console.WriteLine("The Zombie is dead one time but you have to kill him twice. But now he is a little bit weaker. ");
            }
            if (m1.Name == "Golem of Armagedon" && m1.Vie < m1.MaxLife / 2 && !m1.Usepower)
            {
                Console.WriteLine("The Golem of Armagedon is angry ! He enter the \"RAAAAGE MOD\"!");
                m1.Dmg = m1.baseDmg * 15 / 10;
                m1.FireDmg = true;
                m1.PoisonDmg = true;
                m1.inRage = true;
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
            int golem = GolemOfArmagedon.ChanceOfSpawn ;
            int basilic = Basilic.ChanceOfSpawn + golem + (Game.RoomCount - 1) * 1;
            int dragon = Dragon.ChanceOfSpawn + basilic + (Game.RoomCount - 1) * 1;
            int ghost = Ghost.ChanceOfSpawn + dragon + (Game.RoomCount - 1) * 2;
            int zombie = Zombie.ChanceOfSpawn + ghost + (Game.RoomCount - 1) * 1;
            int gobelin = Gobelin.ChanceOfSpawn + zombie + (Game.RoomCount - 1) * 1;
            int slime = Slime.ChanceOfSpawn + gobelin + (Game.RoomCount - 1) * 1;
            int x = RandomInt(101);
            
            if (Game.RoomCount == GolemOfArmagedon.RoomOfSpawning)
            {
                golem = 10000;
            }

            //Console.WriteLine("                                                    Basilic: {0}. Dragon: {1}. Ghost: {2}. Gobelin: {3}. Slime: {4}. x: {5}", basilic, dragon, ghost, gobelin, slime, x);

            Monster m1 = null;
            if (x < golem)
            {
                m1 = new GolemOfArmagedon();
                return m1;
            }
            else if (x < basilic)
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
        public static int ChanceOfSpawn = 25;
        public Slime()
        {
            Name = "Slime";
            MaxLife = 80;
            Vie = MaxLife;
            Dmg = 5;
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = true;
            // Loot
            ChanceOfLoot = 0;
            Numpotion = 1;
            PODrop = 10;
            POCost = 20;

        }
    }
    class Gobelin : Monster
    {
        public static int ChanceOfSpawn = 25;
        public Gobelin()
        {
            Name = "Gobelin";
            MaxLife = 15;
            Vie = MaxLife;
            Dmg = 30;
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;
            //Has a power
            Usepower = false;
            // Loot
            ChanceOfLoot = 0;
            Numpotion = 2;
            PODrop = 15;
            POCost = 30;
        }
    }
    class Zombie : Monster
    {
        public static int ChanceOfSpawn = 25;
        public Zombie()
        {
            Name = "Zombie";
            MaxLife = 60;
            Vie = MaxLife;
            Dmg = 30;
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = false;
            //Has a power
            Usepower = false;
            // Loot
            ChanceOfLoot = 8;
            Numpotion = 3;
            PODrop = 40;
            POCost = 100;
        }
        
    }
    class Ghost : Monster
    {
        public static int ChanceOfSpawn = 20;
        public Ghost()
        {
            Name = "Ghost";
            MaxLife = 100;
            Vie = MaxLife;
            Dmg = 40;
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;
            // Loot
            ChanceOfLoot = 8;
            Numpotion = 3;
            PODrop = 40;
            POCost = 100;
        }
    }
    class Dragon : Monster
    {
        public static int ChanceOfSpawn = 2;
        public Dragon()
        {
            Name = "Dragon";
            MaxLife = 500;
            Vie = MaxLife;
            Dmg = 60;
            FireDmg = true;
            PoisonDmg = false;
            MagicResistance = true;
            // Loot
            ChanceOfLoot = 30;
            Numpotion = 5;
            PODrop = 100;
            POCost =200;
        }
    }
    class Basilic : Monster
    {
        public static int ChanceOfSpawn = 2;
        public Basilic()
        {
            Name = "Basilic";
            MaxLife = 350;
            Vie = MaxLife;
            Dmg = 35;
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = false;
            // Loot
            ChanceOfLoot = 30;
            Numpotion = 5;
            PODrop = 100;
            POCost = 200;
        }
    }

    class GolemOfArmagedon : Monster
    {
        public static int ChanceOfSpawn = 1;
        public static int RoomOfSpawning = 25;

        public GolemOfArmagedon()
        {
            Name = "Golem of Armagedon";
            MaxLife = 2000;
            Vie = MaxLife;
            baseDmg = 100;
            Dmg = 100;
            FireDmg = false;
            PoisonDmg = false;
            Usepower = false;
            ChanceOfLoot = 100000;
            MagicResistance = true;
            inRage = false;
            Console.WriteLine("It is the final Fight. You can not run away.");

        }
    }
}
