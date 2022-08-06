namespace School.Models;
using School.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Student : BaseModel
{
    [Required]
    public string FirstName { get; set; }=String.Empty;
   
    [Required]
    public string? MiddleName { get; set; } 

    public string? LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [JsonIgnore]
    public IEnumerable<Course?>? Courses { get; set; } 

    
}

