using School.Persistance;

namespace School.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolDbContext _context;
    public UnitOfWork(SchoolDbContext context)
    {
        _context = context;
    }

    public IStudentRepository Students => 
        new StudentRepository(_context);

    public ICourseRepository Courses => 
        new CourseRepository(_context);

    public int Persist() => 
         _context.SaveChanges();
    
    public void Dispose() =>
        _context.Dispose();
}

