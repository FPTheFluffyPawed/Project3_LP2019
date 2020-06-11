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

        private Direction InputDirection()
        {
            Direction dir = Direction.None;

            Console.CursorVisible = true;

            while (dir == Direction.None)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch(keyInfo.Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        dir = Direction.Up;
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        dir = Direction.Left;
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        dir = Direction.Right;
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        dir = Direction.Down;
                        break;
                }
            }

            Console.CursorVisible = false;

            return dir;
        }
    }
}
