using Mahali.Models;
using Mahali.Repositories.Interfaces;
using Mahali.Security;
using MahaliDtos;

namespace Mahali.Services
{
    public class CustomerService
    {
        private readonly ICustomerInterface _customerInterface;
        private readonly ICartInterface _cartInterface;
        private readonly IWishListInterface _wishListInterface;
        private readonly IProductInterface _productInterface;
        private readonly ILatestProductVisitedInterface _latestProductVisitedInterface;
        private readonly IReviewRequestInterface _reviewRequestInterface;

        public CustomerService(ICustomerInterface customerInterface, ICartInterface cartInterface, IWishListInterface wishListInterface, IProductInterface productInterface, ILatestProductVisitedInterface latestProductVisitedInterface, IReviewRequestInterface reviewRequestInterface)
        {
            _customerInterface = customerInterface;
            _cartInterface = cartInterface;
            _wishListInterface = wishListInterface;
            _productInterface = productInterface;
            _latestProductVisitedInterface = latestProductVisitedInterface;
            _reviewRequestInterface = reviewRequestInterface;
        }

        public async Task RegisterAsync(CustomerRegister parameters)
        {
            var customers = await _customerInterface.GetAllAsync();
            foreach (var customer in customers)
            {
                if ((customer.FirstName != parameters.FirstName || customer.LastName != parameters.LastName) && customer.Email != parameters.Email && customer.Password != parameters.Password) continue;
                else return;
            }

            var newCustomer = Customer.Create(parameters.FirstName, parameters.LastName, parameters.Email, MD5Hasher.ComputeHash(parameters.Password));
            await _customerInterface.AddAsync(newCustomer);
            var cart = Cart.Create(newCustomer.Id);
            var wishList = WishList.Create(newCustomer.Id);
            await _cartInterface.AddAsync(cart);
            await _wishListInterface.AddAsync(wishList);
        }

        public async Task<CustomerListItems?> LoginAsync(CustomerLogin login)
        {
            var customers = await _customerInterface.GetAllAsync();
            foreach (var customer in customers)
            {
                string fullName = customer.FirstName + " " + customer.LastName;
                if ((fullName == login.UserName_Email || customer.Email == login.UserName_Email) && customer.Password == MD5Hasher.ComputeHash(login.Password))
                {
                    return new CustomerListItems
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        ID = customer.Id,
                    };
                }

            }
            return null;
        }

        public async Task ModifyFirstName(CustomerUpdateParameters parameters)
        {
            var customer = await _customerInterface.GetByIdAsync(parameters.Id);
            var customers = await _customerInterface.GetAllAsync();
            if (customer == null) return;

            foreach (var custom in customers)
            {
                if (custom.FirstName != parameters.NewFirstName || custom.LastName != parameters.NewLastName) continue;
                else return;
            }
            customer.SetFirstName(parameters.NewFirstName);
            await _customerInterface.UpdateAsync(customer);
        }
        public async Task ModifyLastName(CustomerUpdateParameters parameters)
        {
            var customer = await _customerInterface.GetByIdAsync(parameters.Id);
            var customers = await _customerInterface.GetAllAsync();
            if (customer == null) return;

            foreach (var custom in customers)
            {
                if (custom.FirstName != parameters.NewFirstName || custom.LastName != parameters.NewLastName) continue;
                else return;
            }
            customer.SetLastName(parameters.NewLastName);
            await _customerInterface.UpdateAsync(customer);
        }

        public async Task ModifyPassword(CustomerUpdateParameters parameters)
        {
            var customer = await _customerInterface.GetByIdAsync(parameters.Id);
            var customers = await _customerInterface.GetAllAsync();
            if (customer == null) return;
            if (customer.Password == MD5Hasher.ComputeHash(parameters.CurrentPassword))
            {

                foreach (var custom in customers)
                {

                    if (custom.Password != MD5Hasher.ComputeHash(parameters.NewPassword)) continue;
                    else return;
                }
                customer.SetPassword(MD5Hasher.ComputeHash(parameters.NewPassword));
                await _customerInterface.UpdateAsync(customer);
            }
        }

