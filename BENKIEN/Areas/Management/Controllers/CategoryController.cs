using BENKIEN.Data;
using BENKIEN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BENKIEN.Areas.Management.Controllers
{
    [Area("Management"), Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoryController(DatabaseContext context) // S.O.L.I.D Prensipleri - Clean Code
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Categories.ToListAsync();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // gerçek bir istek mi sahte bir istek mi onu kontrol eder.
        public ActionResult Create(Category collection)
        {
            try
            {
                _context.Categories.Add(collection); // context üzerindeki Categories tablosuna collection daki kategoriyi ekle
                _context.SaveChanges(); // yukardaki ekleme işlemini veritabanına işle
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            var model = _context.Categories.Find(id); // context üzerinden veritabanındaki kategorilerden id si route dan gelenle eşleşen kaydı getirir find metodu 
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category collection)
        {
            try
            {
                _context.Categories.Update(collection);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _context.Categories.Find(id);
            return View(model);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                _context.Categories.Remove(collection); // ekrandan gelen kategoriyi sil 
                _context.SaveChanges(); // silme işlemini db ye işle
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
