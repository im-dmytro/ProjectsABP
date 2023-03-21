
using System;
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
    public LinkedListNode<T> AddAfter(LinkedListNode<T> nodeToAddAfter, T valueToAdd)
    {
        LinkedListNode<T> addingNode = new LinkedListNode<T>(valueToAdd);

        var next = nodeToAddAfter.Next;
        if (next is not null) { next.Previous = addingNode; addingNode.Next = next; }

        nodeToAddAfter.Next = addingNode;
        addingNode.Previous = nodeToAddAfter;

        Count++;

        return addingNode;
    }
    public LinkedListNode<T> AddBefore(LinkedListNode<T> nodeToAddBefore, T valueToAdd)
    {
        LinkedListNode<T> addingNode = new LinkedListNode<T>(valueToAdd);

        var previous = nodeToAddBefore.Previous;
        if (previous is not null) { previous.Next = addingNode; addingNode.Previous = previous; }

        nodeToAddBefore.Previous = addingNode;
        addingNode.Next = nodeToAddBefore;
        Count++;

        return addingNode;
    }
    public LinkedListNode<T> AddLast(T value)
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
        return addingValue;

    }
    public LinkedListNode<T> AddFirst(T value)
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
        return addingValue;
    }
    public LinkedListNode<T>? Find(T valueToFind)
    {
        LinkedListNode<T> founded;

        foreach (var currnet in this)
        {
            founded = currnet;
            if (EqualityComparer<T>.Default.Equals(founded.Value, valueToFind))
            {
                return founded;
            }
        }
        return default;
    }
    public void RemoveFromBegin(T value)
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
    public void RemoveFirst()
    {
        var next = Head.Next;
        if (next is not null) { next.Previous = null; Head = next; }
        else Head = null;
        Count--;
    }
    public void RemoveLast()
    {
        LinkedListNode<T> current = null;

        foreach (var node in this) current = node;

        if (current is not null)
        {
            var prev = current.Previous;
            if (prev is not null) prev.Next = null;
        }
        Count--;
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
        LinkedList<int> linkedList = new LinkedList<int>();

        linkedList.AddFirst(6);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddFirst(8);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        var newNode = linkedList.AddLast(9);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddFirst(43);
        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.RemoveLast();

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddFirst(65);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddLast(6);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddAfter(newNode, 89);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        linkedList.AddBefore(newNode, 63);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));


        Console.WriteLine("Count" + linkedList.Count);

        linkedList.RemoveFromBegin(6);

        Console.WriteLine(String.Join(" ", linkedList.Select(x => x.Value)));

        Console.WriteLine("Count" + linkedList.Count);


    }
}
