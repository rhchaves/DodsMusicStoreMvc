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
    }
}
