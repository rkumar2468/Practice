using System;
using System.Collections.Generic;

namespace Practice2019
{
    public class LRUCache
    {
        int CACHE_LIMIT = 100;
        DoubleLinkedList<int> cacheListStart;
        DoubleLinkedList<int> cacheListEnd;
        int count;
        Dictionary<int, DoubleLinkedList<int>> lookupDict;

        public LRUCache()
        {
            lookupDict = new Dictionary<int, DoubleLinkedList<int>>();
            count = 0;
        }

        public void DumpCaches()
        {
            DoubleLinkedList<int> start = cacheListStart;
            DoubleLinkedList<int> end = cacheListEnd;
            while (start != end)
            {
                Console.Write($"{start.GetValue()}->");
                start = start.GetNextElement();
            }

            if (end != null)
            {
                Console.Write($"{end.GetValue()}->");
            }

            Console.WriteLine($"null");
        }

        public int GetValueFromCache(int key)
        {
            int retVal = -1;

            if (lookupDict.ContainsKey(key))
            {
                DoubleLinkedList<int> curr = lookupDict[key];
                retVal = curr.GetValue();

                UpdateCaches(curr);
            }

            return retVal;
        }

        public void AddItemToCache(int key, int value)
        {
            if (lookupDict.ContainsKey(key))
            {
                throw new ArgumentException("Given key already exists. Pls choose a new key and try again.");
            }

            lock (this)
            {
                DoubleLinkedList<int> node = CreateNode(key, value);

                if (count < CACHE_LIMIT)
                {
                    count++; 
                }
                else
                {
                    // evict
                    if (cacheListEnd != null)
                    {
                        DoubleLinkedList<int> prev = cacheListEnd.GetPreviousElement();
                        cacheListEnd = prev;
                        cacheListEnd.SetNext(null);
                    }
                }

                // Since this is a newly created node
                // prev is always null.
                node.SetNext(cacheListStart);
                if (cacheListStart != null)
                {
                    cacheListStart.SetPrevious(node);
                }

                cacheListStart = node;
                if (cacheListEnd == null)
                {
                    cacheListEnd = node;
                }
            }
        }

        private DoubleLinkedList<int> CreateNode(int key, int value)
        {
            if (lookupDict == null)
            {
                lookupDict = new Dictionary<int, DoubleLinkedList<int>>();
            }

            DoubleLinkedList<int> node = new DoubleLinkedList<int>(value);
            lookupDict.Add(key, node);

            return node;
        }

        private void UpdateCaches(DoubleLinkedList<int> curr)
        {
            // This is to ensure multiple threads are not going to try updating the cache at the same time.
            DoubleLinkedList<int> prev = curr.GetPreviousElement();
            if (prev == null)
            {
                // do nothing as the curr is the first element.
                return;
            }

            lock (this)
            {
                DoubleLinkedList<int> next = curr.GetNextElement();
                prev.SetNext(next);
                if (next != null)
                {
                    next.SetPrevious(prev);
                }

                curr.SetNext(cacheListStart);
                if (cacheListStart != null)
                {
                    cacheListStart.SetPrevious(curr);
                }
                
                cacheListStart = curr;
                cacheListStart.SetPrevious(null);

                if (cacheListEnd == curr)
                {
                    cacheListEnd = prev;
                }
            }
        }
    }

    internal class DoubleLinkedList<T>
    {
        T val;
        DoubleLinkedList<T> next;
        DoubleLinkedList<T> prev;

        public DoubleLinkedList(T val)
        {
            this.val = val;
        }

        public void SetPrevious(DoubleLinkedList<T> node)
        {
            prev = node;
        }

        public void SetNext(DoubleLinkedList<T> node)
        {
            next = node;
        }

        public DoubleLinkedList<T> GetPreviousElement()
        {
            return prev;
        }

        public DoubleLinkedList<T> GetNextElement()
        {
            return next;
        }

        public T GetValue()
        {
            return val;
        }
    }
}
