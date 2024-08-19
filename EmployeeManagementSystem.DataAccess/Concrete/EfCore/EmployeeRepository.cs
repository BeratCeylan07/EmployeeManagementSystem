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
    public Employee GetEmployeeWithDetails(int employeeId)
    {
        return _context.Employees
            .Include(e => e.Department)
            .Include(e => e.EmployeePayments)
            .FirstOrDefault(e => e.ID == employeeId);
    }
    public override int Update(Employee entity)
    {
        var existingEntity = _context.Set<Employee>().Find(entity.ID);
        if (existingEntity != null)
        {
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            existingEntity.ISMODIFIEDDATE = DateTime.Now;
            existingEntity.ISMODIFIEDUSERID = entity.ISMODIFIEDUSERID;
        }

        return _context.SaveChanges();
    }
    public bool HasEmployees(int employeeId)
    {
        return _context.EmployeePayments.Any(e => e.EmployeeId == employeeId);
    }
    public override List<Employee> GetAll()
    {
        return _context.Employees.Include(e => e.Department).ToList();
    }
    public Employee GetByEmailAndPassword(string email, string password)
    {
        return _context.Employees
            .FirstOrDefault(e => e.Email == email && e.Password == password);
    }
    
}