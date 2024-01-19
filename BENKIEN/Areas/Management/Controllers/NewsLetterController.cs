using BENKIEN.Data;
using BENKIEN.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace BENKIEN.Areas.Management.Controllers
{
    [Area("Management"), Authorize(Roles = "Admin")]
    public class NewsLetterController : Controller
    {
        private readonly DatabaseContext _context;

        public NewsLetterController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.NewsletterSubscriptions.ToListAsync();
            return View(model);
        }
        // BURAYI HALLETÇEN

        public async Task<IActionResult> SendBulkEmailAsync()
        {
            try
            {
                // E-posta gönderiminde kullanılacak SMTP ayarları
                var smtpClient = new SmtpClient("mail.benkien.com")
                {
                    Port = 465,  // SSL/TLS için genellikle 465 portu kullanılır
                    Credentials = new NetworkCredential("serkan@benkien.com", "Ben2400*"),
                    EnableSsl = true,
                };

                // Abonelere e-posta gönderimi
                foreach (var subscription in _context.NewsletterSubscriptions)
                {
                    var mailMessage = new MailMessage("your_email@example.com", subscription.Email)
                    {
                        IsBodyHtml = true,
                        Subject = subscription.EmailSubject,
                        Body = subscription.EmailBody
                    };

                    await smtpClient.SendMailAsync(mailMessage);
                }

                ViewBag.SuccessMessage = "Toplu e-posta başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Toplu e-posta gönderilirken hata oluştu: {ex.Message}";
            }

            return View(); // Bu kısmı ekledim
        }











        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(NewsletterSubscription newsletter)
        {
            if (ModelState.IsValid)
            {
                _context.NewsletterSubscriptions.Add(newsletter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewsletterSubscriptions == null)
            {
                return NotFound();
            }

            var NewsletterSubscriptions = await _context.NewsletterSubscriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (NewsletterSubscriptions == null)
            {
                return NotFound();
            }

            return View(NewsletterSubscriptions);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewsletterSubscriptions == null)
            {
                return Problem("Entity set 'DatabaseContext.NewsletterSubscriptions'  is null.");
            }
            var NewsletterSubscriptions = await _context.NewsletterSubscriptions.FindAsync(id);
            if (NewsletterSubscriptions != null)
            {
                _context.NewsletterSubscriptions.Remove(NewsletterSubscriptions);
            }

            await _context.SaveChangesAsync();
            return View();
        }

        private bool NewsletterSubscriptionsExists(int id)
        {
            return (_context.NewsletterSubscriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }


}
