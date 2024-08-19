using MahaliMvc.Models.UserDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MahaliMvc.Controllers
{
    public class BaseController : Controller
    {
        protected Uri baseAddress = new Uri("https://localhost:7177/api");
        protected readonly HttpClient _client;

        public BaseController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var userInfo = new UserInfo
            {
                Email = HttpContext.Session.GetString("UserEmail"),
                UserName = HttpContext.Session.GetString("UserName"),
                UserType = HttpContext.Session.GetString("UserType"),
                PhoneNumber = HttpContext.Session.GetString("PhoneNumber"),

            };

            ViewData["UserInfo"] = userInfo;
        }
    }
}