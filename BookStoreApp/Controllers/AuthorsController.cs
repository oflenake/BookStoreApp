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

        // GET: api/authors - Get All Authors
        [HttpGet]
        public IActionResult GetAllAction()
        {
            try
            {
                var authors = _dataRepository.GetAllData();
                _logger.LogInfo($"Returned all authors from database.");

                return Ok(authors); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside 'AuthorsController.GetAllAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/authors/4 - Get Author by id
        [HttpGet("{id}", Name = "GetByIDAuthor")]
        public IActionResult GetByIDAction(int id)
        {
            try
            {
                var author = _dataRepository.GetByIDDataDto(id);

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
                _logger.LogError($"Something went wrong inside 'AuthorsController.GetByIDAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: api/authors - Add Author
        [HttpPost]
        public IActionResult PostCreateAction([FromBody] Author author)
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

                _dataRepository.AddData(author);
                _logger.LogInfo("Author object added.");
                return CreatedAtRoute("GetByIDAuthor", new { Id = author.Id }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside 'AuthorsController.PostCreateAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/authors/4 - Update Author
        [HttpPut("{id}")]
        public IActionResult PutUpdateAction(int id, [FromBody] Author author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogError("Author object sent from client is null.");
                    return BadRequest("Author is null.");
                }

                var authorToUpdate = _dataRepository.GetByIDData(id);
                if (authorToUpdate == null)
                {
                    _logger.LogError($"Author record with id: {id}, hasn't been found in db.");
                    return NotFound("The Author record couldn't be found.");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid author object sent from client.");
                    return BadRequest();
                }

                _dataRepository.UpdateData(authorToUpdate, author);
                _logger.LogInfo("Author object updated.");
                return NoContent(); // Ok status code is: 200
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside 'AuthorsController.PutUpdateAction' action method: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
