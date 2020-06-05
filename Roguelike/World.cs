using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class World
    {
        private Agent[,] world;

        public World(int x, int y)
        {
            world = new Agent[x, y];
        }

        public bool IsOccupied(Position pos)
        {
            // Return true if nothing is here, return false if there is something here.
            return world[pos.X, pos.Y] != null;
        }

        public void MoveAgent(Agent agent, Position destination)
        {
            world[destination.X, destination.Y] = agent;

            world[agent.Pos.X, agent.Pos.Y] = null;
        }

        public void AddAgent(Agent agent)
        {
            world[agent.Pos.X, agent.Pos.Y] = agent;
        }
    }
}
