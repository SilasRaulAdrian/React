using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
        {}

        public DbSet<Carte> Carti { get; set; }
    }
}
