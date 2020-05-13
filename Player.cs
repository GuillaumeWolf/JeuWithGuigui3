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
        public int potions = 2;
        public int PotionMaxHP = 1;

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

        public bool UsePotions()
        {
            Console.WriteLine("What kind of potion do you want to take? (Heal or MaxHP)\nWrite \"back\" to go back to commande.");
            while (true)
            {
                string rep = Console.ReadLine();
                if (rep == "Heal")
                {
                    if (HP != BaseHP)
                    {
                        if (potions != 0)
                        {
                            Console.WriteLine("How many potions do you want to take ? (max {0})\nWrite \"back\" to go back to commande.", potions);
                            while (true)
                            {
                                string stringPotions = Console.ReadLine();
                                if (stringPotions == "back")
                                {
                                    return false;
                                }
                                else
                                {
                                    Console.WriteLine("Choose a correct value.");
                                }
                                int numPotions = Convert.ToInt32(stringPotions);
                                if (numPotions <= potions)
                                {
                                    for (int i = 0; i < numPotions; i++)
                                    {
                                        potions -= 1;
                                        Console.WriteLine("You used a Heal potion.");
                                        Heal(30);
                                    }
                                    return true;
                                }
                                if (stringPotions == "back")
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("You don't have  Heal potions.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your HP is alreay full.");
                    }
                }
                else if (rep == "MaxHP")
                {
                    if (PotionMaxHP != 0)
                    {
                        Console.WriteLine("How many potions do you want to take ? (max {0})", PotionMaxHP);
                        while (true)
                        {
                            string stringPotions = Console.ReadLine();
                            int numPotions = Convert.ToInt32(stringPotions);
                            if (numPotions <= PotionMaxHP)
                            {
                                for (int i = 0; i < numPotions; i++)
                                {
                                    PotionMaxHP -= 1;
                                    Console.WriteLine("You used a MaxHP+ potion.");
                                    UpHP(30);
                                }
                                return true;
                            }
                            if (stringPotions == "back")
                            {
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("You don't have enough potion. (max potion {0})", PotionMaxHP);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have MaxHP+ potions.");
                        return false;
                    }
                }
                else if (rep == "back")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Please enter a correct value. (Heal or MaxHP)");
                }
            }
        }

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
