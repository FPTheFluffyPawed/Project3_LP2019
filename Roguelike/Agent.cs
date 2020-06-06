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

            switch(type)
            {
                case AgentType.Player:
                    HP = (world.XDim * world.YDim) / 4;
                    moveBehaviour = new PlayerMovement(world);
                    break;
                case AgentType.SmallEnemy:
                    HP = 5;
                    break;
                case AgentType.BigEnemy:
                    HP = 10;
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
            Position destination = moveBehaviour.WhereToMove(this);

            if(!world.IsOccupied(destination))
            {
                world.MoveAgent(this, destination);

                Pos = destination;
            }
            else
            {
                // The destination we tried to move is occupied, soooo...
            }
        }
    }
}
