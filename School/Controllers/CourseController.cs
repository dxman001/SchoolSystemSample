namespace School.Controllers;
using Microsoft.AspNetCore.Mvc;
using School.Dtos;
using School.Repositories;

[ApiController]
[Route("[Controller]")]
public class CourseController : ControllerBase
{
    protected readonly IStudentRepository _studentRepository;
    protected readonly ICourseRepository _courseRepository;
    public CourseController(IStudentRepository studentRepository, ICourseRepository courseRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }
    [HttpGet]
    public ActionResult<List<CourseDto>> GetAll() =>
          Ok(_courseRepository.GetAll().Select(_ => _.MapToDto()));

    [HttpGet("Id")]
    public ActionResult<CourseDto> GetById(int id) =>
        Ok(_courseRepository.Find(e => e.Id == id).FirstOrDefault().MapToDto());

    [HttpGet("GetStudents")]
    public ActionResult<CourseDto> GetStudents(int id) =>
         Ok(_courseRepository.Find(e => e.Id == id).FirstOrDefault().MapToDto().Students);

    [HttpPost("Create")]
    public ActionResult<int> Create([FromBody] CourseDto courseDto)
    {
        var result = _courseRepository.Add(courseDto.MapToEntity());
        _courseRepository.Persist();
        return Ok(result.Id);
    }

    [HttpPost("AddStudents")]
    public ActionResult<bool> AddStudents([FromBody] CourseStudentsDto courseStudentsDto)
    {

        _courseRepository.GetById(courseStudentsDto.Id).Result.Students 
            = courseStudentsDto.StudentIds.Select(e => _studentRepository.GetById(e).Result).ToList();
        _courseRepository.Persist();
        return Ok(true);
    }
}
