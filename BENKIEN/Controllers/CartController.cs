using BENKIEN.Data;
using BENKIEN.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BENKIEN.Controllers
{
    public class CartController : Controller
    {
        private readonly DatabaseContext _context;

        public CartController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId(); // Kullanıcı kimliğini almak için kendi yönteminizi kullanın
            var user = await _context.Users.Include(u => u.Cart).ThenInclude(c => c.Product).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user.Cart);
        }

        public async Task<IActionResult> Cart()
        {
            // Sepet içeriğini getir
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId.HasValue)
            {
                var user = await _context.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.Product)
                    .FirstOrDefaultAsync(u => u.Id == userId.Value);

                if (user != null && user.Cart != null)
                {
                    return View(user.Cart);
                }
            }

            // Kullanıcı bulunamadı veya sepeti boş
            // İlgili bir hata mesajı veya başka bir sayfaya yönlendirme yapabilirsiniz.
            return View("Error");
        }


        public async Task<IActionResult> AddToCart(int productId, int Price)
        {
            var userId = GetUserId(); // Kullanıcı kimliğini almak için kendi yönteminizi kullanın
            var user = await _context.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == userId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);


            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı. İlk önce giriş yapmalısınız!");
            }

            // Sepetteki aynı ürünü kontrol et
            var cartItem = user.Cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                // Sepette varsa miktarı arttır
                cartItem.Quantity += Price;
            }
            else
            {
                // Sepette yoksa yeni bir öğe ekle
                user.Cart.Add(new Cart
                {
                    ProductId = productId,
                    Quantity = Price
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = HttpContext.Session.GetInt32("userId");// Kullanıcı kimliğini almak için kendi yönteminizi kullanın
            var user = await _context.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Sepetten ürünü kaldır
            var cartItem = user.Cart.FirstOrDefault(c => c.ProductId == productId);

            if (cartItem != null)
            {
                user.Cart.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Cart");
        }

        // Diğer action'lar...

        // Yardımcı metot: Kullanıcı kimliğini almak için kendi yönteminizi kullanın
        public async Task<IActionResult> Checkout()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var user = await _context.Users.Include(u => u.Cart).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            // Ödeme işlemleri ve sipariş oluşturma
            // Bu kısımda sipariş oluşturmak, ödeme almak ve sepeti temizlemek gibi işlemleri gerçekleştirin

            // Örneğin:
            // 1. Sipariş oluştur


            if (userId.HasValue)
            {
                var order = new Order
                {
                    UserId = userId.Value,
                    CustomerName = "Müşteri Adı", // Örnek: Müşteri adı
                    CustomerAddress = "Müşteri Adresi", // Örnek: Müşteri adresi
                    CustomerPhoneNumber = "Müşteri Telefon Numarası", // Örnek: Müşteri telefon numarası
                                                                      // Diğer gerekli bilgiler...
                };

                // Order'ı veritabanına ekleyin
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Siparişe ait ürünleri ve miktarlarını ekleyin
                foreach (var cartItem in user.Cart)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.Product.Price // Product sınıfınıza göre fiyat bilgisini alın
                    };

                    // OrderItem'ı veritabanına ekleyin
                    _context.OrderItems.Add(orderItem);
                }

                // Sepeti temizleyin
                user.Cart.Clear();
                await _context.SaveChangesAsync();

                // Diğer işlemleri gerçekleştirin (örneğin, ödeme işlemleri, e-posta gönderme vb.)

                // Sipariş tamamlandıktan sonra yönlendirme yapabilirsiniz
                return RedirectToAction("OrderComplete");
            }
            else
            {
                // Kullanıcı kimliği yoksa gerekli işlemleri yapın (örneğin, kullanıcıyı giriş yapmaya yönlendirme)
                return RedirectToAction("Login");
            }


            // Yardımcı metot: Kullanıcı kimliğini almak için kendi yönteminizi kullanın
         
        }
        public int GetUserId()
        {
            // Session'dan kullanıcı kimliğini al
            var userId = HttpContext.Session.GetInt32("userId");

            // Eğer userId null ise veya değer alınamazsa, varsayılan bir değer döndür
            return userId ?? 0;
        }
    }
}
