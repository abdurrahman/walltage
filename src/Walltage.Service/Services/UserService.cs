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


        public DatabaseOperationResult RegisterUser(RegisterViewModel model)
        {
            var result = new DatabaseOperationResult();
            model.Email = model.Email.Trim();
            if (this.FindUserByEmail(model.Email) != null)
            {
                result.AddError("The email has already been taken.");
                return result;
            }
            if (this.FindUserByUsername(model.Username) != null)
            {
                result.AddError("The username has already been taken.");
                return result;
            }

            var encryptedPassword = _webHelper.EncryptToMd5(model.Password);
            var currentIpAddress = _webHelper.GetCurrentIpAddress();

            var getUserRole = _unitOfWork.UserRoleRepository.Table()
                .FirstOrDefault(x => x.Name == SystemUserRoleNames.User);
            if (getUserRole == null)
            {
                result.AddError("User acceptance is currently off, please contact to administrator.");
                return result;
            }

            _unitOfWork.UserRepository.Insert(new User
            {
                Email = model.Email.Trim(),
                IPAddress = currentIpAddress,
                LastActivity = System.DateTime.Now,
                Password = encryptedPassword,
                Username = model.Username,
                UserRoleId = getUserRole.Id
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
