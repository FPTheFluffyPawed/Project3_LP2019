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

        public Agent(Position pos, AgentType type, World world)
        {
            Pos = pos;
            Type = type;
            this.world = world;

            switch(type)
            {
                case AgentType.Player:
                    HP = 40;
                    break;
                case AgentType.SmallEnemy:
                    HP = 5;
                    break;
                case AgentType.BigEnemy:
                    HP = 10;
                    break;
            }
        }

        public void PlayTurn()
        {
            Random random = new Random();
            Position destination = new Position(random.Next(0, 5), random.Next(0, 5));

            if(!world.IsOccupied(destination))
            {
                world.MoveAgent(this, destination);

                Pos = destination;
            }
        }
    }
}
