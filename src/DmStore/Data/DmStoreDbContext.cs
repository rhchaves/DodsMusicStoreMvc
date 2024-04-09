using DmStore.Areas.Admin.Models;
using DmStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Data
{
    public class DmStoreDbContext : IdentityDbContext
    {
        public DmStoreDbContext(DbContextOptions<DmStoreDbContext> options) : base(options)
        { }
        public DbSet<Client> CLIENTS { get; set; }
        public DbSet<Supplier> SUPPLIERS { get; set; }
        public DbSet<Product> PRODUCTS { get; set; }
    }
}
