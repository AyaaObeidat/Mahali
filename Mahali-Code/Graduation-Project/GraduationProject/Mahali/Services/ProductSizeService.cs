
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;

namespace Mahali.Services
{
    public class ProductSizeService
    {
        private readonly IProductSizeInterface _productSizeInterface;
        private readonly IProductInterface _productnterface;

        public ProductSizeService(IProductSizeInterface productSizeInterface, IProductInterface productnterface)
        {
            _productSizeInterface = productSizeInterface;
            _productnterface = productnterface;
        }

        public async Task AddProductSizeAsync(ProductSizesCreateParameters parameters)
        {
            var product = await _productnterface.GetByIdAsync(parameters.ProductId);
            if (product == null) { return; }

            var productSize = ProductSizes.Create(parameters.ProductId, parameters.Size);
            await _productSizeInterface.AddAsync(productSize);
        }

        public async Task DeleteProductSizeAsync(ProductSizeDeleteParameters parameters)
        {
            var product = await _productnterface.GetByIdAsync(parameters.ProductId);
            if (product == null) { return; }
            var productSize = await _productSizeInterface.GetByIdAsync(parameters.ProductSizeId);
            if (productSize == null) return;
            await _productSizeInterface.DeleteAsync(productSize);
        }

        public async Task<List<ProductSizesDetails>> GetAllAsync()
        {
            var productsSizes = await _productSizeInterface.GetAllAsync();
            var products = await _productnterface.GetAllAsync();
            return productsSizes.Select(c => new ProductSizesDetails
            {
                Id = c.Id,
                Product =products.Select(p => new ProductDetails
                {
                    Id = p.Id,
                    Name = p.Name ,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    ImageUri = p.ImageUri
                }).FirstOrDefault(y => y.Id == c.ProductId),
                Size = c.Size,
            }).ToList();
        }

        public async Task<ProductSizesDetails?> GetByIdAsync(ProductSizeGetByParameters parameters)
        {
            var productSize = await _productSizeInterface.GetByIdAsync(parameters.Id);
            var products = await _productnterface.GetAllAsync();
            return new ProductSizesDetails
            {
                Id = productSize.Id,
                Product = products.Select(p => new ProductDetails
                {
                    Id = p.Id,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    ImageUri = p.ImageUri
                }).FirstOrDefault(y => y.Id == productSize.ProductId),
                Size = productSize.Size,
            };
        }
    }
}
