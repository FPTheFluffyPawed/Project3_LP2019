using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{  
    /// <summary>
    /// This class works the world/board construction
    /// </summary>
    public class World : IReadOnlyWorld
    {
        /// <summary>
        /// Returns the max X of the world
        /// </summary>
        /// <returns> return the length(0) of the array </returns>
        public int XDim => world.GetLength(0);
        
        /// <summary>
        /// Returns the max X of the world
        /// </summary>
        /// <returns> return the length(1) of the array </returns>
        public int YDim => world.GetLength(1);

        /// <summary>
        /// End level property
        /// </summary>
        /// <value> returns a boolean </value>
        public bool End { get; set; }

        /// <summary>
        /// Agent type array
        /// </summary>
        private Agent[,] world;

        /// <summary>
        /// Constructor method. Initializes the 'world' array.
        /// </summary>
        /// <param name="x"> X Board coordinates </param>
        /// <param name="y"> Y Board coordinates </param>
        public World(int x, int y)
        {
            world = new Agent[x, y];
        }

        /// <summary>
        /// Goes through every element of the board and puts every single one to
        /// to null.
        /// </summary>
        public void Clear()
        {
            for(int x = 0; x < XDim; x++)
                for(int y = 0; y < YDim; y++)
                {
                    world[x, y] = null;
                }
        }

        /// <summary>
        /// Goes through every element of the board and puts them all to null
        /// except the player.
        /// </summary>
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

        /// <summary>
        /// Checks if the board position is occupied
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <returns> Returns a boolean </returns>
        public bool IsOccupied(Position pos)
        {
            // Return true if nothing is here, return false if there is something here.
            return world[pos.X, pos.Y] != null;
        }

        /// <summary>
        /// Checks if the position is out of the board
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <returns> Boolean </returns>
        public bool IsOutOfBounds(Position pos)
        {
            if (pos.X < 0 || pos.X >= XDim || pos.Y < 0 || pos.Y >= YDim)
                return true;
            else
                return false;
        }


        /// <summary>
        /// Updates the world with the new agent position and turns the old one
        /// to null.
        /// </summary>
        /// <param name="agent"> Turns the position to type agent </param>
        /// <param name="destination"> The new agent position </param>
        public void MoveAgent(Agent agent, Position destination)
        {
            world[destination.X, destination.Y] = agent;

            world[agent.Pos.X, agent.Pos.Y] = null;
        }

        /// <summary>
        /// Turns the board cells into agents.
        /// </summary>
        /// <param name="agent"></param>
        public void AddAgent(Agent agent)
        {
            world[agent.Pos.X, agent.Pos.Y] = agent;
        }

        /// <summary>
        /// Return the agent position.
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <returns> Returns the agent position </returns>
        public Agent GetAgentAt(Position pos)
        {
            return world[pos.X, pos.Y];
        }

        /// <summary>
        /// Calculates the vector between two coordinates.
        /// </summary>
        /// <param name="pos1"> One coordinate </param>
        /// <param name="pos2"> Second coordinate </param>
        /// <returns> Returns the result of the calculation </returns>
        public Position VectorBetween(Position pos1, Position pos2)
        {
            int x, y;

            x = pos2.X - pos1.X;
            y = pos2.Y - pos1.Y;

            return new Position(x, y);
        }

        /// <summary>
        /// Gets the new position when moving based on the direction.
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <param name="dir"> Direction </param>
        /// <returns> New position </returns>
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
        
        /// <summary>
        /// Gets the new position when moving based on the vectors.
        /// </summary>
        /// <param name="pos"> Position </param>
        /// <param name="directionVector"> Vector </param>
        /// <returns> New Position </returns>
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