using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pr3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            AnalyzeFile(
                "C:\\Users\\kiril\\OneDrive\\Рабочий стол\\Folder\\story.txt",
                "C:\\Users\\kiril\\OneDrive\\Рабочий стол\\Folder\\report.txt");
            InspectFoulder("C:\\Users\\kiril\\OneDrive\\Рабочий стол\\Folder");
            FindLargestFile("C:\\Users\\kiril\\OneDrive\\Рабочий стол\\Folder");

            string cachePath = "C:\\Users\\kiril\\OneDrive\\Рабочий стол\\Folder\\cache";

            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  ОЧИЩЕННЯ КЕШУ ");
            Console.WriteLine("══════════════════════════════════════");
            ClearCacheRecursive(cachePath);


            string analyzePath;
            if (args.Length > 0)
            {
                analyzePath = args[0];
            }
            else
            {
                Console.WriteLine("\nВведіть шлях для аналізу:");
                analyzePath = Console.ReadLine() ?? "";
            }

            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  FILE ANALYZER CLI");
            Console.WriteLine("══════════════════════════════════════");
            AnalyzePath(analyzePath);

            Console.ReadLine();
        }

        private static (int count, long bytes) DeleteFilesRecursive(DirectoryInfo dir)
        {
            int totalCount = 0;
            long totalBytes = 0;

            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    totalBytes += file.Length;
                    file.Delete();
                    totalCount++;
                    Console.WriteLine($"  [OK] Видалено: {file.FullName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  [ERR] {file.FullName} — {ex.Message}");
                }
            }

            foreach (DirectoryInfo sub in dir.GetDirectories())
            {
                var (c, b) = DeleteFilesRecursive(sub);
                totalCount += c;
                totalBytes += b;
            }

            return (totalCount, totalBytes);
        }

        public static void ClearCacheRecursive(string cachePath)
        {
            if (!Directory.Exists(cachePath))
            {
                Console.WriteLine("Помилка: папку кешу не знайдено.");
                return;
            }

            var dir = new DirectoryInfo(cachePath);
            var (count, bytes) = DeleteFilesRecursive(dir);

            PrintCacheReport(count, bytes);
        }

        private static void PrintCacheReport(int count, long bytes)
        {
            Console.WriteLine("\n--- Звіт очищення кешу ---");
            Console.WriteLine($"  Видалено файлів : {count}");
            Console.WriteLine($"  Звільнено місця : {FormatSize(bytes)}");
        }

        public static void AnalyzePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Помилка: вказаний шлях не існує.");
                return;
            }

            try
            {
                var root = new DirectoryInfo(path);

                DirectoryInfo[] allDirs = root.GetDirectories("*", SearchOption.AllDirectories);
                FileInfo[] allFiles = root.GetFiles("*", SearchOption.AllDirectories);

                long totalSize = 0;
                FileInfo largest = null;

                foreach (FileInfo f in allFiles)
                {
                    totalSize += f.Length;
                    if (largest == null || f.Length > largest.Length)
                        largest = f;
                }

                Console.WriteLine($"  Folders    : {allDirs.Length}");
                Console.WriteLine($"  Files      : {allFiles.Length}");
                Console.WriteLine($"  Total size : {FormatSize(totalSize)}");

                if (largest != null)
                    Console.WriteLine($"  Largest    : {largest.Name}  ({FormatSize(largest.Length)})");
                else
                    Console.WriteLine("  Largest    : —");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Помилка: немає доступу до деяких підпапок.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }

        private static string FormatSize(long bytes)
        {
            if (bytes >= 1_073_741_824) return $"{bytes / 1_073_741_824.0:F2} GB";
            if (bytes >= 1_048_576) return $"{bytes / 1_048_576.0:F2} MB";
            if (bytes >= 1_024) return $"{bytes / 1_024.0:F2} KB";
            return $"{bytes} bytes";
        }

       

        public static void AnalyzeFile(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Помилка: Файл story.txt не знайдено.");
                return;
            }

            int lineCount = 0;
            int wordCount = 0;
            long charCount = 0;

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineCount++;
                        charCount += line.Length;
                        string[] words = line.Split(new[] { ' ', '\t' },
                                                     StringSplitOptions.RemoveEmptyEntries);
                        wordCount += words.Length;
                    }
                }

                string report = $"Кількість рядків: {lineCount}\n"
                              + $"Кількість слів: {wordCount}\n";

                File.WriteAllText(outputPath, report);
                Console.WriteLine("Аналіз завершено. Результат збережено у report.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }

        public static void InspectFoulder(string folderPath)
        {
            Console.WriteLine("\nІНСПЕКТОР ПАПКИ");

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine("Помилка: Папку не знайдено за вказаним шляхом.");
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            Console.WriteLine("\nПідпапки");
            DirectoryInfo[] subFolders = directoryInfo.GetDirectories();
            if (subFolders.Length > 0)
            {
                foreach (var dir in subFolders)
                    Console.WriteLine($"[FOLDER] {dir.Name}");
            }
            else
            {
                Console.WriteLine("Підпапки відсутні.");
            }

            Console.WriteLine("\n--- Файли ---");
            FileInfo[] files = directoryInfo.GetFiles();

            if (files.Length > 0)
            {
                Console.WriteLine($"{"Назва файлу",-30} | {"Розмір (байт)",-15} | {"Дата створення",-20}");
                Console.WriteLine(new string('-', 70));
                foreach (FileInfo file in files)
                    Console.WriteLine($"{file.Name,-30} | {file.Length,-15} | {file.CreationTime,-20}");
            }
            else
            {
                Console.WriteLine("Файли відсутні.");
            }
        }

        public static void FindLargestFile(string folderPath)
        {
            Console.WriteLine("\nПОШУК НАЙБІЛЬШОГО ФАЙЛУ");

            if (!Directory.Exists(folderPath)) return;

            DirectoryInfo di = new DirectoryInfo(folderPath);

            try
            {
                FileInfo[] allFiles = di.GetFiles("*", SearchOption.AllDirectories);

                if (allFiles.Length == 0)
                {
                    Console.WriteLine("У вказаній папці немає файлів.");
                    return;
                }

                FileInfo largest = allFiles[0];
                foreach (var file in allFiles)
                    if (file.Length > largest.Length)
                        largest = file;

                Console.WriteLine($"Name: {largest.Name}");
                Console.WriteLine($"Size: {largest.Length} bytes");
                Console.WriteLine($"Path: {largest.FullName}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Помилка: Немає доступу до однієї з підпапок.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }
    }
}