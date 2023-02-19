namespace DoublyLinkedList;

public class DoubleLinkedListNode<T>
{
    DoubleLinkedList<T> _owner;
    DoubleLinkedListNode<T> _prev;
    DoubleLinkedListNode<T> _next;
    T _data;

    public DoubleLinkedListNode(T data)
    {
        _data = data;
        _owner = null;
    }

    internal DoubleLinkedListNode(DoubleLinkedList<T> owner, T data)
    {
        _data = data;
        _owner = owner;
    }

    public DoubleLinkedListNode<T> Next
    {
        get
        {
            return _next;
        }
        internal set
        {
            _next = value;
        }
    }

    internal DoubleLinkedList<T> Owner
    {
        get
        {
            return _owner;
        }
        set
        {
            _owner = value;
        }
    }

    public DoubleLinkedListNode<T> Previous
    {
        get
        {
            return _prev;
        }
        internal set
        {
            _prev = value;
        }
    }

    public T Data
    {
        get
        {
            return _data;
        }
        internal set
        {
            _data = value;
        }
    }
}
