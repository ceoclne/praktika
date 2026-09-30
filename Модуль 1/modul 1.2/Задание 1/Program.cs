using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите размер массива N: ");
        int n = int.Parse(Console.ReadLine());
        double[] array = new double[n];
        Console.WriteLine("Введите элементы массива:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Элемент {i}: ");
            array[i] = double.Parse(Console.ReadLine());
        }
        // Находим максимальный элемент по модулю
        double maxAbs = Math.Abs(array[0]);
        for (int i = 1; i < n; i++)
        {
            if (Math.Abs(array[i]) > maxAbs)
            {
                maxAbs = Math.Abs(array[i]);
            }
        }
        // Нормируем каждый элемент
        for (int i = 0; i < n; i++)
        {
            array[i] = array[i] / maxAbs;
        }
        Console.WriteLine();
        Console.WriteLine($"Максимальный элемент по модулю: {maxAbs}");
        Console.WriteLine("Нормированный массив:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Элемент {i}: {array[i]:F3}");
        }
    }
}
