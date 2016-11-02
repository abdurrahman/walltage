using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Walltage.Domain.Entities;
using Walltage.Domain;

namespace Walltage.Web.Tests
{
    [TestClass]
    public class EntityCrud
    {
        private WalltageDbContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _userRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new WalltageDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
            _userRepository = new Repository<User>(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext = null;
            _unitOfWork.Dispose();
        }

        [TestMethod]
        public void InsertUser()
        {
            var user = new User
            {
                AddedDate = DateTime.Now,
                Email = "datnetdeveloper@gmail.com",
                FirstName = "Abdurrahman",
                IPAddress = "192.168.2.1",
                LastActivity = DateTime.Now,
                LastName = "Işık",
                ModifiedDate = DateTime.Now,
                Password = "12345678",
                Username = "xJason21",
                UserRoleId = 1
            };
            _userRepository.Insert(user);
            int process = _unitOfWork.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }
    }
}
