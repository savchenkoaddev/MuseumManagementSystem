using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Column(TypeName = "smallint")]
        public short? BirthYear { get; set; }

        [Column(TypeName = "smallint")]
        public short? DeathYear { get; set; }

        [MaxLength(1000)]
        public string? Biography { get; set; }

        public ICollection<Exponat> Exponats { get; set; }
    }
}
