using BENKIEN.Entities;
using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "Ad"), StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Soyad"), StringLength(50)]
        public string Surname { get; set; }

        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon"), StringLength(15)]
        public string Phone { get; set; }

        [Display(Name = "Açık Adres")]
        public string Address { get; set; }
        [Display(Name = "Şu Anki Şifre")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şu anki şifrenizi girmelisiniz.")]
        public string CurrentPassword { get; set; }

        [Display(Name = "Yeni Şifre")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifre Tekrarı")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ile yeni şifre tekrarı eşleşmiyor.")]
        public string ConfirmNewPassword { get; set; }
       
    }

}
