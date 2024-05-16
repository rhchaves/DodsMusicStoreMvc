using DmStore.Areas.Admin.Models;
using DmStore.Data;
using DmStore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Repositories
{
    public interface IProductRepository
    {
        Task<bool> ProductExistsAsync(string productId);
        Task<List<Product>> GetListProductAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<List<Supplier>> GetActiveSuppliersAsync();
        Task<Supplier> GetSupplierByIdAsync(string supplierId);
        Task<Product> CreatNewProductAsync(Product product);
        Product EditProduct(Product product);
        Product DeleteProduct(Product product);
    }
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DmStoreDbContext context) : base(context) { }

        public async Task<bool> ProductExistsAsync(string productId)
        {
            return await ItemExistsAsync(p => p.ID == productId);
        }

        public async Task<List<Product>> GetListProductAsync()
        {
            return await GetListItemAsync();
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            var product = GetItemByIdAsync(p => p.ID == productId).Result;
            product.SUPPLIER = await GetSupplierByIdAsync(product.SUPPLIER_ID);

            return product;
        }

        public async Task<List<Supplier>> GetActiveSuppliersAsync()
        {
            return await _context.SUPPLIERS.Where(s => s.STATUS == true).ToListAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(string supplierId)
        {
            return await _context.SUPPLIERS.FindAsync(supplierId);
        }

        public async Task<Product> CreatNewProductAsync(Product product)
        {
            await AddItemAsync(product);
            return product;
        }

        public Product EditProduct(Product product)
        {
            UpdateItem(product);
            return product;
        }

        public Product DeleteProduct(Product product)
        {
            DeleteItem(product);
            return product;
        }
    }
}
