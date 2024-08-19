
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly ProductColorService _productColorService;

        public ProductColorsController(ProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] ProductColorsCreateParameters parameters)
        {
            await _productColorService.AddProductColorAsync(parameters);
            return (Ok());
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync(ProductColorDeleteParameters parameters)
        {
            await _productColorService.DeleteProductColorAsync(parameters);
            return (Ok());
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            
            return Ok(await _productColorService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody ]ProductColorGetByParameters parameters)
        {

            return Ok(await _productColorService.GetByIdAsync(parameters));
        }
    }
}
