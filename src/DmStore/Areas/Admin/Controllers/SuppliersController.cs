using DmStore.Areas.Admin.Models;
using DmStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("fornecedor")]
    public class SuppliersController : Controller
    {
        private readonly DmStoreDbContext _context;

        public SuppliersController(DmStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Supplier.ToListAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            var supplier = await _context.Supplier.FirstOrDefaultAsync(m => m.Id == id);

            return View(supplier);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Cnpj,PublicPlace,Number,Complement,ZipCode,Neighborhood,City,State,Active,Id")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.Id = Guid.NewGuid().ToString();
                supplier.DateRegister = DateTime.Now;
                supplier.DateUpload = DateTime.Now;
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            Supplier supplier = await _context.Supplier.FindAsync(id);
            
            return View(supplier);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Cnpj,PublicPlace,Number,Complement,ZipCode,Neighborhood,City,State,Active,Id")] Supplier supplier)
        {
            if (id != supplier.Id || !SupplierExists(id))
                return NotFound();

            Supplier supplierUpdate = await _context.Supplier.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (supplierUpdate != null)
                    {
                        supplierUpdate.Name = supplier.Name;
                        supplierUpdate.PublicPlace = supplier.PublicPlace;
                        supplierUpdate.Number = supplier.Number;
                        supplierUpdate.Complement = supplier.Complement;
                        supplierUpdate.ZipCode = supplier.ZipCode;
                        supplierUpdate.Neighborhood = supplier.Neighborhood;
                        supplierUpdate.City = supplier.City;
                        supplierUpdate.State = supplier.State;
                        supplierUpdate.Active = supplier.Active;
                        supplierUpdate.DateUpload = DateTime.Now;

                        _context.Update(supplierUpdate);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplierUpdate);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            var supplier = await _context.Supplier.FirstOrDefaultAsync(m => m.Id == id);

            return View(supplier);
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            _context.Supplier.Remove(await _context.Supplier.FindAsync(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("status/{id}")]
        public async Task<IActionResult> ActivateDeactivate(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            Supplier supplier = await _context.Supplier.FindAsync(id);

            try
            {
                if (supplier != null)
                {
                    supplier.Active = !supplier.Active;
                    supplier.DateUpload = DateTime.Now;

                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(string id)
        {
            return _context.Supplier.Any(e => e.Id == id);
        }
    }
}
