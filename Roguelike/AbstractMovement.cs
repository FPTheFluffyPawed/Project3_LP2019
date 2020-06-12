using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Abstract class that is used for implementation by any form of movement.
    /// </summary>
    public abstract class AbstractMovement
    {
        /// <summary>
        /// Reference variable to the read-only world.
        /// </summary>
        protected readonly IReadOnlyWorld world;

        /// <summary>
        /// Constructor that simply takes and saves a read-only world.
        /// </summary>
        /// <param name="world">Reference variable to the read-only world.</param>
        protected AbstractMovement(IReadOnlyWorld world)
        {
            this.world = world;
        }

        /// <summary>
        /// Abstract method that returns a position to where the Agent has to
        /// move. The sub-classes should have a concrete implementation of
        /// this.
        /// </summary>
        /// <param name="agent">Agent to move.</param>
        /// <returns>Position where the agent wants to move.</returns>
        public abstract Position WhereToMove(Agent agent);
    }
}
