using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(
                user => user.UserName == username && user.Password == password);
            if(user == null)
            {
                return false;
            }

            return true;

        }
    }
}
