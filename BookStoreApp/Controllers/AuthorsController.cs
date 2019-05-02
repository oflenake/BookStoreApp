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
    public class AuthorsController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IDataRepository<Author, AuthorDTO> _dataRepository;

        public AuthorsController(ILoggerManager logger, IDataRepository<Author, AuthorDTO> dataRepository)
        {
            _logger = logger;
            _dataRepository = dataRepository;
        }

        // GET: api/Authors - Get All Authors
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var authors = _dataRepository.GetAll();
                _logger.LogInfo($"Returned all authors from database.");

                return Ok(authors); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Authors/5 - Get Author by id
        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult Get(int id)
        {
            try
            {
                var author = _dataRepository.GetDTO(id);

                if (author == null)
                {
                    _logger.LogError($"Author with id: {id}, hasn't been found in db.");
                    return NotFound("Author not found.");
                }

                _logger.LogInfo($"Returned author with id: {id}");
                return Ok(author); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAuthorById action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/Authors - Add Author
        [HttpPost]
        public IActionResult Post([FromBody] Author author)
        {
            try
            {
                if (author is null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author is null.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid author object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _dataRepository.Add(author);
                _logger.LogInfo("Author added.");
                return CreatedAtRoute("GetAuthor", new { Id = author.Id }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Add action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/Authors/5 - Update Author
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Author author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author is null.");
                }

                var authorToUpdate = _dataRepository.Get(id);
                if (authorToUpdate == null)
                {
                    _logger.LogError($"Employee record with id: {id}, hasn't been found in db.");
                    return NotFound("The Employee record couldn't be found.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid auther object sent from client.");
                    return BadRequest();
                }

                _dataRepository.Update(authorToUpdate, author);
                _logger.LogInfo("Author updated.");
                return NoContent(); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
