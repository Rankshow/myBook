using BookCollect.Data.ViewModels;
using BookCollect.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookCollect.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PulisherController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        public PulisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publisherService.AddPublisher(publisher);
            return Ok();
        }

        [HttpGet("all-publisher")]
        public IActionResult GetAll() 
        { 
            var allPublisher = _publisherService.GetAll();
            return Ok(allPublisher);
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id) 
        { 
            var response = _publisherService.GetPublisherData(id);
            return Ok(response);
        }

    }
}
