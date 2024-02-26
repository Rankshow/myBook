namespace BookCollect.Data.ViewModels
{
    public class AurthorVM
    {
        public string FullName { get; set; } = string.Empty;
    }
    public class AuthorWithBooksVM
    {
        public string FullName { get; set; } = string.Empty;
        public List<string>? BookTitle { get; set; }
    }
}
