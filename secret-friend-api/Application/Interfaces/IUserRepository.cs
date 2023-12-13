using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<User?> GetUserByConfirmCodeAsync(string confirmCode);
        Task<User?> GetUserByPhoneAsync(long phoneNumber);
        Task UpdateUserAsync(User user);
        Task UpdateUserToConfirmedAsync(long phoneNumber);
    }
}
