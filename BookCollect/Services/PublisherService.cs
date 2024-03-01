using BookCollect.Data;
using BookCollect.Exceptions;
using BookCollect.Models;
using BookCollect.Services.ViewModels;
using System.Text.RegularExpressions;

namespace BookCollect.Services
{
    public class PublisherService
    {
        private AppDbContext _appDbContext;
        public PublisherService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        } 

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name start with number", publisher.Name);

            var _publisher = new Publisher()
            { 
                Name = publisher.Name,
            };
            _appDbContext.Add(_publisher);
            _appDbContext.SaveChanges();

            return _publisher;
        }

        public Publisher? GetPublisher(int id) 
        { 
            return _appDbContext.Publishers.FirstOrDefault( x => x.Id == id);
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId) 
        {
            var _publisherData = _appDbContext.Publishers.Where( n => n.Id  == publisherId)
                .Select( n => new PublisherWithBooksAndAuthorsVM() 
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM() 
                { 
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault(); 

            return _publisherData ?? new PublisherWithBooksAndAuthorsVM();
        }

        public List<Publisher> GetAll() 
        {
            var allPublisher = _appDbContext.Publishers.ToList();
            return allPublisher;
        }

        public Publisher? GetPublisherById(int id) 
        {
             var getPusherId =_appDbContext.Publishers.FirstOrDefault(a => a.Id == id);
            return getPusherId;
        }

        public void Delete(int id) 
        {
            var deletePusher = _appDbContext
                .Publishers
                .FirstOrDefault(a => a.Id == id);
            if (deletePusher != null) 
            {
                _appDbContext.Publishers.Remove(deletePusher);
                _appDbContext.SaveChanges();
            }
        }

        public Publisher Update(int publisherId, PublisherVM publisher)
        {
            var _publisher = _appDbContext.Publishers.FirstOrDefault(b => b.Id == publisherId);
            if( _publisher != null ) 
            {
                _publisher.Name = publisher.Name;   
            }
            _appDbContext.SaveChanges();
            return _publisher ?? new Publisher();
        }

        public void DeletePublisherDataById(int id)
        {
            var publisherId = _appDbContext.Publishers.FirstOrDefault(n => n.Id == id);

            if (publisherId != null)
            {
                _appDbContext.Remove(publisherId);
                _appDbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"The Publisher with id: { id } does not exist");
            }
        }

        private bool StringStartWithNumber(string name) => (Regex.IsMatch(name, @"^\d"));
    }
}
 