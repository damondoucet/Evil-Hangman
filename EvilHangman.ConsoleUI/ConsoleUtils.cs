using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvilHangman.ConsoleUI
{
    static class ConsoleUtils
    {
        public static string FormatEnumerableCharacters(IEnumerable<char> characters)
        {
            var list = characters.ToList(); //prevent multiple enumeration

            if (list.Count == 0)
                return string.Empty;

            var builder = list.Aggregate(new StringBuilder(), (sb, c) => sb.Append(c).Append(" "));
            return builder.Remove(builder.Length - 1, 1).ToString();
        }

        public static string GetInput(string prompt, string onInvalidGuess, Predicate<string> isValidGuess)
        {
            return GetInput(prompt, onInvalidGuess, isValidGuess, str => str);
        }

        public static T GetInput<T>(string prompt, string onInvalidGuess, Predicate<string> isValidGuess, Func<string, T> cast)
        {
            Console.Write(prompt + " ");
            string input = Console.ReadLine();

            if (!isValidGuess(input))
            {
                Console.WriteLine(onInvalidGuess);
                return GetInput(prompt, onInvalidGuess, isValidGuess, cast);
            }

            return cast(input);
        }
    }
}
