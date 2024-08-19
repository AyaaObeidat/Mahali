using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Product
    {
    
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "Name must be between 3 and 10 alphabetical characters.")]
        public string Name { get; private set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 50 characters.")]
        public string Description { get; private set; } = null!;

        [Required(ErrorMessage = "Quantity of Product is required.")]
        public int Quantity { get; private set; }

        [Required(ErrorMessage = "Price of Product is required.")]
        public decimal Price { get; private set;} = 0!;

        [Required(ErrorMessage = "ImageUri of Product is required.")]
        public string ImageUri { get; private set; } =null!;

        public Guid? CategoryId { get;private set; } 

        [Required]
        public Guid ShopId { get; private set; }

        [Required(ErrorMessage = "Colors of Product is required.")]
        public List<ProductColors> ColorsList { get; private set; }

        [Required(ErrorMessage = "Sisez of Product is required.")]
        public List<ProductSizes> SizesList { get; private set; }

        public List<CartProducts> Carts {  get; private set; }
        public List<WishListProducts> WishLists { get; private set; }
        public List<OrderProducts> Orders { get; private set; }
        public List<ReviewRequest> Reviews { get; private set; }

        private Product() { }
        private Product(string name , string description , int quantity , decimal price , 
            string imageUri , Guid shopId )
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
            ImageUri = imageUri;
            ShopId = shopId;

        }

        public static Product Create(string name, string description, int quantity, decimal price,
            string imageUri, Guid shopId)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(description)) { throw new ArgumentNullException(); }
            if (quantity < 0) { throw new ArgumentOutOfRangeException(); }
            if (price <= 0) { throw new ArgumentOutOfRangeException(); }
            if(shopId == Guid.Empty) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(imageUri)) { throw new ArgumentNullException(); }

            return new Product(name , description, quantity, price, imageUri,shopId);

        }

        public void SetCarts(List<CartProducts> carts)
        {
            Carts = carts;
        }
        public void SetWishLists(List<WishListProducts> wishLists)
        {
            WishLists = wishLists;
        }

        public void SetOrders(List<OrderProducts> orders)
        {
            Orders = orders;
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string discription)
        {
            Description = discription;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
        public void SetPrice(decimal price)
        {
            Price = price;
        }
        public void SetImageUri(string imageUri)
        {
            ImageUri = imageUri;
        }
        public void SetColors(List<ProductColors> colors)
        {
            ColorsList = colors;
        }
        public void SetSizesList(List<ProductSizes> sizes)
        {
            SizesList = sizes;
        }
        public void SetReviews(List<ReviewRequest> reviews)
        {
            Reviews = reviews;
        }
        public void SetCategoryId(Guid categoryId)
        {
            if (categoryId == Guid.Empty) throw new ArgumentNullException();
            CategoryId = categoryId;
        }
    }
}
