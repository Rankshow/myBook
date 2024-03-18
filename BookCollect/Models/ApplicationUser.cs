using Microsoft.AspNetCore.Identity;

namespace BookCollect.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Custom { get; set; } = string.Empty;
    }
}
