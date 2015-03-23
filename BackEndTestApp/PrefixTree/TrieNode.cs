using System.Collections.Generic;

namespace BackEndTestApp.PrefixTree
{
    public class TrieNode
    {
        public TrieNode(TrieNodeData element)
        {
            Element = element;

            if (Children == null)
                Children = new List<TrieNode>();
        }

        public List<TrieNode> Children { get; set; }
        public TrieNodeData Element { get; set; }
    }

    public class TrieNodeData
    {
        public int Frequency { get; set; }
        public char CharKey { get; set; }
    }
}