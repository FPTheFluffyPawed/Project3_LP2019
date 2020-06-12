using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Main class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that verifies the arguments that the player chose and
        /// starts the game based on that.
        /// </summary>
        /// <param name="args"> Arguments inserted by the user </param>
        static void Main(string[] args)
        {
            int row = 0, column = 0;

            (row, column) = Options(args, row, column);

            if (row <= 0 || column <= 0)
            {
                Console.WriteLine("Invalid options!!");
                Environment.Exit(1);
            }   

            Game game = new Game(row, column);

            game.Start();
        }

        /// <summary>
        /// Conditions for the options that the user must insert.
        /// </summary>
        /// <param name="args"> Arguments inserted by the user </param>
        /// <param name="row"> Rows of the board </param>
        /// <param name="column"> Columns of the board </param>
        /// <returns></returns>
        private static (int, int) Options(string[] args, int row, int column)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-r")
                    Int32.TryParse(args[i + 1], out row);
                if (args[i] == "-c")
                    Int32.TryParse(args[i + 1], out column);
            }
            return (row, column);
        }
    }
}