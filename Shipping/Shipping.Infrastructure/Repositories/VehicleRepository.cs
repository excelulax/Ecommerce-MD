using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;

namespace Shipping.Infrastructure.Repositories
{
    public class VehicleRepository : ICommonRepository<Vehicle>
    {
        private readonly ShippingDbContext _context;

        public VehicleRepository(ShippingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Vehicle>> Get()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return vehicles;
        }
        public async Task<Vehicle> GetById(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            return vehicle;
        }
        public async Task Add(Vehicle entity)
        {
            await _context.Vehicles.AddAsync(entity);
        }
        public void Update(Vehicle entity)
        {
            _context.Vehicles.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(Vehicle entity)
        {
            _context.Vehicles.Remove(entity);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
