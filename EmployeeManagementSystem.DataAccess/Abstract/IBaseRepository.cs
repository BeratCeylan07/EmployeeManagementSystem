using System.Linq.Expressions;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> Add(TEntity entity);
    Task<int> Update(TEntity entity);
    Task<int> Delete(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetById(int id);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter);

}