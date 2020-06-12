using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Enum that contains all types of Agent.
    /// </summary>
    public enum AgentType
    {
        Player,
        SmallEnemy,
        BigEnemy,
        Obstacle,
        SmallPowerUp,
        MediumPowerUp,
        BigPowerUp,
        Exit,
    }
}
