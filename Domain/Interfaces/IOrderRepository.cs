
using Core.Models;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(Guid id);
        Task<Order> CreateAsync(Order task);
        Task<Order?> UpdateAsync(Guid id, Order task);
        Task<bool> DeleteAsync(Guid id);
    }
}
