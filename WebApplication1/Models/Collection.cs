using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Collection
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public ICollection<Exponat> Exponats { get; set; }
    }
}
