using System.Text;

namespace CopyDir
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("Введіть шлях до початкової папки:");
            string sourceDir = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Введіть шлях до вихідної папки:");
            string destinationDir = Console.ReadLine() ?? string.Empty;

            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("Початкова папка не існує.");
                return;
            }

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            try
            {
                CopyFilesParallel(sourceDir, destinationDir);
                Console.WriteLine("Копіювання завершено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        static void CopyFilesParallel(string sourceDir, string destinationDir)
        {
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);

            Parallel.ForEach(files, file =>
            {
                // Обчислення відносного шляху та створення папок
                string relativePath = Path.GetRelativePath(sourceDir, file);
                string destinationPath = Path.Combine(destinationDir, relativePath);

                // Створення папки, якщо вона не існує
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

                // Копіювання файлу
                File.Copy(file, destinationPath, overwrite: true);
                Console.WriteLine($"Скопійовано: {file} -> {destinationPath}");
            });
        }
    }
}
