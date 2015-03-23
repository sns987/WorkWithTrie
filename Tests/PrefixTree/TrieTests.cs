using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BackEndTestApp.PrefixTree;
using NUnit.Framework;

namespace Tests.PrefixTree
{
    [TestFixture]
    public class TrieTests
    {
        private static IEnumerable CreateTrieTestCases
        {
            get
            {
                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("kare", 10),
                        new KeyValuePair<string, int>("kanojo", 20),
                        new KeyValuePair<string, int>("karetachi", 1),
                        new KeyValuePair<string, int>("korosu", 7),
                        new KeyValuePair<string, int>("sakura", 3)
                    },
                    new[]
                    {
                        "k", "ka", "kar"
                    }
                })
                    .Returns(new List<string>
                    {
                        "kanojo",
                        "kare",
                        "korosu",
                        "karetachi",
                        "kanojo",
                        "kare",
                        "karetachi",
                        "kare",
                        "karetachi"
                    });

                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("kare", 10),
                        new KeyValuePair<string, int>("kara", 10),
                        new KeyValuePair<string, int>("kanojog", 20),
                        new KeyValuePair<string, int>("kanojo", 20),
                        new KeyValuePair<string, int>("karetachi", 1),
                        new KeyValuePair<string, int>("karetb", 1),
                        new KeyValuePair<string, int>("korosu", 7),
                        new KeyValuePair<string, int>("korosuba", 7)
                    },
                    new[]
                    {
                        "k"
                    }
                })
                    .Returns(new List<string>
                    {
                        "kanojo",
                        "kanojog",
                        "kara",
                        "kare",
                        "korosu",
                        "korosuba",
                        "karetachi",
                        "karetb"
                    })
                    .SetName("Check sorting for equals frequency");

                yield return new TestCaseData(new object[]
                {
                    new[]
                    {
                        new KeyValuePair<string, int>("kare", 10),
                        new KeyValuePair<string, int>("kara", 10),
                        new KeyValuePair<string, int>("kanojog", 20),
                        new KeyValuePair<string, int>("kanojo", 20),
                        new KeyValuePair<string, int>("kanoja", 20),
                        new KeyValuePair<string, int>("karetachi", 1),
                        new KeyValuePair<string, int>("karetb", 3),
                        new KeyValuePair<string, int>("korosu", 7),
                        new KeyValuePair<string, int>("korosubha", 7),
                        new KeyValuePair<string, int>("korosuba", 7),
                        new KeyValuePair<string, int>("korosubay", 7),
                        new KeyValuePair<string, int>("korosubayrt", 7),
                        new KeyValuePair<string, int>("korosubayr", 7)
                    },
                    new[]
                    {
                        "k"
                    }
                })
                    .Returns(new List<string>
                    {
                        "kanoja",
                        "kanojo",
                        "kanojog",
                        "kara",
                        "kare",
                        "korosu",
                        "korosuba",
                        "korosubay",
                        "korosubayr",
                        "korosubayrt"
                    })
                    .SetName("Check sorting if more than 10 elements");
            }
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

        [Test, TestCaseSource("CreateTrieTestCases")]
        public List<string> CreateTrieTest(KeyValuePair<string, int>[] words, string[] userWords)
        {
            var trie = CreateTrie(words);
            var result = new List<string>();

            foreach (var foundWords in userWords.Select(trie.FindFor).Where(foundWords => foundWords.Count() != 0))
            {
                result.AddRange(foundWords);
            }
            return result;
        }
    }
}