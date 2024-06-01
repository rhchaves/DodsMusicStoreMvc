using DmStore.Areas.Admin.Models;
using DmStore.Areas.Admin.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Services
{
    public interface IProductService
    {
        Task<bool> ProductExistsAsync(string productId);
        Task<List<Product>> GetListProductAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<List<Supplier>> GetActiveSuppliersAsync();
        Task<Product> CreatNewProductAsync(Product product);
        Task<Product> EditProductAsync(Product product);
        void DeleteProduct(string id);
        void ActivateDeactivate(string id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> ProductExistsAsync(string productId)
        {
            return await _productRepository.ItemExistsAsync(productId);
        }

        public async Task<List<Product>> GetListProductAsync()
        {
            return await _productRepository.GetListAllItensAsync();
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            Product product = await _productRepository.GetItemByIdAsync(productId);
            product.SUPPLIER = await _productRepository.GetSupplierByIdAsync(product.SUPPLIER_ID);
            return product;
        }

        public async Task<List<Supplier>> GetActiveSuppliersAsync()
        {
            return await _productRepository.GetActiveSuppliersAsync();
        }

        public async Task<Product> CreatNewProductAsync(Product product)
        {
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(product.IMAGE_UPLOAD, imgPrefixo))
            {
                return product = null; //verificar melhor retorno nessa situação
            }
            product.IMAGE_URI = imgPrefixo + product.IMAGE_UPLOAD.FileName;

            product.SUPPLIER = await _productRepository.GetSupplierByIdAsync(product.SUPPLIER_ID);
            product.CREATE_REGISTER = DateTime.Now;
            product.UPDATE_REGISTER = DateTime.Now;
            product.UPDATE_STATUS = DateTime.Now;
            return await _productRepository.AddItemAsync(product);
        }

        public async Task<Product> EditProductAsync(Product product)
        {
            Product productUpdate = await GetProductByIdAsync(product.ID);
            try
            {
                if (productUpdate != null)
                {
                    if (product.IMAGE_UPLOAD != null)
                    {
                        var imgPrefixo = Guid.NewGuid() + "_";
                        if (!await UploadArquivo(product.IMAGE_UPLOAD, imgPrefixo))
                        {
                            return product = null; //verificar melhor retorno nessa situação
                        }

                        productUpdate.IMAGE_URI = imgPrefixo + productUpdate.IMAGE_UPLOAD.FileName;
                    }

                    productUpdate.NAME = product.NAME;
                    productUpdate.DESCRIPTION = product.DESCRIPTION;
                    productUpdate.PRICE = product.PRICE;
                    productUpdate.STOCK_QTD = product.STOCK_QTD;
                    productUpdate.UPDATE_REGISTER = DateTime.Now;

                    await _productRepository.UpdateItem(productUpdate);
                }
                
                return product;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void DeleteProduct(string id)
        {
            try
            {
                _productRepository.RemoveItem(await GetProductByIdAsync(id));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                //ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        public async void ActivateDeactivate(string id)
        {

            Product product = await GetProductByIdAsync(id);

            try
            {
                if (product != null)
                {
                    product.STATUS = !product.STATUS;
                    product.UPDATE_STATUS = DateTime.Now;

                    await _productRepository.UpdateItem(product);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
