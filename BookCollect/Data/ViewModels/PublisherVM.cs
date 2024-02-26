namespace BookCollect.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; } = string.Empty;
    }
    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; } = string.Empty;
        public List<BookAuthorVM>? BookAuthors { get; set; }
    }
    public class BookAuthorVM
    {
        public string BookName { get; set; } = string.Empty;
        public List<string>? BookAuthors { get; set; }
    }
}
