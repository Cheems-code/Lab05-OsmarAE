﻿using System.Linq.Expressions;
using Lab05_OsmarAE.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab05_OsmarAE.Repositorios.Implementaciones;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly Lab5Context _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(Lab5Context context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
    
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}