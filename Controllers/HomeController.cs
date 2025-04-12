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
