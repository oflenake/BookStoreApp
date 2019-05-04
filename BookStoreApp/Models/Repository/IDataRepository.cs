using System.Collections.Generic;

namespace BookStoreApp.Models.Repository
{
    public interface IDataRepository<TEntity, TDto>
    {
        IEnumerable<TEntity> GetAllData();
        TEntity GetByIDData(long id);
        TDto GetByIDDataDto(long id);
        void AddData(TEntity entity);
        void UpdateData(TEntity entityToUpdate, TEntity entity);
        void DeleteData(TEntity entity);
    }
}
