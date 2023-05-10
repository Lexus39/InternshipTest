using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipTest.Domain.UserAggregate
{
    public class UserUpdateParameters
    {
        public int Id { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        public int GroupId { get; set; }
    }
}
