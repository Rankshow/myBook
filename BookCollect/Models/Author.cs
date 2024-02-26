namespace BookCollect.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        //Many to many (books to Athors) Relation
        public List<Book_Author>? Book_Authors { get; set; }
    }
}
