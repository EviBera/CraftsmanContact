using CraftsmanContact.DTOs.User;
using CraftsmanContact.Models;

namespace CraftsmanContact.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto(this AppUser userModel)
    {
        return new UserDto
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            Phone = userModel.PhoneNumber,
            OfferedServices = userModel.OfferedServices.Select(os => os.ToOfferedServiceDto()).ToList()
        };
    }

    public static AppUser ToAppUserFromRegisterUserRequestDto(this RegisterUserRequestDto requestDto)
    {
        return new AppUser
        {
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName,
            Email = requestDto.Email,
            PhoneNumber = requestDto.PhoneNumber,
            UserName = requestDto.Email,
            PasswordHash = requestDto.Password
        };
    }
}