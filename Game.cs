using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace JeuWithGuigui3

/*
à faire: 

    Obligatoirement :
        - deplacer les commande dans une nouvelle classe

    Nouveau (idée):
        - créer un systeme d'argent(Guillaume -> confirmation de lidée par leander ??)  (enleve juste les poitn d'interogation si tu es daccord)
        - room pour acheter des armes(Guillaume -> confirmation de lidée par leander ??)  (enleve juste les poitn d'interogation si tu es daccord)
        - possibilité de fuir le combat contre de largent ? (Guillaume)


*/
{
    class Game
    {
        //Attribut du jeu
        static public int RoomCount = 0;
        static public Monster m1 = null;

        static void Main()
        {
            Console.WriteLine("(room 0)\n");
            Player p1 = Player.CreatePlayer();
            Commandes.Commande(p1, null);
        }
    }
}




