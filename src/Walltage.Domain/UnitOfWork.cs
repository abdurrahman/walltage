using System;
using Walltage.Domain.Repositories;

namespace Walltage.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private WalltageDbContext _dbContext;

        public UnitOfWork(WalltageDbContext context)
        {
            _dbContext = context;
        }

        //private readonly WalltageDbContext _dbContext;

        //public UnitOfWork(WalltageDbContext context)
        //{
        //    Database.SetInitializer<WalltageDbContext>(null);
        //    if (context == null)
        //        throw new ArgumentNullException("dbContext can not be null");

        //    _dbContext = context;
        //}
        #region Repositories
        private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_dbContext)); }
        }

        private CategoryRepository _categoryRepository;
        public CategoryRepository CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_dbContext)); }
        }

        private ResolutionRepository _resolutionRepository;
        public ResolutionRepository ResolutionRepository
        {
            get { return _resolutionRepository ?? (_resolutionRepository = new ResolutionRepository(_dbContext)); }
        }

        private TagRepository _tagRepository;
        public TagRepository TagRepository
        {
            get { return _tagRepository ?? (_tagRepository = new TagRepository(_dbContext)); }
        }

        private WallpaperRepository _wallpaperRepository;
        public WallpaperRepository WallpaperRepository
        {
            get { return _wallpaperRepository ?? (_wallpaperRepository = new WallpaperRepository(_dbContext)); }
        }

        private UserRoleRepository _userRoleRepository;
        public UserRoleRepository UserRoleRepository
        {
            get { return _userRoleRepository ?? (_userRoleRepository = new UserRoleRepository(_dbContext)); }
        }

        private WallpaperAndTagMappingRepository _wallpaperAndTagMappingRepository;
        public WallpaperAndTagMappingRepository WallpaperAndTagMappingRepository
        {
            get { return _wallpaperAndTagMappingRepository ?? (_wallpaperAndTagMappingRepository = new WallpaperAndTagMappingRepository(_dbContext)); }
        }
        //public IRepository<T> GetRepository<T>() where T : class
        //{
        //    return new Repository<T>(_dbContext);
        //}
        #endregion

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