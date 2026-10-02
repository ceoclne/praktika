using System;

class TemperatureSensor
{
    // Событие изменения температуры
    public event Action<double> TemperatureChanged;

    public void SetTemperature(double temperature)
    {
        Console.WriteLine($"Измеренная температура: {temperature}°C");

        // Вызываем событие
        TemperatureChanged?.Invoke(temperature);
    }
}

class Thermostat
{
    private double minTemperature = 20;

    public Thermostat(TemperatureSensor sensor)
    {
        // Подписываемся на событие датчика
        sensor.TemperatureChanged += OnTemperatureChanged;
    }

    private void OnTemperatureChanged(double temperature)
    {
        if (temperature < minTemperature)
            Console.WriteLine("Отопление включено");
        else
            Console.WriteLine("Отопление выключено");
    }
}

class Program
{
    static void Main()
    {
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat(sensor);

        Console.WriteLine("Датчик температуры запущен.");
        Console.WriteLine("Введите температуру или закройте программу для выхода.");

        // Пользователь может вводить температуру бесконечно
        while (true)
        {
            Console.Write("\nВведите температуру: ");

            double temperature = double.Parse(Console.ReadLine());

            // Передаем температуру датчику
            sensor.SetTemperature(temperature);
        }
    }
}