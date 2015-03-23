using System;
using System.Collections.Generic;
using System.Linq;
using BackEndTestApp.Helpers;
using BackEndTestApp.PrefixTree;


namespace BackEndTestApp
{
    internal class Program
    {
        private static void Main()
        {
            var words = ReadHelper.ReadWordsWithFrequency();
            var userWords = ReadHelper.ReadUserWords();

            var trie = CreateTrie(words);

            foreach (var foundWords in userWords.Select(trie.FindFor).Where(foundWords => foundWords.Count() != 0))
            {
                foreach (var foundWord in foundWords)
                {
                    Console.WriteLine(foundWord);
                }
            }
            Console.ReadLine();
        }

        private static Trie CreateTrie(IEnumerable<KeyValuePair<string, int>> words)
        {
            var trie = new Trie();

            foreach (var @string in words)
            {
                var word = @string.Key;
                var frequency = @string.Value;

                trie.Insert(word, frequency);
            }
            return trie;
        }
    }
}