using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataAccess.Concrete.EfCore;

public class DepartmentRepository : BaseRepository<Department>,IDepartmentRepository
{
    private readonly EmployeeDbContext _context;

    public DepartmentRepository(EmployeeDbContext context) : base(context)
    {
        _context = context;
    }
    public Department GetDepartmentWithEmployees(int departmentId)
    {
        return _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefault(d => d.ID == departmentId);
    }
    public List<Department> GetAllWithEmployeeCount()
    {
        return _context.Departments
            .Select(d => new Department
            {
                ID = d.ID,
                Name = d.Name,
                Employees = d.Employees.ToList() // Bu, Entity Framework'ün çalışan sayısını hesaplamasını sağlar
            })
            .ToList();
    }
    public override int Update(Department entity)
    {
        var existingEntity = _context.Set<Department>().Find(entity.ID);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            // IsModifiedDate ve IsModifiedUserId alanlarının değiştiğinden emin olalım
            existingEntity.ISMODIFIEDDATE = DateTime.Now;
            existingEntity.ISMODIFIEDUSERID = entity.ISMODIFIEDUSERID;
        }

        return _context.SaveChanges();
    }
    public bool HasEmployees(int departmentId)
    {
        return _context.Employees.Any(e => e.DepartmentId == departmentId);
    }
    public override List<Department> GetAll()
    {
        return _context.Departments.ToList();
    }
}