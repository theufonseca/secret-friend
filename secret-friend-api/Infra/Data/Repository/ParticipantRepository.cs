using Application.Interfaces;
using Dapper;
using domain.Entities;
using Infra.Data.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly MySqlConnection _connection;
        public ParticipantRepository(DataContext context)
        {
            this._connection = context.connection;
        }

        public async Task<int> AddParticipantAsync(Participant participant)
        {
            var id = await _connection.ExecuteScalarAsync<int>(
                "INSERT INTO participant (IdGame, IdUser, option1, option2, option3, createdAt) " +
                "VALUES (@IdGame, @IdUser, @Option1, @Option2, @Option3, @CreatedAt); " +
                "SELECT LAST_INSERT_ID();",
                new
                {
                    IdGame = participant.IdGame,
                    IdUser = participant.IdUser,
                    Option1 = participant.Option1,
                    Option2 = participant.Option2,
                    Option3 = participant.Option3,
                    CreatedAt = DateTime.Now
                });

            return id;
        }

        public async Task<Participant?> GetParticipantByUserIdAndGameIdAsync(int userId, int gameId)
        {
            var participantDto = await _connection.QueryFirstOrDefaultAsync<ParticipantDto>(
                "SELECT * FROM participant WHERE IdUser = @IdUser AND IdGame = @IdGame",
                new
                {
                    IdUser = userId,
                    IdGame = gameId
                });

            if (participantDto is null)
                return null;

            return participantDto.GetParticipant();
        }

        public async Task<List<Participant>> GetParticipantsByGame(int idUserHost)
        {
            var participantsDto = await _connection.QueryAsync<ParticipantDto>(
                               "SELECT * FROM participant WHERE IdGame = @IdGame",
                                new { IdGame = idUserHost });

            if (participantsDto.Count() == 0)
                return new List<Participant>();

            return participantsDto.Select(p => p.GetParticipant()).ToList();
        }
    }
}
