using AutoMapper;
using InternshipTest.Application.Interfaces;
using InternshipTest.Domain.UserAggregate;
using IntertnshipTest.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntertnshipTest.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InternshipContext _context;
        private readonly IMapper _mapper;

        public UserRepository(InternshipContext context, IMapper mapper) 
        {
            (_context, _mapper) = (context, mapper);
        }

        public async Task<int> AddUserAsync(User user)
        {
            var userEntity = new UserEntity()
            {
                Login = user.Login,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                UserGroupId = user.UserGroup.Id,
                UserStateId = user.UserState.Id
            };
            var addedUser = await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return addedUser.Entity.Id;
        }

        public async Task<List<User>> GetAllActiveUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .Where(u => u.UserState.Code == "Active")
                .ToListAsync();
            return users.Select(u => _mapper.Map<User>(u)).ToList();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .Where(u => u.Id == id && u.UserState.Code == "Active")
                .FirstOrDefaultAsync();
            if (user == null)
                throw new ArgumentException("User not found");
            return _mapper.Map<User>(user);
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            var user = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .Where(u => u.Login == login && u.UserState.Code == "Active")
                .FirstOrDefaultAsync();
            if (user == null)
                throw new ArgumentException("User not found");
            return _mapper.Map<User>(user);
        }

        public Task UpdateUserAsync(User user)
        {
            var userEntity = _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .Where(u => u.Id == user.Id && user.UserState.Code == "Active")
                .FirstOrDefault();
            if (userEntity == null)
                throw new ArgumentException("User not found");
            userEntity.Password = user.Password;
            userEntity.UserGroupId = user.UserGroup.Id;
            return _context.SaveChangesAsync();
        }

        public async Task<int> GetNumbersOfAdminsAsync()
        {
            var admins = await _context.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .Where(u => u.UserGroup.Code == "Admin" && u.UserState.Code == "Active")
                .ToListAsync();
            return admins.Count;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _context.Users
                .Include(u => u.UserState)
                .Where(u => u.Id == id && u.UserState.Code == "Active")
                .FirstOrDefault();
            if (user == null)
                throw new ArgumentException("User not found");
            var blockedState = await GetUserStateByCodeAsync("Blocked");
            if (blockedState == null)
                throw new ArgumentException("State not found");
            user.UserStateId = blockedState.Id;
            await _context.SaveChangesAsync();
        }

        public async Task<UserGroup> GetUserGroupByIdAsync(int id) => 
            _mapper.Map<UserGroup>(await _context.Groups.FirstAsync(g => g.Id == id));

        public async Task<UserState> GetUserStateByCodeAsync(string code) =>
            _mapper.Map<UserState>(await _context.States.FirstAsync(s => s.Code == code));
    }
}
