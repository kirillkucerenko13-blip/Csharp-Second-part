using System.Text.Json;
using System.Text.Json.Serialization;
namespace Task_Tracker
{
    public enum OrderStatus { Pending, Processing, Completed }
    public class Order { public int Id { get; set; } [JsonConverter(typeof(JsonStringEnumConverter))] public OrderStatus Status { get; set; } }

    public static class Orders
    {
        public static void Run()
        {
            var order = new Order { Id = 1, Status = OrderStatus.Processing };
            string json = JsonSerializer.Serialize(order);
            System.Console.WriteLine($"JSON з енамом: {json}");
        }
    }
}