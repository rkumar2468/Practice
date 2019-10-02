public class Solution {
    public int LengthOfLongestSubstring(string s) {
        Dictionary<char, int> charDict = new Dictionary<char, int>();
        int lMax = 0, max = 0, start = 0, idx = -1;
        foreach(char ch in s)
        {
            idx++;
            if (charDict.ContainsKey(ch))
            {
                // reset the lMax, start, and charDict
                int newStart = charDict[ch] + 1;
                lMax -= (newStart - start);
                RemoveUnwantedChars(charDict, s, start, newStart);
                start = newStart;
            }
            
            // otherwise, update charDict, end and lMax
            charDict.Add(ch, idx);
            lMax++;
            
            // Updating the global max
            if (lMax > max) max = lMax;
        }
        
        return max;
    }
    
    public void RemoveUnwantedChars(Dictionary<char, int> charDict, string s, int start, int end)
    {
        for (int i = start; i<end; i++)
        {
            charDict.Remove(s[i]);
        }
    }
}
