using CraftsmanContact.Models;

namespace CraftsmanContact.Services.AuthService;

public interface ITokenService
{
    string CreateToken(AppUser user, string? role);
}