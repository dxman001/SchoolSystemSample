namespace School.Repositories;
using System.Linq.Expressions;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetById(int id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    Task<T> Add(T entity);
    void AddRange(IEnumerable<T> entities);
    T Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    int Persist();
}

