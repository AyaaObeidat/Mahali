using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class ReviewRequestTestClass
    {
        [Fact]
        public void Create_ValidReviewRequest_ShouldCreateInstance()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var reviewContentBody = "This is a valid review.";

            // Act
            var reviewRequest = ReviewRequest.Create(customerId, productId, reviewContentBody);

            // Assert
            Assert.NotNull(reviewRequest);
            Assert.Equal(customerId, reviewRequest.CustomerId);
            Assert.Equal(productId, reviewRequest.ProductId);
            Assert.Equal(reviewContentBody, reviewRequest.ReviewContentBody);
            Assert.Equal(RequestStatus.Pending, reviewRequest.Status);
        }

        [Theory]
        [InlineData("", "ProductId", "This is a valid review.")]
        [InlineData("CustomerId", "", "This is a valid review.")]
        [InlineData("CustomerId", "ProductId", "")]
        [InlineData("CustomerId", "ProductId", null)]
        public void Create_InvalidReviewRequest_ShouldThrowArgumentNullException(string customerId, string productId, string reviewContentBody)
        {
            // Arrange
            var customerIdGuid = string.IsNullOrEmpty(customerId) ? Guid.Empty : Guid.NewGuid();
            var productIdGuid = string.IsNullOrEmpty(productId) ? Guid.Empty : Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ReviewRequest.Create(customerIdGuid, productIdGuid, reviewContentBody));
        }

        [Fact]
        public void SetReviewContentBody_ValidContent_ShouldUpdateContentBody()
        {
            // Arrange
            var reviewRequest = ReviewRequest.Create(Guid.NewGuid(), Guid.NewGuid(), "Initial review content");
            var newContentBody = "Updated review content";

            // Act
            reviewRequest.SetReviewContentBody(newContentBody);

            // Assert
            Assert.Equal(newContentBody, reviewRequest.ReviewContentBody);
        }

        [Fact]
        public void SetRequestStatus_ValidStatus_ShouldUpdateStatus()
        {
            // Arrange
            var reviewRequest = ReviewRequest.Create(Guid.NewGuid(), Guid.NewGuid(), "Review content");
            var newStatus = RequestStatus.Approved;

            // Act
            reviewRequest.SetRequestStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, reviewRequest.Status);
        }
    }
}
