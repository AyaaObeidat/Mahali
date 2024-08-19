using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class ShopRequest
    {
        
        public Guid Id { get; set; }
        public Guid AdminId { get;private set; }
        public Guid ShopId { get;private set; }
        public RequestStatus Status { get;private set; }

        private ShopRequest() { }
        private ShopRequest(Guid adminId , Guid shopId )
        {
            AdminId = adminId;
            ShopId = shopId;
            Status = RequestStatus.Pending;
        }

        public static ShopRequest Create( Guid adminId , Guid shopId )
        {
            if (adminId == Guid.Empty) throw new ArgumentNullException();
            if(shopId == Guid.Empty)  throw new ArgumentNullException();
            return new ShopRequest(adminId, shopId);
        }
        public void SetRequestStatus(RequestStatus status)
        {
            Status = status;
        }
    }
}
