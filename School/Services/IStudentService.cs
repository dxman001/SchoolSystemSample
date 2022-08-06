namespace School.Services; 
using School.Models;

public interface IStudentService
{
    Task<List<Student>> GetAll();

    Task<Student?> GetById(int id);

    Task<int> Add(Student student);

    Task<bool> AddCourses(int studentId, List<int> courseIds);

    bool Update(Student student);

    Task<bool> Delete(int id);
}
