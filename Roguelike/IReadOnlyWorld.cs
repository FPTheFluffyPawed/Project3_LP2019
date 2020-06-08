using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public interface IReadOnlyWorld
    {
        int XDim { get; }

        int YDim { get; }

        bool IsOccupied(Position pos);

        bool IsOutOfBounds(Position pos);

        Agent GetAgentAt(Position pos);

        void Clear();

        Position VectorBetween(Position pos1, Position pos2);

        Position GetNeighbor(Position pos, Direction dir);

        Position GetNeighbor(Position pos, Position directionVector);
    }
}
