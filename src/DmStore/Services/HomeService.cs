using DmStore.Models;
using DmStore.Repositories;

namespace DmStore.Services
{
    public interface IHomeService
    {
        Task<List<ProductCardViewModel>> ListAllProductAsync();
    }
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;

        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<List<ProductCardViewModel>> ListAllProductAsync()
        {
            var listProduct = await _homeRepository.ListProductAsync();

            var productCards = new List<ProductCardViewModel>();

            if (listProduct != null)
            {
                productCards = listProduct.Select(item => new ProductCardViewModel
                {
                    Name = item.NAME,
                    Description = item.DESCRIPTION,
                    ImageUri = "/images/" + item.IMAGE_URI
                }).ToList();
            }
            return productCards;
        }
    }
}
