using System.ComponentModel.DataAnnotations;

namespace SalesforceWeb.Dtos
{
    public class RegisterationRequestDto
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string UserName { get; set; }        

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = null;
        public string Role { get; set; } = "customer";
    }
}
