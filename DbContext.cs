using Microsoft.EntityFrameworkCore;
using MyApiProject.Models; // ใช้ Model ที่เราสร้างไว้

namespace MyApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Product> Stock { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User to UserProfile one-to-one relationship

            // Define the 1:1 relationship
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Stock)
            .WithOne(s => s.Product)
            .HasForeignKey<Stock>(s => s.Id);


            // modelBuilder.Entity<Stock>()
            //     .ToTable("Stock");
        }
    }
}
