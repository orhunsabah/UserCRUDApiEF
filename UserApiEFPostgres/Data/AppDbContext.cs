using Microsoft.EntityFrameworkCore;
using UserApiEFPostgres.Models;

namespace UserApiEFPostgres.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost:5433;Database=postgres;Username=postgres;Password=postgres");
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
