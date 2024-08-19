using MahaliMvc.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace MahaliMvc.Models.UserDto
{
    public class LoginDto
    {
        [Required]
        [Display(Name = "User Name or Email")]
        public string UserName_Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public UserType UserType { get; set; }

    }
}
