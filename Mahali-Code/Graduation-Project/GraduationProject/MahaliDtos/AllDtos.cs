using System.ComponentModel.DataAnnotations;
using static AllEnums.AllEnums;

namespace MahaliDtos
{
    //Admin
    public class AdminRegister
    {
        [Required(ErrorMessage = "name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "FirstName must be between 3 and 15 alphabetical characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; set; }


    }

    public class AdminLogin
    {
        public string UserName_Email { get; set; }
        public string Password { get; set; }
    }

    public class AdminListItems
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public List<ReportListItems> Reports { get; set; }
        public List<ShopRequestDetails> ShopRequests { get; set; }

    }

    public class AdminUpdateParameters
    {
        [Required(ErrorMessage = "name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "FirstName must be between 3 and 15 alphabetical characters.")]
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string NewPassword { get; set; }
    }
    //AboutUs
    public class AboutUsCreateParameters
    {
       
        public string ContentBody { get; set; }
    }

    public class AboutUsUpdateParameters
    {
      
        public string ContentBody { get; set; }
    }

    public class AboutUsListItem
    {
        public Guid AdminId { get; set;}
        public string ContentBody { get; set;}
    }
    //Report

    public class ReportCreateParameters
    {
        [Required]
        public string ReportText { get; set; }
        public Guid ShopId { get; set; }
    }

    public class ReportUpdateParameters
    {
        public Guid ShopId { get; set; }
        public string ReportText { get; set; }
    }

    public class ReportDeleteParameters
    {
        public Guid ShopId { get; set; }
    }

    public class ReportDetails
    {
        public Guid Id { get; set; }
        public string ReportText { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AdminId { get; set; }
        public Guid ShopId { get; set; }
    }

    public class ReportListItems
    {
        public Guid Id { get; set; }   
        public string ReportText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public ShopDetails? Shop { get; set; }
    }

    public class ReportGetByParameters
    {
        public Guid ShopID { get; set; }
    }

    //Category
    public class CategoryCreateParameters
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

    }

    public class CategoryDeleteParameters
    {
        public Guid CategoryId { get; set; }
    }

    public class CategoryUpdateParameters
    {

        public Guid CategoryId { get; set; }
        public string UpdatedName { get; set; }
    }

    public class CategoryDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CategoryListItems
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<ProductListItems> Products { get; set; }
    }

    public class CategoryGetByParameter
    {
        public Guid CategoryId { get; set; }
    }

    // Shop
    public class ShopRegister
    {

        [Required(ErrorMessage = "Shop name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long PhoneNumber { get; set; }

  

        //[Required]
        //public Guid LocationId { get; private set; }
    }

    public class ShopLogin
    {
        public string Password { get; set; }
        public string UserName_Email { get; set; }
    }

    public class ShopListItems
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string? Description { get; set; }
        public long PhoneNumber { get; set; }
        public List<ShopOrdersDetails> Orders { get; set; }
        public List<ReviewRequestDetails> Reviews { get; set; }
    }
    public class ShopDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public Guid LocationId { get; set; }
        public List<ShopOrdersDetails> Orders { get; set; }
        public List<ReviewRequestDetails> Reviews { get; set; }
    }
    public class ShopGetByParameter
    {
        public Guid ShopId { get; set; }
    }

    public class ShopUpdateParameters
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPassword { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public Guid LocationId { get; set; }
    }

    //ShopRequest
    public class ShopRequestCreateParameters
    {
        public Guid AdminId { get; set; }
        public Guid ShopId { get; set; }
    }

    public class ShopRequestListItems
    {
        public Guid Id { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public ShopDetails Shop { get; set; }
    }

    public class ShopRequestDetails
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public Guid ShopId { get; set; }
        public RequestStatus Status { get; set; }

    }
    public class ShopRequestUpdateParameters
    {
        
        public RequestStatus Status { get; set; }

        public Guid ShopId { get; set; }
    }

    //Product

    public class ProductCreateParameters
    {

        [Required(ErrorMessage = "Product name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "Name must be between 3 and 10 alphabetical characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 50 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Quantity of Product is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price of Product is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "ImageUri of Product is required.")]
        public string ImageUri { get; set; }

        [Required]
        public Guid ShopId { get; set; }


    }

    public class ProductDeleteParameters
    {

