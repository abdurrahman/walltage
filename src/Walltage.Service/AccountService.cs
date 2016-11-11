using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain;
using Walltage.Domain.Entities;
using Walltage.Service.Models;

namespace Walltage.Service
{
    public class AccountService : IAccountService
    {
        private readonly WalltageDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;

        public AccountService()
        {
            _dbContext = new WalltageDbContext();
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        public LoginViewModel Login(LoginViewModel model)
        {
            // ToDo: [abdurrahman] Password need to be hash (md5)
            // ToDo: [abdurrahman] Add what you need here for session or cookie after login
            var query = _userRepository.AsQueryable()
                .Where(x => x.Username == model.Username && x.Password == model.Password)
                .Select(user => new LoginViewModel
                {
                    Username = user.Username,
                    Fullname = string.Format("{0} {1}", user.FirstName, user.LastName)
                });

            return query.FirstOrDefault();
        }

        public bool Register(RegisterViewModel model)
        {
            // ToDo: [abdurrahman] Add a IP checker helper
            // ToDo: [abdurrahman] Dont register before unmatch email or username
            // ToDo: [abdurrahman] Is userroleId unique
            _userRepository.Insert(new User
                {
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    Email = model.Email,
                    IPAddress = "",
                    LastActivity = DateTime.Now,
                    Password = model.Password,
                    Username = model.Username,
                    UserRoleId = 1
                });

            return true;
        }
    }
}
