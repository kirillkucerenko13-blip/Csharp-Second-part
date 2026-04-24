using System;
using System.Text.Json;
namespace Task_Tracker
{
    public static class Errors
    {
        public static void Run()
        {
            string brokenJson = "{ \"Name\": \"Bad JSON\" ";
            try
            {
                var data = JsonSerializer.Deserialize<Student>(brokenJson);
            }
            catch (JsonException)
            {
                Console.WriteLine("Повідомлення: Файл пошкоджено. Використовуємо стандартні дані.");
            }

        }
    }
}