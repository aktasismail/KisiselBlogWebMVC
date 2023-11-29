using System.Linq.Expressions;

namespace ismailaktasblog.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll(bool trackChanges);
        Task<T>? Find(Expression<Func<T, bool>> expression, bool trackChanges);
        Task Add(T entity);
        Task Delete(int id);
        void Update(T entity);
        void Save();
        Task SaveAsync();

    }
}
