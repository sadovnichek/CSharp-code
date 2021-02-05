using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections;

namespace Program
{
    public class ProgramStack<T> : IEnumerable<T>
    {
        private LinkedList<T> list;

        public ProgramStack()
        {
            list = new LinkedList<T>();
        }

        public void Push(T item)
        {
            list.AddLast(item);
        }

        public T Pop()
        {
            var lastNode = list.Last;
            list.RemoveLast();
            return lastNode.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = list.Last;
            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public LinkedListNode<T> Peek()
        {
            return list.Last;
        }
    }
}
