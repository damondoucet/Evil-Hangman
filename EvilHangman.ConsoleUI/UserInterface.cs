using System;

namespace EvilHangman.ConsoleUI
{
    class UserInterface
    {
        private const int InitialGuesses = 20;
        private const string DictionaryPath = "words.txt";
        
        public Game Game { get; set; }

        public UserInterface(int wordLength)
        {
            InitializeGame(wordLength);
        }

        public UserInterface()
        {
            InitializeGame(GetWordLength());
        }

        public void InitializeGame(int wordLength)
        {
            Game = new Game(DictionaryPath, wordLength, InitialGuesses);
        }

        public int GetWordLength()
        {
            return ConsoleUtils.GetInput("How many letters?", "Invalid number of letters",
                letters => { uint x; return uint.TryParse(letters, out x); },
                int.Parse);
        }

        public bool IsDone()
        {
            return Game.HasWon() || Game.HasLost();
        }

        public void WriteInfo()
        {
            Console.WriteLine(ConsoleUtils.FormatEnumerableCharacters(Game.GetCurrentMask()));
            Console.WriteLine("Guessed letters: {0}", ConsoleUtils.FormatEnumerableCharacters(Game.GetGuessedLetters()));
            Console.WriteLine("Unguessed letters: {0}", ConsoleUtils.FormatEnumerableCharacters(Game.GetUnguessedLetters()));
            Console.WriteLine("Guesses left: {0}", Game.GuessesLeft);
            Console.WriteLine();
        }

        public string GetGuess()
        {
            return ConsoleUtils.GetInput("What is your guess?", "Invalid guess", guess => Game.IsValidGuess(guess), str => str.ToUpper());
        }

        public void ApplyGuess(string guess)
        {
            Game.ApplyGuess(guess);
        }

        public void WriteExit()
        {
            if (Game.HasWon())
                WriteWinText();
            else
                WriteLoseText();

            Console.WriteLine();
            Console.WriteLine("Thanks for playing!");
        }

        public void WriteWinText()
        {
            Console.WriteLine("Congratulations! The word was {0}", Game.GetFirstWord());
        }

        public void WriteLoseText()
        {
            Console.WriteLine("Sorry, the word was {0}", Game.GetFirstWord());
        }
    }
}
