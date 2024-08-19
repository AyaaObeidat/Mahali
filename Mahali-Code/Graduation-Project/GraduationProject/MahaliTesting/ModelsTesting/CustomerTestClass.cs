using Mahali.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class CustomerTestClass
    {
        [Fact]
        public void Create_ValidCustomer_ShouldSucceed()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john@example.com";
            var password = "Password123!";

            // Act
            var customer = Customer.Create(firstName, lastName, email, password);

            // Assert
            Assert.NotNull(customer);
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(password, customer.Password);
        }

        [Theory]
        [InlineData("", "Doe", "john@example.com", "Password123!")]
        [InlineData("John", "", "john@example.com", "Password123!")]
        [InlineData("John", "Doe", "", "Password123!")]
        [InlineData("John", "Doe", "john@example.com", "")]
        public void Create_InvalidCustomer_ShouldThrowArgumentNullException(string firstName, string lastName, string email, string password)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Customer.Create(firstName, lastName, email, password));
        }

        [Fact]
        public void SetFirstName_ValidFirstName_ShouldUpdateFirstName()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var newFirstName = "Jane";

            // Act
            customer.SetFirstName(newFirstName);

            // Assert
            Assert.Equal(newFirstName, customer.FirstName);
        }

        [Fact]
        public void SetLastName_ValidLastName_ShouldUpdateLastName()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var newLastName = "Smith";

            // Act
            customer.SetLastName(newLastName);

            // Assert
            Assert.Equal(newLastName, customer.LastName);
        }

        [Fact]
        public void SetEmail_ValidEmail_ShouldUpdateEmail()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var newEmail = "jane@example.com";

            // Act
            customer.SetEmail(newEmail);

            // Assert
            Assert.Equal(newEmail, customer.Email);
        }

        [Fact]
        public void SetPassword_ValidPassword_ShouldUpdatePassword()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var newPassword = "NewPassword123!";

            // Act
            customer.SetPassword(newPassword);

            // Assert
            Assert.Equal(newPassword, customer.Password);
        }
        [Fact]
        public void Create_ValidCustomer_ShouldNotThrowException()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john@example.com";
            var password = "Password123!";

            // Act & Assert
            Assert.Null(Record.Exception(() => Customer.Create(firstName, lastName, email, password)));
        }

        [Fact]
        public void Create_InvalidEmailFormat_ShouldThrowValidationException()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var invalidEmail = "invalidemail"; // Invalid email format
            var password = "Password123!";

            // Act & Assert
            var customer = Customer.Create(firstName,lastName, invalidEmail, password);
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(customer);

            bool isValid = Validator.TryValidateObject(customer, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "Email must be between 14 and 25 characters long.");
        }

        [Fact]
        public void SetFirstName_NullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => customer.SetFirstName(null));
        }

        [Fact]
        public void SetLastName_EmptyValue_ShouldUpdateLastName()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var newLastName = "";

            // Act
            customer.SetLastName(newLastName);

            // Assert
            Assert.Equal(newLastName, customer.LastName);
        }

        [Fact]
        public void SetPassword_InvalidPasswordFormat_ShouldThrowValidationException()
        {
            // Arrange
            var customer = Customer.Create("John", "Doe", "john@example.com", "Password123!");
            var invalidPassword = "pass"; // Less than 8 characters

            
            // Assert
            Assert.Throws<ValidationException>(() => customer.SetPassword(invalidPassword));
        }
    }
}
