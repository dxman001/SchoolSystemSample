namespace School.Persistance;
using School.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IStudentRepository Students { get; }
    public ICourseRepository Courses { get; }
    int Persist();
}

