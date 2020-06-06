using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class Game
    {
        private ConsoleUserInterface ui;

        private IReadOnlyWorld world;

        private int turn, level;

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
            Play();
        }

        private void Play()
        {
            Console.Clear();

            gameOver = false;

            // Setup the level for the first time...
            PlaceAgent(AgentType.Player);

            for(int i = 0; i < 5; i++)
            {
                PlaceAgent(AgentType.SmallEnemy);
            }

            ui.RenderWorld(world);

            // Run through all positions and check
            foreach (Agent a in agents)
            {
                if (a.Type == AgentType.Player)
                    a.PlayTurn();
            }
            ui.RenderWorld(world);


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
                            world.YDim);
                    } while (world.IsOccupied(pos));

                    agent = new Agent(pos, type, (World)world);
                    agents.Add(agent);
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

        private void SetupLevel()
        {

        }

        private void ChangeLevel()
        {

        }

        private void DifficultyScale()
        {

        }
    }
}
