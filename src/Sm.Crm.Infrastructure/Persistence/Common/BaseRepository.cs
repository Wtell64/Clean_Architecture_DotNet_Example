using Microsoft.EntityFrameworkCore;
using Sm.Crm.Domain.Common;
using System.Linq.Expressions;

namespace Sm.Crm.Infrastructure.Persistence.Common;

public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _table;
    private bool _disposed;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _table;
    }

    public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = GetAll();
        return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }

    public IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = GetAll();
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);

        return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
    }

    public async Task<PaginatedEntities<TEntity>> GetAllByPage(
        Expression<Func<TEntity, bool>>? predicate = null,
        int page = 1, int pageCount = 10,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var entities = GetAll(predicate, orderBy, includes);

        return new PaginatedEntities<TEntity>
        {
            Count = entities.Count(),
            Entities = await entities.Skip((page - 1) * pageCount).Take(pageCount).ToListAsync()
        };
    }

    public async Task<IEnumerable<TEntity>> GetList(bool hasTracking = false)
    {
        if (!hasTracking)
            return await _table.AsNoTracking().ToListAsync();

        return await _table.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetList(
        Expression<Func<TEntity, bool>>? predicate = null,
        Expression<Func<TEntity, int>>? orderBy = null,
        bool hasTracking = false)
    {
        IQueryable<TEntity> query = GetAll();
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = query.OrderBy(orderBy);

        if (!hasTracking)
            return await query.AsNoTracking().ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetById(TKey id, bool hasTracking = false)
    {
        if (!hasTracking)
        {
            var entity = _table.Find(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        return await _table.FindAsync(id);
    }

    public async Task<TKey> Create(TEntity entity)
    {
        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> Update(TEntity entity)
    {
        //await CheckCreated(entity);
        _table.Update(entity);
        int affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<bool> Delete(TEntity entity)
    {
        _table.Remove(entity);
        int affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<bool> DeleteById(TKey id)
    {
        var entity = await _table.FindAsync(id);
        if (entity != null) return await Delete(entity);
        return false;
    }

    public async Task<bool> SoftDelete(TEntity entity)
    {
        var entityItem = await GetById(entity.Id);
        if (entity is not BaseAuditableEntity) return false;
        if (entityItem is null) return false;

        (entityItem as BaseAuditableEntity).DeletedAt = DateTime.UtcNow;

        _table.Update(entity);
        int affected = await _context.SaveChangesAsync();
        return affected > 0;
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> query = GetAll();
        if (predicate != null) query = query.Where(predicate);

        return await query.CountAsync();
    }

    private async Task CheckCreated(TEntity entity)
    {
        var dbEntity = await _table.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(entity.Id));
        if (dbEntity != null && dbEntity.GetType().IsAssignableTo(typeof(IAuditableEntity)))
        {
            var dbEntityItem = dbEntity as IAuditableEntity;
            if (dbEntityItem != null)
            {
                if ((entity as IAuditableEntity).CreatedAt.Ticks == 0 && dbEntityItem.CreatedAt.Ticks > 0)
                    (entity as IAuditableEntity).CreatedAt = dbEntityItem.CreatedAt;

                if ((entity as IAuditableEntity).CreatedBy == null && dbEntityItem.CreatedBy != null)
                    (entity as IAuditableEntity).CreatedBy = dbEntityItem.CreatedBy;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}

public class BaseRepository<TEntity> : BaseRepository<TEntity, int>
     where TEntity : class, IEntity<int>
{
    public BaseRepository(ApplicationDbContext context) : base(context)
    {
    }
}