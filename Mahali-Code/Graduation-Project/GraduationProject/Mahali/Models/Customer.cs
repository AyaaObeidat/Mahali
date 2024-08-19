using System.ComponentModel.DataAnnotations;

namespace Mahali.Models
{
    public class Customer
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage ="FirstName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string FirstName { get; private set; } = null!;

        [Required(ErrorMessage = "LastName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "LastName must be between 3 and 10 alphabetical characters.")]
        public string LastName { get; private set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; private set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; private set; } = null!;

        private Customer() { }
        private Customer(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public static Customer Create(string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrEmpty(firstName)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(lastName)) { throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(email)) {  throw new ArgumentNullException(); }
            if (string.IsNullOrEmpty(password)) {  throw new ArgumentNullException(); }

            return new Customer(firstName, lastName, email, password);
        }

        public void SetFirstName(string firstName)
        {
            if(string.IsNullOrEmpty(firstName)) { throw new ArgumentNullException(); }
            FirstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            LastName = lastName ;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetPassword(string password)
        {
            if (password.Length < 8 || password.Length > 16)
            {
                throw new ValidationException();
            }
            Password = password;    
        }
    }
}
