using DmStore.Areas.Admin.Models;
using DmStore.Areas.Admin.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DmStore.Areas.Admin.Services
{
    public interface ISupplierService
    {
        Task<bool> SupplierExistsAsync(string supplierId);
        Task<List<Supplier>> GetListSupplierAsync();
        Task<Supplier> GetSupplierByIdAsync(string id);
        Task<Supplier> CreateNewSupplierAsync(Supplier supplier);
        Task<Supplier> EditSupplierAsync(Supplier supplier);
        void DeleteSupplier(string id);
        void ActivateDeactivate(string id);
    }

    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<bool> SupplierExistsAsync(string supplierId)
        {
            return await _supplierRepository.ItemExistsAsync(supplierId);
        }

        public async Task<List<Supplier>> GetListSupplierAsync()
        {
            return await _supplierRepository.GetListAllItensAsync();
        }

        public async Task<Supplier> GetSupplierByIdAsync(string id)
        {
            return await _supplierRepository.GetItemByIdAsync(id);
        }

        public async Task<Supplier> CreateNewSupplierAsync(Supplier supplier)
        {
            supplier.CREATE_REGISTER = DateTime.Now;
            supplier.UPDATE_REGISTER = DateTime.Now;
            supplier.UPDATE_STATUS = DateTime.Now;

            return await _supplierRepository.AddItemAsync(supplier);
        }

        public async Task<Supplier> EditSupplierAsync(Supplier supplier)
        {
            Supplier supplierUpdate = await GetSupplierByIdAsync(supplier.ID);
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

                    await _supplierRepository.UpdateItem(supplierUpdate);
                }
                return supplier;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void DeleteSupplier(string id)
        {
            try
            {
                _supplierRepository.RemoveItem(await GetSupplierByIdAsync(id));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void ActivateDeactivate(string id)
        {
            Supplier supplier = await GetSupplierByIdAsync(id);

            try
            {
                if (supplier != null)
                {
                    supplier.STATUS = !supplier.STATUS;
                    supplier.UPDATE_STATUS = DateTime.Now;

                    await _supplierRepository.UpdateItem(supplier);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
