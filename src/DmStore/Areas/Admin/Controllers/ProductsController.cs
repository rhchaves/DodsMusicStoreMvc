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
            var dmStoreDbContext = _context.Product.Include(p => p.Supplier);
            return View(await dmStoreDbContext.ToListAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var product = await _context.Product
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Route("novo")]
        public IActionResult Create()
        {
            var activeSuppliers = _context.Supplier.Where(s => s.Active == true).ToList();

            ViewData["Supplier"] = new SelectList(activeSuppliers, "Id", "Name");
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Image,Price,SupplierId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                product.DateRegister = DateTime.Now;
                product.DateUpload = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Supplier"] = new SelectList(_context.Supplier, "Id", "Id", product.SupplierId);
            return View(product);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", product.SupplierId);
            return View(product);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Image,Price,Active,Id")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            Product productUpdate = await _context.Product.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (productUpdate != null)
                    {
                        productUpdate.Name = product.Name;
                        productUpdate.Description = product.Description;
                        productUpdate.Image = product.Image;
                        productUpdate.Price = product.Price;
                        productUpdate.Active = product.Active;
                        productUpdate.DateUpload = DateTime.Now;

                        _context.Update(productUpdate);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", product.SupplierId);
            return View(productUpdate);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _context.Product
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
