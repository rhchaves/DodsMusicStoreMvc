using DmStore.Areas.Admin.Models;
using DmStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("produto")]
    public class ProductsController : Controller
    {
        private readonly DmStoreDbContext _context;

        public ProductsController(DmStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dmStoreDbContext = _context.PRODUCTS.Include(p => p.SUPPLIER);
            return View(await dmStoreDbContext.ToListAsync());

            //return View(await _context.PRODUCTS. .ToListAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (!ProductExists(id))
                return NotFound();

            Product product = await _context.PRODUCTS.FindAsync(id);
            Supplier supplier = await _context.SUPPLIERS.FindAsync(product.SUPPLIER_ID);
            product.SUPPLIER = supplier;

            return View(product);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            var activeSuppliers = _context.SUPPLIERS.Where(s => s.STATUS == true);

            ViewData["Supplier"] = new SelectList(activeSuppliers, "ID", "NAME");
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,STATUS,SUPPLIER_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = await _context.SUPPLIERS.FindAsync(product.SUPPLIER_ID);

                product.SUPPLIER = supplier;
                product.CREATE_REGISTER = DateTime.Now;
                product.UPDATE_REGISTER = DateTime.Now;
                product.UPDATE_STATUS = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ProductExists(id))
                return NotFound();

            Product product = await _context.PRODUCTS.FindAsync(id);
            Supplier supplier = await _context.SUPPLIERS.FindAsync(product.SUPPLIER_ID);
            product.SUPPLIER = supplier;

            return View(product);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,DESCRIPTION,IMAGE_URI,PRICE,STOCK_QTD,STATUS,SUPPLIER_ID")] Product product)
        {
            if (id != product.ID || !ProductExists(id))
                return NotFound();

            Product productUpdate = await _context.PRODUCTS.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (productUpdate != null)
                    {
                        productUpdate.NAME = product.NAME;
                        productUpdate.DESCRIPTION = product.DESCRIPTION;
                        productUpdate.IMAGE_URI = product.IMAGE_URI;
                        productUpdate.PRICE = product.PRICE;
                        productUpdate.STOCK_QTD = product.STOCK_QTD;
                        productUpdate.UPDATE_REGISTER = DateTime.Now;

                        _context.Update(productUpdate);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productUpdate);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ProductExists(id))
                return NotFound();

            Product product = await _context.PRODUCTS.FindAsync(id);
            Supplier supplier = await _context.SUPPLIERS.FindAsync(product.SUPPLIER_ID);
            product.SUPPLIER = supplier;

            return View(product);
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ProductExists(id))
                return NotFound();

            _context.PRODUCTS.Remove(await _context.PRODUCTS.FindAsync(id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("status/{id}")]
        public async Task<IActionResult> ActivateDeactivate(string id)
        {
            if (!ProductExists(id))
                return NotFound();

            Product product = await _context.PRODUCTS.FindAsync(id);

            try
            {
                if (product != null)
                {
                    product.STATUS = !product.STATUS;
                    product.UPDATE_STATUS = DateTime.Now;

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.PRODUCTS.Any(e => e.ID == id);
        }
    }
}
