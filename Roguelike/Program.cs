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
            //Random random = new Random();
            //
            //World world = new World(5, 5);
            //
            //for(int i = 0; i < 5; i++)
            //{
            //    int x = random.Next(0, 5);
            //    int y = random.Next(0, 5);
            //    Agent agent = new Agent(new Position(x, y), AgentType.SmallEnemy, world);
            //    world.AddAgent(agent);
            //
            //    Console.SetCursorPosition(0, 0);
            //    Console.Write("Rendering test!\n\n");
            //
            //    for(int b = 0; b < 5; b++)
            //    {
            //        for(int j = 0; j < 5; j++)
            //        {
            //            Position position = new Position(b, j);
            //            if (world.IsOccupied(position))
            //                Console.Write(" e ");
            //            else
            //                Console.Write(" . ");
            //        }
            //
            //        Console.WriteLine();
            //    }
            //
            //    Console.ReadKey(true);
            //}

            //while(true)
            //{
            //    Console.SetCursorPosition(0, 0);
            //
            //    Console.Write("Rendering test!\n");
            //
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for(int j = 0; j < 10; j++)
            //        {
            //            Console.Write(".");
            //        }
            //        Console.WriteLine();
            //    }
            //
            //    Console.ReadKey(true);
            //}
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
