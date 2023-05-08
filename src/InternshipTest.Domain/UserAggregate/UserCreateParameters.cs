using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTest.Domain.UserAggregate
{
    public class UserCreateParameters
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserGroup UserGroup { get; set; } = null!;
        public UserState UserState { get; set; } = null!;
    }
}
