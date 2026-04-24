using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task_Tracker { 

public class Taskitem { public string Title { get; set; } public bool IsCompleted { get; set; } }

public static class TaskTracker
{
    public static void Run()
    {
        string path = "C:\\Users\\kiril\\OneDrive\\Рабочий стол\\tasks.json";
        List<Taskitem> tasks = File.Exists(path)
            ? JsonSerializer.Deserialize<List<Taskitem>>(File.ReadAllText(path))
            : new List<Taskitem>();

        tasks.Add(new Taskitem { Title = "Нова задача", IsCompleted = false });

        File.WriteAllText(path, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("Завдання 1 виконано: стан збережено.");
    }
}
}
