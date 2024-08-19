
using Mahali.Repositories.Implementations;
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody]CategoryCreateParameters parameters)
        {
            await _categoryService.AddAsync(parameters);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody]CategoryDeleteParameters parameters)
        {
            await _categoryService.DeleteAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("Update")]
        public async Task<IActionResult> ModifyNameAsync([FromBody]CategoryUpdateParameters parameters)
        {
            await _categoryService.ModifyCategoryNameAsync(parameters);
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody]CategoryGetByParameter parameter)
        {
            
            return Ok(await _categoryService.GetByIdAsync(parameter));
        }

      
    }
}
