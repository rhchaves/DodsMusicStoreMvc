using DmStore.Data;
using DmStore.Models;
using Microsoft.AspNetCore.Identity;

namespace DmStore.Repositories
{
    public interface IClientRepository
    {
        Task<bool> ClientExistsAsync(string productId);
        Task<Client> GetClientByIdAsync(string id);
        Task<IdentityUser> GetIdentityClient(string clientId);
        Task<Client> CreatNewClientAsync(Client client);
        Client EditClient(Client client);
    }
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DmStoreDbContext context) : base(context) { }

        public async Task<bool> ClientExistsAsync(string clientId)
        {
            return await ItemExistsAsync(c => c.ID == clientId);
        }

        public async Task<Client> GetClientByIdAsync(string clientId)
        {
            return await _context.CLIENTS.FindAsync(clientId);
        }

        public async Task<IdentityUser> GetIdentityClient(string clientId)
        {
            //return await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _context.Users.FindAsync(clientId);
        }

        public async Task<Client> CreatNewClientAsync(Client client)
        {
            await AddItemAsync(client);
            return client;
        }

        public Client EditClient(Client client)
        {
            UpdateItem(client);
            return client;
        }
    }
}
