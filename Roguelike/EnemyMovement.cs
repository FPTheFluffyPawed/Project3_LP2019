using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Roguelike
{
    public class EnemyMovement : AbstractMovement
    {
        public EnemyMovement(IReadOnlyWorld world) : base(world)
        {

        }

        public override Position WhereToMove(Agent agent)
        {
            Thread.Sleep(500);

            // Vector.
            Position vector = default(Position);

            // Aux.
            Position currentPosition;

            //Random random = new Random();
            //
            //Direction direction;
            //
            //switch (random.Next(3))
            //{
            //    case 0:
            //        direction = Direction.Up;
            //        break;
            //    case 1:
            //        direction = Direction.Down;
            //        break;
            //    case 2:
            //        direction = Direction.Left;
            //        break;
            //    default:
            //        direction = Direction.Right;
            //        break;
            //}

            return world.GetNeighbor(agent.Pos, direction);
        }
    }
}
