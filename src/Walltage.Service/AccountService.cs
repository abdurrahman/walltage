using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain;
using Walltage.Domain.Entities;
using Walltage.Service.Models;
using Walltage.Service.Helpers;

namespace Walltage.Service
{
    public class AccountService : IAccountService
    {
        private readonly WalltageDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly IWebHelper _webHelper;

        public AccountService(IUnitOfWork unitOfWork,
            ILog logger,
            IWebHelper webHelper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHelper = webHelper;
        }

        public LoginViewModel Login(LoginViewModel model)
        {
            var encryptedPassword = _webHelper.EncryptToMd5(model.Password);
            // ToDo: [abdurrahman] Add what you need here for session or cookie after login
            var query = _unitOfWork.UserRepository.Table()
                .Where(x => x.Username == model.Username && x.Password == encryptedPassword)
                .Select(user => new LoginViewModel
                {
                    Username = user.Username
                });

            return query.FirstOrDefault();
        }

        public bool Register(RegisterViewModel model)
        {
            // ToDo: [abdurrahman] Add a IP checker helper
            // ToDo: [abdurrahman] Dont register before unmatch email or username
            // ToDo: [abdurrahman] Is userroleId unique
            var isUserAlreadyRegistered = _unitOfWork.UserRepository.Table().Any(x => x.Email == model.Email);
            if (isUserAlreadyRegistered)
                return false;

            var encryptedPassword = _webHelper.EncryptToMd5(model.Password);
            var currentIpAddress = _webHelper.GetCurrentIpAddress();

            _unitOfWork.UserRepository.Insert(new User
                {
                    Email = model.Email,
                    IPAddress = currentIpAddress,
                    LastActivity = DateTime.Now,
                    Password = encryptedPassword,
                    Username = model.Username,
                    UserRoleId = 1
                });
            _unitOfWork.Save();
            return true;
        }
    }
}
