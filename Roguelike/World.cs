using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class World : IReadOnlyWorld
    {

        public int XDim => world.GetLength(0);

        public int YDim => world.GetLength(1);

        private Agent[,] world;

        public World(int x, int y)
        {
            world = new Agent[x, y];
        }

        public void Clear()
        {
            for(int x = 0; x < XDim; x++)
                for(int y = 0; y < YDim; y++)
                {
                    world[x, y] = null;
                }
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

        public Agent GetAgentAt(Position pos)
        {
            return world[pos.X, pos.Y];
        }

        public Position VectorBetween(Position pos1, Position pos2)
        {
            throw new NotImplementedException();
        }

        public Position GetNeighbor(Position pos, Direction dir)
        {
            int x = pos.X, y = pos.Y;

            switch(dir)
            {
                case Direction.Up:
                    x -= 1;
                    break;
                case Direction.Left:
                    y -= 1;
                    break;
                case Direction.Right:
                    y += 1;
                    break;
                case Direction.Down:
                    x += 1;
                    break;
                case Direction.None:
                    break;
            }

            return new Position(x, y);
        }

        public Position GetNeighbor(Position pos, Position directionVector)
        {
            throw new NotImplementedException();
        }
    }
}
