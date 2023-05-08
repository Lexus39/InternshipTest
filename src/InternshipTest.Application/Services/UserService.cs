using InternshipTest.Application.Interfaces;
using InternshipTest.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTest.Application.Services
{
    public class UserService
    {
        private IUserRepository _userRepository = null!;

        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user.UserState.Code != "Active")
                throw new ArgumentException();
            return user;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            var user = await _userRepository.GetUserByLoginAsync(login);
            if (user.UserState.Code != "Active")
                throw new ArgumentException();
            return user;
        }

        public async Task<List<User>> GetUsersAsync() 
            => await _userRepository.GetAllActiveUsersAsync();

        public async Task<User> UpdateUserAsync(UserUpdateParameters userUpdateParameters)
        {
            var user = await GetUserByIdAsync(userUpdateParameters.Id);
            var updatedUser = User.UpdateUser(userUpdateParameters);
            if (user.Login != updatedUser.Login 
                || user.CreatedDate != updatedUser.CreatedDate)
                throw new ArgumentException();
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<User> CreateUser(UserCreateParameters userCreateParameters)
        {
            var user = User.CreateUser(userCreateParameters);
            return await _userRepository.AddUserAsync(user);
        }
    }
}
