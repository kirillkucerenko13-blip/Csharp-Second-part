using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Task_Tracker
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Dog), "dog")]
    [JsonDerivedType(typeof(Cat), "cat")]
    public abstract class Animal { public string Name { get; set; } }
    public class Dog : Animal { public int BarkVolume { get; set; } }
    public class Cat : Animal { public int Lives { get; set; } }

    public static class Animals
    {
        public static void Run()
        {
            var animals = new List<Animal> { new Dog { Name = "Бровко", BarkVolume = 10 }, new Cat { Name = "Мурка", Lives = 9 } };
            string json = JsonSerializer.Serialize(animals);
            var restored = JsonSerializer.Deserialize<List<Animal>>(json);
            System.Console.WriteLine("Поліморфний список відновлено.");
        }
    }
}