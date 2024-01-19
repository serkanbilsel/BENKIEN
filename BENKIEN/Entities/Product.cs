using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BENKIEN.Entities
{
    public class Product : BaseAuditEntity, IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Resim")]
        public string? Image { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Ürün Kodu")]
        public string? ProductCode { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        [Display(Name = "Stok Adeti")]
        public int Stock { get; set; }

        [Display(Name = "Ürün Mevcut")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Aktif et")]
        public bool IsActive { get; set; }

        [Display(Name = "Anasayfaya Ekle")]
        public bool IsHome { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Brand ve Category sınıfları ile ilişkilendirilen alanlar
        [Display(Name = "Marka")]
        public int BrandId { get; set; }
        [Display(Name = "Marka")]
        public Brand? Brand { get; set; }
        [Display(Name = "Kategori")]
        public Category? Category { get; set; }

        [Display(Name = "Ürün Fotoğrafları")]
        public List<ProductImage>? ProductImages { get; set; }
        // Yeni eklenen özellik
        [NotMapped]
        [JsonIgnore]
        [Display(Name = "Ürün Fotoğrafı")]
        public string ImageUrl
        {
            get
            {
                return ProductImages?.FirstOrDefault()?.ImageUrl;
            }
        }
        public int? RemovedImageIndex { get; set; }

        public List<Cart>? Carts { get; set; }
    }
}
