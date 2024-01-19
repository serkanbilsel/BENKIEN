using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BENKIEN.Areas.Management.Controllers
{
    [Area("Management")]
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }
        //[Area("Management"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(LoginViewModel viewModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == viewModel.Email && x.Password == viewModel.Password && x.IsActive && x.IsAdmin);

            if (user == null)
            {
                ModelState.AddModelError("", "Giriş Başarısız!");
            }
            else
            {
                HttpContext.Session.SetString("userName", user.Name);
                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetString("userGuid", user.UserGuid.ToString());

                if (user.IsAdmin)
                {
                    HttpContext.Session.SetInt32("IsAdmin", 1);
                }
                else
                {
                    HttpContext.Session.Remove("IsAdmin");
                }

                return Redirect("/Home/Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            try
            {
                var kullanici = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive);
                if (kullanici == null)
                {
                    TempData["Mesaj"] = "<div class='alert alert-danger' >Giriş Başarısız!</div>";
                }
                else
                {
                    var haklar = new List<Claim>() // claim = hak
            {
                new Claim(ClaimTypes.Email, kullanici.Email) // giriş için hak tanımladık
            };

                    if (kullanici.IsAdmin)
                    {
                        haklar.Add(new Claim(ClaimTypes.Role, "Admin")); // Admin rolü ekle
                    }

                    var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcıya kimlik tanımladık
                    ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    if (kullanici.IsAdmin)
                    {
                        return Redirect("/Management");
                    }
                    else
                    {
                        return Redirect("/AccessDenied");
                    }
                }
            }
            catch (Exception)
            {
                TempData["Mesaj"] = "Hata Oluştu!";
            }

            return View();
        }








        // adres çubuğpundan yaptığımız yönlendirede login/logout yerine sadece logout a gidince çıkış yapsın
        public async Task<IActionResult> AdminLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Management/Login");
        }
    }
}