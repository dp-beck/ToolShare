using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToolShare.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsyncWithIncludes(params Expression<Func<T, object>>[] includes);
        public Task<T?> GetByIdAsync(int id);
        public Task<T> GetByIdAsyncWithIncludes(int id,
            Expression<Func<T, bool>> filter, 
            params Expression<Func<T, object>>[] includes);
        public Task AddAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task SaveChangesAsync();
    }
}