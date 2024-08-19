using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class CartTestClass
    {

        [Fact]
        public void Create_ValidCart_ShouldSucceed()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var cart = Cart.Create(customerId);

            // Assert
            Assert.NotNull(cart);
            Assert.Equal(customerId, cart.CustomerId);
        }

        [Fact]
        public void Create_EmptyCustomerId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var customerId = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Cart.Create(customerId));
        }

        [Fact]
        public void SetProducts_ValidProducts_ShouldUpdateProducts()
        {
            // Arrange
            var cart = Cart.Create(Guid.NewGuid());
            var products = new List<CartProducts>();

            // Act
            cart.SetProducts(products);

            // Assert
            Assert.Equal(products, cart.Products);
        }

        [Fact]
        public void SetTotalAmount_ValidProducts_ShouldUpdateTotalAmount()
        {
            // Arrange
            var cart = Cart.Create(Guid.NewGuid());
            var products = new List<CartProducts>();
           

            // Act
            cart.SetProducts(products);
            cart.SetTotalAmount();

            // Assert
            Assert.Equal(0.0m, cart.TotalAmount);
        }

        [Fact]
        public void SetTotalAmount_NullProducts_ShouldThrowArgumentNullException()
        {
            // Arrange
            var cart = Cart.Create(Guid.NewGuid());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => cart.SetTotalAmount());
        }
    }
}
