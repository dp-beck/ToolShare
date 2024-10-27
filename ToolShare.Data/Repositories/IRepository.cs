using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
    }
}