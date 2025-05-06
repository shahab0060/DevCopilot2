using DevCopilot2.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using DevCopilot2.Domain.Entities.Common;
using DevCopilot2.Domain.IRepository;

namespace DevCopilot2.DataLayer.Repository
{
    /// <summary>
    /// read /write repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class CrudRepository<TEntity, TKey> :
          ICrudRepository<TEntity, TKey>
        where TEntity : EntityId<TKey>
        where TKey : struct
    {
        #region constructor


        private readonly DevCopilot2DbContext _dbContext;
        public CrudRepository(DevCopilot2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        public async Task Add(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.LatestEditDate = DateTime.Now;
            await this._dbContext.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            entity.LatestEditDate = DateTime.Now;
            entity.EditCounts++;
            _dbContext.Update(entity);
        }

        public void SoftDelete(TEntity entity)
        {
            entity.IsDelete = true;
            entity.DeletedOn = DateTime.Now;
            Update(entity);
        }

        public void Detach(TEntity entity)
            => _dbContext
            .Set<TEntity>()
            .Entry(entity).State = EntityState.Detached;

        public virtual async Task<TEntity?> GetAsTracking(TKey id)
        => await _dbContext.Set<TEntity>()
            .AsTracking()
            .Where(t => !t.IsDelete)
            .FirstOrDefaultAsync(t => t.Id.Equals(id));

        public virtual async Task<TEntity?> GetAsNoTracking(TKey id)
        => await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .Where(t => !t.IsDelete)
            .FirstOrDefaultAsync(t => t.Id.Equals(id));

        public IQueryable<TEntity> GetQueryable()
       => _dbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(t => !t.IsDelete)
            .OrderByDescending(x => x.Id)
            .AsQueryable();

        public async Task<int> CountFromSqlRaw(string sql)
            => 0;
        //.CountAsync();


        public void ClearChangeTracker()
            => _dbContext
            .ChangeTracker
            .Clear();

        public async Task Delete(TKey key)
        {
            TEntity? value = await GetAsTracking(key);
            if (value is not null)
                _dbContext.Remove(value);
        }

        public void Delete(TEntity entity)
        => _dbContext.Remove(entity);

        public void Dispose()
        => _dbContext.Dispose();

        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

        public async Task SaveChanges()
        => await _dbContext.SaveChangesAsync();
    }
}
