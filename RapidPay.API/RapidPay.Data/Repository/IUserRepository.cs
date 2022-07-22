using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public interface IUserRepository
    {
        public Task<bool> AuthenticateAsync(string username, string password);
    }
}
