using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Report
    {
        
        public Guid Id { get; set; }

        [Required]
        public string ReportText { get; private set; } = null!;
        public DateTime LastModifiedTime { get;private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid AdminId { get;private set; }
        public Guid ShopId { get; private set; }

        private Report() { }
        private Report (Guid adminId, Guid shopId , string reportText)
        {
            AdminId = adminId;
            ShopId = shopId;
            ReportText = reportText;
            CreatedAt = DateTime.Now;
            LastModifiedTime = CreatedAt;
        }

        public static Report Create(Guid adminId , Guid shopId, string reportText)
        {
            if (adminId == Guid.Empty) throw new ArgumentNullException();
            if(shopId == Guid.Empty) throw new ArgumentNullException();
            if(string.IsNullOrEmpty(reportText)) throw new ArgumentNullException();
            return new Report(adminId, shopId, reportText);
        }

        public void SetReportText(string reportText)
        {
            ReportText = reportText;
        }

        public void SetLastModifiedTime ()
        {
            LastModifiedTime = DateTime.Now;
        }
    }
}
