using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Display(Name = "Sipariş")]
        public int OrderId { get; set; }

        [Display(Name = "Ürün")]
        public int ProductId { get; set; }

        [Display(Name = "Adet")]
        public int Quantity { get; set; }

        [Display(Name = "Birim Fiyat")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Toplam Fiyat")]
        public decimal TotalPrice { get; set; }

        // Diğer sipariş öğesi bilgilerini ekleyebilirsiniz

        public Order Order { get; set; }
        public Product Product { get; set; }


    }

}
