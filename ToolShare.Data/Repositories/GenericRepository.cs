using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Extensions;

namespace ToolShare.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        
        public async Task<IEnumerable<T>> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            return await _dbSet.IncludeProperties(includes).ToListAsync();
        }

        public async Task<T?> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> FindByIdWithIncludes(int id, 
         Expression<Func<T?, bool>> filter,
         params Expression<Func<T, object>>[] includes)
        {
            return await _dbSet.IncludeProperties(includes)
                .WhereFilter(filter)
                .FirstOrDefaultAsync();
        }

        public async Task Add(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}