using DmStore.Areas.Admin.Models;
using DmStore.Data;
using DmStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Repositories
{
    public interface ISupplierRepository
    {
        Task<bool> SupplierExistsAsync(string supplierId);
        Task<List<Supplier>> GetListSupplierAsync();
        Task<Supplier> GetSupplierByIdAsync(string supplierId);
        Task<List<Supplier>> GetActiveSuppliersAsync();
        Task<Supplier> CreatNewSupplierAsync(Supplier supplier);
        Supplier EditSupplier(Supplier supplier);
        Supplier DeleteSupplier(Supplier supplier);
    }

    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DmStoreDbContext context) : base(context) { }

        public async Task<bool> SupplierExistsAsync(string supplierId)
        {
            return await ItemExistsAsync(p => p.ID == supplierId);
        }

        public async Task<List<Supplier>> GetListSupplierAsync()
        {
            return await GetListItemAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            return await GetItemByIdAsync(p => p.ID == supplierId);
        }

        public async Task<List<Supplier>> GetActiveSuppliersAsync()
        {
            return await _context.SUPPLIERS.Where(s => s.STATUS == true).ToListAsync();
        }
        public async Task<Supplier> CreatNewSupplierAsync(Supplier supplier)
        {
            await AddItemAsync(supplier);
            return supplier;
        }

        public Supplier EditSupplier(Supplier supplier)
        {
            UpdateItem(supplier);
            return supplier;
        }

        public Supplier DeleteSupplier(Supplier supplier)
        {
            DeleteItem(supplier);
            return supplier;
        }
    }
}
