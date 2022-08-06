namespace School.Dtos;
using School.Enums;
using School.Models;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string? MiddleName { get; set; } = String.Empty;
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public List<Course?>? Courses { get; internal set; } = new List<Course?>();
    public int GetAge() =>
        DateTime.Now.Year - this.BirthDate.Year;
    public string GetFullName() =>
        $"{this.FirstName} {this.MiddleName} {this.LastName}";
    
}
public class StudentCoursesDto
{
    public int Id { get; set; }
    public List<int> CourseIds { get; set; }=new List<int>();

}

public static class StudentMapper
{
    public static StudentDto? MapToDto(this Student? student) =>
        student == null
        ? null
        : new StudentDto()
        {
            Id = student.Id,
           FirstName = student.FirstName,
           MiddleName = student.MiddleName,
           LastName = student.LastName,
           BirthDate = student.BirthDate,
           Courses = student.Courses?.ToList(),
           Gender = student.Gender
        };

    public static Student MapToEntity(this StudentDto studentDto,Student? entity = null)
    {
        Student student = entity == null ? new Student() : entity;
        student.FirstName = GetProperty(studentDto.FirstName, student.FirstName);
        student.MiddleName = GetProperty(studentDto?.MiddleName, student.MiddleName);
        student.LastName = GetProperty(studentDto?.LastName, student.LastName);
        student.BirthDate = studentDto?.BirthDate ?? student.BirthDate;
        student.Gender = studentDto?.Gender ?? student.Gender;
        return student;
    }

    private static string GetProperty(string value1, string value2) =>
        string.IsNullOrWhiteSpace(value1) ? value2 : value1;
    
}
