using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data.Abstractions
{
    public interface IRepository<T> where T: class
    {
        Task<T> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, List<string> include = null);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
