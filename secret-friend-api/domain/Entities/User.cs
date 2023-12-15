using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Nickname { get; private set; }

        [JsonIgnore]
        public string Password { get; private set; }

        public User(string userName, string Nickname, string password)
        {
            AddUser(userName);
            AddNickname(Nickname);
            AddPassword(password);
        }

        public User(int id, string userName, string nickname, string password)
        {
            Id = id;
            AddUser(userName);
            AddNickname(nickname);
            AddPassword(password);
        }

        public void AddUser(string userName)
        {
            if(userName.Length < 5)
                throw new Exception("Username is to short");

            UserName = userName;
        }

        public void AddNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
                throw new Exception("Nickname is invalid");

            Nickname = nickname;
        }

        public void AddPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("Password is invalid");

            Password = password;
        }

        public void CheckPassword(string inputPassword)
        {
            if (inputPassword != Password)
                throw new Exception("User or Password is wrong!");
        }
    }
}