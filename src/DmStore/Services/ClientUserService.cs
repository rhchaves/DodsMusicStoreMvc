using DmStore.Models;
using DmStore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Services
{
    public interface IClientUserService
    {
        Task<bool> ClientExistsAsync(string clientId);
        Task<Client> GetClientByIdAsync(string clientId);
        Task<Client> CreatNewClientAsync(Client client);
        Task<Client> EditClientAsync(Client client);
    }
    public class ClientUserService : IClientUserService
    {
        private readonly IClientUserRepository _clientUserRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientUserService(IClientUserRepository clientUserRepository, UserManager<IdentityUser> userManager)
        {
            _clientUserRepository = clientUserRepository;
            _userManager = userManager;
        }

        public async Task<bool> ClientExistsAsync(string clientId)
        {
            return await _clientUserRepository.ItemExistsAsync(clientId);
        }

        public async Task<Client> GetClientByIdAsync(string clientId)
        {
            return await _clientUserRepository.GetItemByIdAsync(clientId);
        }

        public async Task<IdentityUser> GetIdentityClient(string clientId)
        {
            return await _clientUserRepository.GetIdentityClient(clientId);
        }

        public async Task<Client> CreatNewClientAsync(Client client)
        {
            try
            {
                IdentityUser loggedUser = await GetIdentityClient(client.ID);
                //if (loggedUser == null) return false;

                client.ID = loggedUser.Id;
                client.NORMALIZED_NAME = client.NAME.ToUpper();
                client.STATUS = true;
                loggedUser.PhoneNumber = client.PHONE_NUMBER;
                loggedUser.PhoneNumberConfirmed = true;

                await _userManager.UpdateAsync(loggedUser);

                return await _clientUserRepository.AddItemAsync(client);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Client> EditClientAsync(Client client)
        {
            try
            {
                IdentityUser loggedUser = await GetIdentityClient(client.ID);

                client.ID = loggedUser.Id;
                client.NORMALIZED_NAME = client.NAME.ToUpper();
                client.STATUS = true;
                loggedUser.PhoneNumber = client.PHONE_NUMBER;
                loggedUser.PhoneNumberConfirmed = true;

                await _userManager.UpdateAsync(loggedUser);
                await _clientUserRepository.UpdateItem(client);
                return  client;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}