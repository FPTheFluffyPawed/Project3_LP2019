using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class ConsoleUserInterface
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

        // Reference game.
        private Game game;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleUserInterface(Game game)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.CursorVisible = false;

            this.game = game;
        }

        public void Menu()
        {
            bool loop = true;

            while(loop)
            {
                Console.WriteLine("- A Roguelike Game -");
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. High Score");
                Console.WriteLine("3. Instructions");
                Console.WriteLine("4. Credits");
                Console.WriteLine("5. Quit");

                cki = Console.ReadKey(true);

                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        PlayGame();
                        break;
                    case "2":
                        ShowScores();
                        break;
                    case "3":
                        ShowIntructions();
                        break;
                    case "4":
                        ShowCredits();
                        break;
                    case "5":
                        ExitGame();
                        break;
                    default:
                        ErrorOption();
                        break;
                }
            }
        }

        private void PlayGame()
        {
            Console.WriteLine("\n*** LAUNCHING GAME... ***");
            game.Play();
            Console.WriteLine("*** CLOSING GAME... ***\n");
        }

        private void ShowScores()
        {
            // Read from file and show scores
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowIntructions()
        {
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowCredits()
        {
            Console.WriteLine("\nThe creators of this program are:");
            Console.WriteLine("- Diogo Henriques \t(X)");
            Console.WriteLine("- Inácio Amerio \t(21803493)");
            Console.WriteLine("- João Dias \t\t(X)");
            Console.WriteLine("\nMade for LP1 2019 as a third project!\n");

            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ExitGame()
        {
            Console.WriteLine("\nCLOSING...");
            Environment.Exit(1);
        }

        private void ErrorOption()
        {
            Console.WriteLine("\nInsert a valid option!\n");
        }
    }
}
