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
    public class DiscountServiceTestClass
    {
        private readonly Mock<IDiscountInterface> _discountInterfaceMock = new Mock<IDiscountInterface>();
        private readonly Mock<IProductInterface> _productInterfaceMock = new Mock<IProductInterface>();
        private readonly DiscountService _discountService;

        public DiscountServiceTestClass()
        {
            _discountService = new DiscountService(_discountInterfaceMock.Object, _productInterfaceMock.Object);
        }

        [Fact]
        public async Task AddDiscountAsync_ShouldAddDiscount_WhenProductExistsAndNoExistingDiscount()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var parameters = new DiscountCreateParameters
            {
                ProductId = productId,
                DiscountPercentage = 0.1m,
                StartDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss")
            };
            var product = Product.Create("TestProduct", "Description", 10, 100, "imageUri", Guid.NewGuid());
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _discountInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Discount>());

            // Act
            await _discountService.AddDiscountAsync(parameters);

            // Assert
            _discountInterfaceMock.Verify(x => x.AddAsync(It.IsAny<Discount>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDiscountAsync_ShouldDeleteDiscount_WhenProductExistsAndDiscountExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var discountId = Guid.NewGuid();
            var discount = Discount.Create(productId, 0.1m, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss"));
            var product = Product.Create("TestProduct", "Description", 10, 100, "imageUri", Guid.NewGuid());
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _discountInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Discount> { discount });

            // Act
            await _discountService.DeleteDiscountAsync(new DiscountDeleteParameters { ProductId = productId });

            // Assert
            _discountInterfaceMock.Verify(x => x.DeleteAsync(discount), Times.Once);
        }

        [Fact]
        public async Task GetAllDiscountsAsyn_ShouldReturnListOfDiscounts()
        {
            // Arrange
            var discounts = new List<Discount>
            {
                Discount.Create(Guid.NewGuid(), 0.1m, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss"))
            };
            _discountInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(discounts);

            // Act
            var result = await _discountService.GetAllDiscountsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ModifyDiscpuntPercentage_ShouldModifyDiscount_WhenProductExistsAndDiscountExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var discount = Discount.Create(productId, 0.1m, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.UtcNow.AddDays(7).ToString("yyyy-MM-dd HH:mm:ss"));
            var product = Product.Create("TestProduct", "Description", 10, 100, "imageUri", Guid.NewGuid());
            _productInterfaceMock.Setup(x => x.GetByIdAsync(productId)).ReturnsAsync(product);
            _discountInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Discount> { discount });

            // Act
            await _discountService.ModifyDiscountPercentage(new DiscountUpdateParameters { ProductId = productId, DiscountPercentage = 0.2m });

            // Assert
            _discountInterfaceMock.Verify(x => x.UpdateAsync(discount), Times.Once);
        }
    }
}
