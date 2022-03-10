using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BenivoNetwork.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        bool Any(Func<TEntity, bool> filter);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            bool asNoTracking = true);

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Delete(object id);
        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}
