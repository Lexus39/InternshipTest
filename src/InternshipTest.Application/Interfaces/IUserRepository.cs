using InternshipTest.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTest.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByLoginAsync(string login);
        public Task<List<User>> GetAllActiveUsersAsync();
        public Task<User> UpdateUserAsync(User user);
        public Task<User> AddUserAsync(User user);
    }
}
