using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Interface for a read-only version of World.
    /// </summary>
    public interface IReadOnlyWorld
    {
        /// <summary>
        /// Horizontal size of the World.
        /// </summary>
        int XDim { get; }

        /// <summary>
        /// Veritcal size of the World.
        /// </summary>
        int YDim { get; }

        /// <summary>
        /// End property to check if we finished the level.
        /// </summary>
        bool End { get; }

        /// <summary>
        /// Bool that checks if a spot if occupied.
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>True if yes, false if not.</returns>
        bool IsOccupied(Position pos);

        /// <summary>
        /// Bool that checks if the position is out of bounds.
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>True if yes, false if not.</returns>
        bool IsOutOfBounds(Position pos);

        /// <summary>
        /// Method that returns an Agent at a position.
        /// </summary>
        /// <param name="pos">Position to check.</param>
        /// <returns>The Agent at that position.</returns>
        Agent GetAgentAt(Position pos);

        /// <summary>
        /// Method that clears out the World.
        /// </summary>
        void Clear();

        /// <summary>
        /// Method that clears the current level.
        /// </summary>
        void LevelClear();

        /// <summary>
        /// Method that returns the position that is the vector between two
        /// positions.
        /// </summary>
        /// <param name="pos1">Initial position.</param>
        /// <param name="pos2">Destination position.</param>
        /// <returns>Position.</returns>
        Position VectorBetween(Position pos1, Position pos2);

        /// <summary>
        /// Method that returns a Position based on the direction sent.
        /// </summary>
        /// <param name="pos">The current position.</param>
        /// <param name="dir">The direction we are moving to.</param>
        /// <returns>Position based on the direction submitted.</returns>
        Position GetNeighbor(Position pos, Direction dir);

        /// <summary>
        /// Method that returns a Position based on the direction's vector.
        /// </summary>
        /// <param name="pos">Current position.</param>
        /// <param name="directionVector">Destination's vector.</param>
        /// <returns>Position based on the direction calculated.</returns>
        Position GetNeighbor(Position pos, Position directionVector);
    }
}