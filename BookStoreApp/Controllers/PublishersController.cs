using System;
using Contracts;
using BookStoreApp.Models;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IDataRepository<Publisher, PublisherDTO> _dataRepository;

        public PublishersController(ILoggerManager logger, IDataRepository<Publisher, PublisherDTO> dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        // DELETE: api/publishers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteByIDAction(int id)
        {
            try
            {
                var publisher = _dataRepository.GetByIDData(id);
                if (publisher == null)
                {
                    _logger.LogError($"Publisher with id: {id}, hasn't been found in db.");
                    return NotFound("Publisher record couldn't be found.");
                }

                _dataRepository.DeleteData(publisher);
                _logger.LogInfo($"Deleted publisher with id: {id}");
                return NoContent(); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside 'PublishersController.DeleteByIDAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
