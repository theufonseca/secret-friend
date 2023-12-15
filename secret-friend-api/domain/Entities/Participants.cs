using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Participant
    {
        public int Id { get; private set; }
        public int IdGame { get; private set; }
        public int IdUser { get; private set; }
        public string? Option1 { get; private set; }
        public string? Option2 { get; private set; }
        public string? Option3 { get; private set; }
        public int? IdUserSecretFriend { get; private set; }

        public Participant(int id, int idGame, int idUser, string? option1, string? option2, string? option3, int? idUserSecretFriend)
        {
            Id = id;
            AddIdGame(idGame);
            AddIdUser(idUser);
            AddOption1(option1);
            AddOption2(option2);
            AddOption3(option3);
            AddIdUserSecretFriend(idUserSecretFriend);
        }

        private void AddIdUserSecretFriend(int? idUserSecretFriend)
        {
            IdUserSecretFriend = idUserSecretFriend;
        }

        public Participant(int idGame, int idUser)
        {
            AddIdGame(idGame);
            AddIdUser(idUser);            
        }

        public void AddIdGame(int idGame)
        {
            if (idGame <= 0)
                throw new Exception("IdGame must be greater than 0");

            IdGame = idGame;
        }

        public void AddIdUser(int idUser)
        {
            if (idUser <= 0)
                throw new Exception("IdUser must be greater than 0");

            IdUser = idUser;
        }

        public void AddOption1(string? option1)
        {
            Option1 = option1;
        }

        public void AddOption2(string? option2)
        {
            Option2 = option2;
        }

        public void AddOption3(string? option3)
        {
            Option3 = option3;
        }

        public void AddSecretFriend(int idUserSecretFriend)
        {
            if (idUserSecretFriend <= 0)
                throw new Exception("IdUserSecretFriend must be greater than 0");

            if(IdUserSecretFriend == IdUser)
                throw new Exception("Secret friend must be different than user");

            IdUserSecretFriend = idUserSecretFriend;
        }
    }
}
