using MahaliMvc.Models.Enum;

namespace MahaliMvc.Models.UserDto
{
    public class UserInfo
    {
        public string? Email { get; set; }

        public string? UserType { get; set; }

        //Admin Register Dto

        public string? UserName { get; set; }

        // Customer Register Dto 
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        //Shop Register Dto

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
