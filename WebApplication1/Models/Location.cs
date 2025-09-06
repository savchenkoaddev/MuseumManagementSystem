using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Address { get; set; }

        [MaxLength(1000)]
        public string? StorageConditions { get; set; }

        public ICollection<Exponat> Exponats { get; set; }
    }
}
