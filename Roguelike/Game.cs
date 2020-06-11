using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Roguelike
{
    public class Game
    {
        const string filename = "highscores.txt";
        const char tab = '\t';
        public int PlayerHP => agents.Find(a => a.Type == AgentType.Player).HP;

        private ConsoleUserInterface ui;

        private IReadOnlyWorld world;

        public int Level { get; private set; }

        private bool levelOver;

        private bool gameOver;

        private Random random;

        private List<Agent> agents;

        StreamWriter sw;
        StreamReader sr;
        List<Highscore> scores;

        string s;


        public Game(int x, int y) 
        {
            ui = new ConsoleUserInterface(this);
            world = new World(x, y);
            random = new Random();
            agents = new List<Agent>();
            scores = new List<Highscore>();
        }

        public void Start()
        {
            ui.Menu();
        }

        public void NewGame()
        {
            world.Clear();
            agents.Clear();
            gameOver = false;
            Play();
        }

        private void Play()
        {
            Console.Clear();

            for (int i = 0; i < float.PositiveInfinity && !gameOver; i++)
            {
                levelOver = false;
                Level = i;

                PlaceAgent(AgentType.Exit);

                GenerateLevel(i);

                ui.RenderWorld(world);

                while (levelOver == false)
                {
                    // Run through all agents and check
                    foreach (Agent a in agents)
                    {
                        if (a.Type == AgentType.Player
                            || a.Type == AgentType.BigEnemy
                            || a.Type == AgentType.SmallEnemy)
                        {
                            if (a.Type == AgentType.Player)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    if(levelOver != true && !IsPlayerDead())
                                    {
                                        a.PlayTurn();
                                        ui.RenderWorld(world);
                                        if (world.End == true)
                                        {
                                            levelOver = true;
                                        }
                                    }
                                }
                            }
                            else
                                if (levelOver != true && !IsPlayerDead())
                                    a.PlayTurn();
                        }
                        ui.RenderWorld(world);
                    }

                    ui.RenderWorld(world);

                    if (levelOver == true)
                    {
                        world.LevelClear();
                        ResetPlayer();
                        Thread.Sleep(1000);
                    }

                    if (IsPlayerDead())
                    {
                        gameOver = true;
                        break;
                    }
                }
            }

            AddScore();
        }

        private void PlaceAgent(AgentType type)
        {
            /*
             * In this method we'll place the agents in the level
             * which means we place the player, exit, obstacles, powerups,
             * and finally enemies.
             * 
             * This is also for "resetting" the level, so we will check what
             * level we are at. If we're at level 0, clear all slots first
             * to start a new game, basically.
             * 
             * It really does just that.
             */

            Position pos;

            Agent agent;

            switch(type)
            {
                case AgentType.Player:
                    // Place player in the first column
                    do
                    {
                        pos = new Position(
                            random.Next(world.XDim),
                            0);
                    } while (world.IsOccupied(pos));

                    agent = new Agent(pos, type, (World)world);
                    agents.Add(agent);
                    break;
                case AgentType.Exit:
                    do
                    {
                        pos = new Position(
                            random.Next(world.XDim),
                            world.YDim - 1);
                    } while (world.IsOccupied(pos));

                    agent = new Agent(pos, type, (World)world);
                    break;
                default:
                    do
                    {
                        pos = new Position(
                            random.Next(world.XDim),
                            random.Next(world.YDim));
                    } while (world.IsOccupied(pos));

                    agent = new Agent(pos, type, (World)world);
                    agents.Add(agent);
                    break;
            }
        }

        private void GenerateLevel(int level)
        {
            if (level == 0)
                PlaceAgent(AgentType.Player);

            // Setup the level for the first time...
            for (int j = 0; j < 3 + level; j++)
            {
                if (ProbabilityOfBoss(level, random))
                {
                    PlaceAgent(AgentType.BigEnemy);
                }
                else
                    PlaceAgent(AgentType.SmallEnemy);
            }

            for (int j = 3; j > level; j--)
            {
                if (ProbabilityOfPowerup(level, random) == 2)
                {
                    PlaceAgent(AgentType.MediumPowerUp);
                }
                if (ProbabilityOfPowerup(level, random) == 3)
                {
                    PlaceAgent(AgentType.BigPowerUp);
                }
                else
                    PlaceAgent(AgentType.SmallPowerUp);
            }
        }

        private void ResetPlayer()
        {
            agents.RemoveAll(a => a.Type != AgentType.Player);
            agents.Find(a => a.Type == AgentType.Player).ResetPlayerPos();
        }

        private bool IsPlayerDead()
        {
            if (PlayerHP <= 0)
                return true;
            else
                return false;
        }
        private bool ProbabilityOfBoss(int level, Random random)
        {
            int divider = 10 - level;

            if (divider <= 0)
            {
                divider = 0;
            }
            float ProbofBoss = 1f / divider;
            float ProbTry = 1f / random.Next(1,level + 1);

            if (ProbTry <= ProbofBoss)
            {
                return true;
            }
            return false;
        }

        private int ProbabilityOfPowerup(int level, Random random)
        {
            int bigDivider = level + 3;
            int medDivider = level + 2;

            float probOfMed = 1f/medDivider;
            float probOfBig = 1f/bigDivider;

            float probTry = 1f / random.Next(1, level + 1);

            if (probOfBig >= probTry)
            {
                //returns the big powerup
                return 3;
            }
            if (probOfMed >= probTry)
            {
                //returns the medium powerup
                return 2;
            }
            //returns the small powerup
            return 1;
        }

        private void Highscore()
        {
            using(sw = new StreamWriter(filename))
            {
                // Save highscores in txt file 
                foreach (Highscore hs in scores)
                {
                    sw.WriteLine(hs.Name + tab + hs.Score);
                }
            }

            using(sr = new StreamReader(filename))
            {
                while((s = sr.ReadLine()) != null)
                {
                    string[] nameAndScore = s.Split(tab);
                    string name = nameAndScore[0];
                    float score = Convert.ToInt32(nameAndScore[1]);
                    Console.WriteLine("Score of {0} is {1}", name, score);            
                }
            }
        }

        private void AddScore()
        {
            string sName;
            
            Console.WriteLine("Name: ");
            sName = Console.ReadLine();
            Console.WriteLine("Score: ");

            scores.Add(new Highscore (sName, Level));
        }
    }
}