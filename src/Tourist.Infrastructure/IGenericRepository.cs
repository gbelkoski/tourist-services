namespace Tourist.Infrastructure;
public interface IGenericRepository<T> where T : class
{
    Task<T> Insert(T model);
    Task<T> Update(T model);
    Task<T> SelectById(int id);
    Task<List<T>> SelectAll();
}
