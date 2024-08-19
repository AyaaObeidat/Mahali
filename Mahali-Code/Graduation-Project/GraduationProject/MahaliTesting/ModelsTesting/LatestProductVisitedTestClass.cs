using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class LatestProductVisitedTestClass
    {
        [Fact]
        public void Create_ValidInputs_ShouldCreateLatestProductsVisitedInstance()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            // Act
            var latestProductsVisited = LatestProductsVisited.Create(customerId, productId);

            // Assert
            Assert.NotNull(latestProductsVisited);
            Assert.Equal(customerId, latestProductsVisited.CustomerId);
            Assert.Equal(productId, latestProductsVisited.ProductId);
        }

        [Fact]
        public void Create_EmptyCustomerId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => LatestProductsVisited.Create(Guid.Empty, productId));
        }

        [Fact]
        public void Create_EmptyProductId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => LatestProductsVisited.Create(customerId, Guid.Empty));
        }

        [Fact]
        public void SetVisitedDateTime_ValidDateTime_ShouldUpdateVisitedDateTime()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var latestProductsVisited = LatestProductsVisited.Create(customerId, productId);
            var newDateTime = DateTime.Now.AddHours(-1);

            // Act
            latestProductsVisited.SetVisitedDateTime(newDateTime);

            // Assert
            Assert.Equal(newDateTime, latestProductsVisited.VisitedDateTime);
        }

        [Fact]
        public void SetVisitedDateTime_ValidDateTime_ShouldNotThrowException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var latestProductsVisited = LatestProductsVisited.Create(customerId, productId);
            var newDateTime = DateTime.Now.AddHours(-1);

            // Act & Assert
            Assert.Null(Record.Exception(() => latestProductsVisited.SetVisitedDateTime(newDateTime)));
        }
    }
}
