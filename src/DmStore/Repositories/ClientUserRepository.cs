using DmStore.Data;
using DmStore.Models;
using Microsoft.AspNetCore.Identity;

namespace DmStore.Repositories
{
    public interface IClientUserRepository : IRepository<Client>
    {
        Task<IdentityUser> GetIdentityClient(string clientId);
    }
    public class ClientUserRepository : Repository<Client>, IClientUserRepository
    {
        public ClientUserRepository(DmStoreDbContext context) : base(context) { }

        public async Task<IdentityUser> GetIdentityClient(string clientId)
        {
            //return await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _context.Users.FindAsync(clientId);
        }
    }
}
