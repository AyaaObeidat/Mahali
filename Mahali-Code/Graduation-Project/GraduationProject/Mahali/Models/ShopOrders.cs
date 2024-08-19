using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class ShopOrders
    {
       
        public Guid Id { get; set; }
        public int OrderId { get; private set; }
        public Guid ShopId { get;private set; }

        private ShopOrders() { }
        private ShopOrders (int orderId , Guid shopId)
        {
            OrderId = orderId;
            ShopId = shopId;
        }

        public static ShopOrders Create(int orderId , Guid shopId)
        {
            if (orderId < 0) { throw new ArgumentException(); }
            if(shopId == Guid.Empty) { throw new ArgumentException(); }

            return new ShopOrders(orderId , shopId);
        }
    }
}
