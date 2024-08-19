using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.Business.Abstract;

public interface IBaseService<TEntity> where TEntity : BaseEntity
{
    bool Add(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    List<TEntity> GetAll();
}