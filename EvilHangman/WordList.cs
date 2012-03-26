using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace EvilHangman
{
    internal class WordList
    {
        public MaskGroup CurrentMask { get; set; }
        
        public WordList(string dictionaryPath, int wordLength)
        {
            CurrentMask = new MaskGroup(
                new string('_', wordLength),
                ReadWordsOfSpecificLength(dictionaryPath, wordLength)
                );
        }

        private IEnumerable<string> ReadWordsOfSpecificLength(string dictionaryPath, int wordLength)
        {
            using (var sr = new StreamReader(dictionaryPath))
                return sr.ReadToEnd().Split(' ').Where(word => word.Length == wordLength).ToList();
        }

        public void ApplyGuess(char guess)
        {
            CurrentMask = FindBestMaskFromGuess(guess); 
        }

        private MaskGroup FindBestMaskFromGuess(char guess)
        {
            var masks = CurrentMask.GetGroupsByGuessMask(guess);

            //note that we return the immediately best opportunity, not what might be the overall best
            return masks.Aggregate((agg, cur) => cur.Words.Count() > agg.Words.Count() ? cur : agg);
        }
    }
}
