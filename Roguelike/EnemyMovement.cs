using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Roguelike
{
    /// <summary>
    /// Class that defines the movement for enemies.
    /// </summary>
    public class EnemyMovement : AbstractMovement
    {
        /// <summary>
        /// Constructor inherited from base.
        /// </summary>
        /// <param name="world">Read-only world reference.</param>
        public EnemyMovement(IReadOnlyWorld world) : base(world)
        {

        }

        /// <summary>
        /// Concrete method that returns a position based on
        /// chasing the Player.
        /// </summary>
        /// <param name="agent">The Agent that we are.</param>
        /// <returns>The position to move.</returns>
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

                    // Is this spot occupied?
                    if(world.IsOccupied(currentPosition))
                    {
                        // Get this occupied position as a target.
                        target = world.GetAgentAt(currentPosition);

                        // Is this target a Player?
                        if(target.Type == AgentType.Player)
                        {
                            // Calculate the vector.
                            vector = world.VectorBetween(agent.Pos, currentPosition);

                            // Mark that we found the player.
                            foundPlayer = true;
                        }
                    }
                }

            // If we found the player, return the neighbor position.
            if (foundPlayer)
                return world.GetNeighbor(agent.Pos, vector);

            // Shouldn't get here in-game, but this is in case we have
            // no Player.
            return agent.Pos;
        }
    }
}
