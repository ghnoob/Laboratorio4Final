using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FinalLaboratorio4.Models;
using FinalLaboratorio4.Data;

namespace FinalLaboratorio4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 16;

            return View(await PaginatedList<Producto>.CreateAsync(
                _context.Productos
                    .AsNoTracking()
                    .Where(p => p.Favorito)
                    .Include(p => p.Categoria)
                    .OrderBy(p => p.Nombre),
                pageNumber ?? 1,
                pageSize
            ));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
