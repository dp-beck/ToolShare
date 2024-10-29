using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToolShare.Data.Extensions
{
    public static class IQueryableExtensions
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
    }
}