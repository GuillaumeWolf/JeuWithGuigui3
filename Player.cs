using System;
using System.Collections.Generic;
using System.Text;

namespace JeuWithGuigui3
{
    class Player
    {
        #region public properties

        //Charactéristique 
        public readonly string name;
        public string Race;
        public static int BaseHP = 100;
        public int HP;
        //Dégats physique
        public static int BaseDamage = 10;
        public int Damage;
        //Dégats magiques
        public static int BaseMagicDmg = 20;
        public int MagicDmg;

        public bool EnemyMagicRes = false;

        //Charactérisitque des armes
        public bool Poison = false;
        public bool Fire = false;

        //Objets
        public int potions = 20;
        public int PotionMaxHP = 10;

        //Armes
        public Weapon weapon1 = null;
        public Weapon weapon2 = null;

        #endregion

        private Player(string name)
        {
            this.name = name;
            this.HP = BaseHP;
            this.Damage = BaseDamage;
            this.MagicDmg = BaseMagicDmg;
        }

        //Appelée dans Game.Main()
        public static Player CreatePlayer()
        {
            Console.WriteLine("What's your name ?");
            string PlayerName = Console.ReadLine();
            Console.WriteLine("What is your race ?\n(elf +5 damage or dwarf +10 HP)");
            string Race;
            while (true)
            {
                Race = Console.ReadLine();
                if (Race == "elf" || Race == "dwarf")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct race.");
                }
            }
            Player p1 = new Player(PlayerName);
            if (Race == "elf")
            {
                p1.Race = "elf";
                p1.Damage += 5;
                p1.MagicDmg += 5;
            }
            else if (Race == "dwarf")
            {
                p1.Race = "dwarf";
                BaseHP += 10;
                p1.HP += 10;
            }
            return p1;
        }

        //Fonction attaque
        public void Attak(Monster m1)
        {
            int Finaldmg;
            Console.WriteLine("You can choose between a magic attack or a classic attack");
            while (true)
            {
                string chooseattak = Console.ReadLine();
                if (chooseattak == "magic attack")
                {
                    Finaldmg = MagicDmg;
                    break;
                }
                if (chooseattak == "classic attack")
                {
                    Finaldmg = Damage;
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct command (magic attack or clasic attack).");
                }
            }

            m1.Vie -= Finaldmg;
            if (weapon1 != null)
            {
                if (weapon1.PoisonDamage)
                {
                    Poison = true;
                }
                if (weapon1.FireDamage)
                {
                    Fire = true;
                }
                if (weapon1.VoldeVie)
                {
                    Heal(weapon1.Dmg);
                }
            }
            if (weapon2 != null)
            {
                if (weapon2.FireDamage)
                {
                    Fire = true;
                }
                if (weapon2.PoisonDamage)
                {
                    Poison = true;
                }
                if (weapon2.VoldeVie)
                {
                    Heal(weapon2.Dmg);
                }
            }
            if (m1.Vie < 0)
            {
                m1.Vie = 0;
            }

            Console.WriteLine("The enemy lost {0} HP. He is {1} HP left.", Finaldmg, m1.Vie);
        }
        
        
        //Fonction potion
        public bool UsePotions()
        {
            int kas = 0; 
            Console.Write("What kind of potion do you want to take? ");
            if (potions > 0)
            { 
                Console.Write("(heal");
                if (PotionMaxHP > 0 && HP != BaseHP)
                {
                    Console.Write(" or maxHP)");
                    kas = 2;
                }
                else
                {
                    Console.Write(")");
                    kas = 1;
                }
            }
            else if (PotionMaxHP > 0)
            {
                Console.Write("(maxHP)");
                kas = 3;
            }
            Console.WriteLine("\nWrite \"go back\" to go back to commande.");

            if (kas == 0)
            {
                Console.WriteLine("You have zero potion.");
                return false;
            }

            //Premiere fonction
            string typePotion = ChoosePotions(kas);
            
            if (typePotion == "go back")
            {
                return false;
            }

            //Deuxieme fonction
            int numPotion = ChooseNumPotions(typePotion);
            if (numPotion == 0)
            {
                return false;
            }

            //Troisième fonction
            ConsomePotions(typePotion, numPotion);

            return true;
        }

