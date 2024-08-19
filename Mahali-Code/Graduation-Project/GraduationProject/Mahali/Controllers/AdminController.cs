
using Mahali.Services;
using MahaliDtos;
using Microsoft.AspNetCore.Mvc;

namespace Mahali.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

       
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AdminRegister parameters)
        {
            await _adminService.RegisterAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]AdminLogin adminLogin)
        {
            var admin = await _adminService.LoginAsync(adminLogin);
            if (admin == null) { BadRequest(); }
            return Ok(admin);
        }
        
        [HttpPatch]
        [Route("ModifyAccountUserName")]
        public async Task<IActionResult> ModifyAccountUserNameAsync([FromBody]AdminUpdateParameters parameters)
        {
            await _adminService.ModifyAccountUserNameAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("ModifyAccountPassword")]
        public async Task<IActionResult> ModifyAccountPasswordAsync([FromBody]AdminUpdateParameters parameters)
        {
            await _adminService.ModifyAccountPasswordAsync(parameters);
            return Ok();
        }


        [HttpGet]
        [Route("GetAllShopRequest")]
        public async Task<IActionResult> GetAllShopRequestAsync()
        {
            return Ok(await _adminService.GetAllShopRequestAsync());
        }

        [HttpPatch]
        [Route("UpdateShopRequestStatus")]
        public async Task<IActionResult> UpdateShopRequestStatus([FromBody]ShopRequestUpdateParameters parameters)
        {
            await _adminService.UpdateShopRequestStatusAsync(parameters);
            return Ok();
        }

        [HttpPost]
        [Route("CreateAboutUs")]
        public async Task<IActionResult> CreateAboutUsAsync([FromBody]AboutUsCreateParameters parameters)
        {
            await _adminService.CreateAboutUsAsync(parameters);
            return Ok();
        }

        [HttpPatch]
        [Route("UpdateAboutUsContentBody")]
        public async Task<IActionResult> UpdateAboutUsContentBodyAsync([FromBody]AboutUsUpdateParameters parameters)
        {
            await _adminService.UpdateAboutUsContentBody(parameters);
            return Ok();
        }

        [HttpGet]
        [Route("GetAboutUs")]
        public async Task<IActionResult> GetAboutUsAsync()
        {
            return Ok(await _adminService.GetAboutUsAsync());   
        }
    }
}
