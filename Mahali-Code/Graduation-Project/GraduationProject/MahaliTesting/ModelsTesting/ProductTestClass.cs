using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class ProductTestClass
    {

        [Fact]
        public void Create_ValidProduct_ShouldSucceed()
        {
            // Arrange
            var name = "ValidProduct";
            var description = "This is a valid product description.";
            var quantity = 10;
            var price = 20.5m;
            var imageUri = "https://example.com/image.jpg";
            var shopId = Guid.NewGuid();

            // Act
            var product = Product.Create(name, description, quantity, price, imageUri, shopId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(quantity, product.Quantity);
            Assert.Equal(price, product.Price);
            Assert.Equal(imageUri, product.ImageUri);
            Assert.Equal(shopId, product.ShopId);
        }

        [Theory]
        [InlineData("", "ValidDescription", 10, 20.5, "https://example.com/image.jpg", "00000000-0000-0000-0000-000000000000")] // Empty name
        [InlineData("ValidName", "", 10, 20.5, "https://example.com/image.jpg", "00000000-0000-0000-0000-000000000000")] // Empty description
        [InlineData("ValidName", "ValidDescription", 10, 20.5, "", "00000000-0000-0000-0000-000000000000")] // Empty image URI
        public void Create_InvalidProduct_ShouldThrowNullException(string name, string description, int quantity, decimal price, string imageUri, string shopId)
        {
            // Arrange
            var shopGuid = Guid.Parse(shopId);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Product.Create(name, description, quantity, price, imageUri, shopGuid));
        }
        [Theory]
        [InlineData("ValidName", "ValidDescription", -1, 20.5, "https://example.com/image.jpg", "00000000-0000-0000-0000-000000000000")] // Negative quantity
        [InlineData("ValidName", "ValidDescription", 10, -1, "https://example.com/image.jpg", "00000000-0000-0000-0000-000000000000")] // Negative price
        public void Create_InvalidProduct_ShouldThrowOutOfRangeException(string name, string description, int quantity, decimal price, string imageUri, string shopId)
        {
            // Arrange
            var shopGuid = Guid.Parse(shopId);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Product.Create(name, description, quantity, price, imageUri, shopGuid));
        }

        [Fact]
        public void SetName_ValidName_ShouldUpdateName()
        {
            // Arrange
            var product = Product.Create("InitialName", "ValidDescription", 10, 20.5m, "https://example.com/image.jpg", Guid.NewGuid());
            var newName = "UpdatedName";

            // Act
            product.SetName(newName);

            // Assert
            Assert.Equal(newName, product.Name);
        }

        [Fact]
        public void SetDescription_ValidDescription_ShouldUpdateDescription()
        {
            // Arrange
            var product = Product.Create("ValidName", "InitialDescription", 10, 20.5m, "https://example.com/image.jpg", Guid.NewGuid());
            var newDescription = "Updated description.";

            // Act
            product.SetDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, product.Description);
        }

        [Fact]
        public void SetQuantity_ValidQuantity_ShouldUpdateQuantity()
        {
            // Arrange
            var product = Product.Create("ValidName", "ValidDescription", 10, 20.5m, "https://example.com/image.jpg", Guid.NewGuid());
            var newQuantity = 20;

            // Act
            product.SetQuantity(newQuantity);

            // Assert
            Assert.Equal(newQuantity, product.Quantity);
        }

        [Fact]
        public void SetPrice_ValidPrice_ShouldUpdatePrice()
        {
            // Arrange
            var product = Product.Create("ValidName", "ValidDescription", 10, 20.5m, "https://example.com/image.jpg", Guid.NewGuid());
            var newPrice = 25.75m;

            // Act
            product.SetPrice(newPrice);

            // Assert
            Assert.Equal(newPrice, product.Price);
        }

        [Fact]
        public void SetImageUri_ValidImageUri_ShouldUpdateImageUri()
        {
            // Arrange
            var product = Product.Create("ValidName", "ValidDescription", 10, 20.5m, "https://example.com/initial_image.jpg", Guid.NewGuid());
            var newImageUri = "https://example.com/updated_image.jpg";

            // Act
            product.SetImageUri(newImageUri);

            // Assert
            Assert.Equal(newImageUri, product.ImageUri);
        }

        [Fact]
        public void SetCategoryId_ValidCategoryId_ShouldUpdateCategoryId()
        {
            // Arrange
            var product = Product.Create("ValidName", "ValidDescription", 10, 20.5m, "https://example.com/image.jpg", Guid.NewGuid());
            var newCategoryId = Guid.NewGuid();

            // Act
            product.SetCategoryId(newCategoryId);

            // Assert
            Assert.Equal(newCategoryId, product.CategoryId);
        }
    }
}
