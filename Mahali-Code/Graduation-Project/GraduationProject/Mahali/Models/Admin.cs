using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Admin
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Shop name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "FirstName must be between 3 and 15 alphabetical characters.")]
        public string UserName { get; private set; } = null!;



        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is not in a correct format.")]
        public string Email { get; private set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; private set; } = null!;
        public List<Report> Reports { get; private set; }
        public List<ShopRequest> ShopRequests { get; private set; }

        private Admin() { }
        private Admin (string userName ,  string email ,  string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public static Admin Create(string userName, string email, string password)
        {
            if (string.IsNullOrEmpty(userName)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(password)) { throw new ArgumentNullException(); }
            return new Admin(userName, email, password);    
        }
        public void SetUserName(string userName)
        {
            UserName = userName;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetPassword(string password)
        {
            Password = password;
        }
        public void SetReports(List<Report> reports)
        {
            Reports = reports;
        }
        public void SetShopRequests(List<ShopRequest> requests)
        {
           ShopRequests = requests;
        }

       
    }
}
