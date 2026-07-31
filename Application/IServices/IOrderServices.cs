using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IOrderServices
    {

        public Task<Order> CreateOrder(Guid productId, int quantity, Guid userId);
        public Task UpdateOrder(Guid orderId, Guid productId, int quantity);
        public Task<bool> DeleteOrder(Guid orderId);
        public Task<Order?> GetOrder(Guid orderId);
        public Task<IEnumerable<Order>> GetOrders();
    }
}
