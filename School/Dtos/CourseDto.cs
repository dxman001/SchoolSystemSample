namespace School.Dtos;
using School.Models;

public class CourseDto
{
    public int Id { get; internal set; }
    public string? Code { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }  
    public List<Student>? Students { get; internal set; }
}
public class CourseStudentsDto
{
    public int Id { get; set; }
    public List<int> StudentIds { get; set; } = new List<int>();

}
public static class CourseMapper
{
    public static CourseDto? MapToDto(this Course? course) =>
        course == null
        ? null
        : new CourseDto()
        {
            Id= course.Id,
            Code = course.Code,
            Title = course.Title,
            Description = course.Description,
            Students = course?.Students?.ToList()
        };

    public static Course MapToEntity(this CourseDto courseDto) =>
        new Course()
        {
            Code = courseDto.Code,
            Title = courseDto.Title,
            Description = courseDto.Description,
            Students = courseDto?.Students
        };
}
