using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Models
{
    public class NewsletterSubscriptionModel
    {
      

        [Required(ErrorMessage = "E-posta adresi alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }

}
