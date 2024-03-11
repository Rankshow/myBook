using BookCollect.Models;

namespace BookCollect.Services.ViewModels
{
    public class CustomActionResultVM
    {
        public Exception? Exception { get; set; }
        public Publisher? Publisher { get; set; }
    }
}
