using MahaliDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class WishlistController : BaseController
    {


        public async Task<IActionResult> Wishlist()
        {
            List<WishListProductsDetails> WishlistProducts = new List<WishListProductsDetails>();
            var req = new WishListGetByParameters
            {
                CustomerId = Guid.Parse(HttpContext.Session.GetString("CustomerId")),
            };
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/WishList/GetAllWishListProducts", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                WishlistProducts = JsonConvert.DeserializeObject<List<WishListProductsDetails>>(data);
            }
            return View(WishlistProducts);
        }
    }
}
