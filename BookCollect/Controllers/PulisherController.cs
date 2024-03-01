using BookCollect.Exceptions;
using BookCollect.Services;
using BookCollect.Services.ViewModels;
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

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
               var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex) 
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.Publishername}");
            }
        }
         
        [HttpGet("all-publisher")]
        public IActionResult GetAll() 
        { 
            var allPublisher = _publisherService.GetAll();
            return Ok(allPublisher);
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");

            var _response = _publisherService.GetPublisherById(id);
            if (_response != null)
            {
                return Ok(_response);
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
