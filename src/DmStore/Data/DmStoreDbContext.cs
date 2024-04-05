using DmStore.Areas.Admin.Models;
using DmStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Data
{
    public class DmStoreDbContext : IdentityDbContext
    {
        public DmStoreDbContext(DbContextOptions<DmStoreDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Client { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
