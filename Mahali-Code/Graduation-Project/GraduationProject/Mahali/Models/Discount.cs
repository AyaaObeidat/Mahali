using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Discount
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Discount percentage is required")]
        public decimal DiscountPercentage { get;private set; }

        [Required(ErrorMessage = "Start date of discount is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "Date and time must be in the format yyyy-MM-dd HH:mm:ss")]
        public DateTime StartDate { get;private set; }

        [Required(ErrorMessage = "End date of discount is required")]
        public DateTime EndDate { get; private set; }
        public Guid ProductId { get;private set; }

        private Discount() { }
        private Discount(Guid productId, decimal discountPercentage, string startDate, string endDate)
        {
            ProductId = productId;
            DiscountPercentage = discountPercentage;
            StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            if (EndDate < StartDate)
            {
                throw new ArgumentException();
            }
        }
        public static Discount Create(Guid productId, decimal discountPercentage, string startDate, string endDate)
        {
            if(productId==Guid.Empty) { throw new ArgumentException(); }
            if(discountPercentage < 0) { throw new ArgumentOutOfRangeException(); }
            if(string.IsNullOrEmpty(startDate)) { throw new ArgumentException(); }
            if(string.IsNullOrEmpty(endDate)) {  throw new ArgumentException(); }
            return new Discount(productId, discountPercentage, startDate, endDate);
        }

        public void SetDiscountPercentage(decimal discountPercentage)
        {
            DiscountPercentage = discountPercentage;
        }
        public void SetStartDate(string startDate)
        {
            StartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
        public void SetEndDate(string endDate)
        {
            EndDate = DateTime.ParseExact(endDate, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
