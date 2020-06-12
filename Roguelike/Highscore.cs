using System;
using System.Collections;

namespace Roguelike
{
    /// <summary>
    /// Struct that stores the data for highscores.
    /// </summary>
    public struct Highscore : IComparable<Highscore>
    {
        /// <summary>
        /// Auto-implemented property to store the player's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Auto-implemented property to store the player's score.
        /// </summary>
        public int Score { get; }

        /// <summary>
        /// Constructor to assign a new Highscore.
        /// </summary>
        /// <param name="name">Player's name.</param>
        /// <param name="score">Player's score.</param>
        public Highscore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        /// <summary>
        /// IComparable implementation to sort by descending.
        /// </summary>
        /// <param name="hs">Highscore to compare.</param>
        /// <returns>-1, 0, or 1.</returns>
        public int CompareTo(Highscore hs)
        {
            return hs.Score.CompareTo(Score);
        }
    }
}