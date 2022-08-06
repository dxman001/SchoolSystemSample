namespace School.Services; 
using School.Models;

public interface ICourseService
{
    Task<List<Course>> GetAll();
    Task<Course?> GetById(int id);
}
