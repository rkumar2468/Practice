namespace Practice2019
{
    // Reference: https://leetcode.com/articles/implement-trie-prefix-tree/
    public class Trie
    {
        TrieNode Root;
        public Trie()
        {
            Root = new TrieNode();
        }

        public void AddStringToTrie(string word)
        {
            TrieNode node = Root;
            int len = word.Length;
            for (int i = 0; i < len; i++)
            {
                if (node.GetLink(word[i]) == null)
                {
                    node.AddLink(word[i], new TrieNode());
                }

                node = node.GetLink(word[i]);
            }

            node.SetEndFlag();
        }

        public bool HasPrefix(string prefix)
        {
            return SearchString(prefix) != null;
        }

        public bool HasString(string word)
        {
            TrieNode node = SearchString(word);
            return node != null && node.IsEnd();
        }

        private TrieNode SearchString(string word)
        {
            TrieNode node = Root;
            int len = word.Length;
            for (int i = 0; i < len; i++)
            {
                node = node.GetLink(word[i]);
                if (node == null) break;
            }

            return node;
        }
    }
    public class TrieNode
    {
        TrieNode[] links;
        bool isEnd;

        public TrieNode()
        {
            links = new TrieNode[256];
        }

        public void SetEndFlag()
        {
            isEnd = true;
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public TrieNode GetLink(char c)
        {
            return links[c];
        }

        public void AddLink(char ch, TrieNode node)
        {
            links[ch] = node;
        }
    }
}
