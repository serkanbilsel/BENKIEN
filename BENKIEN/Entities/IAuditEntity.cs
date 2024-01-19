

namespace BENKIEN.Entities
{
    public interface IAuditEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
