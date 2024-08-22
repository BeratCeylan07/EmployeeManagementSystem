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
    public async Task<Department> GetDepartmentWithEmployees(int departmentId)
    {
        return await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.ID == departmentId);
    }
    public async Task<List<Department>> GetAllWithEmployeeCount()
    {
        return await _context.Departments
            .Select(d => new Department
            {
                ID = d.ID,
                Name = d.Name,
                Employees = d.Employees.ToList()
            })
            .ToListAsync();
    }

    public async Task<List<Department>> GetAllWithSalaryTotal()
    {
        return await _context.Departments
            .Select(d => new Department
            {
                ID = d.ID,
                Name = d.Name,
                Employees = d.Employees.ToList()
            })
            .ToListAsync();
        
    }

    public override async Task<int> Update(Department entity)
    {
        var existingEntity = _context.Set<Department>().Find(entity.ID);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            // IsModifiedDate ve IsModifiedUserId alanlarının değiştiğinden emin olalım
            existingEntity.ISMODIFIEDDATE = DateTime.Now;
            existingEntity.ISMODIFIEDUSERID = entity.ISMODIFIEDUSERID;
        }

        return await _context.SaveChangesAsync();
    }
    public async Task<bool> HasEmployees(int departmentId)
    {
        return await _context.Employees.AnyAsync(e => e.DepartmentId == departmentId);
    }
    public async Task<IEnumerable<Department>> GetallAsync()
    {
        return await _context.Departments.ToListAsync();
    }
}