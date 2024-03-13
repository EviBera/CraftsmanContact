using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class CraftsmanContactContext : IdentityDbContext<AppUser>
{
    public DbSet<OfferedService> OfferedServices { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<UserOfferedService> UsersAndServicesJoinedTable { get; set; }
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

        // Configure OfferedService
        modelBuilder.Entity<OfferedService>(entity =>
        {
            entity.HasKey(e => e.OfferedServiceId);
            entity.Property(e => e.OfferedServiceName).HasMaxLength(50);
            entity.Property(e => e.OfferedServiceDescription).HasMaxLength(300);
        });

        // Configure UserOfferedService (join table)
        modelBuilder.Entity<UserOfferedService>()
            .HasKey(uos => new { uos.AppUserId, uos.OfferedServiceId });

        modelBuilder.Entity<UserOfferedService>()
            .HasOne(uos => uos.AppUser)
            .WithMany(au => au.UserOfferedServices)
            .HasForeignKey(uos => uos.AppUserId);

        modelBuilder.Entity<UserOfferedService>()
            .HasOne(uos => uos.OfferedService)
            .WithMany(os => os.UserOfferedServices)
            .HasForeignKey(uos => uos.OfferedServiceId);
        
        //Prevent cascading deletion when a user leaves the app
        modelBuilder.Entity<Deal>()
            .HasOne(d => d.CraftsMan)
            .WithMany()
            .HasForeignKey(d => d.CraftsmanId)
            .OnDelete(DeleteBehavior.NoAction); 

        modelBuilder.Entity<Deal>()
            .HasOne(d => d.Client)
            .WithMany()
            .HasForeignKey(d => d.ClientId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}