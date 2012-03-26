using System;
using System.Collections.Generic;
using System.Linq;

namespace EvilHangman.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UserInterface();

            while (!ui.IsDone())
            {
                ui.WriteInfo();
                ui.ApplyGuess(ui.GetGuess());
            }

            ui.WriteExit();
            Console.ReadLine();
        }
    }
}
