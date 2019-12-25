using System;
using System.Linq;
using System.Timers;

namespace Practice2019
{
    public static class Sorting
    {
        public static void MergeSort(int[] array)
        {
            int[] copy = new int[array.Length];
            MergeSort(array, 0, array.Length - 1, copy);
        }

        private static void MergeSort(int[] array, int left, int right, int[] copiedArray)
        {
            if (left >= right) return;

            int half = (left + right) / 2;
            MergeSort(array, left, half, copiedArray);
            MergeSort(array, half + 1, right, copiedArray); // half + 1 is the logic here....
            mergeIterative(array, left, half, half + 1, right, copiedArray);
        }

        private static void mergeIterative(int[] array, int ls, int le, int rs, int re, int[] copiedArray)
        {
            int lStart = ls, lEnd = le, rStart = rs, rEnd = re;
            int index = lStart;
            while(lStart <= lEnd && rStart <= rEnd)
            {
                if (array[lStart] < array[rStart])
                {
                    copiedArray[index] = array[lStart];
                    lStart++;
                }
                else
                {
                    copiedArray[index] = array[rStart];
                    rStart++;
                }

                index++;
            }

            // Copy remaining elements
            ArrayCopy(array, lStart, copiedArray, index, lEnd - lStart + 1);
            ArrayCopy(array, rStart, copiedArray, index, rEnd - rStart + 1);

            // Copy them back to the original array
            ArrayCopy(copiedArray, ls, array, ls, le - ls + 1);
            ArrayCopy(copiedArray, rs, array, rs, re - rs + 1);
        }

        #region QuickSort 02
        public static void QS(int [] array)
        {
            QuickSort2(array, 0, array.Length - 1);
        }

        public static void QuickSort2(int[] array, int left, int right)
        {
            if (left >= right) return;

            int pivotIdx = (left + right) / 2;
            int partition = PartitionArrayAtPivot(array, left, right, array[pivotIdx]);
            QuickSort2(array, left, partition - 1); // partition - 1 is the logic here...
            QuickSort2(array, partition, right);
        }

        public static int PartitionArrayAtPivot(int[] array, int left, int right, int pivot)
        {
            // Move all the elelments which are bigger than pivot to right
            // move all the elements which are smaller than pivot to the left
            while(left <= right)
            {
                while (array[left] < pivot) left++; // <= is a bad idea....
                while (array[right] > pivot) right--; // <= is a bad idea....

                if (left <= right)
                {
                    Swap(array, left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }

        #endregion QuickSort 02

        #region QuickSort 1

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        public static void QuickSort(int[] array, int start, int end)
        {
            if (start >= end) return;

            int pivot = (start + end) / 2;
            int partition = PartitionArray(array, start, end, pivot);
            QuickSort(array, start, partition - 1);
            QuickSort(array, partition, end);
        }

        public static int PartitionArray(int[] array, int start, int end, int pivot)
        {
            while(start <= end)
            {
                while (array[start] < array[pivot] && start < end) start++;
                while (array[end] > array[pivot] && start < end) end--;
                if (start <= end)
                {
                    Swap(array, start, end);
                    start++;
                    end--;
                }
            }

            return start;
        }

        #endregion QuickSort 1

        public static void BubbleSort(int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                for(int j = i; j < array.Length; j++)
                {
                    if(array[j] < array[i])
                    {
                        Swap(array, i, j);
                    }
                }
            }
        }

        #region utils
        public static void Swap(int[] array, int pos1, int pos2)
        {
            int temp = array[pos1];
            array[pos1] = array[pos2];
            array[pos2] = temp;
        }

        public static void PrintArray(int[] array)
        {
            Console.Write("Printing Array: [");
            foreach(int val in array)
            {
                Console.Write($"{val},");
            }
            Console.WriteLine("]");
        }

        public static void ArrayCopy(int[] source, int sStart, int[] destination, int dStart, int number)
        {
            while(number-- >0)
            {
                destination[dStart] = source[sStart];
                sStart++;
                dStart++;
            }
        }
        #endregion utils
    }

    public class Solution
    {
        public string LongestPalindrome(string s)
        {
            // int.MinValue, double.MinValue
            // Empty string handler.
            if (s.Length <= 1) return s;

            return LongestPalinDP(s);
        }

        public string LongestPalinDP(string s)
        {
            // Equations:
            // P(i,j) = true { iff P(i+1, j-1) = true & s[i] = s[j] }
            // P(i,i) = true 
            // P(i, i+1) = true { iff S[i] == S[i+1] }
            int length = s.Length;
            int[][] palindromes = new int[length][];
            for (int k = 0; k < length; k++)
            {
                palindromes[k] = new int[length];
            }

            
            int start = 0, end = 0, maxPalinLength = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    if (i == j)
                    {
                        palindromes[i][j] = 1;
                    }
                    else
                    {
                        if (j == i + 1)
                        {
                            if (s[i] == s[j])
                            {
                                palindromes[i][j] = 1;
                                palindromes[j][i] = 1;
                                // Two length palin.
                                start = i;
                                end = j;
                                maxPalinLength = Math.Max(j - i, maxPalinLength);
                            }
                            else
                            {
                                palindromes[i][j] = 0;
                                palindromes[j][i] = 0;
                            }
                        }
                        else
                        {
                            palindromes[i][j] = -1;
                            palindromes[j][i] = -1;
                        }
                    }
                }
            }
            // PrintArray(palindromes);            

            // i - validating if there is a i length palin starting from 
            //..index I=j to J=(j + i - 1) based on (I-1, J-1) subsctring.
            // since we are building bottom up - 1 char palin, 2 char palin, 3 char palin
            //..4 char palin this can be complete in O(n^2) time complexity.
            for (int i = 3; i <= length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (GetPalinValue(palindromes, j + 1, j + i - 2, length) == 1 && 
                        AreCharsEqual(s, i + j - 1, j))
                    {
                        palindromes[i + j - 1][j] = 1;
                        palindromes[j][i + j - 1] = 1;
                        // maxPalinLength = Math.Max(j - i, maxPalinLength);
                        if (i > maxPalinLength)
                        {
                            start = j;
                            end = i + j - 1;
                            maxPalinLength = end - start;
                        }
                    }
                }
            }

