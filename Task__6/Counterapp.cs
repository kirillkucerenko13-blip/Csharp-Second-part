using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task__6
{
    class CounterApp
    {

        private static int _counter = 0;
        private static bool _paused = false;
        private static bool _running = true;

        private static readonly object _lock = new object();

        private static readonly ConsoleColor[] _colors =
        {
        ConsoleColor.White,
        ConsoleColor.Cyan,
        ConsoleColor.Green,
        ConsoleColor.Yellow,
        ConsoleColor.Magenta,
    };
        private static int _colorIndex = 0;

        static void Main()
        {
            PrintHelp();

            Thread keyThread = new Thread(KeyListenerLoop)
            {
                Name = "KeyListener",
                IsBackground = true
            };
            keyThread.Start();

            CounterLoop();

            Console.ResetColor();
            Console.WriteLine("\n[Програму завершено]");
        }

        static void CounterLoop()
        {
            while (true)
            {
                bool shouldRun;
                lock (_lock) { shouldRun = _running; }

                if (!shouldRun) break;

                bool isPaused;
                lock (_lock) { isPaused = _paused; }

                if (!isPaused)
                {
                    int value;
                    ConsoleColor color;

                    lock (_lock)
                    {
                        _counter++;
                        value = _counter;
                        color = _colors[_colorIndex];
                    }

                    Console.ForegroundColor = color;
                    Console.WriteLine($"Counter: {value}");
                }

                Thread.Sleep(1000);
            }
        }
        static void KeyListenerLoop()
        {
            while (true)
            {
                bool shouldRun;
                lock (_lock) { shouldRun = _running; }
                if (!shouldRun) break;

                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(50);
                    continue;
                }

                ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        lock (_lock)
                        {
                            _paused = !_paused;
                            string state = _paused ? "ПАУЗА" : "ПРОДОВЖЕННЯ";
                            PrintStatus(state);
                        }
                        break;


                    case ConsoleKey.R:
                        lock (_lock)
                        {
                            _counter = 0;
                            PrintStatus("Лічильник скинуто до 0");
                        }
                        break;

                    case ConsoleKey.C:
                        lock (_lock)
                        {
                            _colorIndex = (_colorIndex + 1) % _colors.Length;
                            string colorName = _colors[_colorIndex].ToString();
                            PrintStatus($"Колір змінено → {colorName}");
                        }
                        break;

                    case ConsoleKey.Q:
                        lock (_lock)
                        {
                            _running = false;
                            PrintStatus("Завершення програми...");
                        }
                        return;
                }
            }
        }

        static void PrintStatus(string message)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  [{message}]");
            Console.ForegroundColor = prev;
        }

        static void PrintHelp()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║  P — Пауза / продовження лічильника  ║");
            Console.WriteLine("║  R — Скинути лічильник до 0          ║");
            Console.WriteLine("║  C — Змінити колір тексту            ║");
            Console.WriteLine("║  Q — Завершити програму              ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
