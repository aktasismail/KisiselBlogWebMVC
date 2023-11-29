using ismailaktasblog.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ismailaktasblog.DataAccess.Concrete
{
    public class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        protected readonly ApplicationDbContext _context;
        protected RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }
        public async Task<T>? Find(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return  trackChanges
                ? _context.Set<T>().Where(expression).SingleOrDefault()
                : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task Delete(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(data);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}
