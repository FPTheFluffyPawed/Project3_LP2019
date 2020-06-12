using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Roguelike
{
    /// <summary>
    /// Game class that takes care of the Game logic.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Property that searches for the Player in the list, and when finding
        /// returns the player health.
        /// </summary>
        public int PlayerHP => agents.Find(a => a.Type == AgentType.Player).HP;

        /// <summary>
        /// Instance variable for the user interface.
        /// </summary>
        private ConsoleUserInterface ui;

        /// <summary>
        /// Instance variable for the read-only world.
        /// </summary>
        private IReadOnlyWorld world;

        /// <summary>
        /// Property for the current level.
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Boolean to check if the level is over.
        /// </summary>
        private bool levelOver;

        /// <summary>
        /// Boolean to check if the game is over.
        /// </summary>
        private bool gameOver;

        /// <summary>
        /// Instance variable for a Random.
        /// </summary>
        private Random random;

        /// <summary>
        /// Instance variable for the list of Agent.
        /// </summary>
        private List<Agent> agents;

        /// <summary>
        /// Constructor to create a game.
        /// </summary>
        /// <param name="x">X/Row.</param>
        /// <param name="y">Y/Column.</param>
        public Game(int x, int y) 
        {
            world = new World(x, y);
            ui = new ConsoleUserInterface(this, world);
            random = new Random();
            agents = new List<Agent>();
        }

        /// <summary>
        /// Start the Game by accessing the Menu.
        /// </summary>
        public void Start()
        {
            ui.Menu();
        }

        // Start a new game to play through, clearing out any previous ones.
        public void NewGame()
        {
            world.Clear();
            agents.Clear();
            gameOver = false;
            Play();
        }

        /// <summary>
        /// Method that plays out the game.
        /// </summary>
        private void Play()
        {
            // Clear the Console.
            Console.Clear();

            // Play the game out infinitely until the game is over.
            for (int i = 0; i < float.PositiveInfinity && !gameOver; i++)
            {
                levelOver = false;
                Level = i;

                // Generate a new level.
                GenerateLevel(i);

                ui.RenderWorld(world);

                while (levelOver == false)
                {
                    // Run through all agents and check for them.
                    foreach (Agent a in agents)
                    {
                        if (a.Type == AgentType.Player
                            || a.Type == AgentType.BigEnemy
                            || a.Type == AgentType.SmallEnemy)
                        {
                            if (a.Type == AgentType.Player)
                            {
                                // The player gets two turns for movement.
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

                    // If the level is over, clear the level and go next level.
                    if (levelOver == true)
                    {
                        world.LevelClear();
                        ResetPlayer();
                        Thread.Sleep(1000);
                    }

                    // If the player is dead, mark the game as over.
                    if (IsPlayerDead())
                    {
                        gameOver = true;
                        break;
                    }
                }
            }

            // Once we're out of the game, show the final screen.
            ui.RenderEndGame();
        }

        /// <summary>
        /// Method to place an Agent in the game.
        /// </summary>
        /// <param name="type">The type of Agent.</param>
        private void PlaceAgent(AgentType type)
        {
            Position pos;

            Agent agent;

            switch(type)
            {
                case AgentType.Player:
                    // Place player in the first column.
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
                    // Place the exit in the last column.
                    do
                    {
                        pos = new Position(
                            random.Next(world.XDim),
                            world.YDim - 1);
                    } while (world.IsOccupied(pos));

                    agent = new Agent(pos, type, (World)world);
                    break;
                default:
                    // Place in a random position.
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

        /// <summary>
        /// Generate a new level based on the current level we're in.
        /// </summary>
        /// <param name="level"></param>
        private void GenerateLevel(int level)
        {
            int obs, pow;
            obs = random.Next(0, (Math.Min(world.XDim,world.YDim))-1);
            pow = random.Next(2, Math.Max(world.XDim, world.YDim));

            // If it's the first level, place a new player.
            if (level == 0)
                PlaceAgent(AgentType.Player);

            // Place the exit.
            PlaceAgent(AgentType.Exit);

            // Place the objects.
            for (int j = 0; j < obs; j++)
            {
                    PlaceAgent(AgentType.Obstacle);
            }

            // Setup the enemies...
            for (int j = 0; j < 3 + level && j <= world.XDim * world.YDim / 2; j++)
            {
                if (ProbabilityOfBoss(level))
                {
                    PlaceAgent(AgentType.BigEnemy);
                }
                else
                    PlaceAgent(AgentType.SmallEnemy);
            }

            // Setup the powerups...
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

        /// <summary>
        /// When the level changes, clear out the agent list and place the
        /// player in the starting column.
        /// </summary>
        private void ResetPlayer()
        {
            agents.RemoveAll(a => a.Type != AgentType.Player);
            agents.Find(a => a.Type == AgentType.Player).ResetPlayerPos();
        }

        /// <summary>
        /// Method that checks if the player's health is below or equal to 0.
        /// </summary>
        /// <returns>True if yes, false if not.</returns>
        private bool IsPlayerDead()
        {
            if (PlayerHP <= 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method that returns if it will be a boss or not based on
        /// probability.
        /// </summary>
        /// <param name="level">Current level.</param>
        /// <returns>True or false.</returns>
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

        /// <summary>
        /// Method that returns the probability of a PowerUp.
        /// </summary>
        /// <param name="level">Current level.</param>
        /// <returns>Probability of PowerUp.</returns>
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