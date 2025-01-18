using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;

namespace Shipping.Infrastructure.Repositories
{
    public class OrderRepository : ICommonRepository<Order>
    {
        private readonly ShippingDbContext _context;

        public OrderRepository(ShippingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> Get()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }
        public async Task<Order> GetById(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }
        public async Task Add(Order entity)
        {
            await _context.Orders.AddAsync(entity);
        }
        public async void Update(Order entity)
        {
            _context.Orders.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public async void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
