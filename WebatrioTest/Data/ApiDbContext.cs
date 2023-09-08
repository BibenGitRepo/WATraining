using Microsoft.EntityFrameworkCore;
using WebatrioTest.Models;

namespace WebatrioTest.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Company> companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<List<string>>().HasNoKey();
        }

    }
}
