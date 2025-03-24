using System;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<T>(DbContext context)
    : IBaseRepository<T>
    where T : Entity
{
    public virtual async Task CreateAsync(T entity, CancellationToken cancellationToken)
        => await context.AddAsync(entity, cancellationToken);

    public virtual async Task<T> CreateReturnEntity(T entity, CancellationToken cancellationToken)
    {
        var entry = await context.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public virtual void Update(T entity)
        => context.Update(entity);

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        => await Task.Run(() => context.Remove(entity), cancellationToken);

    public virtual async Task<List<T>> GetAll(CancellationToken cancellationToken, int skip = 0, int take = 10)
        => await context.Set<T>().AsNoTracking()
                             .Skip(skip)
                             .Take(take)
                             .ToListAsync(cancellationToken)
            ?? throw new Exception("No entities found");

    public virtual async Task<T?> GetWithParametersAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        var query = context.Set<T>().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<List<T>> GetAllWithParametersAsync(
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        int skip = 0,
        int take = 10,
        params Expression<Func<T, object>>[] includes)
    {
        var query = context.Set<T>().AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.AsNoTracking()
                          .Skip(skip)
                          .Take(take)
                          .ToListAsync(cancellationToken);
    }

    public async Task<List<TResult>> GetAllProjectedAsync<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>> selector = null!,
        CancellationToken cancellationToken = default,
        int skip = 0,
        int take = 10,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (selector != null)
        {
            return await query.Select(selector)
                                .Skip(skip)
                                .Take(take)
                                .ToListAsync(cancellationToken);
        }
        else
        {
            throw new ArgumentNullException(nameof(selector), "Selector must be provided for projection.");
        }
    }

    public async Task<TResult> GetProjectedAsync<TResult>(
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, TResult>> selector = null!,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (selector != null)
        {
            return await query.Select(selector)
                                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            throw new ArgumentNullException(nameof(selector), "Selector must be provided for projection.");
        }
    }
}