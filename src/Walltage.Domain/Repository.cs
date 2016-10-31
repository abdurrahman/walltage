using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walltage.Domain
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        protected DbContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(WalltageDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void BulkUpdate(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void BulkDelete(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public void BulkDelete(IEnumerable<object> ids)
        {
            throw new NotImplementedException();
        }
    }
}
