using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewContext _context;

        public HomeController(ILogger<HomeController> logger,NewContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult>   Index()
        {
            if (!_context.NewModels.Any())
            {
                IEnumerable<New> list = await AddNewsAndReturnListAsync();
                return View(list);
            }
            else
            {
                IEnumerable<New> list = await _context.NewModels.ToListAsync();
                return View(list);
            }

        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile photo, string title, string summary, string slug,string content)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("Фото не вибрано.");
            }

            // Збереження файлу
            string uploadsFolder = Path.Combine("wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Створення об'єкта для збереження в базі даних
            var photoObject = new New
            {
                title = title,
                summary = summary,
                slug = slug,
                content = content,
                image = uniqueFileName
            };
            

            // Збереження у базу даних
            await _context.NewModels.AddAsync(photoObject);
            await _context.SaveChangesAsync();

            return Ok($"Об'єкт збережено: {title}");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IEnumerable<New>> AddNewsAndReturnListAsync()
        {
            for (int i = 0; i < 10; i++)
            {

                await _context.NewModels.AddAsync(_context.NewFaker.Generate());
            }
            await _context.SaveChangesAsync();
            return _context.NewModels.ToList();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
