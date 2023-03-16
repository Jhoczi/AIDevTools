using System.Linq.Expressions;

namespace PB.MeetUp.AITools.Mongo;

public interface IMongoProvider<TEntity, TId> where TEntity : IEntity<TId>
{
    // Generate CRUD task methods for mongo provider
    Task<TEntity> Create(TEntity entity);
    
    Task<TEntity> Update(TEntity entity);
    
    Task Delete(TEntity entity);
    
    Task<TEntity> Get(TId id);

    Task<TEntity> FindSingle(Expression<Func<TEntity, bool>> predicate);
    Task<ICollection<TEntity>> FindMany(Expression<Func<TEntity, bool>> predicate);
    
    Task<IEnumerable<TEntity>> GetAll();
    
    Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
}