        public Guid ProductId { get; set; }
    }
    public class ProductDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUri { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductColorsDetails> ColorsList { get; set; }
        public List<ProductSizesDetails> SizesList { get; set; }
        public List<CartProductsDetails> Carts { get; set; }
        public List<WishListProductsDetails> WishLists { get; set; }
        public List<OrderProductsDetails> Orders { get; set; }
        public List<ReviewRequestDetails> Reviews { get; set; }
    }

    public class ProductGetByParameter
    {
        public Guid ProductId { get; set; }
       public Guid CategoryId { get; set; }
    }
   
    public class ProductGetById
    {
        public Guid ProductId { get; set; }
    }
    public class ProductListItems
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUri { get; set; }
        public int? Quantity { get;set; }

        public List<ProductColorsDetails> ColorsList { get; set; }
        public List<ProductSizesDetails> SizesList { get; set; }
        public ShopDetails? Shop { get; set; }
        public List<ReviewRequestDetails> Reviews { get; set; }
    }
    public class ProductUpdateParameters
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public int NewQuantity { get; set; }
        public decimal NewPrice { get; set; }
        public string NewImageUri { get; set; }
    }

    //Product Colore and Size

    public class ProductColorsCreateParameters
    {

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Colors Color { get; set; }
    }
    public class ProductColorGetByParameters
    {
        public Guid Id { get; set; }
    }
    public class ProductColorsDetails
    {
        public Guid Id { get; set; }
        public ProductDetails? Product { get; set; }
        public Colors Color { get; set; }
    }

    public class ProductColorDeleteParameters
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid ProductColorId { get; set; }
    }
    public class ProductSizesCreateParameters
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Sizes Size { get; set; }
    }

    public class ProductSizeGetByParameters
    {
        public Guid Id { get; set; }
    }

    public class ProductSizeDeleteParameters
    {

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid ProductSizeId { get; set; }
    }

    public class ProductSizesDetails
    {
        public Guid Id { get; set; }
        public ProductDetails? Product { get; set; }
        public Sizes Size { get; set; }
    }

    //Cart
    public class CartCreateParameters
    {
        public Guid CustomerId { get; set; }
    }
    public class CartDetails
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public List<CartProductsDetails> Products { get; set; }
    }
    public class CartListItems
    {
        public Guid ID { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartProductsListItems> Products { get; set; }
    }
    public class CartUpdateParameters
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartProductsListItems> Products { get; set; }
    }
    public class CartGetByParameters
    {
        public Guid CartId { get; set; }
    }
    //CartProducts
    public class CartProductsCreateParameters
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public Colors Color { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        public Sizes Size { get; set; }
    }

    public class CartProductsDetails
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } = 0!;
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }
    public class CartProductsListItems
    {
        public Guid CartId { get; set; }
        public ProductDetails? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } = 0!;
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }
    public class CartProductsUpdateParameters
    {
       
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }
    public class CartProductsDeleteParameters
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }

    //Wishlist
    public class WishListCreateParameters
    {

        [Required]
        public Guid CustomerId { get; set; }
    }
    public class WishListDetails
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<WishListProductsDetails> Products { get; set; }
    }
    public class WishListListItems
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<WishListProductsDetails> Products { get; set; }
    }
    public class WishListUpdateParameters
    {
        public Guid Id { get; set; }
        public List<WishListProductsDetails> Products { get; set; }
    }
    public class WishListGetByParameters
    {
        public Guid CustomerId { get; set; }
    }
    //WishListProducts
    public class WishListProductsCreateParameters
    {
        [Required]
        public Guid WishListId { get;  set; }
        [Required]
        public Guid ProductId { get;  set; }
    }
    public class WishListProductsDetails
    {
        public Guid Id { get; set; }
        public Guid WishListId { get; set; }
        public ProductDetails? Product { get; set; }
    }
    public class WishListProductsDeleteParameters
    {
        public Guid WishListId { get; set; }
        public Guid ProductId { get; set; }
    }
    //Order
    public class OrderCreateParameters
    {
        public Guid CartId { get; set; }
        public OrderType TypeOfOrder { get; set; }
        public PaymentType TypeOfPayment { get; set; }
    }
    public class OrderDetails
    {
        public int Id { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CartId { get; set; }
        public OrderType TypeOfOrder { get; set; }
        public PaymentType TypeOfPayment { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProductsDetails> Products { get; set; }
        public List<ShopOrdersDetails> Shops { get; set; }
    }
    public class OrderListItems
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class OrderGetByParameters
    {
       public int OrderId { get; set; } 
       public long PhoneNumber { get; set; }
        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must be 16 digits.")]
        public string CardNumber { get;  set; }
    }

    public class OrderGetById
    {
        public int OrderId { set; get; }
    }
    //OrderProducts
    public class OrderProductsCreateParameters
    {

        [Required]
        public int OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public decimal UnitPrice { get; set; } = 0!;

        [Required(ErrorMessage = "Color is required.")]
        public Colors Color { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        public Sizes Size { get; set; }
    }
    public class OrderProductsDetails
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }
    public class OrderProductsListItems
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
    public class OrderProductsUpdateParameters
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Colors Color { get; set; }
        public Sizes Size { get; set; }
    }

    //ShopOrders
    public class ShopOrdersCreateParameters
    {
        public int OrderId { get; set; }
        public Guid ShopId { get; set; }
    }
    public class ShopOrdersDetails
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public Guid ShopId { get; set; }
    }

    //ReviewRequest
    public class ReviewRequestCreateParameters
    {
        [Required(ErrorMessage = "Review content body is required")]
        public string ReviewContentBody { get;  set; } 
        public Guid ProductId { get;  set; }
        public Guid CustomerId { get;  set; }
    }
    public class ReviewRequestDetails
    {
        public Guid Id { get; set; }
        public string ReviewContentBody { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public RequestStatus Status { get; set; }
    }
    public class ReviewRequestListItems
    {
        public CustomerDetails? Customer { get; set; }
        public ProductDetails? Product { get; set; }
        public string ReviewContentBody { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
    }
    public class ReviewRequestUpdateParameters
    {
        [Required] 
        public Guid CustomerId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public RequestStatus Status { get; set; }
    }

    //Discount
    public class DiscountCreateParameters
    {
        [Required(ErrorMessage = "Discount percentage is required")]
        public decimal DiscountPercentage { get;  set; }

        [Required(ErrorMessage = "Start date of discount is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "Date and time must be in the format yyyy-MM-dd HH:mm:ss")]
        public string StartDate { get;  set; }

        [Required(ErrorMessage = "End date of discount is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}$", ErrorMessage = "Date and time must be in the format yyyy-MM-dd HH:mm:ss")]
        public string EndDate { get;  set; }
        public Guid ProductId { get; set; }
    }
    public class DiscountDeleteParameters
    {
       
        public Guid ProductId { get; set; }
    }
    public class DiscountDetails
    {
        public Guid Id { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProductId { get; set; }
    }
    public class DiscountListItems
    {
        public decimal DiscountPercentage { get; set; }
        public Guid ProductId { get; set; }
    }
    public class DiscountUpdateParameters
    {
        
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProductId { get; set; }
    }
   
    //Location
    public class LocationCreateParameters
    {

        [Required(ErrorMessage = "Address text is required.")]
        public string AddressText { get; set; }

        [Required(ErrorMessage = "Address direction is required.")]
        public string AddressDirection { get; set; }
    }

    public class LocationDetails
    {
        public Guid Id { get; set; }
        public string AddressText { get; set; }
        public string AddressDirection { get; set; }
        public Guid ShopId { get; set; }

    }
    public class LocationListItems
    {
        public Guid Id { get; set; }
        public string AddressText { get; set; }
        public string AddressDirection { get; set; }
    }
    public class LocationUpdateParameters
    {
        public Guid Id { get; set; }
        public string AddressText { get; set; }
        public string AddressDirection { get; set; }
    }

    //LatestProductSisited
    public class LatestProductsVisitedCreateParameters
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
    public class LatestProductsVisitedDetails
    {
        public Guid Id { get; set; }
        public DateTime VisitedDateTime { get; set; }
        public CustomerDetails? Customer { get; set; }
        public ProductDetails? Product { get; set; }
    }
    public class LatestProductsGetByParameters
    {
       
        public Guid CustomerId { get; set; }
    }

    //Customer
    public class CustomerRegister
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "LastName must be between 3 and 10 alphabetical characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; set; }
    }
    public class CustomerLogin
    {

        public string UserName_Email { get; set; }
        public string Password { get; set; }
    }


    public class CustomerDetails
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class CustomerListItems
    {
        public Guid ID { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
    public class CustomerUpdateParameters
    {
        public Guid Id { get; set; }
        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class CustomerGetByParameters
    {
        public Guid CustomerId { get; set; }
    }

    public class AddCustomerProductToCard
    {
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class HomePageViewModel
    {
        public List<CategoryListItems> Categories { get; set; }
        public List<ProductListItems> Products { get; set; }
    }

    public class ProductSearchParameters
    {
        public string SearchQuery { get; set; } 
    }

}

