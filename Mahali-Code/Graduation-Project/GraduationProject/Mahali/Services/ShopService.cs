
using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Security;
using MahaliDtos;
using static AllEnums.AllEnums;

namespace Mahali.Services
{
    public class ShopService
    {
        private readonly IShopInterface _shopInterface;
        private readonly IShopRequestInterface _shopRequestInterface;
        private readonly IAdminInterface _adminInterface;
        private readonly IProductInterface _productInterface;
        private readonly ICustomerInterface _customerInterface;
        private readonly IReviewRequestInterface _reviewInterface;

        public ShopService(IShopInterface shopInterface,IShopRequestInterface shopRequestInterface, IAdminInterface adminInterface, IProductInterface productInterface, ICustomerInterface customerInterface, IReviewRequestInterface reviewInterface)
        {
            _shopInterface = shopInterface;
            _shopRequestInterface = shopRequestInterface;
            _adminInterface = adminInterface;
            _productInterface = productInterface;
            _customerInterface = customerInterface;
            _reviewInterface = reviewInterface;
        }

        public async Task RegisterAsync(ShopRegister parameters)
        {
            var shops = await _shopInterface.GetAllAsync();
            foreach (var shop in shops)
            {
                if (shop.Name != parameters.Name && shop.Email != parameters.Email ) continue;
                else return;
            }
            var newShop = Shop.Create(parameters.Name, parameters.Email, MD5Hasher.ComputeHash(parameters.Password), parameters.PhoneNumber);
            await _shopInterface.AddAsync(newShop);
            var admin = await _adminInterface.GetByIdAsync(await _adminInterface.GetIdAsync());
            ShopRequest request = ShopRequest.Create(admin.Id, newShop.Id);
            await _shopRequestInterface.AddAsync(request);
        }

        public async Task<ShopListItems?> LoginAsync(ShopLogin login )
        {
            var shops = await _shopInterface.GetAllAsync();
            foreach (var shop in shops)
            {
                if( ( shop.Name == login.UserName_Email ||  shop.Email == login.UserName_Email) && shop.Password == MD5Hasher.ComputeHash(login.Password))
                {
                    var request = await _shopRequestInterface.GetRequestByShopIdAsync(shop.Id);
                    if(request.Status == RequestStatus.Approved )
                    {
                        var orders = shop.Orders.Select(x => new ShopOrdersDetails
                        {
                            OrderId = x.OrderId,
                            ShopId = x.ShopId,
                        }).ToList();

                        var reviews = shop.Reviews.Select(x => new ReviewRequestDetails
                        {
                           ProductId = x.ProductId,
                           CustomerId = x.CustomerId,
                           ReviewContentBody  = x.ReviewContentBody,
                           CreatedAt = x.CreatedAt,
                           Status = x.Status,
                        }).ToList();

                        return new ShopListItems
                        {
                            Id = shop.Id,   
                            Name = shop.Name,
                            Description = shop.Description,
                            PhoneNumber = shop.PhoneNumber,
                            Orders = orders,
                            Reviews = reviews,
                        };
                    }
                }
            }
            return null;
        }

       
        public async Task ModifyShopNameAsync(ShopUpdateParameters parameters)
        {

           var request = await _shopRequestInterface.GetRequestByShopIdAsync(parameters.Id);
           var shops = await _shopInterface.GetAllAsync();
           if (request.Status == RequestStatus.Approved)
           {
              foreach (var shop in shops)
              {
                if (shop.Name != parameters.Name) { continue; }
                else return;
              }
              var selectedShop = await _shopInterface.GetByIdAsync(parameters.Id);
              selectedShop.SetName(parameters.Name);
              await _shopInterface.UpdateAsync(selectedShop);
           }
        }
        public async Task ModifyShopPasswordAsync(ShopUpdateParameters parameters)
        {
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(parameters.Id);
            var shops = await _shopInterface.GetAllAsync();
            if (request.Status == RequestStatus.Approved)
            {
                foreach (var shop in shops)
                {
                    if (shop.Password != MD5Hasher.ComputeHash(parameters.NewPassword)) { continue; }
                    else return;
                }
                var selectedShop = await _shopInterface.GetByIdAsync(parameters.Id);
                if (selectedShop.Password == MD5Hasher.ComputeHash(parameters.CurrentPassword))
                {
                    selectedShop.SetPassword(MD5Hasher.ComputeHash(parameters.NewPassword));
                    await _shopInterface.UpdateAsync(selectedShop);
                }

            }
        }


