using BookCollect.Data;
using BookCollect.Models;
using BookCollect.Services.ViewModels;

namespace BookCollect.Services
{
    public class BookService
    {
        private readonly AppDbContext _appDbContext;
        public BookService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddBookWithAuthor(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PulisherId = book.PublisherId
            };
            _appDbContext.Add(_book);
            _appDbContext.SaveChanges();
            
            //adding book and authors
            foreach (var id in book.AuthorIds) 
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _appDbContext.Book_Authors.Add(_book_author);
                _appDbContext.SaveChanges();
            }
        }
        public List<Book> GetAllBooks()
        {
            return _appDbContext.Books.ToList();
        }
        public BookWithAuthorsVM? GetById(int bookId)
        {
            var _bookWithAuthor = _appDbContext.Books.Where( n => n.Id == bookId ).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre, 
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select( n => n.Author.FullName ).ToList()   
            }).FirstOrDefault();

            return _bookWithAuthor;
        }
        public Book UpdateBookById(int bookId, BookVM book) 
        {
            var _book = _appDbContext.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null ) 
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.Rate;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
            }
            _appDbContext.SaveChanges ();
            return _book ?? new Book();
        }

        public void DeleteBookById(int bookId)
        {
            var _book = _appDbContext.Books.FirstOrDefault(n => n.Id == bookId);    
            if(_book != null) 
            {
                _appDbContext.Books.Remove( _book );
                _appDbContext.SaveChanges();
            }
        }
    }
        
}
