using System;

namespace ConsoleApp1
{
    internal class MyList<T> where T : IComparable<T>
    {
        public uint Length { get; private set; }
        public bool IsEmpty { get { return Length == 0; } }
        private Node first, last;


        public T this[uint index]
        {
            get { return GetNode(index).data; }

            set { GetNode(index).data = value; }
        }


        public MyList() { Length = 0; first = last = null; }
        public MyList(T data, uint countInserts = 1) : this() { for (int i = 0; i < countInserts; i++) AddLast(data); }


        public void AddFirst(T data)
        {
            if (first == null)
                first = last = new Node(data);
            else
                first = new Node(data, next: first);
            Length++;
        }
        public void AddLast(T data)
        {
            if (last == null)
                first = last = new Node(data);
            else
                last = new Node(data, prev: last);
            Length++;
        }
        public void AddBefore(T data, uint index)
        {
            if (index == 0)
                AddFirst(data);
            else if(index < Length)
            {
                var node = GetNode(index - 1);
                node.next = new Node(data, node.next);
                Length++;
            }
            else
                throw new IndexOutOfRangeException(message: $"Выход за пределы MyList");
        }
        public void AddAfter(T data, uint index)
        {
            if (index == Length - 1)
                AddLast(data);
            else if (index < Length)
            {
                var node = GetNode(index);
                node.next = new Node(data, node.next, node);
                Length++;
            }
            else
                throw new IndexOutOfRangeException(message: $"Выход за пределы MyList");
        }


        public void RemoveFirst()
        {
            if (first == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            first = first.next;
            if (first != null)
                first.prev = null;
            else
                last = null;
            Length--;
        }
        public void RemoveLast()
        {
            if (last == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            last = last.prev;
            if (first != null)
                first.next = null;
            else
                first = null;
            Length--;
        }
        public void Remove(uint index)
        {
            if (index == 0)
                RemoveFirst();
            else if (index == Length - 1)
                RemoveLast();
            else
            {
                Node node = GetNode(index);
                node.prev.next = node.next;
                node.next.prev = node.prev;
                Length--;
            }
        }
        public void RemoveAll() { while (Length != 0) RemoveFirst(); }


        public T First()
        {
            if (first == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            return first.data;
        }
        public T Last()
        {
            if (last == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            return last.data;
        }


        public T Max()
        {
            if (first == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            var max = first.data;
            for (var node = first.next; node != null; node = node.next)
                if (node.data.CompareTo(max) > 0)
                    max = node.data;
            return max;
        }
        public T Min()
        {
            if (first == null)
                throw new InvalidOperationException(message: "Последовательность не содержит элементов");
            var min = first.data;
            for (var node = first.next; node != null; node = node.next)
                if (node.data.CompareTo(min) < 0)
                    min = node.data;
            return min;
        }


        public override string ToString()
        {
            string str = $"Кол-во элементов: {Length}";
            for (Node node = first; node != null; node = node.next)
                str += $"\n{node.data}";
            return str;
        }
        private Node GetNode(uint index)
        {
            if (IsEmpty || index > Length)
                throw new IndexOutOfRangeException(message: "Выход за пределы MyList");
            Node temp;
            if (Length - index > index)
            {
                temp = first;
                for (uint i = 0; i < index; i++)
                    temp = temp.next;
            }
            else
            {
                temp = last;
                for (uint i = Length - 1; i >= index; i--)
                    temp = temp.prev;
            }
            return temp;
        }
        public T[] ToArray()
        {
            T[] arr = new T[Length];
            for (uint i = 0; i < Length; i++)
                arr[i] = GetNode(i).data;
            return arr;
        }


        private class Node
        {
            public Node next, prev;
            public T data;

            public Node(T data, Node next = null, Node prev = null)
            {
                this.data = data;
                this.next = next;
                this.prev = prev;
                if (next != null)
                    next.prev = this;
                if (prev != null)
                    prev.next = this;
            }
        }
    }
}
