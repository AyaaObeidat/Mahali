using MahaliDtos;

namespace MahaliMvc.Models.Product
{
    public class ProductDto
    {
        public ProductDetails product { get; set; }
        public List<LatestProductsVisitedDetails> latestProducts { get; set; }
    }
}
