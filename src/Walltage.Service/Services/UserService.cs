using log4net;
using System.Linq;
using Walltage.Domain;
using Walltage.Domain.Entities;
using Walltage.Service.Helpers;
using Walltage.Service.Models;

namespace Walltage.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly IWebHelper _webHelper;

        public UserService(IUnitOfWork unitOfWork,
            ILog logger,
            IWebHelper webHelper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHelper = webHelper;
        }

        public virtual User FindUserById(int userId)
        {
            if (userId == 0)
                return null;

            return _unitOfWork.UserRepository.FindById(userId);
        }

        public virtual User FindUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var query = from c in _unitOfWork.UserRepository.Table()
                        where c.Username == username
                        select c;
            var user = query.FirstOrDefault();
            return user;
        }

        public virtual User FindUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var query = from c in _unitOfWork.UserRepository.Table()
                        where c.Email == email
                        select c;
            var user = query.FirstOrDefault();
            return user;
        }

        public User ValidateUser(string userName, string password)
        {
            var encryptedPassword = _webHelper.EncryptToMd5(password);
            var user = _unitOfWork.UserRepository.Table()
                .FirstOrDefault(x => x.Username == userName && x.Password == encryptedPassword);

            if (user != null)
            {
                user.LastActivity = System.DateTime.Now;
                _unitOfWork.UserRepository.Update(user);
            }
            return user;
        }


        public DatabaseOperationResult RegisterUser(User entity)
        {
            // ToDo: [abdurrahman] Add a IP checker helper
            // ToDo: [abdurrahman] Is userroleId unique

            var result = new DatabaseOperationResult();
            entity.Email = entity.Email.Trim();
            if (this.FindUserByEmail(entity.Email) != null)
            {
                result.AddError("The email has already been taken.");
                return result;
            }
            if (this.FindUserByUsername(entity.Username) != null)
            {
                result.AddError("The username has already been taken.");
                return result;
            }

            var encryptedPassword = _webHelper.EncryptToMd5(entity.Password);
            var currentIpAddress = _webHelper.GetCurrentIpAddress();

            _unitOfWork.UserRepository.Insert(new User
            {
                Email = entity.Email.Trim(),
                IPAddress = currentIpAddress,
                LastActivity =System.DateTime.Now,
                Password = encryptedPassword,
                Username = entity.Username,
                UserRoleId = 1
            });
            _unitOfWork.Save();
            return result;
        }

        public void ChangePassword(string password)
        {
            throw new System.NotImplementedException();
        }

        public void ForgotPassword(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
