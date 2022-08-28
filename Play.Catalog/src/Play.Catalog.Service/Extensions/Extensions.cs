using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Extensions;

public static class Extensions
{
    public static ItemDto AsDTO(this Item item)
    {
        return new ItemDto(item.Id, item.Description, item.Name, item.Price, item.CreatedAt);
    }
}