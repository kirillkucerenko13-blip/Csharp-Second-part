using System;
using System.Threading.Tasks;
using MyLibrary.Models;
using MyLibrary.Repositories;
using MyLibrary.Services;

namespace MyLibraryApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userService = new UserService(new Repository<User>("users.json"));
            var bookService = new BookService(new Repository<Book>("books.json"));
            var orderService = new OrderService(new Repository<Order>("orders.json"));

            try
            {
                await userService.AddAsync(new User { Id = 1, Name = "Ivan" });
                await bookService.AddAsync(new Book { Id = 1, Title = "C# Pro" });
                await orderService.AddAsync(new Order { Id = 1, UserId = 1, BookId = 1 });

                Console.WriteLine("=== USERS ===");
                var users = await userService.GetAllAsync();
                users.ForEach(u => Console.WriteLine(u.Name));

                Console.WriteLine("\n=== BOOKS ===");
                var books = await bookService.GetAllAsync();
                books.ForEach(b => Console.WriteLine(b.Title));

                Console.WriteLine("\n=== ORDERS ===");
                var orders = await orderService.GetAllAsync();
                orders.ForEach(o =>
                    Console.WriteLine($"Order {o.Id}: User {o.UserId} -> Book {o.BookId}")
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}