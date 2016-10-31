using System;

namespace Walltage.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        
        int SaveChanges();
    }
}
