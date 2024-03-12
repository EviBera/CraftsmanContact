namespace CraftsmanContact.DTOs.User;

public class RegisterUserRequestDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}