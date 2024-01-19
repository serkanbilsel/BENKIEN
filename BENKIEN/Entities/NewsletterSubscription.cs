using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class NewsletterSubscription
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "E-Mail alanı zorunludur.")]
        
        public string Email { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailBody { get; set; }
        // İsteğe bağlı diğer alanları ekleyebilirsiniz, örneğin isim, soyisim, tarih vb.
    }
}