        public async Task ModifyShopPhoneNumberAsync(ShopUpdateParameters parameters)
        {
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(parameters.Id);
            var shops = await _shopInterface.GetAllAsync();
            if (request.Status == RequestStatus.Approved)

            {
                foreach (var shop in shops)
                {
                    if (shop.PhoneNumber != parameters.PhoneNumber) { continue; }
                    else return;
                }
                var selectedShop = await _shopInterface.GetByIdAsync(parameters.Id);
                selectedShop.SetPhoneNumber(parameters.PhoneNumber);
                await _shopInterface.UpdateAsync(selectedShop);
            }
        }

        public async Task ModifyShopDescriptionAsync(ShopUpdateParameters parameters)
        {
            var request = await _shopRequestInterface.GetRequestByShopIdAsync(parameters.Id);
            if (request.Status == RequestStatus.Approved)
            {

                var selectedShop = await _shopInterface.GetByIdAsync(parameters.Id);
                selectedShop.SetDescription(parameters.Description);
                await _shopInterface.UpdateAsync(selectedShop);
            }
        }

        public async Task<List<ShopOrdersDetails>> GetShopOrdersAsync(ShopGetByParameter parameter)
        {
            var shop = await _shopInterface.GetByIdAsync(parameter.ShopId);
            var orders = shop.Orders.ToList();
            return orders.Select(x => new ShopOrdersDetails
            {
                ShopId = x.ShopId,
                OrderId = x.OrderId,
            }).ToList();
        }

        public async Task<List<ProductListItems>> GetShopProductsAsync(ShopGetByParameter parameter)
        {
            var shop = await _shopInterface.GetByIdAsync(parameter.ShopId);
            var products = shop.Products.ToList();
            var shops = await _shopInterface.GetAllAsync();
            return products.Select(x => new ProductListItems
            {
               
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Shop = shops.Select(s => new ShopDetails
                {
                   Id = s.Id,
                   Name = s.Name,
                   Description= s.Description,  
                   Email = s.Email,
                   Password = s.Password
                }).FirstOrDefault(y => y.Id == x.ShopId),
                ImageUri = x.ImageUri,
                Quantity = x.Quantity,
                
            }).ToList();
        }

       
        public async Task<ShopListItems> GetByIdAsync (ShopGetByParameter parameter)
        {
            var shop = await _shopInterface.GetByIdAsync (parameter.ShopId);
            var orders = shop.Orders.Select(x => new ShopOrdersDetails
            {
                OrderId = x.OrderId,
                ShopId = x.ShopId,
            }).ToList();

            var reviews = shop.Reviews.Select(x => new ReviewRequestDetails
            {
                ProductId = x.ProductId,
                CustomerId = x.CustomerId,
                ReviewContentBody = x.ReviewContentBody,
                CreatedAt = x.CreatedAt,
                Status = x.Status,
            }).ToList();
            return new ShopListItems
            {
                Id = shop.Id,
                Name = shop.Name,
                Description = shop.Description,
                PhoneNumber = shop.PhoneNumber,
                Orders = orders,
                Reviews = reviews,
            };
        }

        public async Task<List<ShopListItems>> GetAllAsync()
        {
            var shops = await _shopInterface.GetAllAsync();
            return shops.Select(s => new ShopListItems
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                PhoneNumber = s.PhoneNumber,
                
            }).ToList();
        }

        public async Task<List<ShopListItems>> GetApprovedShopsAsync()
        {
            var allRequests = await _shopRequestInterface.GetAllAsync();
            List<Shop> approvedShops = new List<Shop>();
            foreach (var request in allRequests)
            {
                if(request.Status == RequestStatus.Approved)
                {
                    var shop = await _shopInterface.GetByIdAsync(request.ShopId);
                    approvedShops.Add(shop);
                }
            }

            return approvedShops.Select(s => new ShopListItems
            {
                Name = s.Name,
                Description = s.Description,
                PhoneNumber = s.PhoneNumber,

            }).ToList();
        }

