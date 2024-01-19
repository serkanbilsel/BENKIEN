using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace P013EStore.MVCUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var urunler = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.IsActive)
                .ToListAsync();

            var model = new HomePageViewModel
            {
                Products = urunler
            };

            return View("Index", model);
        }


        public async Task<IActionResult> Search(string q)
        {
            var urunler = await _context.Products
                .Include(p => p.ProductImages)
                .Where(p => p.IsActive && EF.Functions.Like(p.Name, $"%{q}%"))
                .ToListAsync();

            var model = new HomePageViewModel
            {
                Products = urunler,
                // Diğer özellikleri doldurabilirsiniz
            };

            return View("Index", model); // veya hangi view'ı kullanıyorsanız onu belirtin
        }


        public async Task<IActionResult> DetailAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Benzer ürünleri getir
            var similarProducts = await _context.Products
        .Include(p => p.ProductImages)
        .Where(p => p.CategoryId == product.CategoryId && p.Id != id) // Aynı kategoriye sahip, ancak şu anki ürünü dışlayan ürünleri getir
        .Take(4) // İlk 4 benzer ürünü al, isteğe bağlı olarak değiştirebilirsiniz
        .ToListAsync();

            var model = new HomePageViewModel
            {
                ProductDetail = product,
                RelatedProducts = similarProducts,
                // Diğer özellikleri de burada set edebilirsiniz
            };

            return View(model);
        }




    }
}
