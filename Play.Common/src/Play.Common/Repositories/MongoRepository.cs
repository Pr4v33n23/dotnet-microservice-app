using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common.Entities;

namespace Play.Common.Repositories;

public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> _dbCollection;
    private readonly FilterDefinitionBuilder<T> _filterDefinitionBuilder = Builders<T>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
    }

    public async Task<T> GetAsync(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task CreateAsync(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        await _dbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        var filter = _filterDefinitionBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }
}