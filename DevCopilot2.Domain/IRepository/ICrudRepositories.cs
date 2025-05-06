using DevCopilot2.Domain.Entities.Common;

namespace DevCopilot2.Domain.IRepository
{
    public interface ICrudRepository<TEntity, TKey> : IDisposable, IAsyncDisposable
        where TEntity : EntityId<TKey>
        where TKey : struct
    {
        IQueryable<TEntity> GetQueryable();
        Task<int> CountFromSqlRaw(string sql);
        Task<TEntity?> GetAsNoTracking(TKey id); Task<TEntity?> GetAsTracking(TKey id);
        Task Add(TEntity entity);
        void Detach(TEntity entity);
        void ClearChangeTracker();
        void Update(TEntity entity);
        void SoftDelete(TEntity entity);
        Task SaveChanges(); Task Delete(TKey key);
        void Delete(TEntity entity);

    }

}
