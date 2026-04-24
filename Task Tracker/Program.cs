using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    class Program
    {

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Оберіть завдання (1-8):");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1": TaskTracker.Run(); break;
                case "2": Students.Run(); break;
                case "3": Authors.Run(); break;
                case "4": Orders.Run(); break;
                case "5": Animals.Run(); break;
                case "6": Inventories.Run(); break;
                case "7": Versioning.Run(); break;
                case "8": Errors.Run(); break;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }
}