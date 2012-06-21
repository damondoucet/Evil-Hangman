using System.Collections.Generic;
using System.Linq;

namespace EvilHangman
{
    public class Game
    {
        private readonly HashSet<char> _guessedLetters;

        private readonly WordList _wordList;
        public int GuessesLeft { get; set; }

        public Game(string dictionaryPath, int wordLength, int initialGuesses)
        {
            GuessesLeft = initialGuesses;

            _guessedLetters = new HashSet<char>();
            _wordList = new WordList(dictionaryPath, wordLength);
        }

        public bool IsValidGuess(string guess)
        {
            guess = guess.Trim().ToUpper();
            return guess.Length == 1 &&
                char.IsLetter(guess[0]) &&
                !_guessedLetters.Contains(guess[0]);
        }

        public void ApplyGuess(string guess)
        {
            char g = guess.Trim().ToUpper()[0];

            _guessedLetters.Add(g);
            _wordList.ApplyGuess(g);

            GuessesLeft--;
        }

        public string GetCurrentMask()
        {
            return _wordList.CurrentMask.Mask;
        }

        public string GetFirstWord()
        {
            return _wordList.CurrentMask.Words.First();
        }

        public IEnumerable<char> GetGuessedLetters()
        {
            return _guessedLetters;
        }

        public IEnumerable<char> GetUnguessedLetters()
        {
            return Enumerable.Range('A', 'Z' - 'A' + 1).Select(x => (char)x).Except(_guessedLetters);
        }

        public bool HasWon()
        {
            return !_wordList.CurrentMask.Mask.Contains('_');
        }

        public bool HasLost()
        {
            return !HasWon() && GuessesLeft == 0;
        }
    }
}
