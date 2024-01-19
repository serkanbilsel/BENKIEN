using BENKIEN.Entities;

namespace BENKIEN.Models
{
    public class HomePageViewModel
    {
        public List<Slider>? Sliders { get; set; }
        public List<Product>? Products { get; set; }
        public List<ProductImage>? ProductImages{ get; set; }
        public List<Cart>? Cart{ get; set; }
        public Category? Category { get; set; }
        public Product? ProductDetail { get; set; }
        public TopHeader? TopHeader { get; set; }
        public List<Product>? RelatedProducts { get; set; }

    }
}