            return s.Substring(start, maxPalinLength + 1);

        }

        private bool AreCharsEqual(string s, int i, int j)
        {
            int length = s.Length;
            if (i < 0 || j < 0 || i >= length || j >= length) return false;

            return s[i] == s[j];
        }

        private int GetPalinValue(int[][] palindromes, int v1, int v2, int length)
        {
            // boundaries check
            if (v1 < 0 || v2 < 0 || v1 >= length || v2 >= length) return -1;

            return palindromes[v1][v2];
        }

        public void PrintArray(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i][j]},");
                }
                Console.WriteLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TestTrie();
            return;

            TestSegmentTrees();
            return;

            TestLRUCache();
            return;

            Solution s = new Solution();
            string str = "camelsucabacusraja";
            Console.Write($" Longest Palindrome string from [{str}] is [{s.LongestPalindrome(str)}]\n");
            return;

            int[] array = new int[] { 10000, 1, 3, -1, -10, 0, 0, 0, -10 };
            // Sorting.MergeSort(array);
            Sorting.PrintArray(array);

            TestSqrt();
            TestIsPrime();

            Console.WriteLine($"Square Root of 100 is {SqrtFloor(100)}");

            LinkedListFacade list = new LinkedListFacade();
            list.Create(-10);
            list.Create(10);
            list.Create(4);
            list.Create(0);
            list.Create(02);

            list.PrintList();
            // list.ReverseList();
            list.PrintListInReverse();

            TestGraphScenarios();

            double[] ArtifactsMaxLimits = new double[] {0, 10, 50, 100, 1000, double.MaxValue };

            Console.WriteLine(" Binary search: " + ArtifactsMaxLimits.ToList().BinarySearch(52));
        }

        private static void TestTrie()
        {
            Trie t = new Trie();
            t.AddStringToTrie("Hello");
            t.AddStringToTrie("hell");
            t.AddStringToTrie("Raj");
            t.AddStringToTrie("scotomisation");
            t.AddStringToTrie("scotomisatiON");
            t.AddStringToTrie("Scotomisation");
            t.AddStringToTrie("Scotomisation_123!");

            string testStr = "hello";
            TrieSearch(t, testStr);

            testStr = "he";
            TrieSearch(t, testStr);

            testStr = "scoto";
            TrieSearch(t, testStr);

            testStr = "scotomisatiON";
            TrieSearch(t, testStr);

            testStr = "Scotomisation_123!";
            TrieSearch(t, testStr);
        }

        private static void TrieSearch(Trie t, string word)
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"String {word} exists in dictionary: {t.HasString(word)}");
            Console.WriteLine($"Prefix {word} exists in dictionary: {t.HasPrefix(word)}");
            Console.WriteLine("=================================");
        }

        private static void TestSegmentTrees()
        {
            int[] array = new int[] { 0, -1, 11, 18, -5, 24, 33 };
            MinSegTree minSegmentTree = new MinSegTree();
            minSegmentTree.BuildTree(array);
            minSegmentTree.PrintMinSegTree();
            printMinSegValues(minSegmentTree, array, 1, 3);
            printMinSegValues(minSegmentTree, array, 1, 6);
            printMinSegValues(minSegmentTree, array, 2, 3);
        }

        private static void printMinSegValues(MinSegTree segTree, int[] array, int start, int end)
        {
            Console.WriteLine();
            printArrayRange(array, start, end);
            Console.Write($"{segTree.GetMinValueInRange(start, end)}");
            Console.WriteLine();
        }

        private static void printArrayRange(int[] arr, int start, int end)
        {
            Console.Write("[");
            while (start < end)
            {
                Console.Write($"{arr[start]},");
                start++;
            }
            Console.Write($"{arr[end]}]:");
        }

        public static void TestLRUCache()
        {
            LRUCache cache = new LRUCache();
            cache.DumpCaches();

            cache.AddItemToCache(1, 10);
            cache.AddItemToCache(2, 11);
            cache.AddItemToCache(3, 12);

            cache.DumpCaches();

            Console.WriteLine($"cache.GetValueFromCache(2): {cache.GetValueFromCache(2)}\n Dump:");
            cache.DumpCaches();

            Console.WriteLine($"cache.GetValueFromCache(1): {cache.GetValueFromCache(1)}\n Dump:");
            cache.DumpCaches();
        }

        public static void TestIsPrime()
        {
            Console.WriteLine("Testing IsPrime started...");
            Console.WriteLine("*******************************");
            for (int i = 2; i < 1000; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write($"{i},");
                }
            }
            Console.WriteLine("\n*******************************");
            Console.WriteLine("Testing IsPrime complete...");
        }

        public static void TestSqrt()
        {
            Console.WriteLine("Testing sqrt started...");
            for (int i = 10; i < 1000;)
            {
                Console.WriteLine($"Sqrt({i}):{SqrtUsingBinSearch(i)}");
                i += 10;
            }

            Console.WriteLine("Testing sqrt complete...");
        }

        public static void TestGraphScenarios()
        {
            GraphFacade graph = new GraphFacade();
            graph.CreateGraphNode(10);
            graph.CreateGraphNode(5);
            graph.CreateGraphNode(15);
            graph.CreateGraphNode(100);
            graph.CreateGraphNode(25);
            graph.CreateGraphNode(999);
            graph.AddAdjacentNode(10, 5);
            graph.AddAdjacentNode(10, 15);
            graph.AddAdjacentNode(10, 25);
            graph.AddAdjacentNode(100, 999);
            graph.AddAdjacentNode(25, 10);
            graph.AddAdjacentNode(5, 100);
            graph.AddAdjacentNode(5, 10);

            graph.PrintGraph();

            GraphFacade newClonedNode = graph.Clone();
            newClonedNode.PrintGraph();

            Console.WriteLine("\nTimer started...");
            DateTime start = DateTime.Now;
            bool pathExist = graph.HasPathDFS(5, 25);
            TimeSpan timetaken = DateTime.Now - start;
            Console.WriteLine($"Result of path existence using DFS: {pathExist}");
            Console.WriteLine("Execution time: " + timetaken);

            Console.WriteLine("**=====================================================================**");
            Console.WriteLine("Timer started...");
            DateTime bfsStart = DateTime.Now;
            pathExist = graph.HasPathBFS(5, 25);
            TimeSpan bfstimetaken = DateTime.Now - bfsStart;
            Console.WriteLine($"Result of path existence using BFS: {pathExist}");
            Console.WriteLine("Execution time: " + bfstimetaken);
        }

        public static bool IsPrime(int num)
        {
            if (num <= 1) return false;
            if (num == 2 || num == 3) return true;

            int sqrtVal = SqrtUsingBinSearch(num);
            for (int i = 2; i <= sqrtVal; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int SqrtUsingBinSearch(int num)
        {
            if (num < 0) return 0;
            int retVal = 0;
            int start = 0, end = num;
            int mid = 0;
            while(start < end)
            {
                mid = (start + end) / 2;
                if (mid * mid == num)
                {
                    retVal = mid;
                    break;
                }

                if (mid * mid < num)
                {
                    start = mid + 1;
                    retVal = mid;
                }
                else
                {
                    end = mid;
                }
            }

            return retVal;
        }

        // ref: https://www.geeksforgeeks.org/find-square-root-number-upto-given-precision-using-binary-search/
        // Binary search way of implementing a sqrt.
        public static int SqrtFloor(int num)
        {
            int i = 1;
            for (; i <= num/2; i++)
            {
                if (i * i == num) break;
            }

            return i;
        }
    }
}
