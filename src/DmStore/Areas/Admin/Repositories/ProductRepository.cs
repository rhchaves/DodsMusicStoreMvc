using DmStore.Areas.Admin.Models;
using DmStore.Data;
using DmStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Supplier>> GetActiveSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(string supplierId);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DmStoreDbContext context) : base(context) { }


        public async Task<List<Supplier>> GetActiveSuppliersAsync()
        {
            return await _context.SUPPLIERS.Where(s => s.STATUS == true).ToListAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            return await _context.SUPPLIERS.FindAsync(supplierId);
        }
    }
}
