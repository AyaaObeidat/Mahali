using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using MahaliDtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AllEnums.AllEnums;

namespace MahaliTesting.ServicesTesting
{
    public class ReportServiceTestClass
    {
        private readonly Mock<IReportInterface> _reportInterfaceMock;
        private readonly Mock<IShopInterface> _shopInterfaceMock;
        private readonly Mock<IAdminInterface> _adminInterfaceMock;
        private readonly Mock<IShopRequestInterface> _shopRequestInterfaceMock;
        private readonly ReportService _reportService;

        public ReportServiceTestClass()
        {
            _reportInterfaceMock = new Mock<IReportInterface>();
            _shopInterfaceMock = new Mock<IShopInterface>();
            _adminInterfaceMock = new Mock<IAdminInterface>();
            _shopRequestInterfaceMock = new Mock<IShopRequestInterface>();

            _reportService = new ReportService(
                _reportInterfaceMock.Object,
                _shopInterfaceMock.Object,
                _adminInterfaceMock.Object,
                _shopRequestInterfaceMock.Object
            );
        }

        [Fact]
        public async Task WriteReportAsync_Should_Add_Report_When_Request_Is_Approved()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var parameters = new ReportCreateParameters
            {
                ShopId = shopId,
                ReportText = "Test report text"
            };
            var admin = Admin.Create("admin", "admin@example.com", "Admin123!");
            var shop = Shop.Create("ValidShop", "valid@example.com", "Valid123!", 1234567890);
            var request = ShopRequest.Create(adminId, shopId);
            request.SetRequestStatus(RequestStatus.Approved);
            
            _adminInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(adminId);
            _adminInterfaceMock.Setup(x => x.GetByIdAsync(adminId)).ReturnsAsync(admin);
            _shopInterfaceMock.Setup(x => x.GetByIdAsync(shopId)).ReturnsAsync(shop);
            _shopRequestInterfaceMock.Setup(x => x.GetRequestByShopIdAsync(shopId)).ReturnsAsync(request);

            // Act
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _reportService.WriteReportAsync(parameters));
        }

        [Fact]
        public async Task GetAllReportsAsync_Should_Return_All_Reports()
        {
            // Arrange
            
            var adminId = Guid.NewGuid();

            var shops = new List<Shop>();
            var shop1 = Shop.Create("Shop 1", "shop1@test.com", "Shop123!", 1234567890);
            var shop2 = Shop.Create("Shop 2", "shop2@test.com", "Shop123!", 9876543210);
            shops.Add(shop1 );
            shops.Add(shop2);

            var reports = new List<Report>();
            var report1 = Report.Create(adminId,Guid.NewGuid(), "Report 1");
            var report2 = Report.Create(adminId, Guid.NewGuid(), "Report 2");
            reports.Add(report1);
            reports.Add(report2);


            _reportInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(reports);
            _shopInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(shops);

            // Act
            var result = await _reportService.GetAllReportsAsync();

            // Assert
            Assert.Equal(reports.Count, result.Count);

           
    }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Report_When_Report_Exists()
        {
            // Arrange
            var shopId = Guid.NewGuid();
            var reportId = Guid.NewGuid();
            var report = Report.Create(Guid.NewGuid(), shopId, "Report text");
            var shop = Shop.Create("Test Shop","shop@test.com","Shop123!" ,1234567890);

            _reportInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Report> { report });
            _shopInterfaceMock.Setup(x => x.GetByIdAsync(shopId)).ReturnsAsync(shop);

            // Act
            var result = await _reportService.GetByIdAsync(new ReportGetByParameters { ShopID = shopId });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(report.ReportText, result.ReportText);
            Assert.Equal(shop.Id, result.Shop.Id);
            Assert.Equal(shop.Name, result.Shop.Name);
            Assert.Equal(shop.Description, result.Shop.Description);
            Assert.Equal(shop.PhoneNumber, result.Shop.PhoneNumber);
            Assert.Equal(shop.Email, result.Shop.Email);
        
    }
    }
}
