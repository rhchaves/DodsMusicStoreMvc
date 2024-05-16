using DmStore.Areas.Admin.Models;
using DmStore.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("fornecedor")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _supplierService.GetListSupplierAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (!await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            return View(await _supplierService.GetSupplierByIdAsync(id));
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
            try
            {
                if (ModelState.IsValid)
                {
                    await _supplierService.CreateNewSupplierAsync(supplier);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            return View(supplier);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            return View(await _supplierService.GetSupplierByIdAsync(id));
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,CNPJ,ADDRESS,ADDRESS_NUMBER,COMPLEMENT,ZIP_CODE,NEIGHBORHOOD,CITY,STATE,STATUS")] Supplier supplier)
        {
            if (id != supplier.ID || !await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    Supplier supplierUpdate = await _supplierService.EditSupplierAsync(supplier);
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View(supplier);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            return View(_supplierService.GetSupplierByIdAsync(id));
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            try
            {
                _supplierService.DeleteSupplier(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("status/{id}")]
        public async Task<IActionResult> ActivateDeactivate(string id)
        {
            if (!await _supplierService.SupplierExistsAsync(id))
                return NotFound();

            _supplierService.ActivateDeactivate(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
