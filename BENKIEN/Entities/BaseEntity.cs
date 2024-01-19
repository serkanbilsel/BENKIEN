using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BENKIEN.Entities
{
    public abstract class BaseEntity : IEntity
    {
        [Key] // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // n = son kayıt (n + 1) otomatik artıracaktır.
        public int Id { get; set; }
    }
}