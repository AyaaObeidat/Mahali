

using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Security;
using MahaliDtos;
using static AllEnums.AllEnums;

namespace Mahali.Services
{
    public class AdminService
    {
        private readonly IAdminInterface _adminInterface;
        private readonly IShopInterface _shopInterface;
        private readonly IShopRequestInterface _shopRequestInterface;
        private readonly IAboutUsInterface _aboutUsInterface;
        public AdminService(IAdminInterface adminInterface , IShopInterface shopInterface,  IShopRequestInterface shopRequestInterface , IAboutUsInterface aboutUsInterface )
        {
            _adminInterface = adminInterface;
            _shopInterface = shopInterface;
            _shopRequestInterface = shopRequestInterface;
            _aboutUsInterface = aboutUsInterface;
        }

        public async Task RegisterAsync(AdminRegister parameters)
        {
            var admins = await _adminInterface.GetAllAsync();
            if (admins.Count < 1)
            {
                var admin = Admin.Create(parameters.UserName, parameters.Email, MD5Hasher.ComputeHash(parameters.Password));
                await _adminInterface.AddAsync(admin);
               
            }
         
        }

        public async Task<AdminListItems?> LoginAsync(AdminLogin login)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            if (admin == null) return null;
            var reports = admin.Reports.Select(x => new ReportListItems
            {
                ReportText = x.ReportText,
                CreatedAt = x.CreatedAt,
                LastModifiedTime = x.LastModifiedTime,
            }).ToList();
            var requests = admin.ShopRequests.Select(x => new ShopRequestDetails
            {
                Id = x.Id,
                AdminId = x.AdminId,
                ShopId = x.ShopId,
                Status = x.Status,
            }).ToList();
            if ((login.UserName_Email == admin.UserName || login.UserName_Email==admin.Email) && MD5Hasher.ComputeHash(login.Password)==admin.Password)
            {
                return new AdminListItems
                {
                    
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Reports = reports,
                    ShopRequests = requests,

                };
            }
            return null;
        }

        public async Task ModifyAccountUserNameAsync(AdminUpdateParameters parameters)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            admin.SetUserName(parameters.UserName);
            await _adminInterface.UpdateAsync(admin);
        }

        public async Task ModifyAccountPasswordAsync(AdminUpdateParameters parameters)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            if (admin.Password == MD5Hasher.ComputeHash(parameters.CurrentPassword))
            {
                admin.SetPassword(MD5Hasher.ComputeHash(parameters.NewPassword));
                await _adminInterface.UpdateAsync(admin);
            }
            else return;
        }

        public async Task<List<ShopRequestListItems>> GetAllShopRequestAsync()
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            var shops = await _shopInterface.GetAllAsync();
            var shopAllInfo = shops.Select(x => new ShopDetails
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                LocationId = x.LocationId,
               
            }).ToList();
            return admin.ShopRequests.Select(x => new ShopRequestListItems
            {
                Id = x.Id,
                RequestStatus = x.Status,
                Shop = shopAllInfo.Find(n => n.Id == x.ShopId)
                
            }).ToList();
        }


        public async Task UpdateShopRequestStatusAsync(ShopRequestUpdateParameters parameters)
        {
            var shop = await _shopInterface.GetByIdAsync(parameters.ShopId);
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(shop.Id);

            if (request.Status == RequestStatus.Pending)
            {
                if (parameters.Status == RequestStatus.Approved) request.SetRequestStatus(RequestStatus.Approved);
                else if (parameters.Status == RequestStatus.Rejected) request.SetRequestStatus(RequestStatus.Rejected);
                else return;
                await _shopRequestInterface.UpdateAsync(request);
            }
            else return;
        }
        public async Task CreateAboutUsAsync(AboutUsCreateParameters  parameters)
        {
            var abouts =await _aboutUsInterface.GetAllAsync();
            if (abouts.Count  < 1)
            {
                var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
                var aboutUs = AboutUs.Create(admin.Id, parameters.ContentBody);
                await _aboutUsInterface.AddAsync(aboutUs);
            }
            else
            {
                AboutUsUpdateParameters updateOnAboutUs = new AboutUsUpdateParameters();
                updateOnAboutUs.ContentBody = parameters.ContentBody;
                await UpdateAboutUsContentBody(updateOnAboutUs);
            }
        }

        public async Task UpdateAboutUsContentBody(AboutUsUpdateParameters parameters)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            var aboutUs = await _aboutUsInterface.GetByIdAsync(await _aboutUsInterface.GetIdAsync());
            aboutUs.SetContentBody(parameters.ContentBody); 
            await _aboutUsInterface.UpdateAsync(aboutUs);
        }

        public async Task<AboutUsListItem> GetAboutUsAsync()
        {
            var aboutUs = await _aboutUsInterface.GetAllAsync();
            var firstAboutUs = aboutUs.FirstOrDefault();
            if (firstAboutUs != null)
            {
                return new AboutUsListItem
                {
                    AdminId = firstAboutUs.AdminId,
                    ContentBody = firstAboutUs.ContentBody,
                };
            }
            return new AboutUsListItem();
        }
    }
}
