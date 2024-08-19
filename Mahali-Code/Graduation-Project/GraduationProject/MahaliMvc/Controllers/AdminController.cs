using MahaliDtos;
using MahaliMvc.Models;
using MahaliMvc.Models.Enum;
using MahaliMvc.Models.UserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MahaliMvc.Controllers
{
    public class AdminController : BaseController
    {

        public async Task<IActionResult> GetAll()
        {
            List<AdminRegister> admins = new List<AdminRegister>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Admin/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                admins = JsonConvert.DeserializeObject<List<AdminRegister>>(data);
            }
            return View(admins);
        }
        //public async Task<IActionResult> AddCategory(MahaliDtos.CategoryCreateParameters parameters)
        //{
        //    var json = JsonConvert.SerializeObject(parameters);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Category/Add", content);
        //    return View();
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        public async Task<IActionResult> Register(RegisterDto input)
        {

            HttpResponseMessage response = new HttpResponseMessage();

            if (input.UserType == UserType.Admin)
            {
                var adminRegister = new AdminRegister
                {
                    Email = input.Email,
                    Password = input.Password,
                    UserName = input.UserName
                };

                var json = JsonConvert.SerializeObject(adminRegister);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Admin/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    // Save data in session
                    HttpContext.Session.SetString("UserEmail", input.Email);
                    HttpContext.Session.SetString("UserName", input.UserName);
                    HttpContext.Session.SetString("UserType", UserType.Admin.ToString());
                    return RedirectToAction("Dashboard","Admin");
                }
            }
            else if (input.UserType == UserType.Customer)
            {
                var customerRegister = new CustomerRegister
                {
                    Email = input.Email,
                    Password = input.Password,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                };

                var json = JsonConvert.SerializeObject(customerRegister);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Customer/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");

                }
            }
            else if (input.UserType == UserType.Shop)
            {
                var shopRegister = new ShopRegister
                {
                    Email = input.Email,
                    Password = input.Password,
                    Name = input.Name,
                    PhoneNumber = input.PhoneNumber.Value
                };

                var json = JsonConvert.SerializeObject(shopRegister);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Shop/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    // Save data in session
                    //HttpContext.Session.SetString("UserEmail", input.Email);
                    //HttpContext.Session.SetString("UserName", input.Name);
                    //HttpContext.Session.SetString("PhoneNumber", input.PhoneNumber.ToString());

                    HttpContext.Session.SetString("UserType", UserType.Shop.ToString());
                    return RedirectToAction("Login");
                }
            }
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            // Clear the session and redirect to the home page
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            if (loginDto.UserType == UserType.Admin)
            {
                var adminLogin = new AdminLogin
                {
                    UserName_Email = loginDto.UserName_Email,
                    Password = loginDto.Password
                };

                var json = JsonConvert.SerializeObject(adminLogin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Admin/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var admin = JsonConvert.DeserializeObject<AdminListItems>(responseData);
                    if (admin != null)
                    {

                        HttpContext.Session.SetString("UserEmail", admin.Email);
                        HttpContext.Session.SetString("UserName", admin.UserName);

                        HttpContext.Session.SetString("UserType", UserType.Admin.ToString());
                        return RedirectToAction("Dashboard", "Admin");
                    }
                }
            }
            else if (loginDto.UserType == UserType.Customer)
            {
                var customerLogin = new CustomerLogin
                {
                    UserName_Email = loginDto.UserName_Email,
                    Password = loginDto.Password
                };

                var json = JsonConvert.SerializeObject(customerLogin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Customer/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<CustomerListItems>(responseData);
                    if (user != null)
                    {
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
                        HttpContext.Session.SetString("CustomerId", user.ID.ToString());

                        HttpContext.Session.SetString("UserType", UserType.Customer.ToString());
                        return RedirectToAction("index", "Home");
                    }
                }
            }
            else if (loginDto.UserType == UserType.Shop)
            {
                var shopLogin = new ShopLogin
                {
                    UserName_Email = loginDto.UserName_Email,
                    Password = loginDto.Password
                };

                var json = JsonConvert.SerializeObject(shopLogin);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync(_client.BaseAddress + "/Shop/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<ShopListItems>(responseData);
                    if (user != null)
                    {

                        HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber.ToString());
                        HttpContext.Session.SetString("UserName", user.Name);
                        HttpContext.Session.SetString("ShopId", user.Id.ToString());

                        HttpContext.Session.SetString("UserType", UserType.Shop.ToString());
                        return RedirectToAction("index", "Shop");
                    }
                }
            }

            //var errorMessage = "Login failed. Please try again later.";


            //ModelState.AddModelError(string.Empty, errorMessage);

            return View();

        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> GetAllShops()
        {
            List<ShopRequestListItems> shops = new List<ShopRequestListItems>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Admin/GetAllShopRequest");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                shops = JsonConvert.DeserializeObject<List<ShopRequestListItems>>(data);
            }
            return View(shops);
            //// Store the data in TempData
            //TempData["Shops"] = JsonConvert.SerializeObject(shops);
            //return RedirectToAction("UpdateShopRequestStatus");
        }
        [HttpGet]
        public IActionResult EditAdminAccount()
        {
            AdminUpdateParameters user = new AdminUpdateParameters
            {
                UserName = HttpContext.Session.GetString("UserName"),
                CurrentPassword = "",
                NewPassword = "",

            };
            return View(user);
        }


        public async Task<IActionResult> EditAdminAccount(AdminUpdateParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(parameters));
            // Update username
            var usernameJson = JsonConvert.SerializeObject(parameters);
            var usernameContent = new StringContent(usernameJson, Encoding.UTF8, "application/json");
            HttpResponseMessage usernameResponse = await _client.PatchAsync(_client.BaseAddress + "/Admin/ModifyAccountUserName", usernameContent);

            if (!usernameResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update username.");
                return View(parameters);
            }

            // Update password
            var passwordJson = JsonConvert.SerializeObject(parameters);
            var passwordContent = new StringContent(passwordJson, Encoding.UTF8, "application/json");
            HttpResponseMessage passwordResponse = await _client.PatchAsync(_client.BaseAddress + "/Admin/ModifyAccountPassword", passwordContent);

            if (!passwordResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update password.");
                return View(parameters);
            }

            return View("Dashboard");
        }


        [HttpGet]
        public async Task<IActionResult> CreateAboutUs()
        {
            var about = new AboutUsListItem();



            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Admin/GetAboutUs");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if (data != null && data != "")
                {
                    about = JsonConvert.DeserializeObject<AboutUsListItem>(data);
                }
            }
            return View(about);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutUs(AboutUsListItem parameters)
        {
            var about = new AboutUsCreateParameters
            {
                ContentBody = parameters.ContentBody,
            };
            var json = JsonConvert.SerializeObject(about);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Admin/CreateAboutUs", content);

            if (response.IsSuccessStatusCode)
            {
            }
            return RedirectToAction("CreateAboutUs");

        }

        [HttpGet]
        public IActionResult UpdateAboutUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutUs(AboutUsUpdateParameters parameters)
        {
            //var json = JsonConvert.SerializeObject(parameters);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/api/Admin/UpdateAboutUsContentBody", content);

            //if (response.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Dashboard");
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Failed to update About Us content.");
            //    return View(parameters);
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShopRequestStatus(ShopRequestUpdateParameters shopRequest)
        {
            if (shopRequest != null && shopRequest.ShopId != null && shopRequest.Status != null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(shopRequest), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PatchAsync(_client.BaseAddress + "/Admin/UpdateShopRequestStatus", content);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("GetAllShops");
        }
        [HttpGet]
        public async Task<IActionResult> EditShop(Guid input)
        {
            var shops = new ShopListItems();
            var parameter = new ShopGetByParameter
            {
                ShopId = input
            };

            var jsonParameter = JsonConvert.SerializeObject(parameter);

            var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Shop/GetById", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                shops = JsonConvert.DeserializeObject<ShopListItems>(data);
            }
            return View(shops);
        }

        //public async Task<IActionResult> EditShop(ShopListItems shops)
        //{


        //    var jsonParameter = JsonConvert.SerializeObject(shops);

        //    var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Shop/GetById", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string data = await response.Content.ReadAsStringAsync();
        //        shops = JsonConvert.DeserializeObject<ShopListItems>(data);
        //    }
        //    return View(shops.Id);
        //}
        [HttpPost]
        public async Task<IActionResult> AddReport(ReportListItems report)
        {
            ReportCreateParameters req = new ReportCreateParameters
            {
                ReportText = report.ReportText,
                ShopId = report.Shop.Id
            };

            var jsonParameter = JsonConvert.SerializeObject(req);

            var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Report/Add", content);
            if (response.IsSuccessStatusCode)
            {
            }

            return RedirectToAction("GetAllShops");
        }
        [HttpGet]
        public async Task<IActionResult> AddReport(Guid input)
        {
            var report = new ReportListItems();
            var parameter = new ReportGetByParameters
            {
                ShopID = input
            };

            var jsonParameter = JsonConvert.SerializeObject(parameter);

            var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "/Report/GetById", content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                if (data != null && data != "")
                {
                    report = JsonConvert.DeserializeObject<ReportListItems>(data);
                }
                else
                {
                    report = new ReportListItems
                    {
                        Shop = new ShopDetails
                        {
                            Id = input,
                        }
                    };
                }
            }
            return View(report);
        }
        public async Task<IActionResult> GetAllReport()
        {
            List<ReportListItems> report = new List<ReportListItems>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Report/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                report = JsonConvert.DeserializeObject<List<ReportListItems>>(data);
            }
            return View(report);
            //// Store the data in TempData
            //TempData["Shops"] = JsonConvert.SerializeObject(shops);
            //return RedirectToAction("UpdateShopRequestStatus");
        }

    }
}