using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class CraftsmanContactContext : IdentityDbContext<AppUser>
{
    public DbSet<OfferedService> OfferedServices { get; set; }
    public DbSet<Deal> Deals { get; set; }
    //public DbSet<UserOfferedService> UsersAndServicesJoinedTable { get; set; }
    private readonly IConfiguration _configuration;

    public CraftsmanContactContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("dbConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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