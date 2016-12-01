using Walltage.Domain.Entities;
using Walltage.Service.Models;

namespace Walltage.Service.Services
{
    public interface IUserService
    {        
        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>A user</returns>
        User FindUserById(int userId);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User</returns>
        User FindUserByUsername(string username);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>User</returns>
        User FindUserByEmail(string email);

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User</returns>
        User ValidateUser(string username, string password);

        DatabaseOperationResult RegisterUser(RegisterViewModel model);

        void ChangePassword(string password);

        void ForgotPassword(string email);
    }
}
