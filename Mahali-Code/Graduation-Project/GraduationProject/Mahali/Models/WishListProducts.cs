using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Mahali.Models
{
    public class WishListProducts
    {
        public Guid Id { get; set; }
        [Required]
        public Guid WishListId { get; private set; }
        [Required]
        public Guid ProductId { get; private set; }

        private WishListProducts() { }
        private WishListProducts(Guid wishListId , Guid productId)
        {

            WishListId = wishListId;
            ProductId = productId;
            
        }

        public static WishListProducts Create(Guid wishListId, Guid productId)
        {
            if (wishListId == Guid.Empty) { throw new ArgumentNullException(); }
            if (productId == Guid.Empty) { throw new ArgumentNullException(); }
            

            return new WishListProducts(wishListId , productId);
        }
    }
}
