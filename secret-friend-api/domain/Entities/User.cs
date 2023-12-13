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
        public long PhoneNumber { get; private set; }
        public string Nickname { get; private set; }

        [JsonIgnore]
        public string Password { get; private set; }
        public string ConfirmationCode { get; private set; }
        public DateTime EndDateConfirmation { get; private set; }
        public bool Confirmed { get; private set; }


        public User(long phoneNumber, string Nickname, string password)
        {
            AddPhoneNumber(phoneNumber);
            AddNickname(Nickname);
            AddPassword(password);
            GenerateConfirmationCode();
            Confirmed = false;
        }

        public User(long phoneNumber, string nickname, string password, string confirmationCode, DateTime endDateConfirmation, bool confirmed)
        {
            AddPhoneNumber(phoneNumber);
            AddNickname(nickname);
            AddPassword(password);
            ConfirmationCode = confirmationCode;
            EndDateConfirmation = endDateConfirmation;
            Confirmed = confirmed;
        }

        public void AddPhoneNumber(long phoneNumber)
        {
            if (phoneNumber.ToString().Length < 10)   
                throw new Exception("Phone number is invalid");

            PhoneNumber = phoneNumber;
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
            ConfirmationCode = Guid.NewGuid().ToString().Replace("-","");
            EndDateConfirmation = DateTime.Now.AddDays(1);
        }

        public void ConfirmUser()
        {
            Confirmed = true;
        }
    }
}