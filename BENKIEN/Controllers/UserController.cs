using BENKIEN.Data;
using BENKIEN.Entities;
using BENKIEN.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{

    private readonly DatabaseContext _context;

    public UserController(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetInt32("userId");

        if (userId == null)
        {
            TempData["Message"] = "<div class='alert alert-danger'>Lütfen Giriş Yapınız!</div>";
            return RedirectToAction("Login");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            // Kullanıcı bulunamadı veya hata oluştu
            return RedirectToAction("Error");
        }

        // User nesnesini ProfileViewModel'e dönüştür
        var profileViewModel = new ProfileViewModel
        {
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Phone = user.Phone,
            Address = user.Address,
            CurrentPassword = "", // Mevcut şifreyi burada belirleyebilirsiniz, eğer bu alanı kullanıcıya göstermek istemiyorsanız boş bırakabilirsiniz.
            NewPassword = "",
            ConfirmNewPassword = ""
        };
        ViewBag.SuccessMessage = ViewData["SuccessMessage"] as string;
        return View(profileViewModel);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        // E-posta adresine sahip bir kullanıcı var mı kontrolü
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı.");
            return RedirectToAction("Register");

        }

        if (ModelState.IsValid)
        {
            user.IsActive = true;
            _context.Add(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Başarıyla kayıt oldunuz!";
            return RedirectToAction("Login");

        }

        return RedirectToAction("Home/Index/");

    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginViewModel viewModel)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == viewModel.Email && x.Password == viewModel.Password && x.IsActive);

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

    public async Task<IActionResult> UpdateUserAsync(ProfileViewModel model)
    {
        try
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                // Diğer alanları güncelle
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Address = model.Address;

                // Mevcut şifre doğrulaması
                if (!string.IsNullOrEmpty(model.CurrentPassword) && user.Password == model.CurrentPassword)
                {
                    // Yeni şifre doğrulaması
                    if (!string.IsNullOrEmpty(model.NewPassword) && model.NewPassword == model.ConfirmNewPassword)
                    {
                        // Şifre değişmişse yeni şifreyi ata
                        user.Password = model.NewPassword;

                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();

                        // Başarılı kayıt mesajını burada set et
                        ViewData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi!";

                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Yeni şifre ve şifre tekrarı eşleşmiyor.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mevcut şifre doğrulanamadı.");
                }
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Hata Oluştu!");
        }

        // Başarılı kayıt mesajı burada değil, başarı durumunda return RedirectToAction içinde olmalı
        return View("Profile", model);
    }


    [Route("Logout")]// adres çubuğpundan yaptığımız yönlendirede login/logout yerine sadece logout a gidince çıkış yapsın
    public IActionResult Logout()
    {
        try
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("userGuid");
        }
        catch
        {
            HttpContext.Session.Clear();
        }
        return RedirectToAction("Index", "Home");
    }


}
