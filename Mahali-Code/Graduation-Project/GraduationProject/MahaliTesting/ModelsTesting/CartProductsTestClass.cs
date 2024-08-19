using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class CartProductsTestClass
    {
        [Fact]
        public void Create_ValidCartProducts_ShouldSucceed()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;
            var unitPrice = 10.00m;
            var color = Colors.Red;
            var size = Sizes.Large;

            // Act
            var cartProducts = CartProducts.Create(cartId, productId, quantity, unitPrice, color, size);

            // Assert
            Assert.NotNull(cartProducts);
            Assert.Equal(cartId, cartProducts.CartId);
            Assert.Equal(productId, cartProducts.ProductId);
            Assert.Equal(quantity, cartProducts.Quantity);
            Assert.Equal(unitPrice, cartProducts.UnitPrice);
            Assert.Equal(color, cartProducts.Color);
            Assert.Equal(size, cartProducts.Size);
        }

        [Fact]
        public void Create_InvalidCartProducts_ShouldThrowArgumentNullException()
        {
            // Arrange
            var cartId = Guid.Empty;
            var productId = Guid.Empty;
            var quantity = 2;
            var unitPrice = 10.00m;
            var color = Colors.Red;
            var size = Sizes.Large;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => CartProducts.Create(cartId, productId, quantity, unitPrice, color, size));
        }

        [Fact]
        public void Create_InvalidUnitPrice_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 2;
            var unitPrice = -10.00m;
            var color = Colors.Red;
            var size = Sizes.Large;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => CartProducts.Create(cartId, productId, quantity, unitPrice, color, size));
        }

        [Fact]
        public void SetUnitPrice_ValidUnitPrice_ShouldUpdateUnitPrice()
        {
            // Arrange
            var cartProducts = CartProducts.Create(Guid.NewGuid(), Guid.NewGuid(), 1, 10.00m, Colors.Red, Sizes.Large);
            var newUnitPrice = 15.00m;

            // Act
            cartProducts.SetUnitPrice(newUnitPrice);

            // Assert
            Assert.Equal(newUnitPrice, cartProducts.UnitPrice);
        }

        [Fact]
        public void SetQuantity_ValidQuantity_ShouldUpdateQuantity()
        {
            // Arrange
            var cartProducts = CartProducts.Create(Guid.NewGuid(), Guid.NewGuid(), 1, 10.00m, Colors.Red, Sizes.Large);
            var newQuantity = 2;

            // Act
            cartProducts.SetQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, cartProducts.Quantity);
        }

        [Fact]
        public void SetColor_ValidColor_ShouldUpdateColor()
        {
            // Arrange
            var cartProducts = CartProducts.Create(Guid.NewGuid(), Guid.NewGuid(), 1, 10.00m, Colors.Red, Sizes.Large);
            var newColor = Colors.Blue;

            // Act
            cartProducts.SetColor(newColor);

            // Assert
            Assert.Equal(newColor, cartProducts.Color);
        }

        [Fact]
        public void SetSize_ValidSize_ShouldUpdateSize()
        {
            // Arrange
            var cartProducts = CartProducts.Create(Guid.NewGuid(), Guid.NewGuid(), 1, 10.00m, Colors.Red, Sizes.Large);
            var newSize = Sizes.Small;

            // Act
            cartProducts.SetSize(newSize);

            // Assert
            Assert.Equal(newSize, cartProducts.Size);
        }
    }
}
