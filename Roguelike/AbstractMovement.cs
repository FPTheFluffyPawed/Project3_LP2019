using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public abstract class AbstractMovement
    {
        protected readonly IReadOnlyWorld world;

        protected AbstractMovement(IReadOnlyWorld world)
        {
            this.world = world;
        }

        public abstract Position WhereToMove(Agent agent);
    }
}
