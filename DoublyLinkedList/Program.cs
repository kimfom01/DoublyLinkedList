using System.Text;
using DoublyLinkedList;

Lesson2A();

void Lesson2A()
{
    Random rnd = new Random();

    DoubleLinkedList<int> list = new DoubleLinkedList<int>();

    Console.WriteLine("Adding to the list...");

    for (int i = 0; i < 10; ++i)
    {
        int nextValue = rnd.Next(100);

        Console.Write("{0} ", nextValue);

        bool added = false;

        for (DoubleLinkedListNode<int> curr = list.Head; curr != null; curr = curr.Next)
        {
            if (nextValue < curr.Data)
            {
                list.AddBefore(curr, nextValue);
                added = true;
                break;
            }
        }

        if (!added)
        {
            list.AddToEnd(nextValue);
        }
    }

    Console.WriteLine();

    Console.WriteLine("The sorted list is");
    Console.WriteLine(ArrayToString(list.ToArray()));
}

string ArrayToString(Array array)
{
    StringBuilder sb = new StringBuilder();

    sb.Append("[");
    if (array.Length > 0)
    {
        sb.Append(array.GetValue(0));
    }
    for (int i = 1; i < array.Length; ++i)
    {
        sb.AppendFormat(", {0}", array.GetValue(i));
    }

    sb.Append("]");

    return sb.ToString();
}