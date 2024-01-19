using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Adı :"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez.!")]
        public string Name { get; set; }
        [Display(Name = "Soyadı :"), StringLength(50)]
        public string? Surname { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }

        [Display(Name = "Telefon :"), StringLength(15)]
        public string? Phone { get; set; }

        [Display(Name = "Mesajınız :"), DataType(DataType.MultilineText), Required(ErrorMessage = "{0} Boş Geçilemez.!")]
        public string Message { get; set; }
        [Display(Name = "Eklenme Tarihi :"), ScaffoldColumn(false)] // diğer sayfalarda gelmesin diye scaff yaptık
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
