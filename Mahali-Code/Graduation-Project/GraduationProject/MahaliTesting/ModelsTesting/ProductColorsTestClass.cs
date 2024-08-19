using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class ProductColorsTestClass
    {
        [Fact]
        public void Create_ValidProductColor_ShouldSucceed()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var color = AllEnums.AllEnums.Colors.Red;

            // Act
            var productColor = ProductColors.Create(productId, color);

            // Assert
            Assert.NotNull(productColor);
            Assert.Equal(productId, productColor.ProductId);
            Assert.Equal(color, productColor.Color);
        }

        [Fact]
        public void Create_EmptyProductId_ShouldThrowArgumentException()
        {
            // Arrange
            var productId = Guid.Empty;
            var color = AllEnums.AllEnums.Colors.Red;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => ProductColors.Create(productId, color));
        }

}
}
