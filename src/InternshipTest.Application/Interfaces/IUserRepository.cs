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
        public Task UpdateUserAsync(User user);
        public Task<int> AddUserAsync(User user);
        public Task DeleteUserAsync(int id);
        public Task<UserGroup> GetUserGroupByIdAsync(int id);
        public Task<UserState> GetUserStateByCodeAsync(string code);
        public Task<int> GetNumbersOfAdminsAsync();
    }
}
