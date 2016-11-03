using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Walltage.Domain
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T FindById(int id);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

        void BulkInsert(IEnumerable<T> entities);
        void BulkUpdate(IEnumerable<T> entities);
        void BulkDelete(IEnumerable<T> entities);
        void BulkDelete(IEnumerable<object> ids);

        int Count(Expression<Func<T, bool>> match);
    }
}
