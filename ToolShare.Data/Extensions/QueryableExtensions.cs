using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ToolShare.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeProperties<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) 
            where T : class
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public static IQueryable<T> WhereFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter) 
            where T : class?
        {
            query = query.Where(filter);
            return query;
        }     
    }
}