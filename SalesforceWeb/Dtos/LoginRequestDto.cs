using System.ComponentModel.DataAnnotations;

namespace SalesforceWeb.Dtos
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Email is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
