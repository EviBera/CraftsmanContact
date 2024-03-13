using CraftsmanContact.DTOs.Deal;
using CraftsmanContact.Models;

namespace CraftsmanContact.Mappers;

public static class DealMappers
{
    public static Deal ToDealFromCreateDealRequestDto(this CreateDealRequestDto dealDto)
    {
        return new Deal
        {
            CraftsmanId = dealDto.CraftsmanId,
            ClientId = dealDto.ClientId,
            OfferedServiceId = dealDto.OfferedServiceId
        };
    }

    public static DealDto ToDealDto(this Deal dealModel)
    {
        return new DealDto
        {
            DealId = dealModel.DealId,
            ClientId = dealModel.ClientId,
            CraftsmanId = dealModel.CraftsmanId,
            OfferedServiceId = dealModel.OfferedServiceId,
            CreatedAt = dealModel.CreatedAt,
            IsAcceptedByCraftsman = dealModel.IsAcceptedByCraftsman,
            IsClosedByClient = dealModel.IsClosedByClient,
            IsClosedByCraftsman = dealModel.IsClosedByCraftsman
        };
    }
}