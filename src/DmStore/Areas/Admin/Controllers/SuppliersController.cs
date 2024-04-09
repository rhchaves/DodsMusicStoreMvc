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
            return View(await _context.SUPPLIERS.ToListAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            var supplier = await _context.SUPPLIERS.FirstOrDefaultAsync(m => m.ID == id);

            return View(supplier);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //supplier.Id = Guid.NewGuid().ToString();
                supplier.CREATE_REGISTER = DateTime.Now;
                supplier.UPDATE_REGISTER = DateTime.Now;
                supplier.UPDATE_STATUS = DateTime.Now;
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

            Supplier supplier = await _context.SUPPLIERS.FindAsync(id);
            
            return View(supplier);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS")] Supplier supplier)
        {
            if (id != supplier.ID || !SupplierExists(id))
                return NotFound();

            Supplier supplierUpdate = await _context.SUPPLIERS.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (supplierUpdate != null)
                    {
                        supplierUpdate.NAME = supplier.NAME;
                        supplierUpdate.ADDRESS = supplier.ADDRESS;
                        supplierUpdate.ADDRESS_NUMBER = supplier.ADDRESS_NUMBER;
                        supplierUpdate.COMPLEMENT = supplier.COMPLEMENT;
                        supplierUpdate.ZIP_CODE = supplier.ZIP_CODE;
                        supplierUpdate.NEIGHBORHOOD = supplier.NEIGHBORHOOD;
                        supplierUpdate.CITY = supplier.CITY;
                        supplierUpdate.STATE = supplier.STATE;
                        supplierUpdate.UPDATE_REGISTER = DateTime.Now;

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

            var supplier = await _context.SUPPLIERS.FirstOrDefaultAsync(m => m.ID == id);

            return View(supplier);
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            _context.SUPPLIERS.Remove(await _context.SUPPLIERS.FindAsync(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("status/{id}")]
        public async Task<IActionResult> ActivateDeactivate(string id)
        {
            if (!SupplierExists(id))
                return NotFound();

            Supplier supplier = await _context.SUPPLIERS.FindAsync(id);

            try
            {
                if (supplier != null)
                {
                    supplier.STATUS = !supplier.STATUS;
                    supplier.UPDATE_STATUS = DateTime.Now;

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
            return _context.SUPPLIERS.Any(e => e.ID == id);
        }
    }
}
