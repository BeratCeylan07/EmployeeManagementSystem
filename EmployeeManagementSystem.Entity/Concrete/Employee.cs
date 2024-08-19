using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.Entity.Concrete;

[Table("Employees")]
public class Employee : BaseEntity
{
    [Required]
    [Column("Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Column("Surname")]
    [MaxLength(100)]
    public string Surname { get; set; }
    
    [Required]
    [Column("Phone")]
    [MaxLength(100)]
    public string Phone { get; set; }
    
    [Required]
    [Column("Email")]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [Column("Salary")]
    [MaxLength(100)]
    public double Salary { get; set; }
    
    [Required]
    [Column("Password")]
    [MaxLength(10)]
    public string Password { get; set; }
    
    [Required]
    [Column("DepartmentId")]
    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
    
    public virtual ICollection<EmployeePayment> EmployeePayments { get; set; }

}