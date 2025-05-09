﻿using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Contexts;
using Portfolio.DataAccess.IRepositories;
using Portfolio.Domain.Commons;
using System.Linq.Expressions;

namespace Portfolio.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext appDbContext)
    {
        this.dbContext = appDbContext;
        this.dbSet = this.dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
        => await this.dbSet.AddAsync(entity);

    public void Update(TEntity entity)
    {
        entity.UpdatedtAt = DateTime.UtcNow;
        this.dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
        => entity.IsDeleted = true;

    public void Destroy(TEntity entity)
        => this.dbContext.Entry(entity).State = EntityState.Deleted;

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, string[]? includes = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity?> GetAsync(long id, string[]? includes = null)
        => await this.GetAsync(e => e.Id.Equals(id), includes);

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, bool isNoTracked = true, string[]? includes = null)
    {
        IQueryable<TEntity> query = dbSet;

        if (expression is not null)
            query = query.Where(expression);

        if (isNoTracked)
            query = query.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    public async Task SaveAsync()
        => await this.dbContext.SaveChangesAsync();
}
