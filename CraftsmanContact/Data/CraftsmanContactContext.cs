using CraftsmanContact.Models;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class CraftsmanContactContext : DbContext
{
    public DbSet<OfferedService> OfferedServices { get; set; }
    public DbSet<Deal> Deals { get; set; }
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
}