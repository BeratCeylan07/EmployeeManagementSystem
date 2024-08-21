using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataAccess.Concrete.EfCore;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    private readonly EmployeeDbContext _context;

    public EmployeeRepository(EmployeeDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Employee> GetEmployeeWithDetailsAsync(int employeeId)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.EmployeePayments)
            .FirstOrDefaultAsync(e => e.ID == employeeId);
    }

    public async  Task<int> UpdateAsync(Employee entity)
    {
        var existingEntity = await _context.Set<Employee>().FindAsync(entity.ID);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            existingEntity.ISMODIFIEDDATE = DateTime.Now;
            existingEntity.ISMODIFIEDUSERID = entity.ISMODIFIEDUSERID;
        }

        return await _context.SaveChangesAsync();
    }

    public async Task<bool> HasEmployeesAsync(int employeeId)
    {
        return await _context.EmployeePayments.AnyAsync(e => e.EmployeeId == employeeId);
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();
    }
    

    public async Task<Employee> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
    }
}