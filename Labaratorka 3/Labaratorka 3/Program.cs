using System.Text;
using System.Text.Json;

class Massive
{
    private string[] array;
    private int length;

    public Massive(int length)
    {
        this.length = length;
        array = new string[length];
    }

    public int Length
    {
        get { return length; }
    }

    private bool TryIndex(int index)
    {
        return index >= 0 && index < length;
    }

    public string GetElement(int index)
    {
        if (TryIndex(index))
            return array[index];
        else
            throw new IndexOutOfRangeException("Індекс занадто великий");
    }

    public void PlusElement(int index, string value)
    {
        if (TryIndex(index))
            array[index] = value;
        else
            throw new IndexOutOfRangeException("Індекс виходить за межі");
    }

    public Massive Concat(Massive other)
    {
        if (this.length != other.Length)
            throw new ArgumentException("Масиви мають бути однакової довжини ");

        Massive result = new Massive(length);

        for (int i = 0; i < length; i++)
        {
            result.PlusElement(i, this.GetElement(i) + other.GetElement(i));
        }

        return result;
    }

    public Massive Union(Massive other)
    {
        if (this.length != other.Length)
            throw new ArgumentException("Масиви мають бути однаковими ");

        Massive result = new Massive(length);

        for (int i = 0; i < length; i++)
        {
            string element = this.GetElement(i);
            if (!result.Contains(element))
                result.PlusElement(i, element);
        }

        for (int i = 0; i < length; i++)
        {
            string element = other.GetElement(i);
            if (!result.Contains(element))
                result.PlusElement(i, element);
        }

        return result;
    }

    private bool Contains(string value)
    {
        for (int i = 0; i < length; i++)
        {
            if (array[i] == value)
                return true;
        }
        return false;
    }

    public void PrintElement(int index)
    {
        if (TryIndex(index))
            Console.WriteLine($"Елемент з індексом {index}: {array[index]}");
        else
            Console.WriteLine("Індекс виходить за межі");
    }

    public void PrintArray()
    {
        Console.WriteLine("Array:");
        for (int i = 0; i < length; i++)
        {
            Console.WriteLine($"[{i}]: {array[i]}");
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Massive array1 = new Massive(3);
        array1.PlusElement(5, "IK-34 ");
        array1.PlusElement(1, "Best ");
        array1.PlusElement(2, "Group ");

        Massive array2 = new Massive(3);
        array2.PlusElement(0, "forever");
        array2.PlusElement(1, "young");
        array2.PlusElement(2, "and chill");

        array1.PrintArray();
        array2.PrintArray();

        Massive concatenatedArray = array1.Concat(array2);
        concatenatedArray.PrintArray();

        Massive unionArray = array1.Union(array2);
        unionArray.PrintArray();

        array1.PrintElement(1);
    }
}
