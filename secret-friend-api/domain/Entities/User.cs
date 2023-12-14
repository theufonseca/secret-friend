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
        public string UserName { get; private set; }
        public string Nickname { get; private set; }

        [JsonIgnore]
        public string Password { get; private set; }
        public string ConfirmationCode { get; private set; }
        public DateTime EndDateConfirmation { get; private set; }
        public bool Confirmed { get; private set; }


        public User(string userName, string Nickname, string password)
        {
            AddUser(userName);
            AddNickname(Nickname);
            AddPassword(password);
            GenerateConfirmationCode();
            Confirmed = false;
        }

        public User(string userName, string nickname, string password, string confirmationCode, DateTime endDateConfirmation, bool confirmed)
        {
            AddUser(userName);
            AddNickname(nickname);
            AddPassword(password);
            ConfirmationCode = confirmationCode;
            EndDateConfirmation = endDateConfirmation;
            Confirmed = confirmed;
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

        public void GenerateConfirmationCode()
        {
            Random random = new Random();
            string codigo = "";

            for (int i = 0; i < 6; i++)
                codigo += random.Next(0, 9).ToString();
            
            ConfirmationCode = codigo;
            EndDateConfirmation = DateTime.Now.AddMinutes(5);
        }

        public void ConfirmUser()
        {
            if (EndDateConfirmation < DateTime.Now)
                throw new Exception("Confirmation code expired");

            Confirmed = true;
        }
    }
}