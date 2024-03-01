namespace BookCollect.Services.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public int? Rate { get; set; }
        public DateTime? DateRead { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public ICollection<int>? AuthorIds { get; set; }
    }
    public class BookWithAuthorsVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public int? Rate { get; set; }
        public DateTime? DateRead { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public string PublisherName { get; set; } = string.Empty;
        public ICollection<string>? AuthorNames { get; set; }
    }
}
