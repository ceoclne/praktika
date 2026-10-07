using System;

class Program
{
    delegate void SortDelegate(int[] array);

    static void BubbleSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
            for (int j = 0; j < array.Length - i - 1; j++)
                if (array[j] > array[j + 1])
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
    }

    static void QuickSort(int[] array, int left, int right)
    {
        if (left >= right) return;

        int i = left, j = right;
        int pivot = array[(left + right) / 2];

        while (i <= j)
        {
            while (array[i] < pivot) i++;
            while (array[j] > pivot) j--;

            if (i <= j)
            {
                (array[i], array[j]) = (array[j], array[i]);
                i++;
                j--;
            }
        }

        QuickSort(array, left, j);
        QuickSort(array, i, right);
    }

    static void QuickSortStart(int[] array)
    {
        QuickSort(array, 0, array.Length - 1);
    }

    static void Main()
    {
        int[] numbers = { 8, 3, 1, 6, 4, 2, 7, 5 };

        Console.WriteLine("Исходный массив: " + string.Join(" ", numbers));
        Console.WriteLine("1 - Сортировка пузырьком");
        Console.WriteLine("2 - Быстрая сортировка");
        Console.Write("Выберите метод: ");
        string choice = Console.ReadLine();

        SortDelegate sort = choice == "1" ? BubbleSort : QuickSortStart;
        sort(numbers);

        Console.WriteLine("Отсортированный массив: " + string.Join(" ", numbers));
    }
}