        private string ChoosePotions(int kas)
        {
            while (true)
            {
                Console.Write("--> ");
                string ChoosePotions = Console.ReadLine();
                if (kas == 1 && (ChoosePotions == "heal" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else if (kas == 2 && (ChoosePotions == "heal" || ChoosePotions == "maxHP" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else if (kas == 3 && (ChoosePotions == "maxHP" || ChoosePotions == "go back"))
                {
                    return ChoosePotions;
                }
                else
                {
                    Console.Write("Choose a correct value.");
                    if (kas == 1)
                    {
                        Console.Write(" (heal or go back");
                    }
                    else if (kas == 2)
                    {
                        Console.Write(" (heal or maxHP or go back)");
                    }
                    else if (kas == 3)
                    {
                        Console.Write(" (maxHP or go back)");
                    }
                    Console.WriteLine();
                }
            }
        }

        private int ChooseNumPotions(string ChoosePotions)
        {
            while (true)
            {
                int NumberOfPotions = 0;
                Console.Write("How many potions do you want to take ? ");
                if (ChoosePotions == "heal")
                {
                    NumberOfPotions = potions;
                }
                else if (ChoosePotions == "maxHP")
                {
                    NumberOfPotions = PotionMaxHP;
                }
                Console.WriteLine("(max {0})\nWrite \"back\" to go back to commande.\n--> ", NumberOfPotions);

                string Rep = Console.ReadLine();
                int intRep = 0;
                try
                {
                    intRep = Convert.ToInt32(Rep);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Choose a number");
                    continue;
                }
                if (intRep <= 0)
                {
                    Console.WriteLine("The choosen number isn't correct.");
                    continue;
                }
                
            }
        }

        private void ConsomePotions(string ChoosePotions, int intRep)
        {
            for (int i = 0; i < intRep; i++)
            {
                if (ChoosePotions == "heal")
                {
                    Heal(30);
                    Console.Write("You used a heal potions.");
                }
                else if (ChoosePotions == "maxHP")
                {
                    UpHP(30);
                    Console.Write("You used a MaxHP potions.");
                }
            }
            Console.WriteLine("You used {0} postions.",intRep);
        }

        //Modifie les stats
        private void Heal(int x)
        {
            if (HP + x > BaseHP)
            {
                Console.WriteLine("You heal {0} HP. You are full HP.", BaseHP - HP);
                HP = BaseHP;
            }

            else
            {
                HP += x;
                Console.WriteLine("You heal {0} HP.", x);
            }
        }

        private void UpHP(int x)
        {
            BaseHP += x;
        }

        //Mort
        public bool CheckDie()
        {
            bool isDead = false;
            if (HP <= 0)
            {
                isDead = Die();
            }
            return isDead;
        }

        private bool Die()
        {
            Console.WriteLine("You die.");
            return true;
        }


        //Augmente les objets
        public void GetPotions(int x)
        {
            potions += x;
            Console.WriteLine("You find {0} potions. You have {1} potions.", x, potions);
        }
        public void GetMaxHPPotions(int x)
        {
            PotionMaxHP += x;
            Console.WriteLine("You find {0} MaxHP+ potions. You have {1} MaxHP+ potions.", x, PotionMaxHP);
        }


        //Rafraichi les damages
        public void ChangeDamage()
        {
            if (weapon1 == null && weapon2 == null)
            {
                Damage = BaseDamage;
                MagicDmg = BaseMagicDmg;
            }
            else if (weapon1 == null && weapon2 != null)
            {
                MagicDmg = BaseMagicDmg + weapon2.MagicDamage;
                Damage = BaseDamage + weapon2.Dmg;
            }
            else if (weapon1 != null && weapon2 == null)
            {
                MagicDmg = BaseMagicDmg + weapon1.MagicDamage;
                Damage = BaseDamage + weapon1.Dmg;
            }
            else if (weapon1 != null && weapon2 != null)
            {
                MagicDmg = BaseMagicDmg + weapon2.MagicDamage + weapon1.MagicDamage;
                Damage = BaseDamage + weapon1.Dmg + weapon2.Dmg;
            }
        }
    }
}
