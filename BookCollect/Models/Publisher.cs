namespace BookCollect.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Navigation properties
        public virtual ICollection<Book>? Books { get; set;}
    }
}
