using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task_Tracker { 
public class Student { public string Name { get; set; } public int Age { get; set; } public double AverageScore { get; set; } }

public static class Students
{
    public static void Run()
    {
        var students = new List<Student> {
            new Student { Name = "Анна", Age = 19, AverageScore = 90 },
            new Student { Name = "Ігор", Age = 20, AverageScore = 85 },
            new Student { Name = "Олег", Age = 21, AverageScore = 88 },
            new Student { Name = "Яна", Age = 19, AverageScore = 95 },
            new Student { Name = "Максим", Age = 22, AverageScore = 78 }
        };

        File.WriteAllText("students.json", JsonSerializer.Serialize(students));
        var loaded = JsonSerializer.Deserialize<List<Student>>(File.ReadAllText("students.json"));
        loaded.ForEach(s => Console.WriteLine($"{s.Name}: {s.AverageScore}"));
    }
}
}