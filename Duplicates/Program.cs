using System.Numerics;
using BenchmarkDotNet.Running;
using ReferenceTest;


namespace ReferenceTest
{
    public class Program
    {

        public static int[][] CreateArray()
        {
            Random random = new Random();

            int[][] result = new int[3][];
            for (int i = 0; i < 3; i++)
                result[i] = new int[random.Next(10, 100)];

            foreach (int[] row in result)
                for (int number = 0; number < row.Length; number++)
                    row[number] = random.Next(-100, 100);

            foreach (int[] row in result)
                Array.Sort(row);

            return result;
        }

        public static void ShowMe(int[][] array)
        {
            foreach (int[] row in array)
            { Console.WriteLine("Array:"); 
                foreach (int number in row)
                    Console.Write($"{number}, ");

                Console.WriteLine();
            }
        }

        public static void ShowMe(List<int> list)
        {
            Console.WriteLine("Duplicates:");
            foreach (int number in list)
                Console.Write($"{number}, ");

            Console.WriteLine();
        }


        //  Я сначала хотел сделать влоб через Array.Contains. Но потом подумал, что зря что ли массивы упорядоченные?
        //
        //  В итоге я взял первые элементы из каждого массива и сравнивал их между собой.Если какой-то из них был
        // меньше другого - я брал следующий элемент этого массива. И так до тех пор, пока номер элемента
        // не превысит размер своего массива. 
        //  Ну а если ни один из элементов не меньше другого - значит они все равны и это число я
        // добавляю в список дубликатов, и беру следующий элемент в каждом из массивов.
        //
        //  Единственное что, я потом получившийся список прогнал через List.Distinct(), на
        // случай, если все массивы будут заполнены одним и тем же числом.



        public static void Main(string[] args)
        {

            int[][] mainArray = CreateArray();
            ShowMe(mainArray);

            List<int> duplicates = new List<int>();

            for (int i1 = 0, i2 = 0, i3 = 0;
                 i1 < mainArray[0].Length && i2 < mainArray[1].Length && i3 < mainArray[2].Length;)
            {
                if (mainArray[0][i1] < mainArray[1][i2] || mainArray[0][i1] < mainArray[2][i3]) i1++;
                else if (mainArray[1][i2] < mainArray[0][i1] || mainArray[1][i2] < mainArray[2][i3]) i2++;
                else if (mainArray[2][i3] < mainArray[0][i1] || mainArray[2][i3] < mainArray[1][i2]) i3++;
                else
                {
                    duplicates.Add(mainArray[0][i1]);
                    i1++;
                    i2++;
                    i3++;
                }

            }

            duplicates = duplicates.Distinct().ToList();

            ShowMe(duplicates);

            Console.ReadLine();
            

        }

    }
}
