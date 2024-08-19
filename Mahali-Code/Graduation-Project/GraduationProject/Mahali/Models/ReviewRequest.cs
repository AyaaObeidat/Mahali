using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class ReviewRequest
    {
        

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Review content body is required")]
        public string ReviewContentBody { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } 
        public Guid ProductId { get;private set; }
        public Guid CustomerId { get;private set; }
        public RequestStatus Status { get; private set; }
        private ReviewRequest() { }
        private ReviewRequest (Guid customerId, Guid productId, string reviewContentBody)
        {
            ProductId = productId;
            ReviewContentBody = reviewContentBody;
            CreatedAt = DateTime.Now;
            CustomerId = customerId;
            Status = RequestStatus.Pending;
        }

        public static ReviewRequest Create(Guid customerId , Guid productId , string reviewContentBody)
        {
            if (customerId == Guid.Empty) throw new ArgumentNullException();
            if(productId == Guid.Empty) throw new ArgumentNullException();
            if(string.IsNullOrEmpty(reviewContentBody)) throw new ArgumentNullException();
            return new ReviewRequest(customerId,productId,reviewContentBody);
        }
        public void SetReviewContentBody (string reviewContentBody)
        {
            ReviewContentBody = reviewContentBody;
        }
        public void SetRequestStatus(RequestStatus status)
        {
            Status = status;
        }
    }
}
