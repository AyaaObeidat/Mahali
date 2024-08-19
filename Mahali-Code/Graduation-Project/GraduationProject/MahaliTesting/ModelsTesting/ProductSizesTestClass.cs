using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class ProductSizesTestClass
    {
        [Fact]
        public void Create_ValidProductSize_ShouldSucceed()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var size = Sizes.Small;

            // Act
            var productSize = ProductSizes.Create(productId, size);

            // Assert
            Assert.NotNull(productSize);
            Assert.Equal(productId, productSize.ProductId);
            Assert.Equal(size, productSize.Size);
        }

        [Fact]
        public void Create_EmptyProductId_ShouldThrowArgumentException()
        {
            // Arrange
            var productId = Guid.Empty;
            var size = Sizes.Small;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProductSizes.Create(productId, size));
        }

        
}
}
