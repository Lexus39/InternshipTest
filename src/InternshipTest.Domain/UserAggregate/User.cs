using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InternshipTest.Domain.UserAggregate
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public DateTime CreatedDate { get; private set; }
        public UserGroup UserGroup { get; set; } = null!;
        public UserState UserState { get; set; } = null!;

        public User(int id, string login, string password, DateTime createdDate, UserGroup userGroup, UserState userState)
        {
            Id = id;
            UpdateLogin(login);
            UpdatePassword(password);
            CreatedDate = createdDate;
            UserGroup = userGroup;
            UserState = userState;
        }

        public static User CreateUser(string login, string password, UserGroup group, UserState state)
            => new(id: 0, login: login, password: password, 
                createdDate: DateTime.Now, userGroup: group, userState: state);

        public void UpdatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 20)
                throw new ArgumentException("The length of the password must be more than 7 and not exceed 20 characters. " +
                    $"You passed {nameof(password)} = {password}");
            string pattern = @"^[\w]{8,20}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(password))
                throw new ArgumentException("Password can consist only of letters and numbers" +
                    $"You passed {nameof(password)} = {password}");
            Password = password;
        }

        private void UpdateLogin(string login)
        {
            if (login.Length < 4 || login.Length > 20)
                throw new ArgumentException("The length of the login must be more than 3 and not exceed 20 characters. " +
                    $"You passed {nameof(login)} = {login}");
            string pattern = @"^[a-z|A-Z][\w]+$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(login))
                throw new ArgumentException("Login must start with a letter, " +
                    "and can consist only of letters, numbers and underscores." +
                    $"You passed {nameof(login)} = {login}");
            Login = login;
        }
    }
}
