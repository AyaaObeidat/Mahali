using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] DiscountCreateParameters parameters)
        {
            await _discountService.AddDiscountAsync(parameters);
            return Ok();
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
           
            return Ok(await _discountService.GetAllDiscountsAsync());
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DiscountDeleteParameters parameters)
        {
            await _discountService.DeleteDiscountAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateDiscountPercentage")]
        public async Task<IActionResult> UpdateDiscountPercentageAsync([FromBody] DiscountUpdateParameters parameters)
        {
            await _discountService.ModifyDiscountPercentage(parameters);
            return Ok();
        }
    }
}
