using EmployeeManagementSystem.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DataAccess.Concrete;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId);
        
        modelBuilder.Entity<EmployeePayment>()
            .HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeePayments)
            .HasForeignKey(ep => ep.EmployeeId);
    }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeePayment> EmployeePayments { get; set; }
}