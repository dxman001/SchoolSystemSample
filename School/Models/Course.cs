namespace School.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Course:BaseModel
{
    [Required]
    public string? Code { get; set; }
    [Required]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public IEnumerable<Student>? Students { get; set; }
}

