using System.Collections.Generic;
using System.Linq;
using BackEndTestApp.Helpers;

namespace BackEndTestApp.PrefixTree
{
    public class Trie
    {
        private const int WithSamePrefixWordMaxCount = 10;
        private const char EndOfWordElement = '.';
        
        public Trie()
        {
            Root = new TrieNode(new TrieNodeData
            {
                CharKey = ' ',
            });
        }

        public TrieNode Root { get; set; }

        public void Insert(string word, int frequency)
        {
            var endOfWordElement = new TrieNode(new TrieNodeData
            {
                CharKey = EndOfWordElement,
                Frequency = frequency
            });

            var node = Root;

            foreach (var ch in word)
            {
                var next = node.Children.FirstOrDefault(n => n.Element.CharKey == ch);

                if (next == null)
                {
                    next = new TrieNode(new TrieNodeData
                    {
                        CharKey = ch,
                        Frequency = frequency
                    });
                    node.Children.Add(next);
                }
                node = next;
            }

            node.Children.Add(endOfWordElement);
        }

        public IEnumerable<string> FindFor(string str)
        {
            var result = new List<KeyValuePair<string, int>>();
            var parentNode = Root;
            var prefix = string.Empty;
            var fail = false;

            foreach (var ch in str)
            {
                parentNode = FindNode(ch, parentNode);
                if (parentNode == null)
                {
                    fail = true;
                    break;
                }
                prefix += ch;
            }

            if (!fail && parentNode.Element.CharKey.Equals(EndOfWordElement))
                result.Add(new KeyValuePair<string, int>(prefix, parentNode.Element.Frequency));

            if (fail)
                return Enumerable.Empty<string>();

            GetWords(parentNode, result, prefix);
            return result.Select(r => r.Key);
        }

        private static void GetWords(TrieNode parentNode, List<KeyValuePair<string, int>> result, string prefix)
        {
            if (parentNode.Children == null)
                return;

            foreach (var node in parentNode.Children)
            {
                if (node.Element.CharKey.Equals(EndOfWordElement))
                {
                    var newItem = new KeyValuePair<string, int>(prefix, node.Element.Frequency);
                    var position = BinarySearch.BinarySearchForStringWithFrequency(result, newItem);

                    if (position >= WithSamePrefixWordMaxCount)
                        continue;
                    result.Insert(position, newItem);

                    if (result.Count > WithSamePrefixWordMaxCount)
                        result.RemoveAt(WithSamePrefixWordMaxCount);
                    continue;
                }

                GetWords(node, result, prefix + node.Element.CharKey);
            }
        }

        private static TrieNode FindNode(char ch, TrieNode node)
        {
            node = node.Children.Find(n => n.Element.CharKey.Equals(ch));
            return node;
        }
    }
}