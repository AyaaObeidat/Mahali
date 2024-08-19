using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class OrderTestClass
    {
        [Fact]
        public void Create_Order_With_Valid_Data_Should_Work()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var orderType = OrderType.InStorePickup;
            var paymentType = PaymentType.Cash;

            // Act
            var order = Order.Create(cartId, orderType, paymentType);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(cartId, order.CartId);
            Assert.Equal(orderType, order.TypeOfOrder);
            Assert.Equal(paymentType, order.TypeOfPayment);
            Assert.Equal(OrderStatus.NotPaid, order.Status);
            Assert.True((DateTime.Now - order.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void Create_Order_With_Empty_CartId_Should_Throw_ArgumentException()
        {
            // Arrange
            var cartId = Guid.Empty;
            var orderType = OrderType.Delivery;
            var paymentType = PaymentType.Visa;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Order.Create(cartId, orderType, paymentType));
        }

        [Fact]
        public void SetProducts_Should_Assign_Products_To_Order()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);
            var products = new List<OrderProducts>
            {
               OrderProducts.Create(1 , Guid.NewGuid(),50,10m,Colors.Gray,Sizes.US_41),
               OrderProducts.Create(1 , Guid.NewGuid(),30,30m,Colors.Red,Sizes.US_38),
            };

            // Act
            order.SetProducts(products);

            // Assert
            Assert.Equal(products, order.Products);
        }

        [Fact]
        public void SetShops_Should_Assign_Shops_To_Order()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);
            var shops = new List<ShopOrders>
            {
                ShopOrders.Create(1,Guid.NewGuid())
            };

            // Act
            order.SetShops(shops);

            // Assert
            Assert.Equal(shops, order.Shops);
        }

        [Fact]
        public void SetPhoneNumber_Should_Assign_PhoneNumber_To_Order()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);
            var phoneNumber = 1234567890L;

            // Act
            order.SetPhoneNumber(phoneNumber);

            // Assert
            Assert.Equal(phoneNumber, order.PhoneNumber);
        }

        [Fact]
        public void SetCardNumber_Should_Assign_CardNumber_To_Order()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);
            var cardNumber = "1234567812345678";

            // Act
            order.SetCardNumber(cardNumber);

            // Assert
            Assert.Equal(cardNumber, order.CardNumber);
        }

        [Fact]
        public void SetOrderStatus_Should_Change_Status_To_Paid()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);

            // Act
            order.SetOrderStatus();

            // Assert
            Assert.Equal(OrderStatus.Paid, order.Status);
        }

        [Fact]
        public void SetTotalAmount_Should_Calculate_TotalAmount_Correctly()
        {
            // Arrange
            var order = Order.Create(Guid.NewGuid(), OrderType.InStorePickup, PaymentType.Cash);
            var products = new List<OrderProducts>
    {
        OrderProducts.Create(order.Id, Guid.NewGuid(), 50, 10m, Colors.Gray, Sizes.US_41),
        OrderProducts.Create(order.Id, Guid.NewGuid(), 30, 30m, Colors.Red, Sizes.US_38),
    };
            order.SetProducts(products);

            // Act
            order.SetTotalAmount();

            // Assert
            Assert.Equal(40m, order.TotalAmount);
        }

    }

}