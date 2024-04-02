using CraftsmanContact.Data;
using CraftsmanContact.Models;

namespace CraftsmanContactIntegrationTests;

public static class Utilities
{
    public static void InitializeDbForTests(CraftsmanContactContext db)
    {
        var os1 = new OfferedService
        {
            OfferedServiceId = 1,
            OfferedServiceName = "Test service 1",
            OfferedServiceDescription = "Service 1"
        };

        var os2 = new OfferedService
        {
            OfferedServiceId = 2,
            OfferedServiceName = "Delete this",
            OfferedServiceDescription = "Service to delete"
        };
        
        var os11 = new OfferedService
        {
            OfferedServiceId = 11,
            OfferedServiceName = "Update this",
            OfferedServiceDescription = "Service to update"
        };

        var user1 = new AppUser
        {
            Id = "user1",
            FirstName = "Testuser1",
            LastName = "One",
            Email = "test1@email.com"
        };

        var user2 = new AppUser
        {
            Id = "user2",
            FirstName = "Testuser2",
            LastName = "Two",
            Email = "test2@email.com"
        };

        var user3 = new AppUser
        {
            Id = "user3",
            FirstName = "Testuser3",
            LastName = "Three",
            Email = "test3@email.com"
        };
        
        user1.OfferedServices.Add(os1);
        os1.AppUsers.Add(user1);
        
        user2.OfferedServices.Add(os1);
        os1.AppUsers.Add(user2);
        user2.OfferedServices.Add(os2);
        os2.AppUsers.Add((user2));
        
        db.OfferedServices.AddRange(os1, os2, os11);
        db.Users.AddRange(user1, user2, user3);
        
        db.SaveChanges();
    }
}