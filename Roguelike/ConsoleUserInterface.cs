using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class ConsoleUserInterface
    {
        // Empty space
        private const string EMPTY = null;

        // Colors
        private readonly ConsoleColor defBackground = Console.BackgroundColor;
        private readonly ConsoleColor defForeground = Console.ForegroundColor;
        private readonly ConsoleColor playerColor = ConsoleColor.Yellow;
        private readonly ConsoleColor enemyColor = ConsoleColor.Red;
        private readonly ConsoleColor powerUpColor = ConsoleColor.Blue;

        // other variables
        private ConsoleKeyInfo cki;

        // Reference game.
        private Game game;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleUserInterface(Game game)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.CursorVisible = false;

            this.game = game;
        }

        public void Menu()
        {
            bool loop = true;

            while(loop)
            {
                Console.Clear();
                Console.WriteLine("- A Roguelike Game -");
                Console.WriteLine("1. New Game");
                Console.WriteLine("2. High Score");
                Console.WriteLine("3. Instructions");
                Console.WriteLine("4. Credits");
                Console.WriteLine("5. Quit");

                cki = Console.ReadKey(true);

                switch (cki.KeyChar.ToString())
                {
                    case "1":
                        PlayGame();
                        break;
                    case "2":
                        ShowScores();
                        break;
                    case "3":
                        ShowIntructions();
                        break;
                    case "4":
                        ShowCredits();
                        break;
                    case "5":
                        ExitGame();
                        break;
                    default:
                        ErrorOption();
                        break;
                }
            }
        }

        private void PlayGame()
        {
            Console.Clear();
            Console.WriteLine("\n*** LAUNCHING GAME... ***\n");
            game.NewGame();
            Console.WriteLine("\n*** CLOSING GAME... ***\n");
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowScores()
        {
            Console.Clear();
            // Read from file and show scores
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowIntructions()
        {
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("The creators of this program are:");
            Console.WriteLine("- Diogo Henriques \t(X)");
            Console.WriteLine("- Inácio Amerio \t(21803493)");
            Console.WriteLine("- João Dias \t\t(X)");
            Console.WriteLine("\nMade for LP1 2019 as a third project!\n");

            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ExitGame()
        {
            Console.WriteLine("\nCLOSING...");
            Environment.Exit(1);
        }

        private void ErrorOption()
        {
            Console.WriteLine("\nInsert a valid option!\n");
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        public void RenderWorld(IReadOnlyWorld world)
        {
            Console.SetCursorPosition(0, 0);
            for(int x = 0; x < world.XDim; x++)
            {
                for(int y = 0; y < world.YDim; y++)
                {
                    Position pos = new Position(x, y);

                    if(world.IsOccupied(pos))
                    {
                        Agent agent = world.GetAgentAt(pos);

                        if (agent.Type == AgentType.Player)
                            Console.Write(" P ");
                        else if (agent.Type == AgentType.Exit)
                            Console.Write(" O ");
                        else if (agent.Type == AgentType.SmallEnemy)
                            Console.Write(" e ");
                        else if (agent.Type == AgentType.BigEnemy)
                            Console.Write(" E ");
                        else if (agent.Type == AgentType.SmallPowerUp)
                            Console.Write(" q ");
                        else if (agent.Type == AgentType.MediumPowerUp)
                            Console.Write(" p ");
                        else if (agent.Type == AgentType.BigPowerUp)
                            Console.Write(" Q ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            RenderInterface(world);
        }

        private void RenderInterface(IReadOnlyWorld world)
        {
            Console.SetCursorPosition(0 + world.XDim * 3, 0);
            Console.Write("Your health: " + game.PlayerHP + " HP.");
            Console.SetCursorPosition(0, world.YDim);
        }
    }
}
