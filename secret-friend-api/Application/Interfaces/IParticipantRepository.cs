using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IParticipantRepository
    {
        Task<int> AddParticipantAsync(Participant participant);
        Task<Participant?> GetParticipantByUserIdAndGameIdAsync(int userId, int gameId);
        Task<List<Participant>> GetParticipantsByGame(int idUserHost);
    }
}
