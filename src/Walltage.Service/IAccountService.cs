using Walltage.Service.Models;

namespace Walltage.Service
{
    public interface IAccountService
    {
        LoginViewModel Login(LoginViewModel model);

        bool Register(RegisterViewModel model);
    }
}
