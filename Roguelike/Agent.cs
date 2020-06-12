using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Class that represents an agent.
    /// </summary>
    public class Agent
    {
        /// <summary>
        /// Auto-implemented property that represents an Agent's current HP.
        /// </summary>
        public int HP { get; private set; }

        /// <summary>
        /// Auto-implemented property that represents an Agent's
        /// current position.
        /// </summary>
        public Position Pos { get; private set; }

        /// <summary>
        /// Auto-implemented property that represents an Agent's type.
        /// </summary>
        public AgentType Type { get; private set; }

        /// <summary>
        /// Instance variable to the world.
        /// </summary>
        private World world;

        /// <summary>
        /// Instance variable that determines the movement to be done by the
        /// Agent.
        /// </summary>
        private AbstractMovement moveBehaviour;

        /// <summary>
        /// Instance variable for a Random to be used for rolls.
        /// </summary>
        private Random random;

        /// <summary>
        /// Constructor for creating an Agent.
        /// </summary>
        /// <param name="pos">Initial position.</param>
        /// <param name="type">The type of Agent based on AgentType.</param>
        /// <param name="world">Reference to the world.</param>
        public Agent(Position pos, AgentType type, World world)
        {
            // Random.
            random = new Random();

            Pos = pos;
            Type = type;
            this.world = world;

            // Assign different values based on what type of Agent it is.
            switch (type)
            {
                case AgentType.Player:
                    HP = (world.XDim * world.YDim) / 4;
                    moveBehaviour = new PlayerMovement(world);
                    break;
                case AgentType.SmallEnemy:
                    HP = 5;
                    moveBehaviour = new EnemyMovement(world);
                    break;
                case AgentType.BigEnemy:
                    HP = 10;
                    moveBehaviour = new EnemyMovement(world);
                    break;
                case AgentType.SmallPowerUp:
                    HP = 4;
                    break;
                case AgentType.MediumPowerUp:
                    HP = 8;
                    break;
                case AgentType.BigPowerUp:
                    HP = 16;
                    break;
                default:
                    HP = 0;
                    break;
            }

            // Add the agent to the world!
            world.AddAgent(this);
        }

        /// <summary>
        /// Method to call for playing an Agent's turn.
        /// </summary>
        public void PlayTurn()
        {
            Position destination;

            // Keep asking for a position!
            do
            {
                destination = moveBehaviour.WhereToMove(this);

                // Since the player is forced to move, we check if he ran into
                // something, and give him a retry to move.
                if(Type == AgentType.Player)
                    if(!world.IsOutOfBounds(destination))
                        if(world.IsOccupied(destination))
                        {
                            if (world.GetAgentAt(destination).Type == AgentType.Obstacle
                            || world.GetAgentAt(destination).Type == AgentType.SmallEnemy
                            || world.GetAgentAt(destination).Type == AgentType.BigEnemy)
                            destination = new Position(-1, -1);
                        }

            } while (world.IsOutOfBounds(destination));

            if (!world.IsOccupied(destination))
            {
                world.MoveAgent(this, destination);

                Pos = destination;
            }
            else
            {
                // The destination we tried to move is occupied, soooo...
                // Get the agent at that destination.
                Agent other = world.GetAgentAt(destination);

                // If the current agent is an enemy, apply its health as damage to the player.
                if ((Type == AgentType.SmallEnemy && other.Type == AgentType.Player)
                    || (Type == AgentType.BigEnemy && other.Type == AgentType.Player))
                {
                    other.HP -= HP;
                }
                // If we're the player and we enter the exit, change level!
                else if (Type == AgentType.Player && other.Type == AgentType.Exit)
                {
                    world.End = true;
                }
                // If we're the player and bump into a PowerUp, get HP.
                else if ((Type == AgentType.Player && other.Type == AgentType.SmallPowerUp)
                    || (Type == AgentType.Player && other.Type == AgentType.MediumPowerUp)
                    || (Type == AgentType.Player && other.Type == AgentType.BigPowerUp))
                {
                    HP += other.HP;
                    world.MoveAgent(this, destination);

                    Pos = destination;

                }
                // If we're an enemy and run into an obstacle, move randomly.
                else if((Type == AgentType.SmallEnemy && other.Type == AgentType.Obstacle)
                    || (Type == AgentType.BigEnemy && other.Type == AgentType.Obstacle))
                {
                    MoveRandomPosition();
                }
                // If we're an enemy and run into another, move randomly.
                else if((Type == AgentType.SmallEnemy && other.Type == Type)
                    || (Type == AgentType.BigEnemy && other.Type == Type))
                {
                    MoveRandomPosition();
                }
                else
                {
                    // Do nothing.
                }
            }

            // Always reduce the player by 1 HP when he moves.
            if (Type == AgentType.Player)
                HP--;
        }

        /// <summary>
        /// Method that makes the Agent move in a random direction.
        /// </summary>
        private void MoveRandomPosition()
        {
            Position destination;

            do
            {
                Direction direction = default;
                switch (random.Next(4))
                {
                    case 0:
                        direction = Direction.Up;
                        break;
                    case 1:
                        direction = Direction.Down;
                        break;
                    case 2:
                        direction = Direction.Left;
                        break;
                    case 3:
                        direction = Direction.Right;
                        break;
                }

                destination = world.GetNeighbor(Pos, direction);

                if (!world.IsOutOfBounds(destination))
                    if (world.IsOccupied(destination))
                        destination = new Position(-1, -1);
            } while (world.IsOutOfBounds(destination));

            world.MoveAgent(this, destination);

            Pos = destination;
        }

        /// <summary>
        /// Method that resets the player Agent's position to the starting
        /// line. Used for when starting a new level.
        /// </summary>
        public void ResetPlayerPos()
        {
            world.End = false;
            if (Type == AgentType.Player)
            {
                Position pos = new Position(random.Next(world.XDim), 0);
                world.MoveAgent(this, pos);
                Pos = pos;
            }
        }
    }
}
