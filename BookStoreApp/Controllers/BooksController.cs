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
    public class BooksController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IDataRepository<Book, BookDTO> _dataRepository;

        public BooksController(ILoggerManager logger, IDataRepository<Book, BookDTO> dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        // GET: api/books/4
        [HttpGet("{id}")]
        public IActionResult GetByIDAction(int id)
        {
            try
            {
                var book = _dataRepository.GetByIDData(id);
                if (book == null)
                {
                    _logger.LogError($"Book with id: {id}, hasn't been found in db.");
                    return NotFound("Book not found.");
                }

                _logger.LogInfo($"Returned book with id: {id}");
                return Ok(book); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside 'BooksController.GetByIDAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
