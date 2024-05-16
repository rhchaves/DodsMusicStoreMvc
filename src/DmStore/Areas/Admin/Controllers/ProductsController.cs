using DmStore.Areas.Admin.Models;
using DmStore.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("produto")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetListProductAsync());
        }

        [Route("detalhes/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            return View(await _productService.GetProductByIdAsync(id));
        }

        [Route("novo")]
        public IActionResult Create()
        {
            var activeSuppliers = _productService.GetActiveSuppliersAsync().Result;

            ViewData["Supplier"] = new SelectList(activeSuppliers, "ID", "NAME");
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NAME,DESCRIPTION,IMAGE_UPLOAD,PRICE,STOCK_QTD,STATUS,SUPPLIER_ID")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreatNewProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
            return View(product);
        }

        [Route("editar/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            return View(await _productService.GetProductByIdAsync(id));
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,NAME,DESCRIPTION,IMAGE_UPLOAD,PRICE,STOCK_QTD,STATUS,SUPPLIER_ID")] Product product)
        {
            if (id != product.ID || !await _productService.ProductExistsAsync(id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    Product productUpdate = await _productService.EditProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View(product);
        }

        [Route("excluir/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            return View(await _productService.GetProductByIdAsync(id));
        }

        [HttpPost("excluir/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            try
            {
                _productService.DeleteProduct(id);
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
            if (!await _productService.ProductExistsAsync(id))
                return NotFound();

            _productService.ActivateDeactivate(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
