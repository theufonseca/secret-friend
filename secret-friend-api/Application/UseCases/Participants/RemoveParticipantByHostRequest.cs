using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Participants
{
    public record RemoveParticipantByHostRequest(int UserHostId, int GameId, int UserIdToRemove) : IRequest<RemoveParticipantByHostResponse>;
    public record RemoveParticipantByHostResponse(bool Success);

    public class RemoveParticipantByHostRequestHandler : IRequestHandler<RemoveParticipantByHostRequest, RemoveParticipantByHostResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IParticipantRepository participantRepository;

        public RemoveParticipantByHostRequestHandler(IGameRepository gameRepository, IParticipantRepository participantRepository)
        {
            this.gameRepository = gameRepository;
            this.participantRepository = participantRepository;
        }

        public async Task<RemoveParticipantByHostResponse> Handle(RemoveParticipantByHostRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.GameId);

            if (game is null) throw new Exception("Game not found");
            if (game.IdUserHost != request.UserHostId) throw new Exception("Not Authorized");

            var participant = await participantRepository.GetParticipantByUserIdAndGameIdAsync(request.UserIdToRemove, game.Id);

            if (participant is null) throw new Exception("Participant not found");

            await participantRepository.DeleteParticipant(game.Id, request.UserIdToRemove);

            game.GenerateGameCode();

            await gameRepository.UpdateNewGameCode(game.Id, game.GameCode);

            return new RemoveParticipantByHostResponse(true);
        }
    }
}
