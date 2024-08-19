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
using System.Xml.Linq;
using static AllEnums.AllEnums;

namespace MahaliTesting.ServicesTesting
{
    public class ProductServiceTestClass
    {
        private readonly Mock<IProductInterface> _productInterfaceMock = new Mock<IProductInterface>();
        private readonly Mock<IShopInterface> _shopInterfaceMock = new Mock<IShopInterface>();
        private readonly Mock<IShopRequestInterface> _shopRequestInterfaceMock = new Mock<IShopRequestInterface>();

        [Fact]
        public async Task AddAsync_Should_Add_Product_When_ShopRequest_Is_Approved()
        {
            // Arrange
            var productService = new ProductService(_productInterfaceMock.Object, _shopInterfaceMock.Object, _shopRequestInterfaceMock.Object);

            var shopId = Guid.NewGuid();
            var productCreateParameters = new ProductCreateParameters
            {
                Name = "ValidProduct",
                Description = "This is a valid product description.",
                Quantity = 10,
                Price = 20.5m,
                ImageUri = "https://example.com/image.jpg",
                ShopId = shopId
            };

            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var request = ShopRequest.Create(Guid.NewGuid(), shopId);

            _shopInterfaceMock.Setup(x => x.GetByIdAsync(shopId)).ReturnsAsync(shop);
            _shopRequestInterfaceMock.Setup(x => x.GetRequestByShopIdAsync(shopId)).ReturnsAsync(request);
            _productInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>await productService.AddAsync(productCreateParameters));
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            var productService = new ProductService(_productInterfaceMock.Object, _shopInterfaceMock.Object, _shopRequestInterfaceMock.Object);
            var productId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var parameters = new ProductDeleteParameters { ProductId = productId };
            var productToDelete = Product.Create("ValidProduct", "This is a valid product description.", 10, 20.5m, "https://example.com/image.jpg", shopId);
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(productToDelete);

            // Act
            await productService.DeleteAsync(parameters);

            // Assert
            _productInterfaceMock.Verify(x => x.DeleteAsync(productToDelete), Times.Once);
        }
    }
    }

