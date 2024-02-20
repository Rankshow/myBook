using BookCollect.Data;
using BookCollect.Data.ViewModels;
using BookCollect.Models;

namespace BookCollect.Services
{
    public class BookService
    {
        private readonly AppDbContext _appDbContext;
        public BookService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                //DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.Rate,
                Author = book.Author,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
            };
            _appDbContext.Add(_book);
            _appDbContext.SaveChanges();
        }
        public List<Book> GetAllBooks()
        {
            return _appDbContext.Books.ToList();
        }
        public Book? GetById(int bookId)
        {
            return _appDbContext
                 .Books
                 .FirstOrDefault(m => m.Id == bookId);
        }
        public Book UpdateBookById(int bookId, BookVM book) 
        {
            var _book = _appDbContext.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null ) 
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
               // _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.Rate;
                _book.Author = book.Author;
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
