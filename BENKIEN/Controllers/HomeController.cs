using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BENKIEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Products = await _context.Products.Where(p => p.IsActive && p.IsHome).ToListAsync(),
                ProductImages = await _context.ProductImages.ToListAsync(),
                Cart = await _context.Cart.ToListAsync(),               
                TopHeader = await _context.TopHeaders.Where(t => t.Active).FirstOrDefaultAsync(),
                Category = await _context.Categories.FirstOrDefaultAsync() // veya istediğiniz bir sorgu ile doldurun



            };

            return View("Index", model);
        }



        [Route("ContactUs")]     
        public async Task<IActionResult> ContactUsAsync(Contact contact)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    TempData["Mesaj"] = "<div class = 'alert alert-danger'>Mesajınız Gönderildi.. Teşekkürler..</div>";
                    return RedirectToAction("ContactUs");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluştu!.");
                }
            }
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }
        [Route("ERROR")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      
        public IActionResult About()
        {
            return View();
        }

     
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
