using CraftsmanContact.Data;
using CraftsmanContact.Models;

namespace CraftsmanContactIntegrationTests;

public static class Utilities
{
    public static void InitializeDbForTests(CraftsmanContactContext db)
    {
        db.OfferedServices.Add(new OfferedService
        {
            OfferedServiceId = 1,
            OfferedServiceName = "Update this",
            OfferedServiceDescription = "Service to update"
        });
        db.OfferedServices.Add(new OfferedService
        {
            OfferedServiceId = 2,
            OfferedServiceName = "Delete this",
            OfferedServiceDescription = "Service to delete"
        });
        db.SaveChanges();
    }
}