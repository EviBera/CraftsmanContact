using CraftsmanContact.DTOs.OfferedService;
using CraftsmanContact.Models;

namespace CraftsmanContact.Mappers;

public static class OfferedServiceMappers
{
    public static OfferedServiceDto ToOfferedServiceDto(this OfferedService offeredServiceModel)
    {
        return new OfferedServiceDto
        {
            OfferedServiceId = offeredServiceModel.OfferedServiceId,
            OfferedServiceName = offeredServiceModel.OfferedServiceName,
            OfferedServiceDescription = offeredServiceModel.OfferedServiceDescription
        };
    }

    public static OfferedService ToOfferedServiceFromCreateRequestOfferedServiceDto(this CreateRequestOfferedServiceDto serviceDto)
    {
        return new OfferedService
        {
            OfferedServiceName = serviceDto.OfferedServiceName,
            OfferedServiceDescription = serviceDto.OfferedServiceDescription
        };
    }
}