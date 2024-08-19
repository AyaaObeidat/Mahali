using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace Mahali.Models
{
    public class ProductSizes
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get;private set; }

        [Required]
        public Sizes Size { get;private set; }

        private ProductSizes() { }
        private ProductSizes(Guid productId, Sizes size)
        {
            ProductId = productId;
            Size = size;
        }

        public static ProductSizes Create(Guid productId, Sizes size)
        {
            if (productId == Guid.Empty) { throw new ArgumentException(); }
            return new ProductSizes(productId, size);
        }
    }
}
