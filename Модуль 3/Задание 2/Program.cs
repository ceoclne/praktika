using System;
class Notification
{
    public event Action<string> Message;
    public event Action<string> Call;
    public event Action<string> Email;
    public void SendMessage(string text) => Message?.Invoke(text);
    public void MakeCall(string number) => Call?.Invoke(number);
    public void SendEmail(string address) => Email?.Invoke(address);
}

class Program
{
    static void Main()
    {
        Notification notification = new Notification();
        notification.Message += text => Console.WriteLine($"Сообщение: {text}");
        notification.Call += number => Console.WriteLine($"Звонок: {number}");
        notification.Email += address => Console.WriteLine($"Электронное письмо: {address}");
        notification.SendMessage("Вам пришло новое сообщение");
        notification.MakeCall("+375 29 123-45-67");
        notification.SendEmail("example@mail.ru");
    }
}
