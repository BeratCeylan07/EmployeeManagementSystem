using System.Linq.Expressions;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.DataAccess.Concrete.EfCore;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity // Her class gönderilemesin diye BaseEntity den miras alan classlar gelebilir şeklinde bir işaretleme yaptım.
{
    private readonly EmployeeDbContext _context;

    public BaseRepository(EmployeeDbContext context)
    {
        _context = context;
    }
    public int Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity); 
        return _context.SaveChanges();
    }
    public virtual int Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity); 
        return _context.SaveChanges();
    }
    public int Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity); 
        return _context.SaveChanges();
    }
    public virtual List<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
    {
        return _context.Set<TEntity>().Where(filter).ToList();
    }

    public TEntity? GetById(int id)
    {
        return _context.Set<TEntity>().Find(id);
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> filter)
    {
        return _context.Set<TEntity>().FirstOrDefault(filter);
    }
}