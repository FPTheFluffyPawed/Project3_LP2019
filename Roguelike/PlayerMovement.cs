using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class PlayerMovement : AbstractMovement
    {
        public PlayerMovement(IReadOnlyWorld world) : base(world)
        {

        }

        public override Position WhereToMove(Agent agent)
        {
            Direction direction = InputDirection();

            return world.GetNeighbor(agent.Pos, direction);
        }

        // Place this in console interface later
        private Direction InputDirection()
        {
            Direction dir = Direction.None;

            Console.CursorVisible = true;

            while (dir == Direction.None)
            {
                //Console.WriteLine("Insert a movement option!");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch(keyInfo.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        //Console.WriteLine(Direction.Up);
                        dir = Direction.Up;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        //Console.WriteLine(Direction.Left);
                        dir = Direction.Left;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        //Console.WriteLine(Direction.Right);
                        dir = Direction.Right;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        //Console.WriteLine(Direction.Down);
                        dir = Direction.Down;
                        break;
                }
            }

            Console.CursorVisible = false;

            return dir;
        }
    }
}
