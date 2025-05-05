using ExamNET;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        static ForbiddenWordsProcessor? processor;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckForbiddenWords(string? folderPath, string forbiddenWords,string destinationPath)
        {
            // Розділення заборонених слів
            var words = forbiddenWords.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(w => w.Trim())
;

            
            processor = new ForbiddenWordsProcessor(words.ToArray(), destinationPath);
            if (folderPath != null)
            {
               await processor.StartSearchAsync(folderPath);
            }
            else
            {
                var drives = DriveInfo.GetDrives()
                    .Where(d => d.IsReady)
                    .Select(d => d.Name)
                    .ToArray();
                await processor.StartSearchAsync(drives);
            }

            return View("Result");
        }
        public IActionResult Result()
        {
            // Приклад: зчитування звіту з файлу
            var reportPath = Path.Combine(processor._outputDirectory, "Report.txt");
            Console.WriteLine(reportPath);
            string[] reportContent = System.IO.File.Exists(reportPath)
                ? System.IO.File.ReadAllLines(reportPath)
                : Array.Empty<string>();

            ViewData["ReportContent"] = reportContent;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
