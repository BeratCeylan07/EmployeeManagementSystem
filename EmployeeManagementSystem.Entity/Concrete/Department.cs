using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.Entity.Concrete;

[Table("Departments")]
public class Department : BaseEntity
{
    [Required]
    [Column("Name")]
    [MaxLength(100)]
    public string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }

    public Department()
    {
        Employees = new List<Employee>();
    }
}