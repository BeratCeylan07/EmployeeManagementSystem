using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entity.Core;

public abstract class BaseEntity
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; } 
    
    public int ISCREATEDUSERID { get; set; }
    
    public int? ISMODIFIEDUSERID { get; set; }
    
    public DateTime ISCREATEDDATE { get; set; }
    
    public DateTime? ISMODIFIEDDATE { get; set; }
    
    public bool isActive { get; set; }
}