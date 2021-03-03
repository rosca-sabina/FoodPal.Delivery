using FoodPal.Delivery.Data.Abstractions;
using System.Threading.Tasks;

namespace FoodPal.Delivery.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeliveryDbContext _dbContext;
        public UnitOfWork(DeliveryDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
