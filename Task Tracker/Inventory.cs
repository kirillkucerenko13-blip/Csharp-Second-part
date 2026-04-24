using System.Collections.Generic;
using System.Text.Json;
namespace Task_Tracker
{
    public class Inventory { public List<string> Items { get; set; } = new List<string>(); }
    public class Player { public string Name { get; set; } public Inventory Inventory { get; set; } }

    public static class Inventories
    {
        public static void Run()
        {
            string jsonWithoutInventory = "{\"Name\":\"Гравець1\"}";
            var player = JsonSerializer.Deserialize<Player>(jsonWithoutInventory);

            player.Inventory ??= new Inventory();
            System.Console.WriteLine($"Гравець: {player.Name}, Предметів: {player.Inventory.Items.Count}");
        }
    }
}