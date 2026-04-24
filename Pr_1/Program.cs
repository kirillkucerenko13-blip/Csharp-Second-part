using System;
using System.Collections.Generic;

namespace Pr_1
{
    
    public delegate double MathOperation(double a, double b);
    public delegate void NotificationHandler(string message);
    public delegate bool FilterPredicate(int value);
    public delegate bool Validator(string text);


    public class Task1
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    public class Task2 : Task1
    {
        public static void SendEmail(string message) =>
            Console.WriteLine($"Email sent: {message}");

        public static void SendSMS(string message) =>
            Console.WriteLine($"SMS sent: {message}");
    }

    public class Task3 : Task2
    {
        public static void FilterArray(int[] numbers, FilterPredicate predicate)
        {
            foreach (var n in numbers)
                if (predicate(n))
                    Console.Write($"{n} ");
            Console.WriteLine();
        }

        public static bool IsEven(int n) => n % 2 == 0;
        public static bool IsGreaterThanFive(int n) => n > 5;
    }


    public class Logger
    {
        public Action<string> LogHandler;

        public void Log(string message) => LogHandler?.Invoke(message);
    }

    public class Task6
    {
        public static Validator GetValidator(int minLength) =>
            (str) => str.Length >= minLength;
    }


    public class Program : Task3
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== TASK 4: Delegates & Multicast =====");

            MathOperation operation = Add;
            Console.WriteLine($"Addition: {operation(10, 5)}");
            operation = Subtract;
            Console.WriteLine($"Subtraction: {operation(10, 5)}");
            operation = Multiply;
            Console.WriteLine($"Multiplication: {operation(10, 5)}");
            operation = Divide;
            Console.WriteLine($"Division: {operation(10, 5)}");

            NotificationHandler handler = SendEmail;
            handler += SendSMS;
            handler("Hello, this is a notification!");

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Console.WriteLine("Even numbers:");
            FilterArray(numbers, IsEven);
            Console.WriteLine("Numbers greater than 5:");
            FilterArray(numbers, IsGreaterThanFive);
            Console.WriteLine("Odd numbers (divisible by 3):");
            FilterArray(numbers, n => n % 3 == 0);

            Console.WriteLine("\n===== TASK 5: Func & Anonymous delegates =====");

            Func<double, double, double> func;
            func = Add;
            Console.WriteLine($"Add: {func(5, 3)}");
            func = Subtract;
            Console.WriteLine($"Subtract: {func(5, 3)}");
            func = Multiply;
            Console.WriteLine($"Multiply: {func(5, 3)}");
            func = Divide;
            Console.WriteLine($"Divide: {func(5, 3)}");

            List<string> students = new List<string>
            {
                "Emeli", "Djon", "Anna", "Michael", "Alex"
            };

            List<string> filteredStudents = students.FindAll(delegate (string name)
            {
                return name.StartsWith("A");
            });

            Console.WriteLine("Students whose names start with 'A':");
            foreach (string student in filteredStudents)
                Console.WriteLine(student);

            Console.WriteLine("\n===== TASK 6: Logger & Validator =====");

            Logger logger = new Logger();
            logger.LogHandler = msg => Console.WriteLine($"[Console]: {msg}");
            logger.Log("This is a log message.");
            logger.LogHandler = msg => Console.WriteLine($"[LOG-UPPER]: {msg.ToUpper()}");
            logger.Log("This is another log message.");

            var passwordVal = Task6.GetValidator(8);
            var loginVal = Task6.GetValidator(3);
            Console.WriteLine($"passwordVal(\"12345\"): {passwordVal("12345")}");
            Console.WriteLine($"passwordVal(\"12345678\"): {passwordVal("12345678")}");
            Console.WriteLine($"loginVal(\"Ya\"): {loginVal("Ya")}");
        }
    }
}