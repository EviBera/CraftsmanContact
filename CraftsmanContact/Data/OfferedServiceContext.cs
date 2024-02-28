using CraftsmanContact.Models;
using Microsoft.EntityFrameworkCore;

namespace CraftsmanContact.Data;

public class OfferedServiceContext : DbContext
{
    public DbSet<OfferedService> OfferedServices { get; set; }
    private readonly IConfiguration _configuration;

    public OfferedServiceContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("dbConnectionString");
        optionsBuilder.UseSqlServer(connectionString);
    }
}