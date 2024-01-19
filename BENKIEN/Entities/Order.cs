using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{

    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı")]
        public int UserId { get; set; }

        [Display(Name = "Sipariş Tarihi")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Display(Name = "Toplam Tutar")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Ödeme Durumu")]
        public bool IsPaid { get; set; }

        // Diğer sipariş bilgilerini ekleyebilirsiniz
     
        public string CustomerName { get; set; }
     
        public string CustomerSurName { get; set; }
      
        public string CustomerAddress { get; set; }
      
        public string CustomerPhoneNumber { get; set; }
        public User User { get; set; }
      
        public List<OrderItem> OrderItems { get; set; } // Siparişe ait ürünler
    }


}
