using Walltage.Domain.Entities;
using Walltage.Service.Models;

namespace Walltage.Service
{
    public interface IAccountService
    {
        User ValidateAccount(string email, string password);

        bool Register(RegisterViewModel model);
    }
}
