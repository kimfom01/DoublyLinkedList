namespace DoublyLinkedList;

public class DoubleLinkedList<T>
{
    private int _count;
    private DoubleLinkedListNode<T> _head;
    private DoubleLinkedListNode<T> _tail;
    private int _updateCode;

    public int Count
    {
        get
        {
            return _count;
        }
    }
    public DoubleLinkedListNode<T> Head
    {
        get
        {
            return _head;
        }

        private set
        {
            _head = value;
        }
    }
    public bool IsEmpty
    {
        get
        {
            return _count <= 0;
        }
    }
    public DoubleLinkedListNode<T> Tail
    {
        get
        {
            return _tail;
        }

        private set
        {
            _tail = value;
        }
    }

    public DoubleLinkedList()
    {
    }

    public DoubleLinkedList(IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            AddToEnd(item);
        }
    }

    public void AddAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
    {
        if (node == null)
        {
            throw new ArgumentNullException("node");
        }

        if (newNode == null)
        {
            throw new ArgumentNullException("newNode");
        }

        if (node.Owner != this)
        {
            throw new InvalidOperationException("node is not owned by this list");
        }

        if (newNode.Owner != this)
        {
            throw new InvalidOperationException("newNode is not owned by this list");
        }

        if (node == _tail)
        {
            _tail = newNode;
        }

        if (node.Next != null)
        {
            node.Next.Previous = newNode;
        }

        newNode.Next = node.Next;
        newNode.Previous = node;

        node.Next = newNode;

        ++_count;
        ++_updateCode;
    }

    public DoubleLinkedListNode<T> AddAfter(DoubleLinkedListNode<T> node, T value)
    {
        DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

        AddAfter(node, newNode);

        return newNode;
    }

    public void AddBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
    {
        if (node == null)
        {
            throw new ArgumentNullException("node");
        }

        if (newNode == null)
        {
            throw new ArgumentNullException("newNode");
        }

        if (node.Owner != this)
        {
            throw new InvalidOperationException("node is not owned by this list");
        }

        if (newNode.Owner != this)
        {
            throw new InvalidOperationException("newNode is not owned by this list");
        }

        if (_head == node)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
        else
        {
            if (node.Previous != null)
            {
                node.Previous.Next = newNode;
            }

            newNode.Previous = node.Previous;
            newNode.Next = newNode;

            node.Previous = newNode;
        }

        ++_count;
        ++_updateCode;
    }

    public DoubleLinkedListNode<T> AddBefore(DoubleLinkedListNode<T> node, T value)
    {
        DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

        AddBefore(node, newNode);

        return newNode;
    }

    public DoubleLinkedListNode<T> AddToBeginning(T value)
    {
        DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

        if (IsEmpty)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }

        ++_count;
        ++_updateCode;

        return newNode;
    }

    public DoubleLinkedListNode<T> AddToEnd(T value)
    {
        DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(this, value);

        if (IsEmpty)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Previous = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }

        ++_count;
        ++_updateCode;

        return newNode;
    }

    public void Clear()
    {
        DoubleLinkedListNode<T> temp;

        for (DoubleLinkedListNode<T> node = _head; node != null;)
        {
            temp = node.Next;

            _head = temp;

            if (temp != null)
            {
                temp.Previous = null;
            }

            --_count;

            node.Next = null;
            node.Previous = null;
            node.Owner = null;

            node = temp;
        }

        if (_count <= 0)
        {
            _head = null;
            _tail = null;
        }

        ++_updateCode;
    }

    public bool Contains(T data)
    {
        return Find(data) != null;
    }

    public DoubleLinkedListNode<T> Find(T data)
    {
        if (IsEmpty)
        {
            return null;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        for (DoubleLinkedListNode<T> curr = Head; curr != null; curr = curr.Next)
        {
            if (comparer.Equals(curr.Data, data))
            {
                return curr;
            }
        }

        return null;
    }

    public bool Remove(T item)
    {
        return Remove(item, false);
    }

    public void Remove(DoubleLinkedListNode<T> node)
    {
        if (IsEmpty)
        {
            return;
        }

        if (node == null)
        {
            throw new ArgumentNullException("node");
        }

        if (node.Owner != this)
        {
            throw new ArgumentNullException("The node doesn't belong to this list.");
        }

        DoubleLinkedListNode<T> prev = node.Previous;
        DoubleLinkedListNode<T> next = node.Next;

        if (_head == node)
        {
            _head = next;
        }

        if (_tail == node)
        {
            _tail = prev;
        }

        if (prev != null)
        {
            prev.Next = next;
        }

        if (next != null)
        {
            next.Previous = prev;
        }

        node.Previous = null;
        node.Next = null;
        node.Owner = null;

        --_count;
        ++_updateCode;
    }

    public bool Remove(T item, bool alllOccurrences)
    {
        if (IsEmpty)
        {
            return false;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        bool removed = false;
        DoubleLinkedListNode<T> curr = Head;

        while (curr != null)
        {
            if (!comparer.Equals(curr.Data, item))
            {
                curr = curr.Next;
                continue;
            }

            if (curr.Previous != null)
            {
                curr.Previous.Next = curr.Next;
            }

            if (curr == Head)
            {
                Head = curr.Next;
            }

            if (curr == Tail)
            {
                Tail = curr.Previous;
            }

            DoubleLinkedListNode<T> temp = curr;

            curr = curr.Next;

            temp.Next = null;
            temp.Previous = null;
            temp.Owner = null;

            --_count;
            removed = true;

            if (!alllOccurrences)
            {
                break;
            }
        }

        if (removed)
        {
            ++_updateCode;
        }

        return removed;
    }

    public T[] ToArray()
    {
        T[] retval = new T[_count];

        int index = 0;
        for (DoubleLinkedListNode<T> i = Head; i != null; i = i.Next)
        {
            retval[index] = i.Data;
            ++index;
        }

        return retval;
    }

    public T[] ToArrayReversed()
    {
        T[] retval = new T[_count];

        int index = 0;
        for (DoubleLinkedListNode<T> i = Tail; i != null; i = i.Previous)
        {
            retval[index] = i.Data;
            ++index;
        }

        return retval;
    }
}
