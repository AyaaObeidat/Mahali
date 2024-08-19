
using Mahali.Models;
using Mahali.Repositories.Implementations;
using Mahali.Repositories.Interfaces;
using MahaliDtos;
using System.Data.Common;
using System.Reflection.Metadata;
using static AllEnums.AllEnums;

namespace Mahali.Services
{
    public class ReportService
    {
        private readonly IReportInterface _reportInterface;
        private readonly IShopInterface _shopInterface;
        private readonly IAdminInterface _adminInterface;
        private readonly IShopRequestInterface _shopRequestInterface;

        public ReportService(IReportInterface reportInterface , IShopInterface shopInterface, IAdminInterface adminInterface,IShopRequestInterface shopRequestInterface)
        {
            _reportInterface = reportInterface;
            _shopInterface = shopInterface;
            _adminInterface = adminInterface;
            _shopRequestInterface = shopRequestInterface;
        }
        public async Task WriteReportAsync(ReportCreateParameters parameters)
        {
            var admin = await  _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            var shop = await _shopInterface.GetByIdAsync(parameters.ShopId);
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(shop.Id);
            var reports = await _reportInterface.GetAllAsync();
            if (request.Status == RequestStatus.Approved)
            {
                if (reports.Count > 0)
                {
                    foreach (var report in reports)
                    {
                        if (report.ShopId == shop.Id)
                        {
                            ReportUpdateParameters updateOnReport = new ReportUpdateParameters();
                            updateOnReport.ShopId = shop.Id;
                            updateOnReport.ReportText = parameters.ReportText;
                            await EditReportTextAsync(updateOnReport);
                            return;
                        }

                    }
                }
                    var newReport = Report.Create(admin.Id, shop.Id, parameters.ReportText);
                    await _reportInterface.AddAsync(newReport);
            }

        }

        public async Task EditReportTextAsync(ReportUpdateParameters parameters)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            var shop = await _shopInterface.GetByIdAsync(parameters.ShopId);
            if (shop == null) return;
            var reports = await _reportInterface.GetAllAsync();
            var report = reports.FirstOrDefault(x => x.ShopId == shop.Id && x.AdminId == admin.Id);
            if (report == null) return;
            report.SetReportText(parameters.ReportText);
            report.SetLastModifiedTime();
            await _reportInterface.UpdateAsync(report);
        }

        public async Task DeleteReportAsync(ReportDeleteParameters reportDelete)
        {
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            var shop = await _shopInterface.GetByIdAsync(reportDelete.ShopId);
            if (shop == null) return;
            var reports = await _reportInterface.GetAllAsync();
            var report = reports.FirstOrDefault(x => x.ShopId == shop.Id && x.AdminId == admin.Id);
            if (report == null) return;
            await _reportInterface.DeleteAsync(report);
        }

        public async Task<List<ReportListItems>> GetAllReportsAsync()
        {
            var reports = await _reportInterface.GetAllAsync();
            var shops = await _shopInterface.GetAllAsync();
            return reports.Select(x => new ReportListItems
            {
                Id = x.Id,
                Shop = shops.Select(x => new ShopDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description ,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email
                }).FirstOrDefault(n => n.Id == x.ShopId),

                ReportText = x.ReportText,
                CreatedAt = x.CreatedAt,
                LastModifiedTime = x.LastModifiedTime,

            }).ToList();
        }

        public async Task<ReportListItems?> GetByIdAsync(ReportGetByParameters parameters)
        {
            var reports = await _reportInterface.GetAllAsync();
            var shop =  await _shopInterface.GetByIdAsync(parameters.ShopID);

            foreach (var report in reports)
            {
                if (report.ShopId == parameters.ShopID)
                {
                    return new ReportListItems
                    {
                        Id = report.Id,
                        Shop = new ShopDetails
                        {
                            Id = shop.Id,
                            Name = shop.Name,
                            Description = shop.Description,
                            PhoneNumber = shop.PhoneNumber,
                            Email = shop.Email
                        },
                        ReportText = report.ReportText,
                        CreatedAt = report.CreatedAt,
                        LastModifiedTime = report.LastModifiedTime,

                    };
                }
            }
            return null;
           
        }
    }
}
