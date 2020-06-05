using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    class ConsoleUserInterface
    {
        // Empty space
        private const string EMPTY = null;

        // Colors
        private readonly ConsoleColor defBackground = Console.BackgroundColor;
        private readonly ConsoleColor defForeground = Console.ForegroundColor;
        private readonly ConsoleColor playerColor = ConsoleColor.Yellow;
        private readonly ConsoleColor enemyColor = ConsoleColor.Red;
        private readonly ConsoleColor powerUpColor = ConsoleColor.Blue;

        // other variables
        private ConsoleKeyInfo cki;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleUserInterface()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.CursorVisible = false;
        }

        public void StartMenu()
        {
            do
            {
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. High Score");
                Console.WriteLine("3. Instructions");
                Console.WriteLine("4. Credits");
                Console.WriteLine("5. Quit");
                
                cki = Console.ReadKey(false);
                switch(cki.KeyChar.ToString())
                {
                    case "1":
                        Console.WriteLine("I'm playing wow");
                        break;
                    case "2":
                        Console.WriteLine("BIG SCORES");
                        break;
                    case "3":
                        Console.WriteLine("BIG TUTORIAl");
                        break;
                    case "4":
                        Console.WriteLine("Devs Bless");
                        break;
                    case "5":
                        break;
                }
                Console.WriteLine();
            } while (cki.Key != ConsoleKey.D5);
        }
    }
}
