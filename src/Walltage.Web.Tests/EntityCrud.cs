using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Walltage.Domain.Entities;
using Walltage.Domain;
using System.Collections.Generic;

namespace Walltage.Web.Tests
{
    [TestClass]
    public class EntityCrud
    {
        private WalltageDbContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _userRepository;
        private IRepository<UserRole> _userRoleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new WalltageDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
            _userRepository = new Repository<User>(_dbContext);
            //_userRoleRepository = new Repository<UserRole>(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext = null;
            _unitOfWork.Dispose();
        }

        [TestMethod]
        public void InsertTest()
        {
        //    var userRole = new UserRole
        //    {
        //        AddedDate = DateTime.Now,
        //        Name = "Admin"
        //    };
        //    _userRoleRepository.Insert(userRole);

            var user = new User
            {
                Email = "datnetdeveloper@gmail.com",
                FirstName = "Abdurrahman",
                IPAddress = "192.168.2.1",
                LastActivity = DateTime.Now,
                LastName = "Işık",
                Password = "12345678",
                Username = "xJason21",
                UserRoleId = 1
            };
            //_unitOfWork.UserRepository.Insert(user);
            _userRepository.Insert(user);
            _unitOfWork.Save();

            //Assert.AreNotEqual(-1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var user = _userRepository.FindById(2);

            user.Username = "Skip60Yataga";
            user.UserRoleId = 2;

            _userRepository.Update(user);
            _unitOfWork.Save();
        }

        [TestMethod]
        public void DeleteTest()
        {
            var user = _userRepository.FindById(3);
            _userRepository.Delete(user);

            _unitOfWork.Save();
        }
    }
}