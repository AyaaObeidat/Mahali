using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class AboutUsTestClass
    {
        [Fact]
        public void Create_ValidAboutUs_ShouldSucceed()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var contentBody = "Valid content body.";

            // Act
            var aboutUs = AboutUs.Create(adminId, contentBody);

            // Assert
            Assert.NotNull(aboutUs);
            Assert.Equal(adminId, aboutUs.AdminId);
            Assert.Equal(contentBody, aboutUs.ContentBody);
        }

        [Fact]
        public void Update_ContentBody_ShouldSucceed()
        {
            // Arrange
            var aboutUs = AboutUs.Create(Guid.NewGuid(), "Initial content body.");
            var newContentBody = "Updated content body.";

            // Act
            aboutUs.SetContentBody(newContentBody);

            // Assert
            Assert.Equal(newContentBody, aboutUs.ContentBody);
        }

        [Fact]
        public void Create_EmptyContentBody_ShouldThrowArgumentNullException()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            string contentBody = "";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => AboutUs.Create(adminId,contentBody));
        }

        [Fact]
        public void Create_EmptyAdminId_ShouldThrowArgumentNullException()
        {
            // Arrange
            Guid adminId = Guid.Empty;
            var contentBody = "Valid content body.";

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => AboutUs.Create(adminId, contentBody));
            Assert.Equal("adminId", exception.ParamName);
        }

        [Fact]
        public void Update_NullContentBody_ShouldSucceed()
        {
            // Arrange
            var aboutUs = AboutUs.Create(Guid.NewGuid(), "Initial content body.");
            string newContentBody = null!;

            // Act
            aboutUs.SetContentBody(newContentBody);

            // Assert
            Assert.Null(aboutUs.ContentBody);
        }
    }
}

