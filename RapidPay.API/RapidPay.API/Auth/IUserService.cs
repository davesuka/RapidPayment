using System.Threading.Tasks;

namespace RapidPay.API.Auth
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string username, string password);
    }
}
