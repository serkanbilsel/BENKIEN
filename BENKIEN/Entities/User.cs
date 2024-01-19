using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class User
    {

        public int Id { get; set; }

        [Display(Name = "Ad"), StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Soyad"), StringLength(50)]
        public string? Surname { get; set; }

        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        
        [Display(Name = "Telefon"), StringLength(15)]
        public string? Phone { get; set; }
        [Display(Name = "Açık Adres")]
        public string? Address { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public Guid? UserGuid { get; set; } // Bu guid i session veya cookie de saklayarak kullanıcıyı tanımak için kullanırız
        public List<Order>? Orders { get; set; } // Kullanıcının siparişleri

        public List<Cart>? Cart { get; set; }


    }
}
