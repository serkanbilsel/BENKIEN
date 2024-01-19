using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Models
{
    public class OrderCheckoutViewModel
    {
        [Required(ErrorMessage = "Müşteri adı zorunlu bir alan.")]
        [Display(Name = "Müşteri Adı")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Müşteri adresi zorunlu bir alan.")]
        [Display(Name = "Müşteri Adresi")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Müşteri telefon numarası zorunlu bir alan.")]
        [Display(Name = "Müşteri Telefon Numarası")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string CustomerPhoneNumber { get; set; }
    }

}
