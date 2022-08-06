namespace School.Services;

using Microsoft.EntityFrameworkCore;
using School.Models;
using School.Persistance;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }

    public async Task<int> Add(Student student)
    {
        var result = await _unitOfWork.Students.Add(student);
        _unitOfWork.Persist();
        return result.Id;
    }

    public async Task<bool> AddCourses(int studentId, List<int> courseIds)
    {
        var student = await _unitOfWork.Students.GetById(studentId);
        student.Courses = courseIds.Select(e => _unitOfWork.Courses.GetById(e).Result).ToList();
        _unitOfWork.Persist();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        _unitOfWork.Students.Remove(await _unitOfWork.Students.GetById(id));
        _unitOfWork.Persist();
        return true;
    }

    public async Task<List<Student>> GetAll() =>
        await _unitOfWork.Students.GetAll().ToListAsync();
    
    public async Task<Student?> GetById(int id) => 
        await _unitOfWork.Students.Find(e=>e.Id==id).FirstOrDefaultAsync();

    public bool Update(Student student)
    {
        _unitOfWork.Students.Update(student);
        _unitOfWork.Persist();
        return true;
    }
        
    
}
