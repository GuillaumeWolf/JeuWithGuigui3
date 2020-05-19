using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace JeuWithGuigui3
{
    class Room
    {
        //Sall
        static public int LegendaryRoom = 1;
        static public int MarchandRoom = 5 + LegendaryRoom;
        static public int ChanceNothing = 15 + MarchandRoom;
        static public int ChanceTrap = 15 + ChanceNothing;
        static public int ChanceMonster = 60 + ChanceTrap;

        static readonly int[] RoomsChances = {LegendaryRoom, ChanceNothing, ChanceTrap, ChanceMonster};
                // - Sall temporelle          
                //static private int Chance2Monster = 10;


        static public void EnterRoom(Player p1)
        {

            int _LegendaryRoom = LegendaryRoom;
            int _ChanceNothing = ChanceNothing - (Game.RoomCount - 1) * 1;
            int _ChanceTrap = ChanceTrap - (Game.RoomCount - 1) * 1;
            int _ChanceMonster = ChanceMonster + (Game.RoomCount - 1) * 2;

            int[] RoomsChances = { _LegendaryRoom, _ChanceNothing, _ChanceTrap, _ChanceMonster };
            int x = RandomInt(_ChanceMonster + 1);

            Console.WriteLine();
            Console.WriteLine("                                                    LegendaryRoom: {0}. ChanceNothing: {1}. ChanceTrap: {2}. ChanceMonster: {3}. x: {4}", _LegendaryRoom, _ChanceNothing, _ChanceTrap, _ChanceMonster, x);
            Console.WriteLine();

            string sortie = "";
            bool isDead = false;
            var indexRoom = RoomsChances.Count()- RoomsChances
                .Where(y => y > x)
                .Count();

            Console.WriteLine("IndexRoom = {0}.", indexRoom);

            if (Game.RoomCount != GolemOfArmagedon.RoomOfSpawning)
            {
                switch (indexRoom)
                {
                    case 0:
                        EnterLegendaryRoom(p1);
                        break;
                    case 1:
                        EnterEmptyRoom(p1);
                        break;
                    case 2:
                        isDead = EnterTrapRoom(p1);
                        break;
                    case 3:
                        sortie = EnterMonsterRoom(p1);
                        break;
                }
            }
            else 
            {
                sortie = EnterMonsterRoom(p1);        
            }

            if (isDead)
            {
                return;
            }
            if (sortie == "BoosDead")
            {
                Console.WriteLine("You finished the game. GG !!");
                return;
            }
            else if (sortie == "ex")
            {
                return;
            }
            else if (sortie == "Alive" && Game.RoomCount == 50)
            {
                return;
            }

            Commandes.Commande(p1, null);
            return;
        }

        static private void EnterLegendaryRoom(Player p1)
        {
            Console.WriteLine("You enter the Legendary Room and you found the Legendary Sword.");
            Weapon w1 = new LegendarySword();
            Weapon.AddWeapon(p1, w1, null);
            LegendaryRoom = -1;
        }


        static private void EnterEmptyRoom(Player p1)
        {
            Console.Write("You enter a empty room. ");
            int y = RandomInt(101);
            int ChanceOfLooting = 30;
            int ChanceOfNothing = 70 + ChanceOfLooting;

            if (y < ChanceOfLooting)
            {
                Console.WriteLine("But there is loot inside");
                Loot.GetRandomLoot(p1, null);
            }
            else if (y < ChanceOfNothing)
            {
                Console.WriteLine("Totaly empty...");
            }

        }


        static private bool EnterTrapRoom(Player p1)
        {
            Console.WriteLine("You enter a tricked room.");
            int y = RandomInt(1001);
            int DeadTrap = 5;
            int FireTrap = 100 + DeadTrap;
            int PicTrap = 300 + FireTrap;
            int RockTrap = 595 + PicTrap;

            //Console.WriteLine("                                                    DeadTrap: {0}. FireTrap: {1}. PicTrap: {2}. RockTrap: {3}. y: {4}", DeadTrap, FireTrap, PicTrap, RockTrap, y);

            if (y < DeadTrap)
            {
                Console.WriteLine("You found a legendary sword !!!!\n\nNo I'm kiding, YOU ARE DEAD ! ");
                p1.HP = 0;
            }
            else if (y < FireTrap)
            {
                Console.WriteLine("You get burn in fire. You lost 50 HP");
                p1.HP -= 50;
            }
            else if (y < PicTrap)
            {
                Console.WriteLine("A trap open under your feet. You fall into it and loos 20 HP but you can reach the top and get out of here.");
                p1.HP -= 20;
            }
            else if (y < RockTrap)
            {
                Console.WriteLine("A big rock fall on you. You lost 10 HP.");
                p1.HP -= 10;
            }
            
            
            bool isDead = p1.CheckDie();
            //Autre loot
            int z = RandomInt(100);
            if (z < 10)
            {
                Console.Write("You are lucky, there is another loot here. ");
                Loot.GetRandomLoot(p1, null);
            }
            else if (z < 20)
            {
                Console.Write("Hey, there is other potions here ! ");
                Loot.LootPotion(p1, z / 10);
            }
            Console.WriteLine();
            return isDead;
        }



        static private string EnterMonsterRoom(Player p1)
        {
            //Mettre write au lieu de writeline
            Console.WriteLine("You enter a room and there is a monster. ");
            Monster m1 = Monster.CreatRandomMonster(p1);

            Console.WriteLine("It's a {0}.", m1.Name);

            if (p1.COfP.ClassName == "Druide" && m1.Name != "Golem of Armagedon")
            { p1.COfP.ClassCapacity(p1, m1); }
            if (m1.Name == "Golem of Armagedon")
            {
                Commandes.SpecialBossCommande(p1);
                p1.FightBoos = true;
            }

            string sortie = Fight_Organizer.Fight(p1, m1);
            if (sortie == "BoosDead")
            {
                return sortie;
            }
            else if (sortie == "ex")
            {
                p1.InFight = false;
                return sortie;
            }
            else if (sortie == "Dead")
            {
                p1.InFight = false;
                return "ex";
            }
            else if (sortie == "es")
            {
                p1.InFight = false;
                return sortie;
            }    
            Loot.GetRandomLoot(p1, m1);
            Loot.LootPotion(p1, m1.Numpotion);
            Loot.LootMoney(p1, m1.PODrop);
            int z = RandomInt(100);
            if (z < 10)
            {
                Console.Write("You are lucky, there is another loot here. ");
                Loot.GetRandomLoot(p1, null);
            }
            else if (z < 20)
            {
                Console.Write("Hey, there is other potions here ! ");
                Loot.LootPotion(p1, z / 10);
            }
            Console.WriteLine();
            return "";
        }

        static public int RandomInt(int Max)
        {
            int randomNum;
            var rand = new Random();
            randomNum = rand.Next(Max);
            return randomNum;
        }
    }
}
