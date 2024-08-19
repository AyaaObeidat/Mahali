using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class ProductColors
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get;private set; }

        [Required]
        public Colors Color { get;private set; }

        private ProductColors() { }
        private ProductColors(Guid productId, Colors color)
        {
            ProductId = productId;
            Color = color;
        }

        public static ProductColors Create(Guid productId , Colors color)
        { 
            if(productId == Guid.Empty) { throw new ArgumentException(); }
            return new ProductColors(productId, color);
        }
    }
}
