using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace IntertnshipTest.DAL.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        [Column(TypeName="timestamp")]
        public DateTime CreatedDate { get; set; }
        public int UserGroupId { get; set; }
        public UserGroupEntity UserGroup { get; set; } = null!;
        public int UserStateId { get; set; }
        public UserStateEntity UserState { get; set; } = null!;
    }
}
