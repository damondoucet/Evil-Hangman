using System;

namespace EvilHangman.ConsoleUI
{
    class Program
    {
        static void Main()
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
