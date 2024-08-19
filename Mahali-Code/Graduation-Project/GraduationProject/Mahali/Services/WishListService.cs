using Mahali.Models;
using Mahali.Repositories.Implementations;
using Mahali.Repositories.Interfaces;
using MahaliDtos;

namespace Mahali.Services
{
    public class WishListService
    {
        private readonly IWishListInterface _wishListInterface;
        private readonly IProductInterface _productInterface;
        private readonly IWishListProductsInterface _wishListProductsInterface;

        public WishListService(IWishListInterface wishListInterface, IProductInterface productInterface, IWishListProductsInterface wishListProductsInterface)
        {
            _productInterface = productInterface;
            _wishListInterface = wishListInterface;
            _wishListProductsInterface = wishListProductsInterface;
        }

        public async Task<List<WishListListItems>> GetAllAsync()
        {
            var wishList = await _wishListInterface.GetAllAsync();
            return wishList.Select(x => new WishListListItems
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
               
            }).ToList();
        }

        public async Task<WishListListItems?> GetByIdAsync(WishListGetByParameters parameters)
        {
            var wishList = await _wishListInterface.GetByCustomerIdAsync(parameters.CustomerId);
            var allProducts = await _productInterface.GetAllAsync();
            var products = wishList.Products.Select(x => new WishListProductsDetails
            {
                Product = allProducts.Select(x => new ProductDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUri = x.ImageUri

                }).FirstOrDefault(n => n.Id == x.ProductId)
            }).ToList();
            return new WishListListItems
            {
                CustomerId = wishList.CustomerId,
                Products = products
            };
        }

        public async Task AddProductAsync(WishListProductsCreateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var wishList = await _wishListInterface.GetByIdAsync(parameters.WishListId);
            if (wishList == null) return;

            var wishListProduct = WishListProducts.Create(parameters.WishListId , parameters.ProductId);
            await _wishListProductsInterface.AddAsync(wishListProduct);

        }

        public async Task DeleteProductAsync(WishListProductsDeleteParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var wishList = await _wishListInterface.GetByIdAsync(parameters.WishListId);
            if (wishList == null) return;

            var wishListProducts = await _wishListProductsInterface.GetAllAsync();
            foreach (var wishListProduct in wishListProducts)
            {
                if (wishListProduct.WishListId == wishList.Id && wishListProduct.ProductId == product.Id)
                {
                    await _wishListProductsInterface.DeleteAsync(wishListProduct);
                }
            }

        }

        public async Task<List<WishListProductsDetails>> GetAllWishListProductsAsync(WishListGetByParameters parameters)
        {
            var wishList = await _wishListInterface.GetByCustomerIdAsync(parameters.CustomerId);
            var wishlistProducts = wishList.Products.ToList();
            var allProducts = await _productInterface.GetAllAsync();
            return wishlistProducts.Select(x => new WishListProductsDetails
            {
                Product = allProducts.Select(x => new ProductDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUri = x.ImageUri
                   
                }).FirstOrDefault(n => n.Id == x.ProductId)
                
            }).ToList();
        }
    }
}
