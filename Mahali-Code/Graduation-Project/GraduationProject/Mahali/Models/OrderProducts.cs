using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class OrderProducts
    {
        public Guid Id { get; set; }

        [Required]
        public int OrderId { get; private set; }

        [Required]
        public Guid ProductId { get; private set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; private set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; private set; } = 0!;

        [Required(ErrorMessage = "Color is required.")]
        public Colors Color { get; private set; }

        [Required(ErrorMessage = "Size is required.")]
        public Sizes Size { get; private set; }

        private OrderProducts() { }
        private OrderProducts(int orderId, Guid productId, int quantity, decimal unitPrice, Colors color, Sizes size)
        {

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Color = color;
            Size = size;
        }

        public static OrderProducts Create(int orderId, Guid productId, int quantity, decimal unitPrice, Colors color, Sizes size)
        {
            if (orderId < 0) { throw new ArgumentNullException(); }
            if (productId == Guid.Empty) { throw new ArgumentNullException(); }
            if (unitPrice <= 0) { throw new ArgumentOutOfRangeException(); }
            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }
            if (color == null) { throw new ArgumentNullException(); }
            if (size == null) { throw new ArgumentNullException(); }

            return new OrderProducts(orderId, productId, quantity, unitPrice, color, size);
        }

        public void SetUnitPrice(decimal unitPrice)
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
