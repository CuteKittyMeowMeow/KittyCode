using System.Numerics;


namespace Duplicates
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

            //Создали массивы
            int[][] mainArray = CreateArray(rank);
            ShowMe(mainArray);

            //Сюда будем писать дубликаты
            List<int> duplicates = new List<int>();

            // массив индексов всех массивов
            int[] index = new int[rank];
            // в каждом индексе будет храниться номер последнего элемента
            for (int i = 0; i < rank; i++)
                index[i] = mainArray[i].Length - 1;


            //Сравниваем элементы (начиная с концы) нулевого массива с элементами остальных массивов
            // Индекс < 0 будет сведетельствовать о конце массива
            
            // флаг о том, что какой-то из массивов окончился
            bool moreArrayLeft = true;
            // Номер массива, с которым производится сравнение
            int arrayNumber = 1;
            do
            {
                //Уменьшаем на один индекс того массива, который больше
                //Если нулевой массив больше, то возвращаемся к сравнению с первым массивом
                if (mainArray[0][index[0]] > mainArray[arrayNumber][index[arrayNumber]])
                {
                    index[0]--;
                    if (index[0] < 0) //если индекс <0, то массив закончился и выходим из цикла
                        moreArrayLeft = false;
                    arrayNumber = 1;
                }
                //Если ненулевой массив больше, то продолжаем сравнение с того же массива
                else if (mainArray[0][index[0]] < mainArray[arrayNumber][index[arrayNumber]])
                {
                    index[arrayNumber]--;
                    if (index[arrayNumber] < 0) //если индекс <0, то массив закончился и выходим из цикла
                        moreArrayLeft = false;
                }
                //Если дошли до сюда, значит элементы равны. Переходим на следующий массив
                else if (arrayNumber < rank - 1)
                { arrayNumber++; }
                // Если это был последний массив, то мы нашли дубликат. Уменьшаем все индексы и начинаем заново
                else if (arrayNumber == rank - 1)
                {
                    duplicates.Add(mainArray[0][index[0]]);
                    
                    for (int i = 0; i < rank; i++)
                    {
                        index[i]--;
                        if (index[i] < 0) //если индекс <0, то массив закончился и выходим из цикла
                            moreArrayLeft = false;
                    }
                    arrayNumber = 1;
                }
            } while (moreArrayLeft);

            duplicates = duplicates.Distinct().ToList();

            ShowMe(duplicates);

            Console.ReadLine();
        }

    }
}
