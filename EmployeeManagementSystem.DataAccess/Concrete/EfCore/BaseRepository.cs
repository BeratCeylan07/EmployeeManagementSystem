using System.Linq.Expressions;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;
using EmployeeManagementSystem.Entity.Core;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataAccess.Concrete.EfCore;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity // Her class gönderilemesin diye BaseEntity den miras alan classlar gelebilir şeklinde bir işaretleme yaptım.
{
    private readonly EmployeeDbContext _context;

    public BaseRepository(EmployeeDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity); 
        return await _context.SaveChangesAsync();
    }
    public virtual async Task<int> Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity); 
        return await _context.SaveChangesAsync();
    }
    public async Task<int> Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity); 
        return await _context.SaveChangesAsync();
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
    }
}