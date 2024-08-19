using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class WishList
    {
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; private set; }
        public List<WishListProducts> Products { get; private set; }

        private WishList() { }

        private WishList(Guid customerId)
        {
            CustomerId = customerId;
        }

        public static WishList Create(Guid customerId)
        {
            if (customerId == Guid.Empty) { throw new ArgumentNullException(); }
            return new WishList(customerId);
        }

        public void SetProducts(List<WishListProducts> products)
        {
            Products = products;
        }
       
    }
}
