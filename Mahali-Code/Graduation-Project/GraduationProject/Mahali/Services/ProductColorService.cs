
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;

namespace Mahali.Services
{
    public class ProductColorService
    {
        private readonly IProductColorInterface _productColorInterface;
        private readonly IProductInterface _productInterface;

        public ProductColorService(IProductColorInterface productColorInterface , IProductInterface productInterface)
        {
            _productColorInterface = productColorInterface;
            _productInterface = productInterface;
        }

        public async Task AddProductColorAsync (ProductColorsCreateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync (parameters.ProductId);
            if (product == null) { return; }

            var productColor = ProductColors.Create(parameters.ProductId, parameters.Color);
            await _productColorInterface.AddAsync (productColor);
        }

        public async Task DeleteProductColorAsync(ProductColorDeleteParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) { return; }
            var productColor = await _productColorInterface.GetByIdAsync(parameters.ProductColorId);
            if (productColor == null) return;
            await _productColorInterface.DeleteAsync(productColor);
        }

        public async Task<List<ProductColorsDetails>> GetAllAsync()
        {
            var productsColors = await _productColorInterface.GetAllAsync();
            var products = await _productInterface.GetAllAsync();
            return productsColors.Select(c => new ProductColorsDetails
            {
                Id = c.Id,
                Product = products.Select(p => new ProductDetails
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUri = p.ImageUri,
                    Quantity = p.Quantity,
                    Price = p.Price,
                }).FirstOrDefault(y => y.Id == c.ProductId),
                Color = c.Color,
            }).ToList();
        }

        public async Task<ProductColorsDetails?> GetByIdAsync(ProductColorGetByParameters parameters)
        {
            var productColor = await _productColorInterface.GetByIdAsync(parameters.Id);
            var products = await _productInterface.GetAllAsync();
            return new ProductColorsDetails
            {
                Id = productColor.Id,
                Product = products.Select(p => new ProductDetails
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUri = p.ImageUri,
                    Quantity = p.Quantity,
                    Price = p.Price,
                }).FirstOrDefault(y => y.Id == productColor.ProductId),
                Color = productColor.Color,
            };
        }
    }
}
