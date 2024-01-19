using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public class TopHeader
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Top Header Başlık"),StringLength(500)]
        public string? Title { get; set; }

        [Display(Name = "Top Header Link"), StringLength(100)]
        public string? Link { get; set; }
        [Display(Name = "Yayınla / Aktif Et")]
        public bool Active { get; set; }

    }
}
