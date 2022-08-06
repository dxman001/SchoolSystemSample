namespace School.Repositories;
using School.Persistance;
using System.Linq.Expressions;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly SchoolDbContext _context;
   
    public BaseRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        var result = await _context.Set<T>().AddAsync(entity);
        return result.Entity;
    }
        
  
    public void AddRange(IEnumerable<T> entities) =>
        _context.Set<T>().AddRange(entities);

    public T Update(T entity) =>
        _context.Set<T>().Update(entity).Entity;

    public void UpdateRange(IEnumerable<T> entities) =>
        _context.Set<T>().UpdateRange(entities);

    public virtual IQueryable<T> Find(Expression<Func<T, bool>> expression) =>
        _context.Set<T>().Where(expression);

    public virtual IQueryable<T> GetAll() =>
        _context.Set<T>();

    public virtual async Task<T?> GetById(int id) =>
        await _context.Set<T>().FindAsync(id);
   
    public void Remove(T entity) =>
         _context.Set<T>().Remove(entity);
   
    public void RemoveRange(IEnumerable<T> entities) =>
        _context.Set<T>().RemoveRange(entities);

    public int Persist() =>
        _context.SaveChanges();

}

