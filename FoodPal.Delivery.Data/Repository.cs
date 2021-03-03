using FoodPal.Delivery.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DeliveryDbContext _dbContext;
        private readonly DbSet<T> _set;

        public Repository(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression, List<string> include = null)
        {
            IQueryable<T> result = _set.Where(expression);
            if(include is not null)
            {
                foreach(string field in include)
                {
                    result.Include(field);
                }
            }

            return await result.ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public void Update(T entity)
        {
            _set.Update(entity);
        }
    }
}
