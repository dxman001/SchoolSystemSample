using Microsoft.EntityFrameworkCore;
using School.Models;
using School.Persistance;

namespace School.Services;

public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork=unitOfWork;
    }
    public async Task<List<Course>> GetAll() =>    
        await _unitOfWork.Courses.GetAll().ToListAsync();
    

    public async Task<Course?> GetById(int id) =>
        await _unitOfWork.Courses.Find(e=>e.Id==id).FirstOrDefaultAsync();
}
