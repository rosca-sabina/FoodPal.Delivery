using System.Threading.Tasks;

namespace FoodPal.Delivery.Data.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<bool> SaveChangesAsync();
    }
}
