using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exponat> Exponats { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
