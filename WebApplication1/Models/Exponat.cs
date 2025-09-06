using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Exponat
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Material { get; set; }

        [Required]
        [MaxLength(50)]
        public string Technique { get; set; }

        [Column(TypeName = "smallint")]
        public short? CreationYear { get; set; }

        [MaxLength(100)]
        public string? Condition { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        public Guid CollectionId { get; set; }

        public Collection Collection { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        public Location Location { get; set; }
    }
}
