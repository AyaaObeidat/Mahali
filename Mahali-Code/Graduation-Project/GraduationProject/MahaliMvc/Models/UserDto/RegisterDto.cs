using MahaliMvc.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace MahaliMvc.Models.UserDto
{
    public class RegisterDto
    {

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(25, MinimumLength = 14, ErrorMessage = "Email must be between 14 and 25 characters long.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()-+=])[a-zA-Z\d!@#$%^&*()-+=]{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters long, contain at least 3 numbers, 1 uppercase letter, and 1 special symbol.")]
        public string Password { get; set; }

        public UserType  UserType { get; set; }

        //Admin Register Dto

        [Required(ErrorMessage = "name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,15}$", ErrorMessage = "FirstName must be between 3 and 15 alphabetical characters.")]
        public string? UserName { get; set; }




        // Customer Register Dto 
        [Required(ErrorMessage = "FirstName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "LastName must be between 3 and 10 alphabetical characters.")]
        public string? LastName { get; set; }



        //Shop Register Dto

        [Required(ErrorMessage = "Shop name is required.")]
        [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "FirstName must be between 3 and 10 alphabetical characters.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long? PhoneNumber { get; set; }

    }
}
