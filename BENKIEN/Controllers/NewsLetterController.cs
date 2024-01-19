using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Mvc;

namespace BENKIEN.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly DatabaseContext _context;

        public NewsLetterController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe(string Email, string returnUrl)
        {
            if (!string.IsNullOrEmpty(Email))
            {
                try
                {
                    // Veritabanında aynı e-posta adresiyle bir kayıt olup olmadığını kontrol et
                    if (_context.NewsletterSubscriptions.Any(ns => ns.Email == Email))
                    {
                        TempData["DangerMessage"] = "Bu e-posta adresi zaten kayıtlı.";
                    }
                    else
                    {
                        // Veritabanına kayıt ekleme işlemi
                        var subscription = new NewsletterSubscription { Email = Email };
                        _context.NewsletterSubscriptions.Add(subscription);
                        _context.SaveChanges();

                        TempData["SuccessMessage"] = "Haber bültenine başarıyla kayıt oldunuz!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["DangerMessage"] = $"Bir hata oluştu: {ex.Message}";
                }
            }
            else
            {
                TempData["DangerMessage"] = "E-Mail alanı boş bırakılamaz.";
            }

            return Redirect(returnUrl ?? "/");
        }


    }
}
