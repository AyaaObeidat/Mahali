using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutOpertaionController : ControllerBase
    {
        private readonly CheckOutOpertaionService _checkOutOpertaionService;

        public CheckOutOpertaionController(CheckOutOpertaionService checkOutOpertaionService)
        {
            _checkOutOpertaionService = checkOutOpertaionService;
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrderAsync(OrderCreateParameters parameters)
        {
            await _checkOutOpertaionService.PlaceOrderAsync(parameters);
            return (Ok());
        }

        [HttpPost]
        [Route("CompletePlaceOrderByVisa")]
        public async Task<IActionResult> CompletePlaceOrderByVisaAsync(OrderGetByParameters parameters)
        {
            //you can call this endpoint id payment type => visa
            await _checkOutOpertaionService.CompletePlaceOrderByVisaAsync(parameters);
            return (Ok());
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
           
            return (Ok(await _checkOutOpertaionService.GetAllAsync()));
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(OrderGetById order)
        {

            return (Ok(await _checkOutOpertaionService.GetByIdAsync(order)));

        }

        [HttpPatch]
        [Route("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatusAsync(OrderGetById order)
        {
            await _checkOutOpertaionService.UpdateOrderStatusAsync(order);
            return (Ok());
        }
    }
}
