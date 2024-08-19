using Mahali.Models;
using Mahali.Repositories.Interfaces;
using MahaliDtos;
using static AllEnums.AllEnums;

namespace Mahali.Services
{
    public class CartService
    {
        private readonly ICartInterface _cartInterface;
        private readonly IProductInterface _productInterface;
        private readonly ICartProductsInterface _cartProductsInterface; 

        public CartService(ICartInterface cartInterface, IProductInterface productInterface , ICartProductsInterface cartProductsInterface)
        {
            _cartInterface = cartInterface;
            _productInterface = productInterface;
            _cartProductsInterface = cartProductsInterface;
        }

        public async Task<List<CartListItems>> GetAllAsync()
        {
            var carts = await _cartInterface.GetAllAsync();
            return carts.Select(x => new CartListItems
            {
                ID = x.Id,
                CustomerId = x.CustomerId,
                TotalAmount = x.TotalAmount,
            }).ToList();
        }

        public async Task<CartListItems?> GetByIdAsync(CustomerGetByParameters parameters)
        {
            var cart = await _cartInterface.GetByCustomerIdAsync(parameters.CustomerId);
            var allProducts = await _productInterface.GetAllAsync();
            var products = cart.Products.Select(x => new CartProductsListItems
            {
                Product = allProducts.Select(x => new ProductDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUri = x.ImageUri,

                }).FirstOrDefault(n => n.Id == x.ProductId),
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Color = x.Color,
                Size = x.Size,
            }).ToList();
            return new CartListItems
            {
                CustomerId = cart.CustomerId,
                TotalAmount = cart.TotalAmount,
                Products = products
            };
        }

        public async Task AddProductAsync(CartProductsCreateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var cart = await _cartInterface.GetByIdAsync(parameters.CartId);
            if(cart == null) return;

            if(parameters.Quantity <= product.Quantity)
            {
                for(int i = 1; i<=parameters.Quantity; i++)
                {
                    var cartProduct = CartProducts.Create(parameters.CartId, parameters.ProductId, parameters.Quantity, product.Price, parameters.Color, parameters.Size);
                    await _cartProductsInterface.AddAsync(cartProduct);
                    cart.SetTotalAmount();
                    await _cartInterface.UpdateAsync(cart);
                }
               
            }
          
        }

        public async Task AddCustomerProductToCard(AddCustomerProductToCard parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var carts = await _cartInterface.GetAllAsync();
            var cart = carts.Where(x => x.CustomerId == parameters.CustomerId).FirstOrDefault();
            if (cart == null) return;

            var cartProduct = CartProducts.Create(cart.Id, parameters.ProductId, 1, product.Price, Colors.Red, Sizes.US_45);
            await _cartProductsInterface.AddAsync(cartProduct);
            cart.SetTotalAmount();
            await _cartInterface.UpdateAsync(cart);
        }

        public async Task DeleteProductAsync(CartProductsDeleteParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var cart = await _cartInterface.GetByCustomerIdAsync(parameters.CartId);
            if (cart == null) return;

            var cartProducts = await _cartProductsInterface.GetAllAsync();   
            foreach(var cartProduct in cartProducts)
            {
                if(cartProduct.CartId == cart.Id && cartProduct.ProductId == product.Id)
                {
                    if (parameters.Quantity <= cartProduct.Quantity)
                    {
                        for(int i=1; i<=parameters.Quantity; i++)
                        {
                            await _cartProductsInterface.DeleteAsync(cartProduct);
                            cart.SetTotalAmount();
                            await _cartInterface.UpdateAsync(cart);
                            return;
                        }
                       
                    }
                    else return;
                }
            }
            
        }

        //public async Task UpdateCartProductQuantityAsync(CartProductsUpdateParameters parameters)
        //{
        //    var product = await _productInterface.GetByIdAsync(parameters.ProductId);
        //    if (product == null) return;
        //    var cart = await _cartInterface.GetByIdAsync(parameters.CartId);
        //    if(cart == null) return;
        //    var cartProducts =await _cartProductsInterface.GetAllAsync();

        //    foreach(var cartProduct in cartProducts)
        //    {
        //        if(cartProduct.CartId == cart.Id && cartProduct.ProductId == product.Id)
        //        {
        //            if(parameters.Quantity <= product.Quantity)
        //            {
        //                cartProduct.SetQuantity(parameters.Quantity);
        //                await _cartProductsInterface.UpdateAsync(cartProduct);
        //                if (parameters.Quantity > cartProduct.Quantity)
        //                {
        //                    for (int i = cartProduct.Quantity; i <= parameters.Quantity; i++)
        //                    {
        //                        var newCartProduct = CartProducts.Create(cartProduct.CartId, cartProduct.ProductId,cartProduct.Quantity, cartProduct.UnitPrice, cartProduct.Color, cartProduct.Size);
        //                        await _cartProductsInterface.AddAsync(newCartProduct);
        //                        cart.SetTotalAmount();
        //                        await _cartInterface.UpdateAsync(cart);
        //                        return;
        //                    }
        //                }
        //                else
        //                {
        //                    for (int i = cartProduct.Quantity; i >= parameters.Quantity; i--)
        //                    {
        //                        await _cartProductsInterface.DeleteAsync(cartProduct);
        //                        cart.SetTotalAmount();
        //                        await _cartInterface.UpdateAsync(cart);
        //                        return;
        //                    }

        //                }
        //            }
                   
        //        }
                
        //    }
        //    return;
        //}

        public async Task UpdateCartProductColorAsync(CartProductsUpdateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var cart = await _cartInterface.GetByIdAsync(parameters.CartId);
            if (cart == null) return;
            var cartProducts = await _cartProductsInterface.GetAllAsync();
            foreach (var cartProduct in cartProducts)
            {
                if (cartProduct.CartId == cart.Id && cartProduct.ProductId == product.Id)
                {
                        cartProduct.SetColor(parameters.Color);
                        await _cartProductsInterface.UpdateAsync(cartProduct);
                }

            }
            return;
        }

        public async Task UpdateCartProductSizeAsync(CartProductsUpdateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var cart = await _cartInterface.GetByIdAsync(parameters.CartId);
            if (cart == null) return;
            var cartProducts = await _cartProductsInterface.GetAllAsync();
            foreach (var cartProduct in cartProducts)
            {
                if (cartProduct.CartId == cart.Id && cartProduct.ProductId == product.Id)
                {
                    cartProduct.SetSize(parameters.Size);
                    await _cartProductsInterface.UpdateAsync(cartProduct);
                }

            }
            return;
        }

        public async Task<List<CartProductsListItems>> GetAllCartProductsAsync(CustomerGetByParameters parameters)
        {
            var cart = await _cartInterface.GetByCustomerIdAsync(parameters.CustomerId);
            var cartProducts = cart.Products.ToList();
            var allProducts = await _productInterface.GetAllAsync();
            return cartProducts.Select(x => new CartProductsListItems
            {
                
                Product = allProducts.Select(x => new ProductDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description =   x.Description,
                    Price = x.Price,
                    ImageUri = x.ImageUri,

                }).FirstOrDefault(n => n.Id == x.ProductId),

                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                Color = x.Color,
                Size = x.Size,
            }).ToList();
        }
       
    }
}
