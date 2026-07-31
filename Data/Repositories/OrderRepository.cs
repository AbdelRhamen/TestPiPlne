using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManager.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders.OrderByDescending(t => t.CreatedOn).ToListAsync();

        public async Task<Order?> GetByIdAsync(Guid id)
            => await _context.Orders.FindAsync(id);

        public async Task<Order> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateAsync(Guid id, Order task)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) return null;

            existing.ProductId = task.ProductId;
            existing.Quantity = task.Quantity;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Orders.FindAsync(id);
            if (task == null) return false;

            _context.Orders.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
