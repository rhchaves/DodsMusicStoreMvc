using DmStore.Models;
using DmStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Controllers
{
    [Route("cliente")]
    public class ClientsController : Controller
    {
        private readonly IClientUserService _clientUserService;

        public ClientsController(IClientUserService clientUserService)
        {
            _clientUserService = clientUserService;
        }

        [Route("completar-cadastro")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("completar-cadastro")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NAME,CPF,PHONE_NUMBER,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,NORMALIZED_NAME")] Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientUserService.CreatNewClientAsync(client);
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!await _clientUserService.ClientExistsAsync(id))
                return NotFound();

            return View(await _clientUserService.GetClientByIdAsync(id));
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,CPF,PHONE_NUMBER,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,NORMALIZED_NAME")] Client client)
        {
            if (id != client.ID || !await _clientUserService.ClientExistsAsync(client.ID))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    Client clientUpdate = await _clientUserService.EditClientAsync(client);
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View(client);
        }
    }
}
