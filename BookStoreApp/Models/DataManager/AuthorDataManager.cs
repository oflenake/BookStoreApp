using System.Collections.Generic;
using System.Linq;
using Contracts;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Models.DataManager
{
    public class AuthorDataManager : IDataRepository<Author, AuthorDTO>
    {
        // Fields
        private ILoggerManager _logger;
        readonly BookStoreContext _bookStoreContext;

        // Constructor
        public AuthorDataManager(ILoggerManager logger, BookStoreContext storeContext)
        {
            _logger = logger;
            _bookStoreContext = storeContext;
        }

        // GET: GetAll Authors - Eager Loading
        public IEnumerable<Author> GetAllData()
        {
            _logger.LogInfo($"AuthorDataManager.GetAllData - Getting all authors data.");
            return _bookStoreContext.Author
                .Include(author => author.AuthorContact)
                .ToList();
        }

        // GET: Get Author by id - Explicit Loading
        public Author GetByIDData(long id)
        {
            _logger.LogInfo($"AuthorDataManager.GetByIDData - Getting author with id: {id}, data.");
            var author = _bookStoreContext.Author
                .SingleOrDefault(b => b.Id == id);

            return author;
        }

        // GET: Get Author by id - Lazy Loading
        public AuthorDTO GetByIDDataDto(long id)
        {
            _logger.LogInfo($"AuthorDataManager.GetByIDDataDto - Getting author DTO with id: {id}, data.");
            _bookStoreContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new BookStoreContext())
            {
                var author = context.Author
                    .SingleOrDefault(b => b.Id == id);

                return AuthorDTOMapper.MapToDTO(author);
            }
        }

        // POST: Add Author
        public void AddData(Author entity)
        {
            _logger.LogInfo($"AuthorDataManager.AddData - Adding author data.");
            _bookStoreContext.Author.Add(entity);
            _bookStoreContext.SaveChanges();
        }

        // PUT: Update Author
        public void UpdateData(Author entityToUpdate, Author entity)
        {
            _logger.LogInfo($"AuthorDataManager.UpdateData - Updating author data.");
            entityToUpdate = _bookStoreContext.Author
                .Include(a => a.BookAuthors)
                .Include(a => a.AuthorContact)
                .Single(b => b.Id == entityToUpdate.Id);

            entityToUpdate.Name = entity.Name;

            entityToUpdate.AuthorContact.Address = entity.AuthorContact.Address;
            entityToUpdate.AuthorContact.ContactNumber = entity.AuthorContact.ContactNumber;

            var deletedBooks = entityToUpdate.BookAuthors.Except(entity.BookAuthors).ToList();
            var addedBooks = entity.BookAuthors.Except(entityToUpdate.BookAuthors).ToList();

            deletedBooks.ForEach(bookToDelete =>
                entityToUpdate.BookAuthors.Remove(
                    entityToUpdate.BookAuthors
                        .First(b => b.BookId == bookToDelete.BookId)));

            foreach (var addedBook in addedBooks)
            {
                _bookStoreContext.Entry(addedBook).State = EntityState.Added;
            }

            _bookStoreContext.SaveChanges();
        }

        // DELETE: Delete Author
        public void DeleteData(Author entity)
        {
            _logger.LogInfo($"AuthorDataManager.DeleteData - Deleting author data.");
            throw new System.NotImplementedException();
        }
    }
}
