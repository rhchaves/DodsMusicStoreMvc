using DmStore.Data;
using DmStore.Models;

namespace DmStore.Services
{
    public class ClientService
    {
        private readonly DmStoreDbContext _context;
        public ClientService(DmStoreDbContext context)
        {
            _context = context;
        }

        public async Task<Client> GetClient(string id)
        {
            var client = await _context.CLIENTS.FindAsync(id);
            return client;
        }
    }
}
