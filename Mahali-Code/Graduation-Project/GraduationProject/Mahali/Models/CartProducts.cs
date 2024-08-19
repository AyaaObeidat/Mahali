using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class CartProducts

    {
        
        public Guid Id { get; set; }

        [Required]
        public Guid CartId { get;private set; }

        [Required]
        public Guid ProductId { get;private set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get;private set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; private set; } = 0!;

        [Required(ErrorMessage = "Color is required.")]
        public Colors Color { get;private set; }

        [Required(ErrorMessage = "Size is required.")]
        public Sizes Size { get; private set; }

        private CartProducts() { }
        private CartProducts( Guid cartId, Guid productId, int quantity, decimal unitPrice, Colors color, Sizes size)
        {
           
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Color = color;
            Size = size;
        }

        public static CartProducts Create(Guid cartId, Guid productId, int quantity, decimal unitPrice, Colors color, Sizes size)
        {
            if(cartId == Guid.Empty) { throw new ArgumentNullException();}
            if(productId == Guid.Empty) { throw new ArgumentNullException();}
            if(unitPrice <=0 ) { throw new ArgumentOutOfRangeException(); }
            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }
            if(color == null) { throw new ArgumentNullException(); }
            if(size == null) { throw new ArgumentNullException(); }

            return new CartProducts(cartId , productId, quantity, unitPrice, color , size);
        }

        public void SetUnitPrice (decimal unitPrice)
        {
            UnitPrice = unitPrice;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public void SetColor(Colors color)
        {
            Color = color;
        }
        public void SetSize(Sizes size)
        {
            Size = size;
        }
    }
}
