using Mahali.Models;
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync( [FromBody] CustomerRegister customer)
        {
            await _customerService.RegisterAsync(customer);
            return Ok(customer);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> RegisterAsync([FromBody]CustomerLogin customer)
        {
           
            return Ok(await _customerService.LoginAsync(customer));
        }

        [HttpPatch]
        [Route("ModifyFirstName")]
        public async Task<IActionResult> ModifyFirstNameAsync([FromBody] CustomerUpdateParameters parameters)
        {
            await _customerService.ModifyFirstName(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyLastName")]
        public async Task<IActionResult> ModifyLastNameAsync([FromBody] CustomerUpdateParameters parameters)
        {
            await _customerService.ModifyLastName(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyPassword")]
        public async Task<IActionResult> ModifyPasswordAsync([FromBody] CustomerUpdateParameters parameters)
        {
            await _customerService.ModifyPassword(parameters);
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _customerService.GetAllAsync());
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromBody] CustomerGetByParameters parameters)
        {
            return Ok(await _customerService.GetByIdAsync(parameters));
        }

        [HttpPost]
        [Route("AddLatestProductVisited")]
        public async Task<IActionResult> AddLatestProductVisitedAsync([FromBody] LatestProductsVisitedCreateParameters parameters)
        {
            await _customerService.AddLatestProductVisitedAsync(parameters);
            return(Ok());   
        }

        [HttpPost]
        [Route("GetAllLatestProductVisited")]
        public async Task<IActionResult> GetAllLatestProductVisitedAsync([FromBody] LatestProductsVisitedCreateParameters parameters)
        {
            
            return (Ok(await _customerService.GetAllLatestProductVisitedAsync(parameters)));
        }

        [HttpPost]
        [Route("GetLatestProductVisitedByCustomerId")]
        public async Task<IActionResult> GetLatestProductVisitedByCustomerIdAsync([FromBody] LatestProductsGetByParameters parameters)
        {
            
            return (Ok(await _customerService.GetLatestProductVisitedByCustomerIdAsync(parameters)));
        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddCommentAsync([FromBody] ReviewRequestCreateParameters parameters)
        {
            await _customerService.AddCommentOnProductAsync(parameters);
            return(Ok());
        }
    }
}
