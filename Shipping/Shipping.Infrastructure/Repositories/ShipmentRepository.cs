using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;

namespace Shipping.Infrastructure.Repositories
{
    public class ShipmentRepository : ICommonRepository<Shipment>
    {
        private readonly ShippingDbContext _context;

        public ShipmentRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipment>> Get()
        {
            var shipments = await _context.Shipments.ToListAsync();
            return shipments;
        }

        public async Task<Shipment> GetById(Guid id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            return shipment;
        }

        public async Task Add(Shipment entity)
        {
            await _context.Shipments.AddAsync(entity);
        }
        public async void Update(Shipment entity)
        {
            _context.Shipments.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async void Delete(Shipment entity)
        {
            _context.Shipments.Remove(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
