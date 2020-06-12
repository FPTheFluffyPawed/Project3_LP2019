using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Works the player movement
    /// </summary>
    public class PlayerMovement : AbstractMovement
    {
        /// <summary>
        /// Empty Constructor that extends from the constructor from 
        /// the class AbstractMovement
        /// </summary>
        /// <param name="world"> World </param>
        /// <returns></returns>
        public PlayerMovement(IReadOnlyWorld world) : base(world)
        {

        }

        /// <summary>
        /// Gets the input from the user to know the direction to move 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public override Position WhereToMove(Agent agent)
        {
            Direction direction = InputDirection();

            return world.GetNeighbor(agent.Pos, direction);
        }

        /// <summary>
        /// Asks the the user for the input
        /// </summary>
        /// <returns> Direction </returns>
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