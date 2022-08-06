namespace School.Repositories;

using Microsoft.EntityFrameworkCore;
using School.Models;
using School.Persistance;
using System.Linq.Expressions;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(SchoolDbContext context) : base(context)
    {
       
    }

    public override IQueryable<Student> GetAll() =>
       base.GetAll().Include(c=>c.Courses);
    public override IQueryable<Student> Find(Expression<Func<Student, bool>> expression) =>
        base.Find(expression).Include(c => c.Courses);
}

