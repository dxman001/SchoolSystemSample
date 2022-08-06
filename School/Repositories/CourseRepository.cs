namespace School.Repositories;

using Microsoft.EntityFrameworkCore;
using School.Models;
using School.Persistance;
using System.Linq.Expressions;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(SchoolDbContext context) : base(context)
    {
    }

    public override IQueryable<Course> GetAll() =>
       base.GetAll().Include(c => c.Students);
    public override IQueryable<Course> Find(Expression<Func<Course, bool>> expression) =>
        base.Find(expression).Include(c => c.Students);
}

