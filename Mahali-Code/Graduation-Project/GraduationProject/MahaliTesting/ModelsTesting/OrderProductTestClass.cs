using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class OrderProductTestClass
    {
        [Fact]
        public void Create_Should_Initialize_Correctly()
        {
            // Arrange
            int orderId = 1;
            Guid productId = Guid.NewGuid();
            int quantity = 5;
            decimal unitPrice = 10m;
            Colors color = Colors.Gray;
            Sizes size = Sizes.US_41;

            // Act
            var orderProduct = OrderProducts.Create(orderId, productId, quantity, unitPrice, color, size);

            // Assert
            Assert.Equal(orderId, orderProduct.OrderId);
            Assert.Equal(productId, orderProduct.ProductId);
            Assert.Equal(quantity, orderProduct.Quantity);
            Assert.Equal(unitPrice, orderProduct.UnitPrice);
            Assert.Equal(color, orderProduct.Color);
            Assert.Equal(size, orderProduct.Size);
        }

        [Fact]
        public void Create_Should_ThrowException_When_InvalidArguments()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => OrderProducts.Create(-1, Guid.NewGuid(), 5, 10m, Colors.Gray, Sizes.US_41));
            Assert.Throws<ArgumentNullException>(() => OrderProducts.Create(1, Guid.Empty, 5, 10m, Colors.Gray, Sizes.US_41));
            Assert.Throws<ArgumentOutOfRangeException>(() => OrderProducts.Create(1, Guid.NewGuid(), 5, -10m, Colors.Gray, Sizes.US_41));
            Assert.Throws<ArgumentOutOfRangeException>(() => OrderProducts.Create(1, Guid.NewGuid(), -5, 10m, Colors.Gray, Sizes.US_41));
        }

        [Fact]
        public void SetUnitPrice_Should_Update_UnitPrice()
        {
            // Arrange
            var orderProduct = OrderProducts.Create(1, Guid.NewGuid(), 5, 10m, Colors.Gray, Sizes.US_41);
            decimal newUnitPrice = 15m;

            // Act
            orderProduct.SetUnitPrice(newUnitPrice);

            // Assert
            Assert.Equal(newUnitPrice, orderProduct.UnitPrice);
        }

        [Fact]
        public void SetQuantity_Should_Update_Quantity()
        {
            // Arrange
            var orderProduct = OrderProducts.Create(1, Guid.NewGuid(), 5, 10m, Colors.Gray, Sizes.US_41);
            int newQuantity = 10;

            // Act
            orderProduct.SetQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, orderProduct.Quantity);
        }

        [Fact]
        public void SetColor_Should_Update_Color()
        {
            // Arrange
            var orderProduct = OrderProducts.Create(1, Guid.NewGuid(), 5, 10m, Colors.Gray, Sizes.US_41);
            Colors newColor = Colors.Red;

            // Act
            orderProduct.SetColor(newColor);

            // Assert
            Assert.Equal(newColor, orderProduct.Color);
        }

        [Fact]
        public void SetSize_Should_Update_Size()
        {
            // Arrange
            var orderProduct = OrderProducts.Create(1, Guid.NewGuid(), 5, 10m, Colors.Gray, Sizes.US_41);
            Sizes newSize = Sizes.US_38;

            // Act
            orderProduct.SetSize(newSize);

            // Assert
            Assert.Equal(newSize, orderProduct.Size);
        }
    }
}
