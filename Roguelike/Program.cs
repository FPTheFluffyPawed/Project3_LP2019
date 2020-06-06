using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            int row = 0, column = 0;

            (row, column) = Ola(args, row, column);

            if (row <= 0 || column <= 0)
            {
                Console.WriteLine("Invalid options!!");
                Environment.Exit(1);
            }   

            Game game = new Game(row, column);

            game.Start();
        }

        private static (int, int) Ola(string[] args, int row, int column)
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
