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
        static private int LegendaryRoom = 1;
        static private readonly int ChanceNothing = 10 + LegendaryRoom;
        static private readonly int ChanceTrap = 20 + ChanceNothing;
        static private readonly int ChanceMonster = 69 + ChanceTrap;

        static readonly int[] RoomsChances = {LegendaryRoom, ChanceNothing, ChanceTrap, ChanceMonster};
                // - Sall temporelle          
                //static private int Chance2Monster = 10;


        static public void EnterRoom(Player p1, Monster m1)
        {

            int _LegendaryRoom = 1 ;
            int _ChanceNothing = 10 + _LegendaryRoom;
            int _ChanceTrap = 20 + _ChanceNothing - (Game.RoomCount - 1) * 1;
            int _ChanceMonster = 69 + _ChanceTrap + (Game.RoomCount - 1) * 1;

            int[] RoomsChances = { _LegendaryRoom, _ChanceNothing, _ChanceTrap, _ChanceMonster };
            int x = RandomInt(100);

            Console.WriteLine();
            //Console.WriteLine("                                                    LegendaryRoom: {0}. ChanceNothing: {1}. ChanceTrap: {2}. ChanceMonster: {3}. x: {4}", _LegendaryRoom, _ChanceNothing, _ChanceTrap, _ChanceMonster, x);
            Console.WriteLine();

            bool isDead = false;
            var indexRoom = RoomsChances.Count()- RoomsChances
                .Where(y => y > x)
                .Count();
            
            switch(indexRoom)
            {
                case 0:
                    EnterLegendaryRoom(p1, m1);
                    break;
                case 1:
                    EnterEmptyRoom(p1);
                    break;
                case 2:
                    isDead = EnterTrapRoom(p1);
                    break;
                case 3:
                    isDead = EnterMonsterRoom(p1);
                    break;
            }
            
            if (isDead)
            {
                return;
            }

            int z = RandomInt(100);
            if (z < 10)
            {
                Console.Write("You are lucky, there is another loot here. ");
                Loot.GetRandomLoot(p1, null);
            }
            else if (z < 20)
            {
                Console.Write("Hey, there is other potions here ! ");
                Loot.LootPotion(p1, z/10);
            }
            Console.WriteLine();

            Game.Commande(p1);
            return;
        }

        static private void EnterLegendaryRoom(Player p1, Monster m1)
        {
            Console.WriteLine("You enter the Legendary Room and you found the Legendary Sword.");
            Weapon w1 = new LegendarySword();
            Weapon.AddWeapon(p1, w1, m1);
            LegendaryRoom = -1;
        }

        static private bool EnterTrapRoom(Player p1)
        {
            Console.WriteLine("You enter a tricked room.");
            int y = RandomInt(1001);
            int DeadTrap = 5;
            int PicTrap = 195 + DeadTrap;
            int FireTrap = 300 + PicTrap;
            int RockTrap = 500 + FireTrap;
            
            //Console.WriteLine("                                                    DeadTrap: {0}. PicTrap: {1}. FireTrap: {2}. RockTrap: {3}. y: {4}", DeadTrap, PicTrap, FireTrap, RockTrap, y);


            if (y < RockTrap)
            {
                Console.WriteLine("A big rock fall on you. You loos 10 HP.");
                p1.HP -= 10;
            }
            else if (y < PicTrap)
            {
                Console.WriteLine("A trap open under your feet. You fall into it and loos 20 HP but you can reach the top and get out of here.");
                p1.HP -= 20;
            }
            else if (y < FireTrap)
            {
                Console.WriteLine("You get burn by a fire strik. You loos 50 HP");
                p1.HP -= 50;
            }
            else if (y < DeadTrap)
            {
                Console.WriteLine("A big rock fall on you. You loos 10 HP.");
                p1.HP = 0;
            }

            bool isDead = p1.CheckDie();
            return isDead;
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

        static private bool EnterMonsterRoom(Player p1)
        {
            //Mettre write au lieu de writeline
            Console.WriteLine("You enter a room and there is a monster. ");
            Monster m1 = Monster.CreatRandomMonster(p1);
            Console.WriteLine("It's a {0}.", m1.Name);
            if (p1.COfP.ClassName == "Druide")
            { p1.COfP.ClassCapacity(p1, m1); }
            bool Won = Fight_Organizer.Fight(p1, m1);
            if (!Won)
            {
                return !Won;
            }

            Loot.GetRandomLoot(p1, m1);
            Loot.LootPotion(p1, m1.Numpotion);
            return !Won;
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
