using Mahali.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace MahaliTesting.ModelsTesting
{
    public class AdminTestClass
    {
        [Fact]
        public void Create_ValidParameters_ShouldReturnAdmin()
        {
            // Arrange
            var userName = "ValidUser";
            var email = "validemail@example.com";
            var password = "Valid123!";

            // Act
            var admin = Admin.Create(userName, email, password);

            // Assert
            Assert.NotNull(admin);
            Assert.Equal(userName, admin.UserName);
            Assert.Equal(email, admin.Email);
            Assert.Equal(password, admin.Password);
        }
        [Fact]
        public void Create_NullUserName_ShouldThrowArgumentNullException()
        {
            // Arrange
            string userName = null!;
            var email = "validemail@example.com";
            var password = "Valid123!";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Admin.Create(userName, email, password));
        }

        [Fact]
        public void Create_InvalidEmail_ShouldThrowValidationException()
        {
            // Arrange
            var userName = "ValidUser";
            var email = "sh"; // Invalid email
            var password = "Valid123!";

            // Act
            var admin = Admin.Create(userName, email, password);
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(admin);

            bool isValid = Validator.TryValidateObject(admin, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email must be between 14 and 25 characters long.");
        }

        [Fact]
        public void SetUserName_ValidUserName_ShouldUpdateUserName()
        {
            // Arrange
            var admin = Admin.Create("ValidUser", "validemail@example.com", "Valid123!");
            var newUserName = "NewUserName";

            // Act
            admin.SetUserName(newUserName);

            // Assert
            Assert.Equal(newUserName, admin.UserName);
        }

        [Fact]
        public void SetEmail_ValidEmail_ShouldUpdateEmail()
        {
            // Arrange
            var admin = Admin.Create("ValidUser", "validemail@example.com", "Valid123!");
            var newEmail = "newemail@example.com";

            // Act
            admin.SetEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, admin.Email);
        }

        [Fact]
        public void SetPassword_ValidPassword_ShouldUpdatePassword()
        {
            // Arrange
            var admin = Admin.Create("ValidUser", "validemail@example.com", "Valid123!");
            var newPassword = "NewValid123!";

            // Act
            admin.SetPassword(newPassword);

            // Assert
            Assert.Equal(newPassword, admin.Password);
        }

        [Fact]
        public void SetReports_ValidReports_ShouldUpdateReports()
        {
            // Arrange
            var admin = Admin.Create("ValidUser", "validemail@example.com", "Valid123!");
            var reports = new List<Report>();

            // Act
            admin.SetReports(reports);

            // Assert
            Assert.Equal(reports, admin.Reports);
        }

        [Fact]
        public void SetShopRequests_ValidRequests_ShouldUpdateShopRequests()
        {
            // Arrange
            var admin = Admin.Create("ValidUser", "validemail@example.com", "Valid123!");
            var requests = new List<ShopRequest>();

            // Act
            admin.SetShopRequests(requests);

            // Assert
            Assert.Equal(requests, admin.ShopRequests);
        }
    }
}