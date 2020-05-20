using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace JeuWithGuigui3

/*
à faire: 

    Obligatoirement :


    Nouveau (idée):
        - room pour acheter des armes(Guillaume -> confirmation de lidée par leander ??) 




*/
{
    class Game
    {
        //Attribut du jeu
        static public int RoomCount = 0;

        static void Main()
        {
            Console.WriteLine("(room 0)\n");
            Player p1 = Player.CreatePlayer();
            Commandes.Commande(p1, null);
            PlayAgain();
        }


        public static void PlayAgain()
        {
            while (true)
            {
                Console.WriteLine("\n\n\nDo you want to play again ? (yes or no)");
                Console.Write("--> ");
                string rep = Console.ReadLine();
                if (rep == "yes")
                {
                    Game.RoomCount = 0; 
                    Main();
                    return;
                }
                else
                {
                    return;
                }
            }
        }
    }
}




