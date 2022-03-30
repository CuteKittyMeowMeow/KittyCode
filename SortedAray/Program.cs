class Program
{

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите массив через пробел");
        int[] array = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        Console.WriteLine("Введенный массив:");

        foreach (int i in array)
            Console.Write(i+", ");
        Console.WriteLine();

        int incCount = 0;
        int decCount = 0;
                      
        for (int i = 0; i < array.Length - 1; i++) 
        {
            if (array[i] < array[i + 1]) incCount++;
            else if (array[i] > array[i + 1]) decCount++;
        }

        if (incCount != 0 && decCount != 0)
            Console.WriteLine("Неупорядоченный массив");
        else if (incCount == 0 && decCount ==0) Console.WriteLine("Массив из одинаковых чисел");
        else if (incCount == 0) Console.WriteLine("Массив по убыванию");
        else Console.WriteLine("Массив по возрастанию");

    }
}
