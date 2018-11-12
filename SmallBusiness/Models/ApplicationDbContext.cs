using Microsoft.EntityFrameworkCore;

namespace SmallBusiness.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Classification> Classifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Gender);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.City);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Region);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Classification);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User);
        }
    }
}
