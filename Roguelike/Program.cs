using System;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            World world = new World(5, 5);

            for(int i = 0; i < 5; i++)
            {
                int x = random.Next(0, 5);
                int y = random.Next(0, 5);
                Agent agent = new Agent(new Position(x, y), AgentType.SmallEnemy, world);
                world.AddAgent(agent);

                Console.SetCursorPosition(0, 0);
                Console.Write("Rendering test!\n\n");

                for(int b = 0; b < 5; b++)
                {
                    for(int j = 0; j < 5; j++)
                    {
                        Position position = new Position(b, j);
                        if (world.IsOccupied(position))
                            Console.Write(" e ");
                        else
                            Console.Write(" . ");
                    }

                    Console.WriteLine();
                }

                Console.ReadKey(true);
            }

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
    }
}
