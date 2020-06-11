using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Roguelike
{
    public class FileReader
    {
        private string filename;
        private const string tab = "\t";

        private StreamReader sr;
        private StreamWriter sw;
        private List<Highscore> scores;
        private Game game;
        private IReadOnlyWorld world;

        public FileReader(Game game, IReadOnlyWorld world)
        {
            scores = new List<Highscore>();
            this.game = game;
            this.world = world;
            CreateFile();
        }

        public void WriteToFile()
        {
            using (sw = new StreamWriter(filename))
            {
                // Save highscores in txt file 
                foreach (Highscore hs in scores)
                {
                    sw.WriteLine(hs.Name + tab + hs.Score);
                }
            }
        }

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

        private void CreateFile()
        {
            StringBuilder sb = new StringBuilder("highscores");

            sb.AppendFormat($"-{world.XDim}{world.YDim}.txt");

            filename = sb.ToString();

            if (!File.Exists(filename))
                File.Create(filename).Dispose();
            else
                ReadFile();
        }

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

        public void OutputScores()
        {
            int i = 1;

            scores.Sort();

            if (scores.Count != 0)
                foreach (Highscore hs in scores)
                {
                    if (i <= 10)
                        Console.WriteLine($"{i}. {hs.Name} - {hs.Score}");

                    i++;
                }
            else
                Console.WriteLine("No scores yet!\n");
        }
    }
}
