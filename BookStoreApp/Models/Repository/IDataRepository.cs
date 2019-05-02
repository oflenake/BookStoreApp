using System.Collections.Generic;

namespace BookStoreApp.Models.Repository
{
    public interface IDataRepository<TEntity, TDTO>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        TDTO GetDTO(long id);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
