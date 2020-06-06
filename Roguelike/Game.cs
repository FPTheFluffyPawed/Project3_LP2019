using System;
using System.Collections.Generic;
using System.Text;

namespace Roguelike
{
    public class Game
    {
        private ConsoleUserInterface ui;

        public Game()
        {
            ui = new ConsoleUserInterface(this);
        }

        public void Start()
        {
            ui.Menu();
        }

        public void Play()
        {
            Console.WriteLine("\nGame!\n");
        }
    }
}
