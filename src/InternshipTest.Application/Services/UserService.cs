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
        private readonly IUserRepository _userRepository = null!;

        public UserService(IUserRepository userRepository)
            => _userRepository = userRepository;

        public async Task<User> GetUserByIdAsync(int id)
            => await _userRepository.GetUserByIdAsync(id);

        public async Task<User> GetUserByLoginAsync(string login)
            => await _userRepository.GetUserByLoginAsync(login);

        public async Task<List<User>> GetUsersAsync()
            => await _userRepository.GetAllActiveUsersAsync();

        public async Task UpdateUserAsync(UserUpdateParameters userUpdateParameters)
        {
            var user = await GetUserByIdAsync(userUpdateParameters.Id);
            user.UpdatePassword(userUpdateParameters.Password);
            if (user.UserGroup.Id != userUpdateParameters.GroupId)
            {
                var group = await _userRepository
                    .GetUserGroupByIdAsync(userUpdateParameters.GroupId);
                var adminCount = await _userRepository.GetNumbersOfAdminsAsync();
                if (user.UserGroup.Code != "Admin" && group.Code == "Admin" &&
                    adminCount >= 1)
                    throw new ArgumentException("Сan be only one admin");
                user.UserGroup = group;
            }
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<int> CreateUserAsync(UserCreateParameters userCreateParameters)
        {
            var group = await _userRepository.GetUserGroupByIdAsync(userCreateParameters.GroupId);
            var state = await _userRepository.GetUserStateByCodeAsync("Active");
            if (group == null)
                throw new ArgumentException("Uncorrect groupid");
            if (group.Code == "Admin" && await _userRepository.GetNumbersOfAdminsAsync() >= 1)
                throw new ArgumentException("Сan be only one admin");
            var user = User.CreateUser(userCreateParameters.Login, userCreateParameters.Password,
                group, state);
            await Task.Delay(5000);
            return await _userRepository.AddUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
            => await _userRepository.DeleteUserAsync(id);
    }
}