        public async Task UpdateReviewRequestStatusAsync(ReviewRequestUpdateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var customer = await _customerInterface.GetByIdAsync(parameters.CustomerId); 
            if (customer == null) return;
            var reviews = await _reviewInterface.GetAllAsync();
            foreach (var review in reviews)
            {
                if(review.ProductId == parameters.ProductId && review.CustomerId ==  parameters.CustomerId)
                {
                    if (parameters.Status == RequestStatus.Approved)
                    {
                        review.SetRequestStatus(RequestStatus.Approved);
                        await _reviewInterface.UpdateAsync(review);
                    }

                    else if (parameters.Status == RequestStatus.Rejected)
                    {
                        review.SetRequestStatus(RequestStatus.Rejected);
                        await _reviewInterface.UpdateAsync(review);
                    }

                    else return;
                }
            }
        }

        public async Task<List<ReviewRequestListItems>?> GetAllReviewsByShopIdAsync(ShopGetByParameter parameter)
        {
            List<ReviewRequest> reviews = new List<ReviewRequest>();
            var products = await _productInterface.GetAllAsync();
            var allReviews = await _reviewInterface.GetAllAsync();
            var shop = await _shopInterface.GetByIdAsync(parameter.ShopId);
            var customers = await _customerInterface.GetAllAsync();
            foreach (var review in allReviews)
            {
                foreach (var product in products)
                {
                    if (review.ProductId == product.Id && product.ShopId == parameter.ShopId)
                    {
                        reviews.Add(review);

                    }
                }
            }
            shop.SetReviews(reviews);
            await _shopInterface.UpdateAsync(shop);

            return shop.Reviews.ToList().Select(x => new ReviewRequestListItems
            {

                Customer = customers.Select(x => new CustomerDetails
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password
                }).FirstOrDefault(c => c.Id == x.CustomerId),
                Product = products.Select(x => new ProductDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ImageUri =  x.ImageUri,
                }).FirstOrDefault(c => c.Id == x.ProductId),
                ReviewContentBody = x.ReviewContentBody,
                CreatedAt = x.CreatedAt,
                Status = x.Status,

            }).ToList();
        }

        public async Task<List<ReviewRequestListItems>?> GetAllReviewsOnSpecificProductAsync(ProductGetById parameter)
        {
            var product = await _productInterface.GetByIdAsync(parameter.ProductId);
            if (product == null) return null;
            var reviews = product.Reviews.ToList();
            var customers = await _customerInterface.GetAllAsync();
            return reviews.Select(r => new ReviewRequestListItems
            {
                Customer = customers.Select(x => new CustomerDetails
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password
                }).FirstOrDefault(c => c.Id == r.CustomerId),
                Product = new ProductDetails
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ImageUri = product.ImageUri,
                },
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                ReviewContentBody = r.ReviewContentBody,
            }).ToList();
        }

        public async Task<List<ReviewRequestListItems>?> GetAllApprovedReviewsOnSpecificProductAsync(ProductGetById parameter)
        {
            var product = await _productInterface.GetByIdAsync(parameter.ProductId);
            if (product == null) return null;
            var reviews = product.Reviews.ToList();
            var customers = await _customerInterface.GetAllAsync();
            return reviews.Select(r => new ReviewRequestListItems
            {
                Customer = customers.Select(x => new CustomerDetails
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password
                }).FirstOrDefault(c => c.Id == r.CustomerId),
                Product = new ProductDetails
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    ImageUri = product.ImageUri,
                },
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                ReviewContentBody = r.ReviewContentBody,
            }).Where(x => x.Status == RequestStatus.Approved).ToList();
        }

        public async Task<ShopUpdateParameters> GetShopByIdAsync(ShopGetByParameter parameter)
        {
            var shop = await _shopInterface.GetByIdAsync(parameter.ShopId);
            if (shop != null)
            {
                return new ShopUpdateParameters
                {
                    Id = shop.Id,
                    Description = shop.Description,
                    Name = shop.Name,
                    Email = shop.Email,
                    PhoneNumber = shop.PhoneNumber,
                    CurrentPassword = "",
                    NewPassword = ""
                };
            }
            return new ShopUpdateParameters();
        }

        }
}
   
