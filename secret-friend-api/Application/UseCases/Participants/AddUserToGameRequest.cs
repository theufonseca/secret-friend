using Application.Interfaces;
using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Participants
{
    public record AddUserToGameRequest(string GameCode, int UserId, string? Option1, string? Option2, string? Option3) : IRequest<AddUserToGameResponse>;
    public record AddUserToGameResponse(int participantId);

    public class AddUserToGameRequestHandler : IRequestHandler<AddUserToGameRequest, AddUserToGameResponse>
    {
        private readonly IParticipantRepository participantRepository;
        private readonly IGameRepository gameRepository;

        public AddUserToGameRequestHandler(IParticipantRepository participantRepository, IGameRepository gameRepository)
        {
            this.participantRepository = participantRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<AddUserToGameResponse> Handle(AddUserToGameRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetByGameCode(request.GameCode);

            if (game is null)
                throw new Exception("Game not found");

            var participant = await participantRepository.GetParticipantByUserIdAndGameIdAsync(request.UserId, game.Id);

            if (participant is not null)
                throw new Exception("Participant aready added");

            var newParticipant = new Participant(game.Id, request.UserId);

            if (request.Option1 is not null)
                newParticipant.AddOption1(request.Option1);

            if (request.Option2 is not null)
                newParticipant.AddOption2(request.Option2);

            if (request.Option3 is not null)
                newParticipant.AddOption3(request.Option3);

            var participantId = await participantRepository.AddParticipantAsync(newParticipant);

            return new AddUserToGameResponse(participantId);
        }
    }
}
