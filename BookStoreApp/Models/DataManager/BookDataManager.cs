using System.Collections.Generic;
using System.Linq;
using Contracts;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;

namespace BookStoreApp.Models.DataManager
{
    public class BookDataManager : IDataRepository<Book, BookDTO>
    {
        // Fields
        private ILoggerManager _logger;
        readonly BookStoreContext _bookStoreContext;

        // Constructor
        public BookDataManager(ILoggerManager logger, BookStoreContext storeContext)
        {
            _logger = logger;
            _bookStoreContext = storeContext;
        }

        // GET: GetAll Books - Explicit Loading
        public IEnumerable<Book> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        // GET: Get Book by id - Explicit Loading
        public Book GetByIDData(long id)
        {
            _logger.LogInfo($"BookDataManager.GetByIDData - Getting book with id: {id}, data.");
            _bookStoreContext.ChangeTracker.LazyLoadingEnabled = false;

            var book = _bookStoreContext.Book
                .SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return null;
            }

            _bookStoreContext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Load();

            _bookStoreContext.Entry(book)
                .Reference(b => b.Publisher)
                .Load();

            return book;
        }

        // GET: Get Book by id - Explicit Loading
        public BookDTO GetByIDDataDto(long id)
        {
            throw new System.NotImplementedException();
        }

        // POST: Add Book
        public void AddData(Book entity)
        {
            throw new System.NotImplementedException();
        }

        // PUT: Update Book
        public void UpdateData(Book entityToUpdate, Book entity)
        {
            throw new System.NotImplementedException();
        }

        // DELETE: Delete Book
        public void DeleteData(Book entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
