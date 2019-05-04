using System.Collections.Generic;
using System.Linq;
using Contracts;
using BookStoreApp.Models.DTO;
using BookStoreApp.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Models.DataManager
{
    public class PublisherDataManager : IDataRepository<Publisher, PublisherDTO>
    {
        // Fields
        private ILoggerManager _logger;
        readonly BookStoreContext _bookStoreContext;

        // Constructer
        public PublisherDataManager(ILoggerManager logger, BookStoreContext storeContext)
        {
            _logger = logger;
            _bookStoreContext = storeContext;
        }

        // GET: GetAll Publishers - Explicit Loading
        public IEnumerable<Publisher> GetAllData()
        {
            throw new System.NotImplementedException();
        }

        // GET: Get Publisher by id - Eager Loading
        public Publisher GetByIDData(long id)
        {
            _logger.LogInfo($"PublisherDataManager.GetByIDData - Getting publisher with id: {id}, data.");
            return _bookStoreContext.Publisher
                .Include(a => a.Book)
                .Single(b => b.Id == id);
        }

        // GET: Get Publisher by id - Explicit Loading
        public PublisherDTO GetByIDDataDto(long id)
        {
            throw new System.NotImplementedException();
        }

        // POST: Add Publisher
        public void AddData(Publisher entity)
        {
            throw new System.NotImplementedException();
        }

        // PUT: Update Publisher
        public void UpdateData(Publisher entityToUpdate, Publisher entity)
        {
            throw new System.NotImplementedException();
        }

        // DELETE: Delete Publisher
        public void DeleteData(Publisher entity)
        {
            _logger.LogInfo($"PublisherDataManager.DeleteData - Deleting publisher data.");
            _bookStoreContext.Remove(entity);
            _bookStoreContext.SaveChanges();
        }
    }
}
