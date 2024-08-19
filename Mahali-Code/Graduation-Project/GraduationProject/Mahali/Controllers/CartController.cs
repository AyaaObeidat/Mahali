using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _cartService.GetAllAsync());    
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody] CustomerGetByParameters parameters)
        {
            return Ok(await _cartService.GetByIdAsync(parameters));
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] CartProductsCreateParameters parameters)
        {
            await _cartService.AddProductAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("AddCustomerProductToCard")]
        public async Task<IActionResult> AddCustomerProductToCard([FromBody] AddCustomerProductToCard parameters)
        {
            await _cartService.AddCustomerProductToCard(parameters);
            return Ok();
        }
        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProductAsync([FromBody] CartProductsDeleteParameters parameters)
        {
            await _cartService.DeleteProductAsync(parameters);
            return Ok();
        }

        //[HttpPatch]
        //[Route("UpdateCartProductQuantity")]
        //public async Task<IActionResult> UpdateCartProductQuantityAsync( [FromBody] CartProductsUpdateParameters parameters)
        //{
        //    await _cartService.UpdateCartProductQuantityAsync(parameters);
        //    return Ok();
        //}

        [HttpPatch]
        [Route("UpdateCartProductColor")]
        public async Task<IActionResult> UpdateCartProductColorAsync([FromBody] CartProductsUpdateParameters parameters)
        {
            await _cartService.UpdateCartProductColorAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateCartProductSize")]
        public async Task<IActionResult> UpdateCartProductSizeAsync([FromBody] CartProductsUpdateParameters parameters)
        {
            await _cartService.UpdateCartProductSizeAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("GetAllCartProducts")]
        public async Task<IActionResult> GetAllCartProductsAsync( [FromBody] CustomerGetByParameters parameters)
        {
            
            return Ok(await _cartService.GetAllCartProductsAsync(parameters));
        }
    }
}
