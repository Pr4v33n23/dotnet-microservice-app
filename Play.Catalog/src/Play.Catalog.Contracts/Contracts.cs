namespace Play.Catalog.Contracts;

public record CatalogItemCreated(Guid id, string Name, string Description);
public record CatalogItemUpdated(Guid id, string Name, string Description);
public record CatalogItemDeleted(Guid id);