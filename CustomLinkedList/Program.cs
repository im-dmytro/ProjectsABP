
using System.Collections;
using System.Collections.Generic;

class LinkedListEnumerator<T> : IEnumerator<LinkedListNode<T>>
{
    LinkedListNode<T>? Head;
    public LinkedListEnumerator(LinkedListNode<T> first)
    {
        Head = first;
    }
    LinkedListNode<T> _current;
    public LinkedListNode<T>? Current
    {
        get { return _current; }
        set { _current = value; }
    }

    object? IEnumerator.Current
    {
        get { return _current; }
    }

    public void Dispose()
    {
        Console.WriteLine("Disposed");
    }

    public bool MoveNext()
    {

        if (Head is not null && Current == null) { Current = Head; return true; }
        else if (Current.Next is not null)
        {
            Current = Current.Next;
            return true;
        }

        return false;


    }
    public void Reset()
    {
        Current = null;
    }
}

class LinkedList<T> : IEnumerable<LinkedListNode<T>>
{
    public LinkedListNode<T>? Head { get; set; }
    public int Count { get; set; }
    public void AddLast(T value)
    {
        LinkedListNode<T> addingValue = new LinkedListNode<T>(value);
        if (Head is null)
        {
            Head = addingValue;
        }
        else
        {
            LinkedListNode<T> Current = Head;

            foreach (var current in this)
            {
                Current = current;
            }

            addingValue.Previous = Current;
            Current.Next = addingValue;
        }
        Count++;
    }
    public LinkedListNode<T>? Find(T valueToFind)
    {
        LinkedListNode<T> aux;

        foreach (var currnet in this)
        {
            aux = currnet;
            if (EqualityComparer<T>.Default.Equals(aux.Value, valueToFind))
            {
                return aux;
            }
        }
        return default;
    }
    public void Remove(T value)
    {
        LinkedListNode<T> found = Find(value);
        if (found is null)
        {
            return;
        }
        var next = found.Next;
        var prev = found.Previous;

        if (prev is not null)
        {
            prev.Next = next;
        }
        if (next is not null)
        {
            next.Previous = prev;
        }
        Count--;
    }

    public void AddFirst(T value)
    {
        LinkedListNode<T> addingValue = new LinkedListNode<T>(value);
        if (Head is null)
        {
            Head = addingValue;
        }
        else
        {
            addingValue.Next = Head;
            Head.Previous = addingValue;
            Head = addingValue;

        }
        Count++;
    }
    public IEnumerator<LinkedListNode<T>> GetEnumerator()
    {
        return new LinkedListEnumerator<T>(Head);
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
class LinkedListNode<T>
{
    public T? Value { get; set; }
    public LinkedListNode<T>? Next { get; set; }
    public LinkedListNode<T>? Previous { get; set; }
    public LinkedListNode(T value)
    {
        Value = value;
    }
}
class Program
{

    public static void Main(string[] args)
    {
        LinkedList<int> first = new LinkedList<int>();
        //first.AddLast(6);
        //first.AddLast(8);
        //first.AddLast(43);
        //first.AddLast(65);

        first.AddFirst(6);
        first.AddFirst(8);
        first.AddFirst(43);
        first.AddFirst(65);
        first.AddLast(6);

        first.Remove(6);
        first.Remove(6);

        foreach (var item in first)
        {
            Console.WriteLine(item.Value);
        }
    }
}
