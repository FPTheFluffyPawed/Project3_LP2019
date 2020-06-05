using System;

namespace Roguelike
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.SetCursorPosition(0, 0);

                Console.Write("Rendering test!\n");

                for (int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        Console.Write(".");
                    }
                    Console.WriteLine();
                }

                Console.ReadKey(true);
            }
        }
    }
}
