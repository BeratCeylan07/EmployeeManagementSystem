using System.Linq.Expressions;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.DataAccess.Abstract;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    int Add(TEntity entity);
    int Update(TEntity entity);
    int Delete(TEntity entity);
    List<TEntity> GetAll();
    List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    TEntity? GetById(int id);
    TEntity? Get(Expression<Func<TEntity, bool>> filter);
}