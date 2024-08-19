using MahaliDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class CheckOutOperationController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderCreateParameters parameters)
        {
            // Serialize the parameters to JSON
            var json = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Make a POST request to the backend API to place the order
            HttpResponseMessage response = await _client.PostAsync("Checkout/PlaceOrder", content);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                // Redirect to a success page or take further action
                return RedirectToAction("OrderSuccess");
            }
            else
            {
                // Handle the error and display an appropriate message
                ModelState.AddModelError(string.Empty, "Failed to place the order.");
                return RedirectToAction("Index","Home");
            }
        }

        public IActionResult OrderSuccess()
        {
            // Render a view indicating that the order was successfully placed
            return View();
        }
    }
}
