using System.Collections.Generic;
using System.Linq;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Models.DataManager
{
    public class PublisherDataManager : IDataRepository<Publisher, PublisherDTO>
    {
        // Field
        readonly BookStoreContext _bookStoreContext;

        // Constructer
        public PublisherDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        // GET: GetAll Publishers - Explicit Loading
        public IEnumerable<Publisher> GetAll()
        {
            throw new System.NotImplementedException();
        }

        // GET: Get Publisher by id - Eager Loading
        public Publisher Get(long id)
        {
            return _bookStoreContext.Publisher
                .Include(a => a.Book)
                .Single(b => b.Id == id);
        }

        // GET: Get Publisher by id - Explicit Loading
        public PublisherDTO GetDTO(long id)
        {
            throw new System.NotImplementedException();
        }

        // POST: Add Publisher
        public void Add(Publisher entity)
        {
            throw new System.NotImplementedException();
        }

        // PUT: Update Publisher
        public void Update(Publisher entityToUpdate, Publisher entity)
        {
            throw new System.NotImplementedException();
        }

        // DELETE: Delete Publisher
        public void Delete(Publisher entity)
        {
            _bookStoreContext.Remove(entity);
            _bookStoreContext.SaveChanges();
        }
    }
}
