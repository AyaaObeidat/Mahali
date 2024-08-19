using Mahali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MahaliTesting.ModelsTesting
{
    public class ReportTestClass
    {

        [Fact]
        public void Create_ValidReport_ShouldSucceed()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var reportText = "This is a valid report.";

            // Act
            var report = Report.Create(adminId, shopId, reportText);

            // Assert
            Assert.NotNull(report);
            Assert.Equal(adminId, report.AdminId);
            Assert.Equal(shopId, report.ShopId);
            Assert.Equal(reportText, report.ReportText);
            Assert.NotEqual(default(DateTime), report.CreatedAt);
            Assert.NotEqual(default(DateTime), report.LastModifiedTime);
        }

        [Fact]
        public void Create_EmptyAdminId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var adminId = Guid.Empty;
            var shopId = Guid.NewGuid();
            var reportText = "This is a valid report.";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Report.Create(adminId, shopId, reportText));
        }

        [Fact]
        public void Create_EmptyShopId_ShouldThrowArgumentNullException()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopId = Guid.Empty;
            var reportText = "This is a valid report.";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Report.Create(adminId, shopId, reportText));
        }

        [Fact]
        public void Create_EmptyReportText_ShouldThrowArgumentNullException()
        {
            // Arrange
            var adminId = Guid.NewGuid();
            var shopId = Guid.NewGuid();
            var reportText = "";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Report.Create(adminId, shopId, reportText));
        }

        [Fact]
        public void SetReportText_ValidReportText_ShouldUpdateReportText()
        {
            // Arrange
            var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), "Initial report text.");
            var newReportText = "Updated report text.";

            // Act
            report.SetReportText(newReportText);

            // Assert
            Assert.Equal(newReportText, report.ReportText);
        }

        [Fact]
        public void SetLastModifiedTime_ShouldUpdateLastModifiedTime()
        {
            // Arrange
            var report = Report.Create(Guid.NewGuid(), Guid.NewGuid(), "Initial report text.");

            // Act
            report.SetLastModifiedTime();

            // Assert
            Assert.NotEqual(default(DateTime), report.LastModifiedTime);
        }
    }
}
