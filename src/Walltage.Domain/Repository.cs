using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walltage.Domain
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        //public Repository()
        //{
        //}

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
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Deleted;
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = FindById(id);
            Delete(entity);
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
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


        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> match)
        {
            return _dbSet.Count(match);
        }
    }
}
