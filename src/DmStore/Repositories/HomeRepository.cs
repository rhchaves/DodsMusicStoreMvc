using DmStore.Areas.Admin.Models;
using DmStore.Data;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Repositories
{
    public interface IHomeRepository
    {
        Task<List<Product>> ListProductAsync();
    }
    public class HomeRepository : IHomeRepository
    {
        private readonly DmStoreDbContext _context;
        public HomeRepository(DmStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> ListProductAsync()
        {
            return await _context.PRODUCTS.ToListAsync();
        }
    }
}
