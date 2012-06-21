using System.Collections.Generic;
using System.Linq;

namespace EvilHangman
{
    internal class MaskGroup
    {
        //Mask might look like A _ _ B E _ R
        public readonly string Mask;
        public readonly IEnumerable<string> Words;

        public MaskGroup(string mask, IEnumerable<string> words)
        {
            Mask = mask;
            Words = words;
        }

        public IEnumerable<MaskGroup> GetGroupsByGuessMask(char guess)
        {
            return Words.GroupBy(word => GetMaskFromWord(word, guess))
                .Select(group => new MaskGroup(group.Key, group));
        }

        private string GetMaskFromWord(string word, char letter)
        {
            //the new mask is just like the old mask except adding the new letter in everywhere it occurs in the word
            return new string(
                word.Zip(Mask,
                    (dirty, masked) => dirty == letter ? dirty : masked)
                .ToArray());
        }
    }
}
