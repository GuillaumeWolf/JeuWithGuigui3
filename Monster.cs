using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace JeuWithGuigui3
{
    abstract class Monster
    {
        // - crée des dégats magique pour les monstres

        #region public propreties
        //Name
        public string Name;

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

        #endregion

        public void MonsterCapacity(Monster m1)
        {
            int x = RandomInt(2);
            if (m1.Name == "Gobelin" && Vie <= 0 && !m1.Usepower)
            {
                m1.Vie = 1;
                m1.Usepower = true;
                Console.Write("The gobelin survived with 1 HP.");
            }
            if (m1.Name == "Great Gobelin" && Vie <= 0 && !m1.Usepower)
            {
                m1.Vie = m1.MaxLife / 2 ;
                m1.Usepower = true;
                Console.Write("The Great Gobelin restore half of his life.");
            }
            if (m1.Name == "Gobelin" && Vie <= 0 && !m1.Usepower)
            {
                m1.Vie = 1;
                m1.Usepower = true;
                Console.Write("The gobelin survived with 1 HP.");
            }
            if (m1.Name == "Zombie" && Vie <= 0 && x == 0)
            {
                m1.Vie = m1.MaxLife;
                m1.Dmg -= 5;
                Console.WriteLine("The Zombie isn't dead!");
            }
            if (m1.Name == "Evolved Zombie" && Vie <= 0 && x == 0)
            {
                m1.Vie = m1.MaxLife;
                m1.Dmg -= 5;
                Console.WriteLine("The Zombie isn't dead!");
            }
            if (m1.Name == "Ancient Zombie" && Vie <= 0 )
            {
                int y = RandomInt(3);
                if( y == 0 || y == 1)
                {
                    m1.Vie = m1.MaxLife;
                    Console.WriteLine("The Zombie isn't dead!");
                }
            }
            if (m1.Name == "Golem of Armagedon" && m1.Vie < m1.MaxLife / 2 && !m1.Usepower)
            {
                Console.WriteLine("The Golem of Armagedon is angry !He enter the \"RAAAAGE MOD\"!");
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
                if  (finalDmg < 0)
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

            int golem = 0;
            int basilic = 0; 
            int dragon = 0;
            int evolveghost = 0;
            int evolvezombie = 0;
            int evolvegobelin = 0;
            int evolveslime = 0;
            int ghost = 0;
            int zombie = 0;
            int gobelin = 0;
            int slime = 0;

            if (Game.RoomCount <= 10)
            {
                basilic = Basilic.ChanceOfSpawn;
                dragon = Dragon.ChanceOfSpawn + basilic;
                ghost = Ghost.ChanceOfSpawn + dragon;
                zombie = Zombie.ChanceOfSpawn + ghost;
                gobelin = Gobelin.ChanceOfSpawn + zombie;
                slime = Slime.ChanceOfSpawn + gobelin;
            }
            else if (Game.RoomCount <= 20)
            {
                basilic = Basilic.ChanceOfSpawn2;
                dragon = Dragon.ChanceOfSpawn2 + basilic;
                evolveghost = Ghost.ChanceOfSpawn + dragon;
                evolvezombie = Zombie.ChanceOfSpawn + evolveghost;
                evolvegobelin = Gobelin.ChanceOfSpawn + evolvezombie;
                evolveslime = Slime.ChanceOfSpawn + evolvegobelin;
            }
            else if (Game.RoomCount <= 30)
            {
                basilic = Basilic.ChanceOfSpawn;
                dragon = Dragon.ChanceOfSpawn + basilic;
                evolveghost = Ghost.ChanceOfSpawn + dragon;
                evolvezombie = Zombie.ChanceOfSpawn + evolveghost;
                evolvegobelin = Gobelin.ChanceOfSpawn + evolvezombie;
                evolveslime = Slime.ChanceOfSpawn + evolvegobelin;
            }

            int x = RandomInt(slime);



            Monster m1 = null;
            if (x < golem || Game.RoomCount == GolemOfArmagedon.RoomOfSpawning)
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
            else if (x < evolveghost)
            {
                m1 = new EvolvedGhost();
                return m1;
            }
            else if (x < evolvezombie)
            {
                m1 = new EvolvedZombie();
                return m1;
            }
            else if (x < evolvegobelin)
            {
                m1 = new EvolvedGobelin();
                return m1;
            }
            else if (x < evolveslime)
            {
                m1 = new EvolvedSlime();
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
                Console.WriteLine("Basilic: {0}. Dragon: {1}. evolveghost: {6}. evolvezombie: {7}. evolvegobelin: {8}. evolveslime: {9}. Ghost: {2}. Gobelin: {3}. Slime: {4}. x: {5}", basilic, dragon, ghost, gobelin, slime, x, evolveghost, evolvezombie, evolvegobelin, evolveslime);
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


    //Slime:
    //Slime 1-10
    class Slime : Monster
    {
        public static int ChanceOfSpawn = 35;
        public Slime()
        {
            //Name
            Name = "Slime";

            //Vie
            MaxLife = 40;
            Vie = MaxLife;
            baseDmg = 10;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 0;
            Numpotion = 1;
            PODrop = 10;
            POCost = 20;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Slime 11-20
    class EvolvedSlime : Monster
    {
        public static int ChanceOfSpawn = 35;
        public EvolvedSlime()
        {
            //Name
            Name = "Evolved Slime";

            //Vie
            MaxLife = 80;
            Vie = MaxLife;
            baseDmg = 20;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 5;
            Numpotion = 2;
            PODrop = 20;
            POCost = 40;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Slime 21-30
    class MagmaSlime : Monster
    {
        public static int ChanceOfSpawn = 35;
        public MagmaSlime()
        {
            //Name
            Name = "Magma Slime";

            //Vie
            MaxLife = 120;
            Vie = MaxLife;
            baseDmg = 30;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = true;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 10;
            Numpotion = 4;
            PODrop = 40;
            POCost = 80;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }


    //Gobelin
    //Gobelin 1-10
    class Gobelin : Monster
    {
        public static int ChanceOfSpawn = 35;
        public Gobelin()
        {
            //Name
            Name = "Gobelin";

            //Vie
            MaxLife = 15;
            Vie = MaxLife;
            baseDmg = 20;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 0;
            Numpotion = 2;
            PODrop = 15;
            POCost = 30;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Gobelin 11-20
    class EvolvedGobelin : Monster
    {
        public static int ChanceOfSpawn = 35;
        public EvolvedGobelin()
        {
            //Name
            Name = "Evolved Gobelin";

            //Vie
            MaxLife = 30;
            Vie = MaxLife;
            baseDmg = 40;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 5;
            Numpotion = 4;
            PODrop = 30;
            POCost = 60;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Gobelin 21-30
    class GreatGobelin : Monster
    {
        public static int ChanceOfSpawn = 35;
        public GreatGobelin()
        {
            //Name
            Name = "Great Gobelin";

            //Vie
            MaxLife = 90;
            Vie = MaxLife;
            baseDmg = 60;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 10;
            Numpotion = 8;
            PODrop = 60;
            POCost = 120;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }


    //Zombie
    //Zombie 1-10
    class Zombie : Monster
    {
        public static int ChanceOfSpawn = 15;
        public Zombie()
        {
            //Name
            Name = "Zombie";

            //Vie
            MaxLife = 40;
            Vie = MaxLife;
            baseDmg = 30;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 8;
            Numpotion = 3;
            PODrop = 40;
            POCost = 100;

            //Capacité utilisé du monstre
            Usepower = false;
        }

    }
    //Zombie 11-20
    class EvolvedZombie : Monster
    {
        public static int ChanceOfSpawn = 15;
        public EvolvedZombie()
        {
            //Name
            Name = "Evolved Zombie";

            //Vie
            MaxLife = 80;
            Vie = MaxLife;
            baseDmg = 60;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 16;
            Numpotion = 6;
            PODrop = 80;
            POCost = 200;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Zombie 21-30
    class AncientZombie : Monster
    {
        public static int ChanceOfSpawn = 15;
        public AncientZombie()
        {
            //Name
            Name = "Ancient Zombie";

            //Vie
            MaxLife = 160;
            Vie = MaxLife;
            baseDmg = 60;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 32;
            Numpotion = 12;
            PODrop = 160;
            POCost = 400;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }


    //Ghost
    //Ghost 1-10
    class Ghost : Monster
    {
        public static int ChanceOfSpawn = 10;
        public Ghost()
        {
            //Name
            Name = "Ghost";

            //Vie
            MaxLife = 90;
            Vie = MaxLife;
            baseDmg = 25;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 8;
            Numpotion = 3;
            PODrop = 40;
            POCost = 100;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Ghost 11-20
    class EvolvedGhost : Monster
    {
        public static int ChanceOfSpawn = 10;
        public EvolvedGhost()
        {
            //Name
            Name = "Evolved Ghost";

            //Vie
            MaxLife = 180;
            Vie = MaxLife;
            baseDmg = 50;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 16;
            Numpotion = 6;
            PODrop = 80;
            POCost = 200;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    //Ghost 21-30
    class GodGhost : Monster
    {
        public static int ChanceOfSpawn = 10;
        public GodGhost()
        {
            //Name
            Name = "God Ghost";

            //Vie
            MaxLife = 240;
            Vie = MaxLife;
            baseDmg = 75;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 32;
            Numpotion = 12;
            PODrop = 160;
            POCost = 400;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }



    class Dragon : Monster
    {
        public static int ChanceOfSpawn = 2;
        public static int ChanceOfSpawn2 = 5;
        public Dragon()
        {
            //Name
            Name = "Dragon";

            //Vie
            MaxLife = 500;
            Vie = MaxLife;
            baseDmg = 60;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = true;
            PoisonDmg = false;
            MagicResistance = true;

            // Pour les loots
            ChanceOfLoot = 30;
            Numpotion = 5;
            PODrop = 100;
            POCost = 200;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }
    class Basilic : Monster
    {
        public static int ChanceOfSpawn = 2;
        public static int ChanceOfSpawn2 = 5;
        public Basilic()
        {
            //Name
            Name = "Basilic";

            //Vie
            MaxLife = 350;
            Vie = MaxLife;
            baseDmg = 40;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = true;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 30;
            Numpotion = 5;
            PODrop = 100;
            POCost = 200;

            //Capacité utilisé du monstre
            Usepower = false;
        }
    }








    /// <summary>
    /// BOSS
    /// </summary>
    class GolemOfArmagedon : Monster
    {
        public static int ChanceOfSpawn = 0;
        public static int RoomOfSpawning = 25;

        public GolemOfArmagedon()
        {
            //Name
            Name = "Golem of Armagedon";

            //Vie
            MaxLife = 2000;
            Vie = MaxLife;
            baseDmg = 100;
            Dmg = baseDmg;

            //Capacité de feu, poison, etc...
            FireDmg = false;
            PoisonDmg = false;
            MagicResistance = false;

            // Pour les loots
            ChanceOfLoot = 1000000;
            Numpotion = 50000;
            PODrop = 1000000;
            POCost = 2000000;

            //Capacité utilisé du monstre
            Usepower = false;

            //Special
            Console.WriteLine("It is the final Fight. You can not run away.");

        }
    }
}
