using System.Linq.Expressions;
using MongoDB.Driver;

namespace PB.MeetUp.AITools.Mongo;

public class MongoProvider<TModel> : IMongoProvider<TModel, string> where TModel : IEntity<string>
{
    private readonly IMongoDatabase _db;
    private readonly IMongoCollection<TModel> _collection;

    public MongoProvider(IMongoClient mongoClient, string databaseName)
    {
        _db = mongoClient.GetDatabase(databaseName);
        _collection = _db.GetCollection<TModel>(typeof(TModel).Name);
    }
    
    public async Task<TModel> Create(TModel entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<TModel> Update(TModel entity)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return entity;
    }

    public async Task Delete(TModel entity)
    {
        await _collection.DeleteOneAsync(x => x.Id == entity.Id);
    }

    public async Task<TModel> Get(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<TModel> Find(Expression<Func<TModel, bool>> predicate)
    {
        return _collection.Find(predicate).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TModel>> GetAll()
    {
        return await _collection.Find(x => true).ToListAsync();
    }

    public async Task<IEnumerable<TModel>> GetAll(Expression<Func<TModel, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }
}