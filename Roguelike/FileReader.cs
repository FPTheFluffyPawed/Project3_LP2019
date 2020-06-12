using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Roguelike
{
    /// <summary>
    /// Class that takes care of writing and reading files.
    /// </summary>
    public class FileReader
    {
        /// <summary>
        /// The filename that we will write/read to.
        /// </summary>
        private string filename;

        // Ease of access for tabs.
        private const string tab = "\t";

        /// <summary>
        /// StreamReader instance variable.
        /// </summary>
        private StreamReader sr;

        /// <summary>
        /// StreamWriter instance variable.
        /// </summary>
        private StreamWriter sw;

        /// <summary>
        /// Instance variable to contain the scores.
        /// </summary>
        private List<Highscore> scores;

        /// <summary>
        /// Instance variable as a reference to Game.
        /// </summary>
        private Game game;

        /// <summary>
        /// Instance variable read-only to World.
        /// </summary>
        private IReadOnlyWorld world;

        /// <summary>
        /// Constructor that accepts a Game and a read-only World.
        /// </summary>
        /// <param name="game">Reference to Game.</param>
        /// <param name="world">Reference to World.</param>
        public FileReader(Game game, IReadOnlyWorld world)
        {
            scores = new List<Highscore>();
            this.game = game;
            this.world = world;
            CreateFile();
        }

        /// <summary>
        /// Write to the file and place the items from scores.
        /// </summary>
        public void WriteToFile()
        {
            // Begin writing to the file.
            using (sw = new StreamWriter(filename))
            {
                // Save highscores in the text file.
                foreach (Highscore hs in scores)
                {
                    sw.WriteLine(hs.Name + tab + hs.Score);
                }
            }
        }

        /// <summary>
        /// Add a score to the list if the level we reached is greater than the
        /// lowest score in the list. If yes, we will remove the smallest score
        /// and add the new one.
        /// </summary>
        public void AddScore()
        {
            string sName;

            if(scores.Exists(s => s.Score < game.Level) || scores.Count == 0)
            {
                Console.WriteLine("Name: ");
                sName = Console.ReadLine();
                scores.Remove(scores.Find(s => s.Score <= game.Level));
                scores.Add(new Highscore(sName, game.Level));
                WriteToFile();
            }
        }

        /// <summary>
        /// Create a file for the appropriate level.
        /// </summary>
        private void CreateFile()
        {
            // Start with the default name...
            StringBuilder sb = new StringBuilder("highscores");

            // Append the X and Y dimensions, and finish the name.
            sb.AppendFormat($"-{world.XDim}x{world.YDim}.txt");

            // Convert to string.
            filename = sb.ToString();

            // If the file doesn't exist, create one.
            if (!File.Exists(filename))
                File.Create(filename).Dispose();
            // Else, just read the file.
            else
                ReadFile();
        }

        /// <summary>
        /// Reads the file and adds them to the list.
        /// </summary>
        private void ReadFile()
        {
            string s;

            using (sr = new StreamReader(filename))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    string[] nameAndScore = s.Split("\t");
                    string name = nameAndScore[0];
                    int score = Convert.ToInt32(nameAndScore[1]);

                    if (scores.Count <= 10)
                    {
                        if (!scores.Exists(s => s.Name == name && s.Score == score))
                            scores.Add(new Highscore(name, score));
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the scores, reading from the list and writing it out.
        /// </summary>
        public void OutputScores()
        {
            // Counter for the sake of niceness.
            int i = 1;

            scores.Sort();

            // If the list has something...
            if (scores.Count != 0)
                foreach (Highscore hs in scores)
                {
                    if (i <= 10)
                        Console.WriteLine($"{i}. {hs.Name} - {hs.Score}");

                    i++;
                }
            // If it doesn't have anything...
            else
                Console.WriteLine("No scores yet!\n");
        }
    }
}
