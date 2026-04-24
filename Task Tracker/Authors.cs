using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Task_Tracker
{
    public class Author { public string Name { get; set; } public List<Book> Books { get; set; } }
    public class Book { public string Title { get; set; } public Author Author { get; set; } }

    public static class Authors
    {
        public static void Run()
        {
            var author = new Author { Name = "Стівен Кінг", Books = new List<Book>() };
            author.Books.Add(new Book { Title = "Воно", Author = author });

            var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };
            string json = JsonSerializer.Serialize(author, options);
            System.Console.WriteLine("Завдання 3: Циклічність оброблена.");
        }
    }
}