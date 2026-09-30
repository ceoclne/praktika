using System;
class Program
{
    static void Main()
    {
        // Создаём вещественный массив из 10 элементов
        double[] array = new double[10];
        Random random = new Random();
        // Заполняем массив случайными значениями от -10 до 10
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(-100, 100) / 10.0;
        }
        Console.WriteLine("ИСХОДНЫЙ МАССИВ");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine($"Индекс {i}: значение = {array[i]:F1}");
        }
        // Создаём массив индексов
        int[] indexes = new int[10];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }
        // Сортируем индексы по значениям исходного массива
        for (int i = 0; i < indexes.Length - 1; i++)
        {
            for (int j = i + 1; j < indexes.Length; j++)
            {
                if (array[indexes[i]] > array[indexes[j]])
                {
                    int temp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = temp;
                }
            }
        }
        // Выводим результат
        Console.WriteLine();
        Console.WriteLine("ИНДЕКСЫ В ПОРЯДКЕ ВОЗРАСТАНИЯ");
        for (int i = 0; i < indexes.Length; i++)
        {
            Console.WriteLine(
                $"Позиция {i + 1}: индекс {indexes[i]}, значение {array[indexes[i]]:F1}"
            );
        }
        Console.WriteLine();
        Console.WriteLine("ИТОГОВЫЙ МАССИВ ИНДЕКСОВ");
        for (int i = 0; i < indexes.Length; i++)
        {
            Console.Write(indexes[i] + " ");
        }
        Console.WriteLine();
    }
}