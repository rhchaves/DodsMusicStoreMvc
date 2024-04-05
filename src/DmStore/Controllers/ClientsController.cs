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
        public async Task<IActionResult> Create([Bind("Name,Cpf,PhoneNumber,Id,PublicPlace,Number,Complement,ZipCode,Neighborhood,City,State")] Client client)
        {
            IdentityUser loggedUser = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (loggedUser == null) return NotFound();

            if (ModelState.IsValid)
            {
                client.Id = loggedUser.Id;
                client.NormalizedName = client.Name.ToUpper();
                client.Active = true;
                loggedUser.PhoneNumber = client.PhoneNumber;
                loggedUser.PhoneNumberConfirmed = true;

                await _userManager.UpdateAsync(loggedUser);
                _context.Client.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Cpf,Active,Id")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(client);
        }

        private bool ClientExists(string id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
