using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models
{
    public class ParticipantDto
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public int IdUser { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set;}
        public int? IdUserSecretFriend { get; set; }


        public Participant GetParticipant()
        {
            return new Participant(Id, IdGame, IdUser, Option1, Option2, Option3, IdUserSecretFriend);
        }
    }
}
