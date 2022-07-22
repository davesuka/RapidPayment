using RapidPay.Data.Repository;
using System.Threading.Tasks;

namespace RapidPay.API.Auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userService;

        public UserService(IUserRepository userService)
        {
            _userService = userService;
        }

        public async Task<bool> ValidateCredentials(string username, string password)
        {
            return await _userService.AuthenticateAsync(username, password);
        }
    }
}
