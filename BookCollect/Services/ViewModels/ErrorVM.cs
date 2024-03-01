using Newtonsoft.Json;

namespace BookCollect.Services.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; } 
        public string Message { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
