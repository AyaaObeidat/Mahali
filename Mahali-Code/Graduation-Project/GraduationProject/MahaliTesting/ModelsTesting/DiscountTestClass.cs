using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MahaliTesting.ModelsTesting
{
    public class DiscountTestClass
    {
        [Fact]
        public void Create_ValidDiscount_ShouldSucceed()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var discountPercentage = 10.5m;
            var startDate = "2024-06-01 00:00:00";
            var endDate = "2024-06-15 00:00:00";

            // Act
            var discount = Discount.Create(productId, discountPercentage, startDate, endDate);

            // Assert
            Assert.NotNull(discount);
            Assert.Equal(productId, discount.ProductId);
            Assert.Equal(discountPercentage, discount.DiscountPercentage);
            Assert.Equal(DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), discount.StartDate);
            Assert.Equal(DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), discount.EndDate);
        }

        [Theory]
        [InlineData(-1)] // Negative discount percentage
        public void Create_InvalidDiscountPercentage_ShouldThrowArgumentOutOfRangeException(decimal discountPercentage)
        {
            // Arrange
            var productId = Guid.NewGuid();
            var startDate = "2024-06-01 00:00:00";
            var endDate = "2024-06-15 00:00:00";

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Discount.Create(productId,discountPercentage,startDate,endDate));
        }

        [Fact]
        public void Create_EmptyProductId_ShouldThrowArgumentException()
        {
            // Arrange
            var productId = Guid.Empty;
            var discountPercentage = 10.5m;
            var startDate = "2024-06-01 00:00:00";
            var endDate = "2024-06-15 00:00:00";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Discount.Create(productId, discountPercentage, startDate, endDate));
        }

       
        [Fact]
        public void SetDiscountPercentage_ValidPercentage_ShouldUpdateDiscountPercentage()
        {
            // Arrange
            var discount = Discount.Create(Guid.NewGuid(), 10.5m, "2024-06-01 00:00:00", "2024-06-15 00:00:00");
            var newDiscountPercentage = 20.0m;

            // Act
            discount.SetDiscountPercentage(newDiscountPercentage);

            // Assert
            Assert.Equal(newDiscountPercentage, discount.DiscountPercentage);
        }

        [Fact]
        public void SetStartDate_ValidDate_ShouldUpdateStartDate()
        {
            // Arrange
            var discount = Discount.Create(Guid.NewGuid(), 10.5m, "2024-06-01 00:00:00", "2024-06-15 00:00:00");
            var newStartDate = "2024-06-16 00:00:00";

            // Act
            discount.SetStartDate(newStartDate);

            // Assert
            Assert.Equal(DateTime.ParseExact(newStartDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), discount.StartDate);
        }

        [Fact]
        public void SetEndDate_ValidDate_ShouldUpdateEndDate()
        {
            // Arrange
            var discount = Discount.Create(Guid.NewGuid(), 10.5m, "2024-06-01 00:00:00", "2024-06-15 00:00:00");
            var newEndDate = "2024-06-30 00:00:00";

            // Act
            discount.SetEndDate(newEndDate);

            // Assert
            Assert.Equal(DateTime.ParseExact(newEndDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture), discount.EndDate);
        }
    }
}
