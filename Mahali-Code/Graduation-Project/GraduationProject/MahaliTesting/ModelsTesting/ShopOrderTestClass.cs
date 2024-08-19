using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class ShopOrderTestClass
    {
        [Fact]
        public void Create_Should_Initialize_Correctly()
        {
            // Arrange
            int orderId = 1;
            Guid shopId = Guid.NewGuid();

            // Act
            var shopOrder = ShopOrders.Create(orderId, shopId);

            // Assert
            Assert.Equal(orderId, shopOrder.OrderId);
            Assert.Equal(shopId, shopOrder.ShopId);
        }

        [Fact]
        public void Create_Should_ThrowException_When_InvalidArguments()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => ShopOrders.Create(-1, Guid.NewGuid()));
            Assert.Throws<ArgumentException>(() => ShopOrders.Create(1, Guid.Empty));
        }
    }
}
