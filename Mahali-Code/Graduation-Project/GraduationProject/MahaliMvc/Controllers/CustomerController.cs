using MahaliDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class CustomerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EditCustomerAccount(CustomerUpdateParameters parameters)
        {
            // Initialize CustomerUpdateParameters object
            CustomerUpdateParameters customer = new CustomerUpdateParameters();

            // Retrieve customer ID from session
            string customerIdString = HttpContext.Session.GetString("CustomerId");
            if (string.IsNullOrEmpty(customerIdString) || !Guid.TryParse(customerIdString, out Guid customerId))
            {
                ModelState.AddModelError(string.Empty, "Invalid or missing customer ID.");
                return View(customer); // Return view with an empty customer object or handle it as needed
            }

            // Create request parameters
            var req = new CustomerGetByParameters
            {
                CustomerId = customerId
            };

            // Serialize request parameters to JSON
            var json = JsonConvert.SerializeObject(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send POST request to retrieve customer details
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Customer/GetById", content);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize response content to CustomerUpdateParameters object
                string data = await response.Content.ReadAsStringAsync();
                customer = JsonConvert.DeserializeObject<CustomerUpdateParameters>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to retrieve customer details.");
            }

            // Return view with customer details
            return View(customer);

            //// Update first name
            //var firstNameJson = JsonConvert.SerializeObject(parameters);
            //var firstNameContent = new StringContent(firstNameJson, Encoding.UTF8, "application/json");
            //HttpResponseMessage firstNameResponse = await _client.PatchAsync(_client.BaseAddress + "/Customer/ModifyFirstName", firstNameContent);

            //if (!firstNameResponse.IsSuccessStatusCode)
            //{
            //    ModelState.AddModelError(string.Empty, "Failed to update first name.");
            //    return View(parameters);
            //}

            //// Update last name
            //var lastNameJson = JsonConvert.SerializeObject(parameters);
            //var lastNameContent = new StringContent(lastNameJson, Encoding.UTF8, "application/json");
            //HttpResponseMessage lastNameResponse = await _client.PatchAsync(_client.BaseAddress + "/Customer/ModifyLastName", lastNameContent);

            //if (!lastNameResponse.IsSuccessStatusCode)
            //{
            //    ModelState.AddModelError(string.Empty, "Failed to update last name.");
            //    return View(parameters);
            //}

            //// Update password
            //var passwordJson = JsonConvert.SerializeObject(parameters);
            //var passwordContent = new StringContent(passwordJson, Encoding.UTF8, "application/json");
            //HttpResponseMessage passwordResponse = await _client.PatchAsync(_client.BaseAddress + "/Customer/ModifyPassword", passwordContent);

            //if (!passwordResponse.IsSuccessStatusCode)
            //{
            //    ModelState.AddModelError(string.Empty, "Failed to update password.");
            //    return View(parameters);
            //}

            //return View();
        }

        public async Task<IActionResult> About()
        {

            AboutUsCreateParameters? About = new AboutUsCreateParameters();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Admin/GetAboutUs");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                About =  JsonConvert.DeserializeObject<AboutUsCreateParameters>(data);
            }
            return View(About);  
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
