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
        public long PhoneNumber { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string ConfirmationCode { get; set; }
        public DateTime EndateConfirmation { get; set; }
        public bool Confirmed { get; set; }
        public DateTime CreatedAt { get; set; }

        public User GetUser()
        {
            return new User(PhoneNumber, Nickname, Password, ConfirmationCode, EndateConfirmation, Confirmed);
        }
    }
}
