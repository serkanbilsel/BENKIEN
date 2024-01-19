using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BENKIEN.Entities
{
    public class ProductImage : BaseAuditEntity, IEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string? ImageName { get; set; }

        public string? ImageUrl { get; set; }
        [JsonIgnore]

        public Product? Product { get; set; }

    }
}
