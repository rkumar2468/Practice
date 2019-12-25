using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice2019
{
    public enum Overlapp
    {
        None,
        Partial,
        Total,
        Invalid
    }
    public static class ListSTreeExtensions
    {
        public static SegmentTree Dequeue(this LinkedList<SegmentTree> queue)
        {
            SegmentTree retVal = queue.First();
            queue.RemoveFirst();
            return retVal;
        }
    }

    public class SegmentTree
    {
        int start;
        int end;
        int value;
        SegmentTree left;
        SegmentTree right;

        public SegmentTree(int value)
        {
            this.start = 0;
            this.end = 0;
            this.value = value;
        }

        public SegmentTree(int start, int end, int value)
        {
            this.start = start;
            this.end = end;
            this.value = value;
        }

        public void SetRanges(int start, int end)
        {
            if (start > end)
            {
                throw new InvalidOperationException("start should be less than or equal end.");
            }

            this.start = start;
            this.end = end;
        }

        public void SetValue(int val)
        {
            this.value = val;
        }

        public void SetLeft(SegmentTree left)
        {
            this.left = left;
        }

        public void SetRight(SegmentTree right)
        {
            this.right = right;
        }

        public int GetStart()
        {
            return start;
        }

        public int GetEnd()
        {
            return end;
        }

        public bool IsInRange(int start, int end)
        {
            return (start <= end) && (this.start <= start && this.end >= end);
        }

        public bool IsInPartialRange(int start, int end)
        {
            return (start <= end) && (this.start <= start || this.end >= end);
        }

        public int GetValue()
        {
            return value;
        }

        public int GetValueInRange(int start, int end, int defaultValue, Func<int, int, int> oper)
        {
            if (start > end)
            {
                throw new InvalidOperationException("invalid range");
            }

            int retValue = defaultValue;

            retValue = GetValueInRangeRecursively(this, start, end, defaultValue, oper);

            return retValue;
        }

        public void PrintSegmentTree()
        {
            Console.WriteLine("Segment Tree:");

            LinkedList<SegmentTree> queue = new LinkedList<SegmentTree>();
            PrintSegmentTreeBFS(queue);
        }

        private void PrintSegmentTreeBFS(LinkedList<SegmentTree> queue)
        {
            queue.AddLast(this);

            while(queue.Any())
            {
                SegmentTree node = queue.Dequeue();

                node.PrintDetail();
                Console.Write("->");
                SegmentTree left = node.GetLeftTreeNode();
                if (left != null)
                {
                    left.PrintDetail();
                    queue.AddLast(left);
                }

                SegmentTree right = node.GetRightTreeNode();
                if (right != null)
                {
                    right.PrintDetail();
                    queue.AddLast(right);
                }

                Console.WriteLine(";");
            }
        }

        private void PrintDetail()
        {
            Console.Write($"[{start}:{end}->{value}]");
        }
        
        private Overlapp GetOverlappState(SegmentTree node, int start, int end)
        {
            if (start <= node.GetStart() && end >= node.GetEnd())
            {
                return Overlapp.Total;
            }
            else if (node.GetStart() > end || node.GetEnd() < start)
            {
                // assuming that start <= end always...
                return Overlapp.None;
            }
            else if (node.GetStart() < start || node.GetEnd() > end)
            {
                return Overlapp.Partial;
            }

            return Overlapp.Invalid;
        }

        private int GetValueInRangeRecursively(SegmentTree node, int start, int end, int defaultValue, Func<int, int, int> oper)
        {
            // Logic:
            // Always return defaultValue if node is null or ranges are out of order.
            // There are always three cases: Total Overlapp, No OverLapp and Partial Overlapp
            // If we use the same order there will be no issues.
            // Total overlapp - no need to traverse further return the current node value.
            // No Overlapp - no need to traverse further return the default value.
            // Partial Overlapp - traverse all its child nodes to find the right value by perform the operations' 
            //..between the two childern. For instance for min seg trees it will be math.min(leftChildVal, rightChildVal) etc.
            if (node == null || start > end)
            {
                return defaultValue;
            }

            switch(GetOverlappState(node, start, end))
            {
                case Overlapp.Total:
                    return node.GetValue();
                case Overlapp.None:
                case Overlapp.Invalid:
                    return defaultValue;
                case Overlapp.Partial:
                    // do magic
                    return oper(GetValueInRangeRecursively(node.GetLeftTreeNode(), start, end, defaultValue, oper), 
                        GetValueInRangeRecursively(node.GetRightTreeNode(), start, end, defaultValue, oper));
                default:
                    return defaultValue;
            }

        }

        private SegmentTree GetRightTreeNode()
        {
            return right;
        }

        private SegmentTree GetLeftTreeNode()
        {
            return left;
        }
    }
    
    public static class Operations
    {
        public static int MinOperation(int a, int b)
        {
            return Math.Min(a, b);
        }
    }

    public class MinSegTree
    {
        SegmentTree minSegTree;        

        public int GetMinValueInRange(int start, int end)
        {
            int ret = int.MaxValue;

            ret = minSegTree.GetValueInRange(start, end, defaultValue: int.MaxValue, oper: Operations.MinOperation);

            return ret;
        }

        public void PrintMinSegTree()
        {
            minSegTree.PrintSegmentTree();
        }
        
        public void BuildTree(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new InvalidOperationException("Invalid array.");
            }

            minSegTree = BuildTreeRecursively(array, 0, array.Length - 1);
        }

        private SegmentTree BuildTreeRecursively(int[] arr, int start, int end)
        {
            // Logic:
            // If the ranges are out of order - return null
            // If we reached the tail end in dfs - build a node with start = end, value = array[start]
            // If not tail 
            //   -> Recursively go left first (start, mid)
            //   -> Recursively go right next (mid+1, end)
            // Build Root with (start, end) and value = Operation(left, right) - here operation is Math.Min
            // Set Left for root: root.SetLeft(left);
            // Set Right for root: root.SetRight(right);
            // Return root.
            // Time: O(2n - 1) as we traverse once per tree node and there will be 2n - 1 nodes.

            if (start > end)
            {
                return null;
            }

            if (start == end)
            {
                return new SegmentTree(start, end, value: arr[start]);
            }

            int mid = (start + end) / 2;

            SegmentTree leftNode = BuildTreeRecursively(arr, start, mid);
            SegmentTree rightNode = BuildTreeRecursively(arr, mid + 1, end);

            SegmentTree root = new SegmentTree(start, end, value: Math.Min(leftNode.GetValue(), rightNode.GetValue()));
            root.SetLeft(leftNode);
            root.SetRight(rightNode);

            return root;
        }
    }
}
