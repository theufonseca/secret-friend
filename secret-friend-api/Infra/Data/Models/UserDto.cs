using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        
        public User GetUser()
        {
            return new User(Id, UserName, Nickname, Password);
        }
    }
}
