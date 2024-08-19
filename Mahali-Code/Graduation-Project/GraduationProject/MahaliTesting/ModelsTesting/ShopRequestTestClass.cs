using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ModelsTesting
{
    public class ShopRequestTestClass
    {

        [Fact]
        public void Create_ValidShopRequest_ShouldSucceed()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopId = Guid.NewGuid();

            // Act
            var shopRequest = ShopRequest.Create(adminId, shopId);

            // Assert
            Assert.NotNull(shopRequest);
            Assert.Equal(adminId, shopRequest.AdminId);
            Assert.Equal(shopId, shopRequest.ShopId);
            Assert.Equal(RequestStatus.Pending, shopRequest.Status);
        }

        [Theory]
        [InlineData("", "valid-shop-id")] // Empty admin ID
        [InlineData("valid-admin-id", "")] // Empty shop ID
        public void Create_InvalidShopRequest_ShouldThrowArgumentNullException(string adminId, string shopId)
        {
            // Arrange
            var guidAdminId = Guid.TryParse(adminId, out Guid resultAdminId) ? resultAdminId : Guid.Empty;
            var guidShopId = Guid.TryParse(shopId, out Guid resultShopId) ? resultShopId : Guid.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => ShopRequest.Create(guidAdminId, guidShopId));
        }

        [Fact]
        public void SetRequestStatus_ValidStatus_ShouldUpdateStatus()
        {
            // Arrange
            var shopRequest = ShopRequest.Create(Guid.NewGuid(), Guid.NewGuid());
            var newStatus = RequestStatus.Approved;

            // Act
            shopRequest.SetRequestStatus(newStatus);

            // Assert
            Assert.Equal(newStatus, shopRequest.Status);
        }
    }
}
