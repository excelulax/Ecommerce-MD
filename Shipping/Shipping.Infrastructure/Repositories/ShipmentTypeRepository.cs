using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;

namespace Shipping.Infrastructure.Repositories
{
    public class ShipmentTypeRepository : ICommonRepository<ShipmentType>
    {
        private readonly ShippingDbContext _context;

        public ShipmentTypeRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShipmentType>> Get()
        {
            var shipmentTypes = await _context.ShipmentTypes.ToListAsync();
            return shipmentTypes;
        }

        public async Task<ShipmentType> GetById(Guid id)
        {
            var shipmentType = await _context.ShipmentTypes.FindAsync(id);
            return shipmentType;
        }
        public async Task Add(ShipmentType entity)
        {
            await _context.ShipmentTypes.AddAsync(entity);
        }

        public async void Update(ShipmentType entity)
        {
            _context.ShipmentTypes.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async void Delete(ShipmentType entity)
        {
            _context.ShipmentTypes.Remove(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
