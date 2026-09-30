using System;
class Program
{
    static void Main()
    {
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());
        int count = 0;
        int number = 2;
        while (count < k)
        {
            bool isPrime = true;
            // Проверяем, является ли число простым
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
            {
                Console.Write($"{number,5}");
                count++;
                // После каждого 10-го числа переходим на новую строку
                if (count % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            number++;
        }
    }
}