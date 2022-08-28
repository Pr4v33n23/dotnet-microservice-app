using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Extensions;

public static class Extensions
{
    public static InventoryItemDto AsDto(this InventoryItem inventoryItem, string name, string description)
    {
        return new InventoryItemDto(inventoryItem.CatalogItemId,
            inventoryItem.Quantity, name, description, inventoryItem.AcquiredDate);
    }
}