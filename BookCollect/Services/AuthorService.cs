using BookCollect.Data;
using BookCollect.Models;
using BookCollect.Services.ViewModels;
using System.Net;

namespace BookCollect.Services
{
    public class AuthorService
    {
        private readonly AppDbContext _appDbContext;
        public AuthorService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(AurthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
            };
            _appDbContext.Add(_author);
            _appDbContext.SaveChanges();
        }

        public AuthorWithBooksVM? GetAuthorWithBooks(int authorIds)
        {
            var _author = _appDbContext.Authors.Where(n => n.Id == authorIds).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitle = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();
            return _author;
        }


    }
}
