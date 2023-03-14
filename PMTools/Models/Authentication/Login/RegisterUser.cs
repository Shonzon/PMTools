using System.ComponentModel.DataAnnotations;

namespace PMTools.Models.Authentication.Login
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="User Name is Required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "User Email is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }
    }
}
