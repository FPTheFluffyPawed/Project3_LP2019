using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class World
    {
        private IAgent[,] world;

        public World(int x, int y)
        {
            world = new IAgent[x, y];
        }
    }
}
