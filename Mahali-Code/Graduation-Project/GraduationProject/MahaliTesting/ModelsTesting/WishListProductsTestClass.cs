using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class WishListProductsTestClass
    {
        [Fact]
        public void Create_ValidWishListProducts_ShouldSucceed()
        {
            // Arrange
            var wishListId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            // Act
            var wishListProducts = WishListProducts.Create(wishListId, productId);

            // Assert
            Assert.NotNull(wishListProducts);
            Assert.Equal(wishListId, wishListProducts.WishListId);
            Assert.Equal(productId, wishListProducts.ProductId);
        }

        [Theory]
        [InlineData("", "valid-product-id")] // Empty WishListId
        [InlineData("valid-wishlist-id", "")] // Empty ProductId
        public void Create_InvalidWishListProducts_ShouldThrowArgumentNullException(string wishListId, string productId)
        {
            // Arrange
            var guidWishListId = Guid.TryParse(wishListId, out Guid resultWishListId) ? resultWishListId : Guid.Empty;
            var guidProductId = Guid.TryParse(productId, out Guid resultProductId) ? resultProductId : Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => WishListProducts.Create(guidWishListId, guidProductId));
        }
    }
}
