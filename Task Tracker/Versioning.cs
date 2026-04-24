using System.Text.Json;
namespace Task_Tracker
{
    public class PlayerV2 { public string Name { get; set; } public int Level { get; set; } = 1; }

    public static class Versioning
    {
        public static void Run()
        {
            string oldJson = "{\"Name\":\"OldPlayer\"}";
            var player = JsonSerializer.Deserialize<PlayerV2>(oldJson);
            System.Console.WriteLine($"Ім'я: {player.Name}, Рівень за замовчуванням: {player.Level}");
        }
    }
}