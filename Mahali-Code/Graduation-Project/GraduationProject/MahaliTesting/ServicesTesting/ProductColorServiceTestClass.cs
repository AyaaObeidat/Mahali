using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using MahaliDtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MahaliTesting.ServicesTesting
{
    public class ProductColorServiceTestClass
    {

        private readonly Mock<IProductColorInterface> _productColorInterfaceMock = new Mock<IProductColorInterface>();
        private readonly Mock<IProductInterface> _productInterfaceMock = new Mock<IProductInterface>();
        private readonly ProductColorService _productColorService;

        public ProductColorServiceTestClass()
        {
            _productColorService = new ProductColorService(_productColorInterfaceMock.Object, _productInterfaceMock.Object);
        }

        [Fact]
        public async Task AddProductColorAsync_ShouldAddProductColor_WhenProductExists()
        {
            var productId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var product = Product.Create("ValidProduct", "This is a valid product description.", 10, 20.5m, "https://example.com/image.jpg", shopId);
            var parameters = new ProductColorsCreateParameters
            {
                ProductId = productId,
                Color = AllEnums.AllEnums.Colors.Red
            };

            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            await _productColorService.AddProductColorAsync(parameters);

            // Assert
            _productColorInterfaceMock.Verify(x => x.AddAsync(It.IsAny<ProductColors>()), Times.Once);
        }

        [Fact]
        public async Task DeleteProductColorAsync_ShouldDeleteProductColor_WhenProductAndProductColorExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productColorId = Guid.NewGuid();
            var parameters = new ProductColorDeleteParameters
            {
                ProductId = productId,
                ProductColorId = productColorId
            };
            var shopId = Guid.NewGuid();
            var product = Product.Create("ValidProduct", "This is a valid product description.", 10, 20.5m, "https://example.com/image.jpg", shopId);
            var productColor = ProductColors.Create (productId,AllEnums.AllEnums.Colors.Green);
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _productColorInterfaceMock.Setup(x => x.GetByIdAsync(productColorId)).ReturnsAsync(productColor);

            // Act
            await _productColorService.DeleteProductColorAsync(parameters);

            // Assert
            _productColorInterfaceMock.Verify(x => x.DeleteAsync(productColor), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProductColors()
        {
            // Arrange
            var productColors = new List<ProductColors>
            {
                ProductColors.Create(Guid.NewGuid(), AllEnums.AllEnums.Colors.Green),
                ProductColors.Create(Guid.NewGuid(), AllEnums.AllEnums.Colors.Black),
            };
            _productColorInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(productColors.ToList());

            // Act
            var result = await _productColorService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(productColors.Count, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductColor_WhenProductColorExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productColor = ProductColors.Create(productId, AllEnums.AllEnums.Colors.Green);
            _productColorInterfaceMock.Setup(x => x.GetByIdAsync(productColor.Id)).ReturnsAsync(productColor);

            // Act
            var result = await _productColorService.GetByIdAsync(new ProductColorGetByParameters { Id = productColor.Id });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productColor.Id, result.Id);
        }
    }
}

