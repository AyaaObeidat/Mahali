
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : ControllerBase
    {
        private readonly ProductSizeService _productSizeService;

        public ProductSizesController(ProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] ProductSizesCreateParameters parameters)
        {
            await _productSizeService.AddProductSizeAsync(parameters);
            return (Ok());
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody]ProductSizeDeleteParameters parameters)
        {
            await _productSizeService.DeleteProductSizeAsync(parameters);
            return (Ok());
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _productSizeService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody]ProductSizeGetByParameters parameters)
        {

            return Ok(await _productSizeService.GetByIdAsync(parameters));
        }
    }
}
