using BookCollect.Services;
using BookCollect.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookCollect.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("all-books")]
        public IActionResult GetBooks() 
        {
            var allBooks = _bookService.GetAllBooks();
            return Ok(allBooks);    
        }

        [HttpPost("add-book-with-Author")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBookWithAuthor(book);
            return Ok();
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id) 
        {
            var bookId = _bookService.GetById(id);
            return Ok(bookId);
        }

        [HttpPut("updateById/{id}")]
        public IActionResult Update(int id, [FromBody]BookVM book)
        {
            var updatedbook = _bookService.UpdateBookById(id, book);
            return Ok(updatedbook);
        }

        [HttpDelete("DeleteById/{id}")]
        public IActionResult Delete(int id) 
        {
            _bookService.DeleteBookById(id);
            return Ok();
        }

    }
}
