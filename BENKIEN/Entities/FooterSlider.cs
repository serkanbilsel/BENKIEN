using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class FooterSlider
    {
        public int Id { get; set; }

        [Display(Name = "Resim"), StringLength(50)]
        public string? Image { get; set; }
        [Display(Name = "Başlık"), StringLength(50)]
        public string? Title { get; set; }
        [Display(Name = "Açıklama"), StringLength(50)]
        public string? Description { get; set; }
        [Display(Name = "Slider Link"), StringLength(50)]
        public string? Link { get; set; }
    }
}
