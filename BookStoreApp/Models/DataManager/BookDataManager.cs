using System.Collections.Generic;
using System.Linq;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;

namespace BookStoreApp.Models.DataManager
{
    public class BookDataManager : IDataRepository<Book, BookDTO>
    {
        // Field
        readonly BookStoreContext _bookStoreContext;

        // Constructor
        public BookDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        // GET: GetAll Books - Explicit Loading
        public IEnumerable<Book> GetAll()
        {
            throw new System.NotImplementedException();
        }

        // GET: Get Book by id - Explicit Loading
        public Book Get(long id)
        {
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
        public BookDTO GetDTO(long id)
        {
            throw new System.NotImplementedException();
        }

        // POST: Add Book
        public void Add(Book entity)
        {
            throw new System.NotImplementedException();
        }

        // PUT: Update Book
        public void Update(Book entityToUpdate, Book entity)
        {
            throw new System.NotImplementedException();
        }

        // DELETE: Delete Book
        public void Delete(Book entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
