using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class World : IReadOnlyWorld
    {

        public int XDim => world.GetLength(0);

        public int YDim => world.GetLength(1);

        public bool End { get; set; }

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

        public void LevelClear()
        {
            for (int x = 0; x < XDim; x++)
                for (int y = 0; y < YDim; y++)
                {
                    Position pos = new Position(x,y);
                    if (IsOccupied(pos))
                    {
                        if (GetAgentAt(pos).Type != AgentType.Player)
                        {
                            world[x, y] = null;
                        }                  
                    }
                }
        }

        public bool IsOccupied(Position pos)
        {
            // Return true if nothing is here, return false if there is something here.
            return world[pos.X, pos.Y] != null;
        }

        public bool IsOutOfBounds(Position pos)
        {
            if (pos.X < 0 || pos.X >= XDim || pos.Y < 0 || pos.Y >= YDim)
                return true;
            else
                return false;
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
            int x, y;

            x = pos2.X - pos1.X;
            y = pos2.Y - pos1.Y;

            return new Position(x, y);
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
            // Create a random to choose either Vertical or Horizontal.
            Random random = new Random();

            // Variable to contain the direction.
            Direction direction = default(Direction);

            // Convert values of the vector to 1, -1 or 0.
            directionVector = new Position(Math.Sign(directionVector.X), Math.Sign(directionVector.Y));

            if (directionVector.X == 1 && directionVector.Y == 1)
            { 
                switch (random.Next(2))
                {
                    case 0:
                        direction = Direction.Down;
                        break;
                    case 1:
                        direction = Direction.Right;
                        break;
                }
            }
            else if (directionVector.X == 1 && directionVector.Y == 0)
                direction = Direction.Down;
            else if (directionVector.X == 1 && directionVector.Y == -1)
            {
                switch (random.Next(2))
                {
                    case 0:
                        direction = Direction.Down;
                        break;
                    case 1:
                        direction = Direction.Left;
                        break;
                }
            }
            else if (directionVector.X == 0 && directionVector.Y == 1)
                direction = Direction.Right;
            else if (directionVector.X == 0 && directionVector.Y == 0)
                direction = Direction.None;
            else if (directionVector.X == 0 && directionVector.Y == -1)
                direction = Direction.Left;
            else if (directionVector.X == -1 && directionVector.Y == 1)
            {
                switch (random.Next(2))
                {
                    case 0:
                        direction = Direction.Up;
                        break;
                    case 1:
                        direction = Direction.Right;
                        break;
                }
            }
            else if (directionVector.X == -1 && directionVector.Y == 0)
                direction = Direction.Up;
            else if (directionVector.X == -1 && directionVector.Y == -1)
            {
                switch (random.Next(2))
                {
                    case 0:
                        direction = Direction.Up;
                        break;
                    case 1:
                        direction = Direction.Left;
                        break;
                }
            }

            return GetNeighbor(pos, direction);
        }
    }
}
