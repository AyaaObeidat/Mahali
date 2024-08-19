
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;
using static AllEnums.AllEnums;

namespace Mahali.Services
{
    public class ProductService
    {
        private readonly IProductInterface _productInterface;
        private readonly IShopInterface _shopInterface;
        private readonly IShopRequestInterface  _shopRequestInterface;

        public ProductService(IProductInterface productInterface , IShopInterface shopInterface , IShopRequestInterface shopRequestInterface)
        {
            _productInterface = productInterface;
            _shopInterface = shopInterface;
            _shopRequestInterface = shopRequestInterface;
        }

        public async Task AddAsync(ProductCreateParameters parameters)
        {
            var shop = await _shopInterface.GetByIdAsync(parameters.ShopId);
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(shop.Id);
            if(request.Status == RequestStatus.Approved)
            {
                foreach (var product in shop.Products.ToList())
                {
                    if (product.Name != parameters.Name) continue;
                    else return;
                }
                var newProduct = Product.Create(parameters.Name, parameters.Description, parameters.Quantity, parameters.Price, parameters.ImageUri, parameters.ShopId);
                await _productInterface.AddAsync(newProduct);
            }
        }

        public async Task DeleteAsync(ProductDeleteParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if(product==null) return;
            await _productInterface.DeleteAsync(product);
        }

        public async Task<List<ProductListItems>?> GetAllAsync()
        {
            var products = await _productInterface.GetAllAsync();
            if (products == null) return null;
            var shops = await _shopInterface.GetAllAsync();
            return products.Select(p => new ProductListItems
            {
                ImageUri = p.ImageUri,
                Quantity = p.Quantity,
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Shop = shops.Select(s => new ShopDetails
                { Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Email = s.Email,
                Password =  s.Password,
                }).FirstOrDefault(y => y.Id == p.ShopId)
               
            }).ToList();
        }

        public async Task<ProductListItems?> GetByIdAsync(ProductGetById parameter)
        {
            var product = await _productInterface.GetByIdAsync(parameter.ProductId);
            if (product == null) return null;
            var colors = product.ColorsList.Select(x => new ProductColorsDetails
            {
                Color = x.Color,
            }).ToList();

            var sizes = product.SizesList.Select(x => new ProductSizesDetails
            {
                Size = x.Size,
            }).ToList();


            var reviews = product.Reviews.Select(x => new ReviewRequestDetails
            {
                CustomerId = x.CustomerId,
                ReviewContentBody = x.ReviewContentBody,
                Status = x.Status

            }).ToList();
            var shops = await _shopInterface.GetAllAsync();
            return new ProductListItems
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Shop = shops.Select(s => new ShopDetails
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Email = s.Email,
                    Password = s.Password,
                }).FirstOrDefault(y => y.Id == product.ShopId),
                ColorsList = colors,
                SizesList = sizes,
                Reviews = reviews,
                ImageUri = product.ImageUri,
                Quantity = product.Quantity,
                Id = product.Id
                
            };
        }

        public async Task ModifyProductNameAsync(ProductUpdateParameters parameters)
        {
            var selectedProduct = await _productInterface.GetByIdAsync(parameters.Id);
            if (selectedProduct == null) return;
            var shop =await _shopInterface.GetByIdAsync(selectedProduct.ShopId);
            foreach(var product in shop.Products.ToList())
            {
                if (product.Name == parameters.NewName) return;
                else continue;
            }

            selectedProduct.SetName(parameters.NewName);
            await _productInterface.UpdateAsync(selectedProduct);
        }

        public async Task ModifyProductDescriptionAsync(ProductUpdateParameters parameters)
        {
            var selectedProduct = await _productInterface.GetByIdAsync(parameters.Id);
            if (selectedProduct == null) return;
            
            selectedProduct.SetDescription(parameters.NewDescription);
            await _productInterface.UpdateAsync(selectedProduct);
        }

        public async Task ModifyProductQuantityAsync(ProductUpdateParameters parameters)
        {
            var selectedProduct = await _productInterface.GetByIdAsync(parameters.Id);
            if (selectedProduct == null) return;
            
            selectedProduct.SetQuantity(parameters.NewQuantity);
            await _productInterface.UpdateAsync(selectedProduct);
        }

        public async Task ModifyProductPriceAsync(ProductUpdateParameters parameters)
        {
            var selectedProduct = await _productInterface.GetByIdAsync(parameters.Id);
            if (selectedProduct == null) return;

            selectedProduct.SetPrice(parameters.NewPrice);
            await _productInterface.UpdateAsync(selectedProduct);
        }

        public async Task ModifyProductImageUriAsync(ProductUpdateParameters parameters)
        {
            var selectedProduct = await _productInterface.GetByIdAsync(parameters.Id);
            if (selectedProduct == null) return;
            var shop = await _shopInterface.GetByIdAsync(selectedProduct.ShopId);
            foreach (var product in shop.Products.ToList())
            {
                if (product.ImageUri == parameters.NewImageUri) return;
                else continue;
            }

            selectedProduct.SetImageUri(parameters.NewImageUri);
            await _productInterface.UpdateAsync(selectedProduct);
        }

        public async Task AddToCategoryAsync(ProductGetByParameter parameter)
        {
            var product = await _productInterface.GetByIdAsync(parameter.ProductId);
            if (product == null) return;
            product.SetCategoryId(parameter.CategoryId);
            await _productInterface.UpdateAsync(product);
        }

        
       
    }
}
