using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly WishListService _wishListService;

        public WishListController(WishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _wishListService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody]WishListGetByParameters parameters)
        {
            return Ok(await _wishListService.GetByIdAsync(parameters));
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] WishListProductsCreateParameters parameters)
        {
            await _wishListService.AddProductAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProductAsync([FromBody] WishListProductsDeleteParameters parameters)
        {
            await _wishListService.DeleteProductAsync(parameters);
            return Ok();
        }


        [HttpPost]
        [Route("GetAllWishListProducts")]
        public async Task<IActionResult> GetAllWishListProductsAsync([FromBody] WishListGetByParameters parameters)
        {
            
            return Ok(await _wishListService.GetAllWishListProductsAsync(parameters));
        }
    }
}
