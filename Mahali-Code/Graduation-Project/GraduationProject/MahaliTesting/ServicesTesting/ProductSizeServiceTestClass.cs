using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using MahaliDtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ServicesTesting
{
    public class ProductSizeServiceTestClass
    {

        private readonly Mock<IProductSizeInterface> _productSizeInterfaceMock = new Mock<IProductSizeInterface>();
        private readonly Mock<IProductInterface> _productInterfaceMock = new Mock<IProductInterface>();
        private readonly ProductSizeService _productSizeService;

        public ProductSizeServiceTestClass()
        {
            _productSizeService = new ProductSizeService(_productSizeInterfaceMock.Object, _productInterfaceMock.Object);
        }

        [Fact]
        public async Task AddProductSizeAsync_ShouldAddProductSize_WhenProductExists()
        {
            var productId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var product = Product.Create("ValidProduct", "This is a valid product description.", 10, 20.5m, "https://example.com/image.jpg", shopId);
            var parameters = new ProductSizesCreateParameters
            {
                ProductId = productId,
                Size = AllEnums.AllEnums.Sizes.Small
            };

            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            await _productSizeService.AddProductSizeAsync(parameters);

            // Assert
            _productSizeInterfaceMock.Verify(x => x.AddAsync(It.IsAny<ProductSizes>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductSizeAsync_ShouldDeleteProductSize_WhenProductAndProductSizeExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productSizeId = Guid.NewGuid();
            var parameters = new ProductSizeDeleteParameters
            {
                ProductId = productId,
                ProductSizeId = productSizeId
            };
            var shopId = Guid.NewGuid();
            var product = Product.Create("ValidProduct", "This is a valid product description.", 10, 20.5m, "https://example.com/image.jpg", shopId);
            var productSize = ProductSizes.Create(productId, AllEnums.AllEnums.Sizes.Small);
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _productSizeInterfaceMock.Setup(x => x.GetByIdAsync(productSizeId)).ReturnsAsync(productSize);

            // Act
            await _productSizeService.DeleteProductSizeAsync(parameters);

            // Assert
            _productSizeInterfaceMock.Verify(x => x.DeleteAsync(productSize), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProductSizes()
        {
            // Arrange
            var productSizes = new List<ProductSizes>
            {
                ProductSizes.Create(Guid.NewGuid(), AllEnums.AllEnums.Sizes.Small),
                ProductSizes.Create(Guid.NewGuid(), AllEnums.AllEnums.Sizes.Large),
            };
            _productSizeInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(productSizes.ToList());

            // Act
            var result = await _productSizeService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(productSizes.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductSize_WhenProductSizeExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productSize = ProductSizes.Create(productId, AllEnums.AllEnums.Sizes.Small);
            _productSizeInterfaceMock.Setup(x => x.GetByIdAsync(productSize.Id)).ReturnsAsync(productSize);

            // Act
            var result = await _productSizeService.GetByIdAsync(new ProductSizeGetByParameters { Id = productSize.Id });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productSize.Id, result.Id);
        }
    }
}
