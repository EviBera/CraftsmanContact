using CraftsmanContact.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class UsersContext : IdentityDbContext<AppUser>
{
    public DbSet<UserOfferedService> UserOfferedServices { get; set; }
    private readonly IConfiguration _configuration;

    public UsersContext(DbContextOptions<UsersContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("dbConnectionString");
            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .HasMany(u => u.UserOfferedServices)
            .WithOne(uso => uso.AppUser)
            .HasForeignKey(uso => uso.AppUserId)
            .IsRequired();

        modelBuilder.Entity<UserOfferedService>(entity =>
        {
            entity.HasKey(uso => uso.Id);

            entity.Property(uso => uso.OfferedServiceId).IsRequired();
        });
    }
}