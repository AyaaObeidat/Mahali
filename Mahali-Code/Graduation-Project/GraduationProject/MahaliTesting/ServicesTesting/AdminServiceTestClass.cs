using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Services;
using MahaliDtos;
using Moq;
using System.Reflection;
using static AllEnums.AllEnums;

namespace MahaliTesting.ServicesTesting
{
    public class AdminServiceTestClass
    {
        private readonly Mock<IAdminInterface> _adminInterfaceMock;
        private readonly Mock<IShopInterface> _shopInterfaceMock;
        private readonly Mock<IShopRequestInterface> _shopRequestInterfaceMock;
        private readonly Mock<IAboutUsInterface> _aboutUsInterfaceMock;
        private readonly AdminService _adminService;

        public AdminServiceTestClass()
        {
            _adminInterfaceMock = new Mock<IAdminInterface>();
            _shopInterfaceMock = new Mock<IShopInterface>();
            _shopRequestInterfaceMock = new Mock<IShopRequestInterface>();
            _aboutUsInterfaceMock = new Mock<IAboutUsInterface>();

            _adminService = new AdminService(
                _adminInterfaceMock.Object,
                _shopInterfaceMock.Object,
                _shopRequestInterfaceMock.Object,
                _aboutUsInterfaceMock.Object
            );
        }

        [Fact]
        public async Task RegisterAsync_ShouldAddAdmin_WhenNoAdminsExist()
        {
            // Arrange
            _adminInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Admin>());
            var parameters = new AdminRegister
            {
                UserName = "admin",
                Email = "admin@example.com",
                Password = "Admin123!"
            };

            // Act
            await _adminService.RegisterAsync(parameters);

            // Assert
            _adminInterfaceMock.Verify(x => x.AddAsync(It.IsAny<Admin>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnAdminListItems_WhenCredentialsAreCorrect()
        {
            // Arrange
            var admin = Admin.Create("admin", "admin@example.com", "Admin123!");
            admin.SetReports(new List<Report>());
            admin.SetShopRequests(new List<ShopRequest>());

            _adminInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(admin.Id);
            _adminInterfaceMock.Setup(x => x.GetByIdAsync(admin.Id)).ReturnsAsync(admin);

            var login = new AdminLogin
            {
                UserName_Email = "admin",
                Password = "Admin123!"
            };

            // Act
            var result = await _adminService.LoginAsync(login);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("admin", result.UserName);
            Assert.Equal("admin@example.com", result.Email);
            Assert.Empty(result.Reports);
            Assert.Empty(result.ShopRequests);
        
    }

        [Fact]
        public async Task ModifyAccountUserNameAsync_ShouldUpdateUserName()
        {
            // Arrange
            var admin = CreateAdmin("oldAdmin", "admin@example.com", "Admin123!");
            _adminInterfaceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(admin);
            _adminInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(admin.Id);

            var parameters = new AdminUpdateParameters
            {
                UserName = "newAdmin"
            };

            // Act
            await _adminService.ModifyAccountUserNameAsync(parameters);

            // Assert
            _adminInterfaceMock.Verify(x => x.UpdateAsync(It.Is<Admin>(a => a.UserName == "newAdmin")), Times.Once);
        }

        [Fact]
        public async Task ModifyAccountPasswordAsync_ShouldUpdatePassword_WhenCurrentPasswordMatches()
        {
            // Arrange
            var admin = Admin.Create("admin", "admin@example.com", "Admin123!");
            admin.SetReports(new List<Report>());
            admin.SetShopRequests(new List<ShopRequest>());

            _adminInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(admin.Id);
            _adminInterfaceMock.Setup(x => x.GetByIdAsync(admin.Id)).ReturnsAsync(admin);

            var parameters = new AdminUpdateParameters
           {
                CurrentPassword = "Admin123!",
                NewPassword = "Admin123@"
            };
           // Act
            await _adminService.ModifyAccountPasswordAsync(parameters);

            // Assert
           Assert.Equal(parameters.NewPassword, admin.Password);
        }

   
        [Fact]
        public async Task GetAllShopRequestAsync_ShouldReturnAllShopRequests()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopRequest1 = ShopRequest.Create(Guid.NewGuid(), Guid.NewGuid());
            var shopRequest2 = ShopRequest.Create(Guid.NewGuid(), Guid.NewGuid());
            var shopRequests = new List<ShopRequest> { shopRequest1, shopRequest2 };

            var admin = CreateAdmin("admin", "admin@example.com", "Admin123!");
            admin.SetShopRequests(shopRequests);

            var shops = new List<Shop>
    {
       Shop.Create ("Shop 1",  "shop1@example.com","Password1!",1234567890 ),
        Shop.Create("Shop 2", "shop2@example.com", "Password2!", 9876543210)
    };

            _adminInterfaceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(admin);
            _adminInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(adminId);
            _shopInterfaceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(shops);

            // Act
            var result = await _adminService.GetAllShopRequestAsync();

            // Assert
            Assert.Equal(shopRequests.Count, result.Count);

            // Additional assertions to check the details of each request

            Assert.Equal(shopRequests.Count, result.Count);
        
    }


        [Fact]
        public async Task UpdateAboutUsContentBody_ShouldUpdateContentBody()
        {
            // Arrange
            var aboutUs = CreateAboutUs("Initial Content");
            _aboutUsInterfaceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(aboutUs);
            _aboutUsInterfaceMock.Setup(x => x.GetIdAsync()).ReturnsAsync(aboutUs.Id);

            var parameters = new AboutUsUpdateParameters
            {
                ContentBody = "Updated Content"
            };

            // Act
            await _adminService.UpdateAboutUsContentBody(parameters);

            // Assert
            _aboutUsInterfaceMock.Verify(x => x.UpdateAsync(It.Is<AboutUs>(a => a.ContentBody == "Updated Content")), Times.Once);
        }

        private Admin CreateAdmin(string userName, string email, string password)
        {
            var admin = (Admin)Activator.CreateInstance(typeof(Admin), true);
            SetPrivateProperty(admin, "UserName", userName);
            SetPrivateProperty(admin, "Email", email);
            SetPrivateProperty(admin, "Password", password);
            SetPrivateProperty(admin, "Reports", new List<Report>());
            SetPrivateProperty(admin, "ShopRequests", new List<ShopRequest>());
            return admin;
        }

       

        private AboutUs CreateAboutUs(string contentBody)
        {
            var aboutUs = (AboutUs)Activator.CreateInstance(typeof(AboutUs), true);
            SetPrivateProperty(aboutUs, "ContentBody", contentBody);
            return aboutUs;
        }

        private void SetPrivateProperty(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (property != null)
            {
                property.SetValue(obj, value);
            }
        }
    }
}
