using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walltage.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalltageDbContext _dbContext;

        public UnitOfWork(WalltageDbContext context)
        {
            Database.SetInitializer<WalltageDbContext>(null);
            if (context == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // DbEntityValidateError can handle right here..
                throw;
            }
        }

        #region IDisposable Methods
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
