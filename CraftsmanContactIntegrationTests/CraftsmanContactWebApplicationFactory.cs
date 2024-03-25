using CraftsmanContact.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
                
                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        });
    }


}

