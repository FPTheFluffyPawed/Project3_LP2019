using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Roguelike
{
    public class Game
    {
        public int PlayerHP => agents.Find(a => a.Type == AgentType.Player).HP;

        private ConsoleUserInterface ui;

        private IReadOnlyWorld world;

        public float Level { get; private set; }

        private bool levelOver;

        private bool gameOver;

        private Random random;

        private List<Agent> agents;

        public Game(int x, int y) 
        {
            ui = new ConsoleUserInterface(this);
            world = new World(x, y);
            random = new Random();
            agents = new List<Agent>();
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

            // Ask for highscore here.
            // < HERE >
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
            int obs, pow;
            obs = random.Next(0, (Math.Min(world.XDim,world.YDim))-1);
            pow = random.Next(2, Math.Max(world.XDim, world.YDim));

            if (level == 0)
                PlaceAgent(AgentType.Player);

            PlaceAgent(AgentType.Exit);

            for (int j = 0; j < obs; j++)
            {
                    PlaceAgent(AgentType.Obstacle);
            }

            // Setup the level for the first time...
            for (int j = 0; j < 3 + level && j <= world.XDim * world.YDim / 2; j++)
            {
                if (ProbabilityOfBoss(level))
                {
                    PlaceAgent(AgentType.BigEnemy);
                }
                else
                    PlaceAgent(AgentType.SmallEnemy);
            }

            for (int j = 0; j < pow; j++)
            {
                if (ProbabilityOfPowerup(level) == 2)
                {
                    PlaceAgent(AgentType.MediumPowerUp);
                }
                if (ProbabilityOfPowerup(level) == 3)
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
        private bool ProbabilityOfBoss(int level)
        {
            float ProbofBoss = 1 + level;
            float ProbTry = random.Next(1,10);

            if (ProbTry <= ProbofBoss)
            {
                return true;
            }
            return false;
        }

        private int ProbabilityOfPowerup(int level)
        {
            float probOfMed = 7;
            float probOfBig = 5;

            float probTry = random.Next(1, 10 + level);

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
    }
}
