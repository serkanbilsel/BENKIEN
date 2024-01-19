using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BENKIEN.Areas.Management.Controllers
{
    [Area("Management"), Authorize(Roles = "Admin")]
    public class TopHeaderController : Controller
    {
        private readonly DatabaseContext _context;

        public TopHeaderController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = await _context.TopHeaders.ToListAsync();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(TopHeader collection)
        {
            try
            {
               
                await _context.TopHeaders.AddAsync(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.TopHeaders.FindAsync(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, TopHeader collection)
        {
            try
            {
                var existingTopHeader = await _context.TopHeaders.FindAsync(id);

                if (existingTopHeader != null)
                {
                    existingTopHeader.Title = collection.Title; // Diğer özellikleri de güncelleyin gerekirse
                    existingTopHeader.Link = collection.Link; // Diğer özellikleri de güncelleyin gerekirse
                    existingTopHeader.Active = collection.Active; // Checkbox'tan gelen değeri buradan alın

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id) // metodu asenkron yaptı. çünkü içinde await var
        {
            var model = await _context.TopHeaders.FindAsync(id);
            return View(model);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, TopHeader collection)
        {
            try
            {
                _context.TopHeaders.Remove(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
