using CraftsmanContact.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CraftsmanContactIntegrationTests;

public class CraftsmanContactWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's CraftsmanContactContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<CraftsmanContactContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add CraftsmanContactContext using an in-memory database for testing.
            services.AddDbContext<CraftsmanContactContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryAppDb");
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (CraftsmanContactContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CraftsmanContactContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CraftsmanContactWebApplicationFactory>>();
                
                // Ensure the database is created.
                db.Database.EnsureCreated();
                
                try
                {
                    // Seed the database with test data.
                    Utilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test offered services. Error: {Message}", ex.Message);
                }
            }
        });
    }
    
    public void ResetDatabase()
    {
        using (var scope = this.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CraftsmanContactContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            Utilities.InitializeDbForTests(db);
        }
    }

}

