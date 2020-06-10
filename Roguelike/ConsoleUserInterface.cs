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
        private readonly ConsoleColor fgExitColor = ConsoleColor.DarkRed;
        private readonly ConsoleColor bgExitColor = ConsoleColor.Gray;
        private readonly ConsoleColor obstacleColor = ConsoleColor.DarkGreen;
        private readonly ConsoleColor titleColor = ConsoleColor.DarkGray;

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

            while (loop)
            {
                Console.Clear();
                SetColor(titleColor, defBackground);
                Console.WriteLine(@"
______                       _     _ _        
| ___ \                     | |   (_) |       
| |_/ /___   __ _ _   _  ___| |    _| | _____ 
|    // _ \ / _` | | | |/ _ \ |   | | |/ / _ \
| |\ \ (_) | (_| | |_| |  __/ |___| |   <  __/
\_| \_\___/ \__, |\__,_|\___\_____/_|_|\_\___|
            __/ |                            
            |___/ ");
                SetDefaultColor();

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
                        Console.Clear();
                        ShowIntructions();
                        break;
                    case "4":
                        Console.Clear();
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
            SetColor(titleColor, defBackground);
            Console.Write(@" 
 _____          _                   _   _                 
|_   _|        | |                 | | (_)                
  | | _ __  ___| |_ _ __ _   _  ___| |_ _  ___  _ __  ___ 
  | || '_ \/ __| __| '__| | | |/ __| __| |/ _ \| '_ \/ __|
 _| || | | \__ \ |_| |  | |_| | (__| |_| | (_) | | | \__ \
 \___/_| |_|___/\__|_|   \__,_|\___|\__|_|\___/|_| |_|___/");
            SetDefaultColor();
            Console.WriteLine("\n\n-The player starts randomly on the left side"
            + " of the map and has to go to the exit of the level that is on the"
            + " right side of the map.\n");
            Console.WriteLine("-Throughout the level the player will encounter"+
            "enemies, obstacles and power-ups.");
            Console.WriteLine("\tPlayer - The player can move up, down, left" +
            " and right. Each turn the player can walk 2 houses and this action"
            + "costs 1hp per house.");
            Console.WriteLine("\tEnemy - Each turn the enemy walks 1 house to" +
            "the player direction. There are 2types of enemies minions and"
            + "bosses.");
            Console.WriteLine("\t\tMinion - Deals 5 damage to the player");
            Console.WriteLine("\t\tBoss - Deals 5 damage to the player\n");
            Console.WriteLine("\tPower-Ups - Power-Ups recover the player hp." +
            "There are 3 types of power-ups and the larger the more rare:");
            Console.WriteLine("\t\tSmall - Recovers 4hp");
            Console.WriteLine("\t\tMedium - Recovers 8hp");
            Console.WriteLine("\t\tLarge - Recovers 16hp");
            Console.WriteLine("\nThe game ends when the player hp reaches 0" +
            " and an highscore will be saved.");
            Console.WriteLine("Press any key to return...\n");
            Console.ReadKey(true);
        }

        private void ShowCredits()
        {

            Console.WriteLine(@"
 _____              _ _ _       
/  __ \            | (_) |      
| /  \/_ __ ___  __| |_| |_ ___ 
| |   | '__/ _ \/ _` | | __/ __|
| \__/\ | |  __/ (_| | | |_\__ \
 \____/_|  \___|\__,_|_|\__|___/");
            Console.WriteLine("\n\nThe creators of this program are:");
            Console.WriteLine("- Diogo Henriques \t(21802132)");
            Console.WriteLine("- Inácio Amerio \t(21803493)");
            Console.WriteLine("- João Dias \t\t(21803573)");
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

            Console.SetCursorPosition(0 + world.XDim * 3, 1);
            Console.Write("Current level : " + game.Level);

            Console.SetCursorPosition(0, world.YDim);
        }

        private void SetColor(ConsoleColor fgColor, ConsoleColor bgColor)
        {
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
        }

        private void SetDefaultColor()
        {
            SetColor(defForeground, defBackground);
        }

        private void AgentColor(AgentType aType)
        {
            if (aType == AgentType.Player) SetColor(playerColor, 0);
            else if (aType == AgentType.SmallEnemy || aType == AgentType.BigEnemy)
                SetColor(playerColor, 0);
            else if (aType == AgentType.SmallPowerUp
            || aType == AgentType.MediumPowerUp
            || aType == AgentType.BigPowerUp) SetColor(powerUpColor, 0);
            else if (aType == AgentType.Obstacle)
                SetColor(0, obstacleColor);
            else if (aType == AgentType.Exit) SetColor(fgExitColor, bgExitColor);
        }
    }
}