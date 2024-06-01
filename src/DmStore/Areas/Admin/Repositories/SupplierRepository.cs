using DmStore.Areas.Admin.Models;
using DmStore.Data;
using DmStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<List<Supplier>> GetActiveSuppliersAsync();
    }

    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DmStoreDbContext context) : base(context) { }

        public async Task<List<Supplier>> GetActiveSuppliersAsync()
        {
            return await _context.SUPPLIERS.Where(s => s.STATUS == true).ToListAsync();
        }
    }
}
