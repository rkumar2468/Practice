using System;

namespace Practice2019
{
    public class LinkedList
    {
        int value;
        LinkedList next;
        public LinkedList(int value)
        {
            this.value = value;
            next = null;
        }

        public void SetNext(LinkedList list)
        {
            next = list;
        }

        public int GetValue()
        {
            return value;
        }

        public LinkedList GetNext()
        {
            return next;
        }

    }

    public class LinkedListFacade
    {
        private static LinkedList Head;
        private static LinkedList Current;

        public void Create(int value)
        {
            if (Head == null)
            {
                Head = new LinkedList(value);
                Current = Head;
            }
            else
            {
                Current.SetNext(new LinkedList(value));
                Current = Current.GetNext();
            }
        }

        public void PrintList()
        {
            Console.Write("List: ");
            LinkedList curr = Head;
            while(curr != null)
            {
                Console.Write($"{curr.GetValue()}->");

                curr = curr.GetNext();
            }
            Console.WriteLine("null");
        }

        public void ReverseList()
        {
            if (Head == null || Head.GetNext() == null) return;

            LinkedList prev = Head;
            LinkedList curr = Head.GetNext();
            LinkedList next;
            while(curr != null)
            {
                next = curr.GetNext();
                curr.SetNext(prev);
                prev = curr;
                curr = next;
            }
            Head.SetNext(null);
            Head = prev;
        }

        public void PrintListInReverse()
        {
            if (Head == null)
                throw new InvalidOperationException("List is empty");
            Console.Write("Reverse List: ");
            PrintReverseListRecursively(Head);
            Console.WriteLine("null");
        }

        private void PrintReverseListRecursively(LinkedList head)
        {
            if (head == null) return;

            PrintReverseListRecursively(head.GetNext());
            Console.Write($"{head.GetValue()}->");
        }
        
    }
}
