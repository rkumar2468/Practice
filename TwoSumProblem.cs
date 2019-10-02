public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        int []ret = new int[2];
        Algo03(nums, target, ret);
            
        return ret;
    }
    
    // Uses dictionary to reduce runtime complexity
    // O(N)
    public void Algo03(int []nums, int target, int []result)
    {
        int len = 0;
        Dictionary<int, int> dict = new Dictionary<int, int>();
        foreach(int val in nums)
        {
            int value = 1;
            if (dict.ContainsKey(val))
            {
                value = dict[val];
                dict.Remove(val);
                dict.Add(val, ++value);
            }
            else
            {
                dict.Add(val, value);
            }
            
            len++;
        }
        
        int counter = 0;
        foreach (int val in nums)
        {
            result[0] = counter;
            if (dict.ContainsKey(target - val) &&
               (val != target - val || dict[val] >= 2))
            {
                result[1] = findIdxOfANum(nums, target - val, counter + 1, len);
                break;
            }
            
            counter++;
        }
    }
    
    public int findIdxOfANum(int []nums, int target, int startIdx, int endIdx)
    {
        int res = -1;
        for(int i = startIdx; i< endIdx; i++)
        {
            if(nums[i] == target)
            {
                res = i;
            }
        }
        
        return res;
    }
    
    // O(N^2) complexity
    public void Algo01(int []nums, int target, int []result)
    {
        int counter = 0, first = -1, second = -1;
        // Assumption - target can be + and -
        // Assumption - unsorted array
        int len = nums.Length();
        foreach(int val in nums)
        {
            first = counter;
            second = findIdxOfANum(nums, target - val, first + 1, len);
            if (second > -1 )
            {
                break;
            }
            
            counter++;
        }
        result[0] = first;
        result[1] = second;
        
    }
    
    public int findNum(int []nums, int start, int end, int target)
    {
        int retIdx = -1;
        for(int i = start; i<end; i++)
        {
            if (nums[i] == target)
            {
                retIdx = i;
                break;
            }
        }
        
        return retIdx;
    }
}

public static class IntArrayExt
{
    public static int Length(this int []nums)
    {
        int counter = 0;
        foreach (int v in nums) 
        {
            counter++;            
        }
        return counter;
    }
}

// Algo 02
// Sort
// binarySearch
// Overall - n*logn time complexity

// Algo 03
// HashSet
// Time - O(n)
// Space - O(n)
