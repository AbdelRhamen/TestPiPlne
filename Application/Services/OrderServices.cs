using Application.IServices;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderServices : IOrderServices

    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrder(Guid productId, int quantity, Guid userId)
        {
          
            var order=  await _unitOfWork.Orders.CreateAsync(new Order()
          {
                ProductId = productId,
                Quantity = quantity,
                CreatedBy = userId
          });  

            return order;
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            
           return await _unitOfWork.Orders.DeleteAsync(orderId);

        }

        public Task<Order?> GetOrder(Guid orderId)
        {
          return _unitOfWork.Orders.GetByIdAsync(orderId);
        }

        public Task<IEnumerable<Order>> GetOrders()
        {
            return _unitOfWork.Orders.GetAllAsync();
        }

        public async Task UpdateOrder(Guid orderId, Guid productId, int quantity)
        {
           await _unitOfWork.Orders.UpdateAsync(orderId, new Order()
            {
                ProductId = productId,
                Quantity = quantity
            });
        }
    }
}
