using System;
using System.Collections;

namespace Roguelike
{
    public struct Highscore : IComparable<Highscore>
    {
        public string Name { get; }
        public int Score { get; }

        public Highscore(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public int CompareTo(Highscore hs)
        {
            return hs.Score.CompareTo(Score);
        }
    }
}