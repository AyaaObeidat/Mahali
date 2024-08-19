using Mahali.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class ShopTestClass
    {
        [Fact]
        public void Create_ValidShop_ShouldSucceed()
        {
            // Arrange
            var name = "ValidShop";
            var email = "valid@example.com";
            var password = "Valid123!";
            var phoneNumber = 1234567890;

            // Act
            var shop = Shop.Create(name, email, password, phoneNumber);

            // Assert
            Assert.NotNull(shop);
            Assert.Equal(name, shop.Name);
            Assert.Equal(email, shop.Email);
            Assert.Equal(password, shop.Password);
            Assert.Equal(phoneNumber, shop.PhoneNumber);
        }

        [Theory]
        [InlineData("", "valid@example.com", "Valid123!", 1234567890)] // Empty name
        [InlineData("ValidShop", "", "Valid123!", 1234567890)] // Empty email
        [InlineData("ValidShop", "valid@example.com", "", 1234567890)] // Empty password
        public void Create_InvalidShop_ShouldThrowArgumentNullException(string name, string email, string password, long phoneNumber)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Shop.Create(name, email, password, phoneNumber));
        }

        [Fact]
        public void Create_InvalidEmailFormat_ShouldThrowValidationException()
        {
            // Arrange
            var name = "ValidShop";
            var email = "invalid-email"; // Invalid email format
            var password = "Valid123!";
            var phoneNumber = 1234567890;

            // Act & Assert
            var shop = Shop.Create(name, email, password,phoneNumber);
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(shop);

            bool isValid = Validator.TryValidateObject(shop, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email must be between 14 and 25 characters long.");
        }

        [Fact]
        public void Create_InvalidPhoneNumberLength_ShouldThrowValidationException()
        {
            // Arrange
            var name = "ValidShop";
            var email = "valid@example.com";
            var password = "Valid123!";
            var phoneNumber = 123456; // Invalid phone number length

            // Act
            var shop = Shop.Create(name, email, password, phoneNumber);
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(shop);

            bool isValid = Validator.TryValidateObject(shop, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Phone number must be exactly 10 digits.");
        }
        [Fact]
        public void SetDescription_ValidDescription_ShouldUpdateDescription()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newDescription = "Updated shop description.";

            // Act
            shop.SetDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, shop.Description);
        }

        [Fact]
        public void SetEmail_ValidEmail_ShouldUpdateEmail()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newEmail = "updated@example.com";

            // Act
            shop.SetEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, shop.Email);
        }

        [Fact]
        public void SetPassword_ValidPassword_ShouldUpdatePassword()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newPassword = "Updated456!";

            // Act
            shop.SetPassword(newPassword);

            // Assert
            Assert.Equal(newPassword, shop.Password);
        }

        [Fact]
        public void SetPhoneNumber_ValidPhoneNumber_ShouldUpdatePhoneNumber()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newPhoneNumber = 9876543210;

            // Act
            shop.SetPhoneNumber(newPhoneNumber);

            // Assert
            Assert.Equal(newPhoneNumber, shop.PhoneNumber);
        }

        [Fact]
        public void SetLocationId_ValidLocationId_ShouldUpdateLocationId()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newLocationId = Guid.NewGuid();

            // Act
            shop.SetLocationId(newLocationId);

            // Assert
            Assert.Equal(newLocationId, shop.LocationId);
        }

        [Fact]
        public void SetOrders_ValidOrders_ShouldUpdateOrders()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newOrders = new List<ShopOrders>();

            // Act
            shop.SetOrders(newOrders);

            // Assert
            Assert.Equal(newOrders, shop.Orders);
        }

        [Fact]
        public void SetReviews_ValidReviews_ShouldUpdateReviews()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newReviews = new List<ReviewRequest>();

            // Act
            shop.SetReviews(newReviews);

            // Assert
            Assert.Equal(newReviews, shop.Reviews);
        }

        [Fact]
        public void SetProducts_ValidProducts_ShouldUpdateProducts()
        {
            // Arrange
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var newProducts = new List<Product>();

            // Act
            shop.SetProducts(newProducts);

            // Assert
            Assert.Equal(newProducts, shop.Products);
        }
    }
}
