using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamNET
{
    public class ForbiddenWordsProcessor
    {
        private readonly string[] _forbiddenWords;
        public readonly string _outputDirectory;
        private readonly ConcurrentBag<string> _report = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public ForbiddenWordsProcessor(string[] forbiddenWords, string outputDirectory)
        {
            _forbiddenWords = forbiddenWords;
            _outputDirectory = outputDirectory;
        }

        public async Task StartSearchAsync(string[] drives)
        {
            var tasks = drives.Select(drive => Task.Run(() => ProcessDrive(drive, _cancellationTokenSource.Token)));
            await Task.WhenAll(tasks);
            GenerateReport();
        }
        public async Task StartSearchAsync(string folder)
        {
            var tasks = new List<Task>();
            var semaphore = new SemaphoreSlim(10); // Обмеження на 10 одночасних завдань

            try
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    await semaphore.WaitAsync(); // Очікування доступу до семафора

                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await ProcessFileAsync(file, _cancellationTokenSource.Token);
                        }
                        finally
                        {
                            semaphore.Release(); // Звільнення семафора
                        }
                    }));
                }

                await Task.WhenAll(tasks); // Очікування завершення всіх завдань
                GenerateReport(); // Генерація звіту після завершення
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при обробці папки {folder}: {ex.Message}");
            }
            finally
            {
                semaphore.Dispose(); // Звільнення ресурсів семафора
            }
        }

        private async Task ProcessFileAsync(string file, CancellationToken token)
        {
            if (token.IsCancellationRequested) return;

            try
            {
                var content = await File.ReadAllTextAsync(file, token);
                var matches = _forbiddenWords.Where(word => content.Contains(word)).ToList();

                if (matches.Count > 0)
                {
                    // Копіювання файлу
                    var destinationPath = Path.Combine(_outputDirectory, Path.GetFileName(file));
                    File.Copy(file, destinationPath);

                    // Заміна заборонених слів
                    var modifiedContent = ReplaceForbiddenWords(content, matches);
                    await File.WriteAllTextAsync(destinationPath, modifiedContent, token);

                    // Додавання до звіту
                    _report.Add($"Файл: {file}, Розмір: {new FileInfo(file).Length} байт, Заміни: {matches.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при обробці файлу {file}: {ex.Message}");
            }
        }
        private void ProcessFolder(string folder, CancellationToken token)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    if (token.IsCancellationRequested) break;
                    var content = File.ReadAllText(file);
                    var matches = _forbiddenWords.Where(word => content.Contains(word)).ToList();
                    if (matches.Count != 0)
                    {
                        // Копіювання файлу
                        var destinationPath = Path.Combine(_outputDirectory, Path.GetFileName(file));
                        File.Copy(file, destinationPath, true);
                        // Заміна заборонених слів
                        var modifiedContent = ReplaceForbiddenWords(content, matches);
                        File.WriteAllText(destinationPath, modifiedContent);
                        // Додавання до звіту
                        _report.Add($"Файл: {file}, Розмір: {new FileInfo(file).Length} байт, Заміни: {matches.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при обробці папки {folder}: {ex.Message}");
            }
        }
        private void ProcessDrive(string drive, CancellationToken token)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(drive, "*.*", SearchOption.AllDirectories))
                {
                    if (token.IsCancellationRequested) break;

                    var content = File.ReadAllText(file);
                    var matches = _forbiddenWords.Where(word => content.Contains(word)).ToList();

                    if (matches.Count != 0)
                    {
                        // Копіювання файлу
                        var destinationPath = Path.Combine(_outputDirectory, Path.GetFileName(file));
                        File.Copy(file, destinationPath, true);

                        // Заміна заборонених слів
                        var modifiedContent = ReplaceForbiddenWords(content, matches);
                        File.WriteAllText(destinationPath, modifiedContent);

                        // Додавання до звіту
                        _report.Add($"Файл: {file}, Розмір: {new FileInfo(file).Length} байт, Заміни: {matches.Count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при обробці диска {drive}: {ex.Message}");
            }
        }

        private string ReplaceForbiddenWords(string content, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                content = Regex.Replace(content, Regex.Escape(word), "*******", RegexOptions.IgnoreCase);
            }
            return content;
        }

        private void GenerateReport()
        {
            var reportPath = Path.Combine(_outputDirectory, "Report.txt");
            File.WriteAllLines(reportPath, _report);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
