using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain.Repositories;

namespace Walltage.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalltageDbContext _dbContext;

        //public UnitOfWork(WalltageDbContext context)
        //{
        //}

        public UnitOfWork(string nameOrConnectionString)
        {
            _dbContext = new WalltageDbContext(nameOrConnectionString);
        }

        public UnitOfWork(WalltageDbContext context)
        {
            Database.SetInitializer<WalltageDbContext>(null);
            if (context == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = context;
        }

        private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_dbContext)); }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public void Save(bool async = false)
        {
            try
            {
                if (async)
                    _dbContext.SaveChangesAsync();
                else
                    _dbContext.SaveChanges();
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
