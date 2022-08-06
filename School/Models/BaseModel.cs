namespace School.Models;
using School.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BaseModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public DateTime LastModified { get; private set; } = DateTime.Now;
    public Status Status { get; set; }
}

