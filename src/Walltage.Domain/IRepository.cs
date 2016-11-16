using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Walltage.Domain
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table();
        T FindById(int id);

        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Delete(int id);
        void Delete(IEnumerable<object> ids);

        int Count(Expression<Func<T, bool>> match);

        void Save(bool async = false);
    }
}
