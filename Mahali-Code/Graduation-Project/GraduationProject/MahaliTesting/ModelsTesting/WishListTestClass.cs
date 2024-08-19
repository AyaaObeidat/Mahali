using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class WishListTestClass
    {
        [Fact]
        public void Create_ValidWishList_ShouldSucceed()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act
            var wishList = WishList.Create(customerId);

            // Assert
            Assert.NotNull(wishList);
            Assert.Equal(customerId, wishList.CustomerId);
        }

        [Fact]
        public void Create_EmptyCustomerId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var customerId = Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WishList.Create(customerId));
        }

        [Fact]
        public void SetProducts_ValidProducts_ShouldUpdateProducts()
        {
            // Arrange
            var wishList = WishList.Create(Guid.NewGuid());
            var products = new List<WishListProducts>();

            // Act
            wishList.SetProducts(products);

            // Assert
            Assert.Equal(products, wishList.Products);
        }
    }
}
