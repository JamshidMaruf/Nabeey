using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.Contexts;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Commons;

namespace Nabeey.DataAccess.Repositories;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _table;

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public async ValueTask<TEntity> CreateAsync(TEntity entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Update(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        _context.Update(entity).State = EntityState.Deleted;
    }

    public void Destroy(TEntity entity)
    {
        _context.Remove(entity);
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
    {
        IQueryable<TEntity> entities = expression == null ? _table.AsQueryable() : _table.Where(expression).AsQueryable();
        
        if(includes!=null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return await entities.FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> entities = expression == null ? _table.AsQueryable() 
            : _table.Where(expression).AsQueryable();

        entities = isTracking ? entities.AsNoTracking() : entities;
        
        if(includes!=null)
            foreach (var include in includes)
                entities = entities.Include(include);

        return entities;
    }

    public async ValueTask<bool> SaveAsync()
    {
        var rowsAffetted = await _context.SaveChangesAsync();
        return rowsAffetted > 0;
    }
}