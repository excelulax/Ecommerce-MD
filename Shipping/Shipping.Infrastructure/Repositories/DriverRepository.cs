using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;

namespace Shipping.Infrastructure.Repositories
{
    public class DriverRepository : ICommonRepository<Driver>
    {
        private readonly ShippingDbContext _context;

        public DriverRepository(ShippingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Driver>> Get()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return drivers;
        }
        public async Task<Driver> GetById(Guid id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            return driver;
        }
        public async Task Add(Driver entity)
        {
            await _context.Drivers.AddAsync(entity);
        }
        public void Update(Driver entity)
        {
            _context.Drivers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(Driver entity)
        {
            _context.Drivers.Remove(entity);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
