using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Shop
    {
       
        public  Guid Id { get; set; }

        
        [Required(ErrorMessage = "Shop name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string Name { get; private set; } = null!;

        
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 50 characters.")]
        public string? Description { get; private set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; private set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; private set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long PhoneNumber { get; private set; } = 10;

        [Required]
        public Guid LocationId { get;private set; }

        public List<ShopOrders> Orders { get;private set; }
        public List<ReviewRequest> Reviews { get;private set; }
        public List<Product> Products { get; private set; } 

        private Shop() { }
        private Shop(string name , string email, string password, long phoneNumber ) 
        { 
            Name = name;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            //LocationId = locationId;
        }

        public static Shop Create(string name, string email, string password, long phoneNumber )
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(password)) {  throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException(); }
            //if(locationId == Guid.Empty) { throw new ArgumentNullException(); }

            return new Shop(name, email, password, phoneNumber );
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string description)
        {
           Description = description;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
        public void SetPhoneNumber(long phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void SetOrders(List<ShopOrders> orders)
        {
            Orders = orders;
        }

        public void SetReviews(List<ReviewRequest> reviews)
        {
            Reviews = reviews;
        }
        public void SetLocationId(Guid id)
        {
            LocationId = id;
        }
        public void SetProducts(List<Product> products)
        {
            Products = products;
        }
    }
}
