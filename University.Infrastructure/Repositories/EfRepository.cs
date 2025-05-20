using Microsoft.EntityFrameworkCore;

namespace University.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly UniversityContext _context;
    private readonly DbSet<T> _set;

    public EfRepository(UniversityContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _set.AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _set.FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
