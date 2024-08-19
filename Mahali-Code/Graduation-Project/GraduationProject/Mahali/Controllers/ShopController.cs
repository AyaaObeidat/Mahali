
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService _shopService;
        public ShopController(ShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] ShopRegister parameters)
        {
            await _shopService.RegisterAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]ShopLogin login)
        {
            var shop = await _shopService.LoginAsync(login);
            if (shop == null) { return BadRequest(); }
            return Ok(shop);
        }

        [HttpPatch]
        [Route("ModifyShopName")]
        public async Task<IActionResult> ModifyShopNameAsync([FromBody]ShopUpdateParameters parameters)
        {
            await _shopService.ModifyShopNameAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyShopPassword")]
        public async Task<IActionResult> ModifyShopPasswordAsync([FromBody]ShopUpdateParameters parameters)
        {
            await _shopService.ModifyShopPasswordAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyShopPhoneNumber")]
        public async Task<IActionResult> ModifyShopPhoneNumberAsync([FromBody]ShopUpdateParameters parameters)
        {
            await _shopService.ModifyShopPhoneNumberAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyShopDescription")]
        public async Task<IActionResult> ModifyShopDescriptionAsync([FromBody]ShopUpdateParameters parameters)
        {
            await _shopService.ModifyShopDescriptionAsync(parameters);
            return Ok();

        }

        [HttpPost]
        [Route("GetAllShopOrders")]
        public async Task<IActionResult> GetShopOrdersAsync([FromBody]ShopGetByParameter parameter)
        {
            var orders = await _shopService.GetShopOrdersAsync(parameter);
            if (orders == null) { return BadRequest(); }
            return Ok(orders);
        }

        [HttpPost]
        [Route("GetAllShopProducts")]
        public async Task<IActionResult> GetShopProductsAsync([FromBody]ShopGetByParameter parameter)
        {
            var products = await _shopService.GetShopProductsAsync(parameter);
            if (products == null) { return BadRequest(); }
            return Ok(products);
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _shopService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody]ShopGetByParameter parameter)
        {
            return Ok(await _shopService.GetByIdAsync(parameter));
        }

        [HttpGet]
        [Route("GetAllApprovedShops")]
        public async Task<IActionResult> GetApprovedShopsAsync()
        {
            return Ok(await _shopService.GetApprovedShopsAsync());
        }

        [HttpPost]
        [Route("UpdateReviewRequestStatus")]
        public async Task<IActionResult> UpdateReviewRequestStatusAsync([FromBody] ReviewRequestUpdateParameters parameter)
        {
           await _shopService.UpdateReviewRequestStatusAsync(parameter);
            return Ok();
        }

        [HttpPost]
        [Route("GetAllReviewsByShopId")]
        public async Task<IActionResult> GetAllReviewsByShopIdAsync([FromBody]ShopGetByParameter parameter)
        {
            return Ok(await _shopService.GetAllReviewsByShopIdAsync(parameter));
        }


        [HttpPost]
        [Route("GetAllReviewsOnSpecificProduct")]
        public async Task<IActionResult> GetAllReviewsOnSpecificProductAsync([FromBody]ProductGetById parameter)
        {
            return Ok(await _shopService.GetAllReviewsOnSpecificProductAsync(parameter));
        }

        [HttpPost]
        [Route("GetAllApprovedReviewsOnSpecificProduct")]
        public async Task<IActionResult> GetAllApprovedReviewsOnSpecificProductAsync([FromBody] ProductGetById parameter)
        {
            return Ok(await _shopService.GetAllApprovedReviewsOnSpecificProductAsync(parameter));
        }



        [HttpPost]
        [Route("GetShopByIdAsync")]
        public async Task<IActionResult> GetShopByIdAsync([FromBody] ShopGetByParameter parameter)
        {
            return Ok(await _shopService.GetShopByIdAsync(parameter));
        }
    }
}