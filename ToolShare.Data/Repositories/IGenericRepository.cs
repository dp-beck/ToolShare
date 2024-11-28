using System.Linq.Expressions;

namespace ToolShare.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object>>[] includes);
        public Task<T?> FindById(int id);
        public Task<T?> FindByIdWithIncludes(int id,
            Expression<Func<T?, bool>> filter, 
            params Expression<Func<T, object>>[] includes);
        public Task Add(T entity);
        public Task Delete(T entity);
        public Task SaveChanges();
    }
}