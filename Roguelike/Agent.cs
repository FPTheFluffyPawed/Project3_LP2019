using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class Agent
    {
        public int HP { get; private set; }
        public Position Pos { get; private set; }
        public AgentType Type { get; private set; }

        // Use this just for reference to the methods.
        private World world;

        // Movement.
        private AbstractMovement moveBehaviour;

        public Agent(Position pos, AgentType type, World world)
        {
            Pos = pos;
            Type = type;
            this.world = world;

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
                default:
                    HP = 0;
                    break;
            }

            // Add the agent to the world!
            world.AddAgent(this);
        }

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
                    // Change level! Probably call world's level change method?
                }
                else if ((Type == AgentType.Player && other.Type == AgentType.SmallPowerUp)
                    || (Type == AgentType.Player && other.Type == AgentType.MediumPowerUp)
                    || (Type == AgentType.Player && other.Type == AgentType.BigPowerUp))
                {
                    HP += other.HP;
                }
                else if((Type == AgentType.SmallEnemy && other.Type == AgentType.Obstacle)
                    || (Type == AgentType.BigEnemy && other.Type == AgentType.Obstacle))
                {
                    MoveRandomPosition();
                }
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

        private void MoveRandomPosition()
        {
            Random random = new Random();
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
            } while (world.IsOccupied(destination));

            world.MoveAgent(this, destination);

            Pos = destination;
        }
    }
}
