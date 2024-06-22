namespace LeagueBoss.Application.DbContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    EntityEntry Add(object entity);
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class;

    void AddRange(params object[] entities);
    Task AddRangeAsync(params object[] entities);
    Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

    EntityEntry Update(object entity);
    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Attach(object entity);
    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Remove(object entity);
    EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        where TEntity : class;
    ChangeTracker ChangeTracker { get; }
    DatabaseFacade Database { get; }
}