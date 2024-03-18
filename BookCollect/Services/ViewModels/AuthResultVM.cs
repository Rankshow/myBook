namespace BookCollect.Services.ViewModels
{
    public class AuthResultVM
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireAt { get; set;}
    }
}
