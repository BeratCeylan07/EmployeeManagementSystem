using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Entity.Core;

namespace EmployeeManagementSystem.Entity.Concrete;

[Table("EmployeePayments")]
public class EmployeePayment : BaseEntity
{
    [Required]
    [Column("Amount")]
    [MaxLength(100)]
    public double Amount { get; set; }
    
    [Required]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }
    
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }
    
    
}