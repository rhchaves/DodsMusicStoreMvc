using DmStore.Models;
using DmStore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Services
{
    public interface IClienteServico
    {
        Task<bool> ClientExistsAsync(string clientId);
        Task<Client> GetClientByIdAsync(string clientId);
        Task<Client> CreatNewClientAsync(Client client);
        Task<Client> EditClientAsync(Client client);
    }
    public class ClienteServico : IClienteServico
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ClienteServico(IClientRepository clientRepository, UserManager<IdentityUser> userManager)
        {
            _clientRepository = clientRepository;
            _userManager = userManager;
        }

        public async Task<bool> ClientExistsAsync(string clientId)
        {
            return await _clientRepository.ClientExistsAsync(clientId);
        }

        public async Task<Client> GetClientByIdAsync(string clientId)
        {
            return await _clientRepository.GetClientByIdAsync(clientId);
        }

        public async Task<IdentityUser> GetIdentityClient(string clientId)
        {
            return await _clientRepository.GetIdentityClient(clientId);
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

                return await _clientRepository.CreatNewClientAsync(client);
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
                return _clientRepository.EditClient(client);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}