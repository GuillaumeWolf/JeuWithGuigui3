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
        public Race RaceOfPlayer = null ;
        public ClassPlayer COfP = new ClassPlayer();
        public int BaseHP = 100;
        public int HP;
        //Dégats physique
        public int BaseDamage = 10;
        public double Damage;
        //Dégats magiques
        public int BaseMagicDmg = 20;
        public double MagicDmg;

        //Charactérisitque des armes
        public bool Poison = false;
        public bool Fire = false;

        //Objets
        public int potions = 2;
        public int PotionMaxHP = 0;

        //Armes 
        public Weapon weapon1 = null;
        public Weapon weapon2 = null;
        public Armor armor = null;

        #endregion

        public Player(string name)
        {
            this.name = name;
            this.HP = BaseHP;
            this.Damage = BaseDamage;
            this.MagicDmg = BaseMagicDmg;
        }

        //Appelée dans Game.Main()
        public static Player CreatePlayer()
        {
            //Nom du joueur
            string PlayerName;
            while (true)
            {
                Console.WriteLine("What's your name ?");
                Console.Write("--> ");
                PlayerName = Console.ReadLine();
                //Check si sur
                /*
                Console.WriteLine("Are you sure {0} is your name ? (yes)", PlayerName);
                Console.Write("--> ");
                string rep = Console.ReadLine();
                if (rep == "yes")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Choose again.");
                }
                */
                break;
            }
            Player p1 = new Player(PlayerName);


            //Race du joueur
            string race;
            while (true)
            {
                Console.WriteLine("What is your race ?\n(elf (e) - dwarf (d) - Cracheur de feu (cf) - Minautaure (m))\n(Tap \"info\" for more information about the race.");
                Console.Write("--> ");
                race = Console.ReadLine();
                if (race == "e" || race == "d" || race == "cf" || race == "m")
                {
                    break;
                }
                else if (race == "info")
                {
                    Console.WriteLine(Race.GetInfos());
                }
                else
                {
                    Console.WriteLine("Choose a correct race.");
                }
            }
            switch (race)
            {
                case "e":
                    p1.RaceOfPlayer = new Elf(p1);
                    break;
                case "d":
                    p1.RaceOfPlayer = new Dwarf(p1);
                    break;
                case "cf":
                    p1.RaceOfPlayer = new Cracheurdefeu(p1);
                    break;
                case "m":
                    p1.RaceOfPlayer = new Minautore(p1);
                    break;
            }


            //Classe du joueur : 
            string classep;
            while (true)
            {
                Console.WriteLine("What is your class?\n Mage (m), Warrior (w), Thief (t) or Druide (d). \nWrite \"I\" to get more informations.");
                Console.Write("--> ");
                classep = Console.ReadLine();
                if (classep == "m" || classep == "w" || classep == "t" || classep == "d")
                {
                    break;            
                }
                if (classep == "I")
                {
                    Console.Write("\nMage : + {0}% of Magic damage.\nWarrior : + {1}% of Classic damage.\nThief : Increase your chance of getting better loot. \nDruide : Can transform itself in a monster.\n", Mage.MagicUp, Warrior.ClassicUp);
                }
                if (classep != "m" && classep != "w" && classep != "t" && classep != "d" && classep != "I")
                {
                    Console.WriteLine("Choose a valid command.");
                }
            }
            switch (classep)
            {
                case "m":
                    p1.COfP = new Mage();
                    break;
                case "w":
                    p1.COfP = new Warrior();
                    break;
                case "t":
                    p1.COfP = new Thief();
                    break;
                case "d":
                    p1.COfP = new Druide();
                    break;
            }
            return p1;
        }




        //Fonction attaque
        public void Attak(Monster m1)
        {
            double Finaldmg;
            Console.WriteLine("You can choose between a magic attack (ma) or a classic attack (ca)");
            while (true)
            {
                Console.Write("--> ");
                string chooseattak = Console.ReadLine();
                if (chooseattak == "ma")
                {
                    Finaldmg = MagicDmg;
                    if(m1.MagicResistance)
                    {
                        Finaldmg /= 2;
                    }
                    break;
                }
                else if (chooseattak == "ca")
                {
                    Finaldmg = Damage;
                    break;
                }
                else
                {
                    Console.WriteLine("Choose a correct command (ma or ca).");
                }
            }

            if (Poison)
            {
                int y = 5 * Game.tour;
                Finaldmg += y;
                Console.Write("The {0} take {1} damages from poisonning ! ", m1.Name);
            }

            if (Fire)
            {
                Finaldmg += 10;
                Console.Write("The {0} take {1} damages from fire ! ", m1.Name);
            }
            //Applique les dégats
            int finaldamageint = Convert.ToInt32(Finaldmg);
            m1.Vie -= finaldamageint;

            if (m1.Vie < 0)
            {
                m1.Vie = 0;
            }

            Console.WriteLine("The enemy lost {0} HP total. He is {1} HP left.", finaldamageint, m1.Vie);
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
                if (weapon2.PoisonDamage)
                {Poison = true;}
                else
                {Poison = false;}
                if (weapon2.FireDamage)
                { Fire = true;}
                else if (RaceOfPlayer.Name != "Cracheur de feu")
                {Fire = false;}
            }


            else if (weapon1 != null && weapon2 == null)
            {
                MagicDmg = BaseMagicDmg + weapon1.MagicDamage;
                Damage = BaseDamage + weapon1.Dmg;
                if (weapon1.PoisonDamage)
                { Poison = true; }
                else
                { Poison = false; }
                if (weapon1.FireDamage)
                { Fire = true; }
                else if (RaceOfPlayer != null && RaceOfPlayer.Name != "Cracheur de feu")
                { Fire = false; }

            }


            else if (weapon1 != null && weapon2 != null)
            {
                MagicDmg = BaseMagicDmg + weapon2.MagicDamage + weapon1.MagicDamage;
                Damage = BaseDamage + weapon1.Dmg + weapon2.Dmg;
                if (weapon1.PoisonDamage || weapon2.PoisonDamage)
                { Poison = true; }
                else
                { Poison = false; }
                if (weapon2.FireDamage || weapon1.PoisonDamage)
                { Fire = true; }
                else if (RaceOfPlayer.Name != "Cracheur de feu")
                { Fire = false; }
            }
        }
    }
}
