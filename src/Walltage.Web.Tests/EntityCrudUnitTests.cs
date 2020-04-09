using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Walltage.Domain.Entities;
using Walltage.Domain;
using System.Collections.Generic;
using log4net;
using Walltage.Service;
using Autofac;

namespace Walltage.Web.Tests
{
    [TestClass]
    public class EntityCrudUnitTests
    {
        //private WalltageDbContext _dbContext;
        private IUnitOfWork _unitOfWork;
        //private IRepository<User> _userRepository;
        private ILog _logger = null;
        private IUserService _accountService = null;

        IContainer _container;

        public EntityCrudUnitTests()
        {
            _container = Walltage.Web.Tests.DependencyContainer.Initialize();
        }

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _container = Walltage.Web.Tests.DependencyContainer.Initialize();
        //    //_dbContext = new WalltageDbContext();
        //    //_unitOfWork = new UnitOfWork(_dbContext);
        //    //_userRepository = new Repository<User>(_dbContext);
        //    //_userRoleRepository = new Repository<UserRole>(_dbContext);
        //}

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    _dbContext = null;
        //    _unitOfWork.Dispose();
        //}

        [TestMethod]
        public void InsertTest()
        {
            try
            {
                _logger = _container.Resolve<ILog>();
                _accountService = _container.Resolve<IUserService>();
                _accountService.Register(new Service.Models.RegisterViewModel
                {
                    Username = "abdurrahman",
                    Password = "123456",
                    Email = "abdurrahman@uykusuzadam.com"
                });
                _logger.Info("User has registered successfully !");
                //var user = new User
                //{
                //    Email = "datnetdeveloper@gmail.com",
                //    FirstName = "Abdurrahman",
                //    IPAddress = "192.168.2.1",
                //    LastActivity = DateTime.Now,
                //    LastName = "Işık",
                //    Password = "12345678",
                //    Username = "xJason21",
                //    UserRoleId = 1
                //};
                ////_unitOfWork.UserRepository.Insert(user);

                //_userRepository.Insert(user);
                //_unitOfWork.Save();

            }
            catch (Exception ex)
            {
                // {"Unable to resolve the type 'Walltage.Domain.UnitOfWork' because the lifetime scope it belongs in can't be located. The following services are exposed by this registration:\n- Walltage.Domain.IUnitOfWork\r\n\nDetails ---> No scope with a tag matching 'AutofacWebRequest' is visible from the scope in which the instance was requested.\n\nIf you see this during execution of a web application, it generally indicates that a component registered as per-HTTP request is being requested by a SingleInstance() component (or a similar scenario). Under the web integration always request dependencies from the dependency resolver or the request lifetime scope, never from the container itself. (See inner exception for details.)"}

                //{"None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' on type 'Walltage.Service.AccountService' can be invoked with the available services and parameters:\r\nCannot resolve parameter 'Walltage.Domain.IRepository`1[Walltage.Domain.Entities.User] userRepository' of constructor 'Void .ctor(Walltage.Domain.IUnitOfWork, log4net.ILog, Walltage.Domain.IRepository`1[Walltage.Domain.Entities.User])'."}

                //"The context cannot be used while the model is being created. This exception may be thrown if the context is used inside the OnModelCreating method or if the same context instance is accessed by multiple threads concurrently. Note that instance members of DbContext and related classes are not guaranteed to be thread safe."
                throw;
            }
        }

        [TestMethod]
        public void UpdateTest()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();
            var user = _unitOfWork.UserRepository.FindById(1);

            user.Username = "xJason";
            user.LastActivity = DateTime.Now;
            //user.UserRoleId = 2;

            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }

        [TestMethod]
        public void DeleteTest()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();
            var user = _unitOfWork.UserRepository.FindById(2);
            _unitOfWork.UserRepository.Delete(user);

            _unitOfWork.Save();
        }

        [TestMethod]
        public void InsertCategoryTest()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();

            var categoryList = new List<Category>
            {
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Abstract" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Animals" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Music" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Movie" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Nature" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "People" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "Science" },
                new Category { AddedBy = "abdurrahman", AddedDate = DateTime.Now, Name = "City" }
            };
            _unitOfWork.CategoryRepository.BulkInsert(categoryList);
            _unitOfWork.Save();
        }
    }
}
