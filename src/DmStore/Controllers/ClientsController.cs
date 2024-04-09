using DmStore.Data;
using DmStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DmStore.Areas.Admin.Controllers
{
    [Route("cliente")]
    public class ClientsController : Controller
    {
        private readonly DmStoreDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientsController(DmStoreDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            IdentityUser loggedUser = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (loggedUser == null) return NotFound();

            if (ModelState.IsValid)
            {
                client.ID = loggedUser.Id;
                client.NORMALIZED_NAME = client.NAME.ToUpper();
                client.STATUS = true;
                loggedUser.PhoneNumber = client.PHONE_NUMBER;
                loggedUser.PhoneNumberConfirmed = true;

                await _userManager.UpdateAsync(loggedUser);
                _context.CLIENTS.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ClientExists(id))
                return NotFound();

            var client = await _context.CLIENTS.FindAsync(id);

            return View(client);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,CPF,PHONE_NUMBER,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,NORMALIZED_NAME")] Client client)
        {
            if (id != client.ID || !ClientExists(client.ID))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        private bool ClientExists(string id)
        {
            return _context.CLIENTS.Any(e => e.ID == id);
        }
    }
}
