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

            do
            {
                destination = moveBehaviour.WhereToMove(this);
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
                else
                {
                    // Do nothing.
                }
            }

            // Always reduce the player by 1 HP when he moves.
            if (Type == AgentType.Player)
                HP--;
        }

        public override string ToString()
        {
            return $"HP = {HP}";
        }
    }
}
