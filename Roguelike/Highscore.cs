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

        public int CompareTo(Highscore obj)
        {
            if(!obj.Equals(this)) return 1;
            Highscore otherHighscore = obj;
            if(otherHighscore.Equals(this))
                return otherHighscore.Score.CompareTo(this.Score);
            else
                throw new ArgumentException("Object is not highscore");
        }
    }
}