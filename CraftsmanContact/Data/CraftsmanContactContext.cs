using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class CraftsmanContactContext : IdentityDbContext<AppUser>
{
    public DbSet<OfferedService> OfferedServices { get; set; }
    public DbSet<Deal> Deals { get; set; }

    public CraftsmanContactContext(DbContextOptions<CraftsmanContactContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
        modelBuilder.Entity<AppUser>().ToTable("AppUsers");
        
        // Configure AppUser
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.OfferedServices)
            .WithMany(s => s.AppUsers);

        // Configure OfferedService
        modelBuilder.Entity<OfferedService>(entity =>
        {
            entity.HasKey(e => e.OfferedServiceId);
            entity.Property(e => e.OfferedServiceName).HasMaxLength(50);
            entity.Property(e => e.OfferedServiceDescription).HasMaxLength(300);
        });
        
    }
}