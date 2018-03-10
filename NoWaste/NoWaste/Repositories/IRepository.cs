using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NoWaste.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> SearchFor(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>> Add(T entity);
        Task Delete(T entity);
    }
}