        public async Task<List<CustomerListItems>> GetAllAsync()
        {
            var customers = await _customerInterface.GetAllAsync();
            return customers.Select(x => new CustomerListItems
            {
                ID = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
            }).ToList();
        }

        public async Task<CustomerListItems?> GetByIdAsync(CustomerGetByParameters parameters)
        {
            var customer = await _customerInterface.GetByIdAsync(parameters.CustomerId);
            return new CustomerListItems
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
            };
        }
        //========
        public async Task AddLatestProductVisitedAsync(LatestProductsVisitedCreateParameters parameters)
        {

            var latestProductsVisited = await _latestProductVisitedInterface.GetAllAsync();
            foreach(var latestProduct in latestProductsVisited)
            {
                if (latestProduct.ProductId == parameters.ProductId && latestProduct.CustomerId == parameters.CustomerId)
                {
                    latestProduct.SetVisitedDateTime(DateTime.Now);
                    await _latestProductVisitedInterface.UpdateAsync(latestProduct);
                    return;

                }

            }

            var customer = await _customerInterface.GetByIdAsync(parameters.CustomerId);
            if (customer == null) { return; }
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            var latestProductVisited = LatestProductsVisited.Create(parameters.CustomerId, parameters.ProductId);
            await _latestProductVisitedInterface.AddAsync(latestProductVisited);
        }

        public async Task<List<LatestProductsVisitedDetails>> GetAllLatestProductVisitedAsync(LatestProductsVisitedCreateParameters parameters)
        {
            var latestProductVisited = await _latestProductVisitedInterface.GetAllAsync();
            var customers = await _customerInterface.GetAllAsync();
            var allProducts = await _productInterface.GetAllAsync();
            var products = latestProductVisited
                .OrderByDescending(x => x.VisitedDateTime)
                .Where(x=>x.CustomerId == parameters.CustomerId)
                .Select(p => new LatestProductsVisitedDetails
                {
                    Customer = customers.Select(c => new CustomerDetails
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName ,
                        Email = c.Email,
                        Password =c.Password
                    }).FirstOrDefault(cc => cc.Id == p.CustomerId),

                    Product = allProducts.Select(pro => new ProductDetails
                    {
                        Id = pro.Id,
                        Name = pro.Name,
                        Price = pro.Price,
                        ImageUri = pro.ImageUri,
                        Quantity = pro.Quantity
                        
                    }).FirstOrDefault(xx => xx.Id == p.ProductId),
                    VisitedDateTime = p.VisitedDateTime,
                }).ToList();

            return products;
        }

        public async Task<List<LatestProductsVisitedDetails>> GetLatestProductVisitedByCustomerIdAsync(LatestProductsGetByParameters parameters)
        {
            var latestProductsVisited = await _latestProductVisitedInterface.GetByCustomerIdAsync(parameters.CustomerId);
            var allProducts = await _productInterface.GetAllAsync();
            return latestProductsVisited.Select(x => new LatestProductsVisitedDetails
            {

                Product = allProducts.Select(pro => new ProductDetails
                {
                    Id = pro.Id,
                    Name = pro.Name,
                    Price = pro.Price,
                    ImageUri = pro.ImageUri,
                    Quantity = pro.Quantity

                }).FirstOrDefault(xx => xx.Id == x.ProductId),
                VisitedDateTime = x.VisitedDateTime,
            }).ToList();
        }

        public async Task AddCommentOnProductAsync(ReviewRequestCreateParameters parameters)
        {
            var product = await _productInterface.GetByIdAsync(parameters.ProductId);
            if (product == null) return;
            var customer = await _customerInterface.GetByIdAsync(parameters.CustomerId);
            if (customer == null) return;

            var comment = ReviewRequest.Create(parameters.CustomerId, parameters.ProductId, parameters.ReviewContentBody);
            await _reviewRequestInterface.AddAsync(comment);
        }
    }
}

