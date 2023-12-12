using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Participants
    {
        public int Id { get; set; }
        public int IdGame { get; set; }
        public int IdUser { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public int? IdUserSecretFriend { get; set; }
        public bool NotificationSent { get; set; }
    }
}
