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
    public record AddUserToGameRequest(int GameId, int UserId, string? Option1, string? Option2, string? Option3) : IRequest<AddUserToGameResponse>;
    public record AddUserToGameResponse(int participantId);

    public class AddUserToGameRequestHandler : IRequestHandler<AddUserToGameRequest, AddUserToGameResponse>
    {
        private readonly IParticipantRepository participantRepository;

        public AddUserToGameRequestHandler(IParticipantRepository participantRepository)
        {
            this.participantRepository = participantRepository;
        }

        public async Task<AddUserToGameResponse> Handle(AddUserToGameRequest request, CancellationToken cancellationToken)
        {
            var participant = await participantRepository.GetParticipantByUserIdAndGameIdAsync(request.UserId, request.GameId);

            if (participant is not null)
                throw new Exception("Participant aready added");

            var newParticipant = new Participant(request.GameId, request.UserId);

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
