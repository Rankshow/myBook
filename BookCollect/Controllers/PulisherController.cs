using BookCollect.Models;
using BookCollect.Services;
using BookCollect.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

namespace BookCollect.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    [Authorize]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService _publisherService;
        private readonly ILogger<PublisherController> _logger;
        public PublisherController(PublisherService publisherService, ILogger<PublisherController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        //Get the list of the publisher by sorting
        [HttpGet("gets-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string search, int pageNumber) 
        {
            try
            {
                var result = _publisherService.GetAllPublishers(sortBy, search, pageNumber);
                return Ok(result);
            }
            catch (Exception) 
            {
                return BadRequest("Sorry, We could not load the publishers");
            }
        }
        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {

            var newPublisher = _publisherService.AddPublisher(publisher);
            return Created(nameof(AddPublisher), newPublisher);
        }
         
        [HttpGet("all-publisher")]
        public IActionResult GetAll() 
        {
            _logger.LogInformation("This is getting all the list of the publishers");

            var allPublisher = _publisherService.GetAll();
            return Ok(allPublisher);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public ActionResult<Publisher> GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publisherService.GetPublisherById(id) ;
            if (_response != null)
            {
                return _response;
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id) 
        { 
            var _response = _publisherService.GetPublisherData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-publisher-id/{id}")]
        public IActionResult DeletePublisherDataById(int id) 
        {
            //Use all global exception handling
            //Remove this try catch from your controller...
            try
            {
                _publisherService.DeletePublisherDataById(id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
