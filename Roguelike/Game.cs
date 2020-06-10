using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class Game
    {
        public int PlayerHP => agents.Find(a => a.Type == AgentType.Player).HP;

        private ConsoleUserInterface ui;

        private IReadOnlyWorld world;

        private float turn, level;

        private bool gameOver;

        private Random random;

        private List<Agent> agents;

        public Game(int x, int y) 
        {
            ui = new ConsoleUserInterface(this);
            world = new World(x, y);
            random = new Random();
            agents = new List<Agent>();
            level = 1;
        }

        public void Start()
        {
            ui.Menu();
        }

        public void NewGame()
        {
            world.Clear();
            agents.Clear();
            Play();
        }

        private void Play()
        {

            Console.Clear();

            gameOver = false;

            PlaceAgent(AgentType.Player);
            PlaceAgent(AgentType.Exit);
            PlaceAgent(AgentType.SmallEnemy);
            PlaceAgent(AgentType.SmallEnemy);
            PlaceAgent(AgentType.SmallEnemy);

            //GenerateLevel();

            ui.RenderWorld(world);

            while(true)
            {
                // Run through all agents and check
                foreach (Agent a in agents)
                {
                    if (a.Type == AgentType.Player
                        || a.Type == AgentType.BigEnemy
                        || a.Type == AgentType.SmallEnemy)
                    {
                        if(a.Type == AgentType.Player)
                        {
                            a.PlayTurn();
                            ui.RenderWorld(world);
                            a.PlayTurn();
                        }
                        else
                            a.PlayTurn();
                    }
                    ui.RenderWorld(world);
                }
                ui.RenderWorld(world);
            }

            // and then start the loop.

            // This will clear any past games (if they exist) and set it up.

            // Start playing the game from here on out!
            // Console.WriteLine("\nGame!\n");

            // Game Loop
            // Render board
            // Check if player is dead (to then break out of the loop)
            // Check if the player is on the exit level tile
            // Play out turns

            // When the player enters the exit tile, immediately setup a new level
            // and then it plays out like normally, except it'll be the player's turn
            gameOver = true;
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
                    //agents.Add(agent);
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

        private void GenerateLevel()
        {
            for (int i = 0; i < level; i++)
            {
                Console.WriteLine($"YOU ARE IN LEVEL {0} !!!", i);

                // Setup the level for the first time...
                for (int j = 0; j < 3 + i; j++)
                {
                    if (ProbabilityOfBoss(i, random))
                    {
                        PlaceAgent(AgentType.BigEnemy);
                    }
                    else
                        PlaceAgent(AgentType.SmallEnemy);
                }

                for (int j = 3; j > i; j--)
                {
                    if (ProbabilityOfPowerup(i, random) == 2)
                    {
                        PlaceAgent(AgentType.MediumPowerUp);
                    }
                    if (ProbabilityOfPowerup(i, random) == 3)
                    {
                        PlaceAgent(AgentType.BigPowerUp);
                    }
                    else
                        PlaceAgent(AgentType.SmallPowerUp);
                }
            }
        }

        private void ChangeLevel()
        {

        }

        private void DifficultyScale()
        {

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
    }
}
