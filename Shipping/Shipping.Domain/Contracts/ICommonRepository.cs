using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Domain.Contracts
{
    public interface ICommonRepository<Tentity>
    {
        Task<IEnumerable<Tentity>> Get();
        Task<Tentity> GetById(Guid id);
        Task Add(Tentity entity);
        void Update(Tentity entity);
        void Delete(Tentity entity);
        Task Save();
    }
}
