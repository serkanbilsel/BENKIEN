using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BENKIEN.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var category = await _context.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductImages)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new HomePageViewModel
            {
                Category = category,
                // Diğer özellikleri doldurabilirsiniz
            };

            return View(model); // Düzeltildi, 'category' yerine 'model' döndürüldü
        }



    }
}
