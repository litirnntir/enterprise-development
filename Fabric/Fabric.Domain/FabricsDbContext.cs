using Fabrics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Domain;

public class FabricsDbContext(DbContextOptions<FabricsDbContext> options) : DbContext(options)
{
    public DbSet<Factory> Fabrics { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Factory entity
        modelBuilder.Entity<Factory>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<Factory>()
            .Property(f => f.Type)
            .IsRequired();

        modelBuilder.Entity<Factory>()
            .Property(f => f.Name)
            .IsRequired();

        modelBuilder.Entity<Factory>()
            .HasMany(f => f.Shipments)
            .WithOne()
            .HasForeignKey(s => s.FabricId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Provider entity
        modelBuilder.Entity<Provider>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Provider>()
            .Property(p => p.Name)
            .IsRequired();

        modelBuilder.Entity<Provider>()
            .HasMany(p => p.Shipments)
            .WithOne()
            .HasForeignKey(s => s.ProviderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Shipment entity
        modelBuilder.Entity<Shipment>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Shipment>()
            .Property(s => s.Date)
            .IsRequired();

        modelBuilder.Entity<Shipment>()
            .Property(s => s.NumberOfGoods)
            .IsRequired();

    }
}
