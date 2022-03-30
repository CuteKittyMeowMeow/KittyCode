using System.Numerics;
using BenchmarkDotNet.Running;
using ReferenceTest;


namespace ReferenceTest
{
    public class Program
    {
        //Создаем, заполняем и упорядочиваем массив массивов ранга rank.
        public static int[][] CreateArray(int rank, int sizeLower = 10, int sizeUpper = 100, int rangeLower = -100, int rangeUpper = 100)
        {
            Random random = new Random();

            int[][] result = new int[rank][];
            for (int i = 0; i < rank; i++)
                result[i] = new int[random.Next(sizeLower, sizeUpper)];

            foreach (int[] row in result)
                for (int number = 0; number < row.Length; number++)
                    row[number] = random.Next(rangeLower, rangeUpper);

            foreach (int[] row in result)
                Array.Sort(row);

            return result;
        }

        //Выводит в консоль все элементы массива
        public static void ShowMe(int[][] array)
        {
            foreach (int[] row in array)
            {
                Console.WriteLine("Array:");
                foreach (int number in row)
                    Console.Write($"{number}, ");

                Console.WriteLine();
            }
        }

        //Выводит в консоль все элементы списка
        public static void ShowMe(List<int> list)
        {
            Console.WriteLine("Duplicates:");
            foreach (int number in list)
                Console.Write($"{number}, ");

            Console.WriteLine();
        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Введите количество массивов");
            int rank;

            try
            {
                rank = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                rank = 3;
                Console.WriteLine($"Исключение, лол: {ex.Message}");
            }

            //получили массивы
            int[][] mainArray = CreateArray(rank);
            ShowMe(mainArray);

            //Сюда пишем дубликаты
            List<int> duplicates = new List<int>();

            // массив индексов всех массивов
            int[] index = new int[rank];
            // в каждом индексе будет храниться номер последнего элемента
            for (int i = 0; i < rank; i++)
                index[i] = mainArray[i].Length-1;


            //Сравниваем элементы нулевого массива с элементами остальных массивов
            //Начинаем с конца. Индекс < 0 будет сведетельствовать о конце массива
            //
            //сравниваем нулевой и i массивы.
            for (int i = 1; i < rank; i++)
            {
                //Уменьшаем на один индекс того массива, который больше
                //и начинаем цикл заново, присваивая i=1
                if (mainArray[0][index[0]] > mainArray[i][index[i]])
                {
                    index[0]--;
                    if (index[0] < 0) //если индекс <0, то массив закончился и выходим из цикла
                        return;
                    i = 1;
                }
                else if (mainArray[0][index[0]] < mainArray[i][index[i]])
                {
                    index[i]--;
                    if (index[i] < 0) //если индекс <0, то массив закончился и выходим из цикла
                        return;
                    i = 1;
                }
                // Если до этого момента условие ни разу не выполнилось, то это дубликат
                // уменьшаем все индексы на 1
                else if (i == rank - 1)
                {
                    duplicates.Add(mainArray[0][index[0]]);
                    bool endOfArray = false;
                    for (int j = 0; j < rank; j++)
                    {
                        index[j]--;
                        if (index[j] < 0) //если индекс <0, то массив закончился и выходим из цикла
                            endOfArray = true;
                    }
                    if (endOfArray) return;
                    i = 1;
                }
            }

            duplicates = duplicates.Distinct().ToList();

            ShowMe(duplicates);

            Console.ReadLine();
        }

    }
}
