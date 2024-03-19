using System.ComponentModel.DataAnnotations;

namespace BookCollect.Services.ViewModels.Authentications
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required is required")]
        public string? Password { get; set; }
    }
}
