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

            // Target.
            Agent target = null;

            // Check if we found the player.
            bool foundPlayer = false;

            // Vector.
            Position vector = default(Position);

            // Aux.
            Position currentPosition;

            for(int x = 0; x < world.XDim && !foundPlayer; x++)
                for(int y = 0; y < world.YDim && !foundPlayer; y++)
                {
                    currentPosition = new Position(x, y);

                    if(world.IsOccupied(currentPosition))
                    {
                        target = world.GetAgentAt(currentPosition);

                        if(target.Type == AgentType.Player)
                        {
                            vector = world.VectorBetween(agent.Pos, currentPosition);

                            foundPlayer = true;
                        }
                    }
                }

            if (foundPlayer)
                return world.GetNeighbor(agent.Pos, vector);

            // Shouldn't get here.
            return agent.Pos;
        }
    }
}
