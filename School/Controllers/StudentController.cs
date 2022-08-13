namespace School.Controllers;
using Microsoft.AspNetCore.Mvc;
using School.Dtos;
using School.Services;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    
    protected readonly IStudentService _studentService;
    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentDto>>> GetAll()
    {
        var students = await _studentService.GetAll();
        return Ok(students.Select(_ => _.MapToDto()));
    }

    [HttpGet("Id")]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _studentService.GetById(id);
        return Ok(student.MapToDto());
    }

    [HttpGet("GetCourses")]
    public async Task<ActionResult<StudentDto>> GetCourses(int id)
    {
        var student = await _studentService.GetById(id);
        return Ok(student.MapToDto().Courses);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create([FromBody] StudentDto studentDto) =>
        Ok(await _studentService.Add(studentDto.MapToEntity()));               

    [HttpPost("AddCourses")]
    public async Task<ActionResult<bool>> AddCourses([FromBody] StudentCoursesDto studentCoursesDto) =>
         Ok(await _studentService.AddCourses(studentCoursesDto.Id, studentCoursesDto.CourseIds));
   

    [HttpPost("Update")]
    public async Task<ActionResult<bool>> Update([FromBody] StudentDto studentDto) => 
        Ok(_studentService.Update(
            studentDto.MapToEntity(await _studentService.GetById(studentDto.Id))
            ));
    

    [HttpPost("Delete")]
    public async Task<ActionResult<bool>> Delete([FromBody] StudentDto studentDto) =>
        Ok(await _studentService.Delete(studentDto.Id));
    
}

