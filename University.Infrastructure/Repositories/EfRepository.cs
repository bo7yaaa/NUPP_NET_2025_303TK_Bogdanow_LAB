using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Repositories;

public class EfRepository<T>(UniversityContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _set = context.Set<T>();

    public async Task AddAsync(T entity) => await _set.AddAsync(entity);
    public async Task DeleteAsync(T entity) => _set.Remove(entity);
    public async Task<List<T>> GetAllAsync() => await _set.ToListAsync();
    public async Task<T?> GetByIdAsync(Guid id) => await _set.FindAsync(id);
    public async Task UpdateAsync(T entity) => _set.Update(entity);
    public async Task SaveAsync() => await context.SaveChangesAsync();
}
