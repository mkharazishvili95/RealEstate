using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Address;
using RealEstate.Core.Agency;
using RealEstate.Core.Apartment;
using RealEstate.Core.Currency;
using RealEstate.Core.PaidService;
using RealEstate.Core.User;

namespace RealEstate.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<UserFavoriteApartment> UserFavoriteApartments { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Subdistrict> Subdistricts { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<RealEstate.Core.File.File> Files { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Apartment>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Currency>()
                .Property(c => c.Rate)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");
        }
    }
}