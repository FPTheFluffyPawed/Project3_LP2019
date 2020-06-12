using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Struct that gets the position of the board.
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Read-only property to get the X value.
        /// </summary>
        /// <value> X </value>
        public int X { get; }
        /// <summary>
        /// Read-only property to get the Y value.
        /// </summary>
        /// <value> Y </value>
        public int Y { get; }

        /// <summary>
        /// Constructor method that initializes the x and y
        /// </summary>
        /// <param name="x"> X </param>
        /// <param name="y"> Y </param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns string with the positions
        /// </summary>
        /// <returns> String </returns>
        public override string ToString() => $"({X}, {Y})";
    }
}