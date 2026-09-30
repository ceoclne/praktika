using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите заданное число: ");
        int number = int.Parse(Console.ReadLine());
        Random random = new Random();
        int sum = 0;
        int count = 0;
        int[] tempArray = new int[number];
        while (true)
        {
            int value = random.Next(1, 10);
            // Если добавление числа превысит заданную сумму,
            // прекращаем формирование массива
            if (sum + value > number)
            {
                break;
            }
            tempArray[count] = value;
            sum += value;
            count++;
        }
        int[] array = new int[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = tempArray[i];
        }
        Console.WriteLine();
        Console.WriteLine("Полученный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine($"Элемент {i}: {array[i]}");
        }
        Console.WriteLine();
        Console.WriteLine($"Количество элементов: {count}");
        Console.WriteLine($"Сумма элементов: {sum}");
        Console.WriteLine($"Заданное число: {number}");
    }
}