using System.Linq.Expressions;
using EmployeeManagementSystem.DataAccess.Abstract;
using EmployeeManagementSystem.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataAccess.Concrete.EfCore;

public class EmplooyePaymentRepository : BaseRepository<EmployeePayment>, IEmployeePaymentRepository
{
    private readonly EmployeeDbContext _context;

    public EmplooyePaymentRepository(EmployeeDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<EmployeePayment>> GetAllPaymentsAsync()
    {
        return await _context.EmployeePayments
            .Include(e => e.Employee)
            .Include(e => e.Employee.Department)
            .ToListAsync();
    }

    public async Task<bool> HasEmployeesAsync(int emplooyePaymentId)
    {
        return await _context.EmployeePayments.AnyAsync(e => e.EmployeeId == emplooyePaymentId);
    }
}