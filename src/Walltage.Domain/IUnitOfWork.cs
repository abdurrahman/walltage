using System;
using System.Data.Entity;
using Walltage.Domain.Repositories;

namespace Walltage.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        UserRepository UserRepository { get; }

        void Save(bool async = false);
    }
}
