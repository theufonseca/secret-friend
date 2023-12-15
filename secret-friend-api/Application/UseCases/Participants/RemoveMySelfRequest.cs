using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Participants
{
    public record RemoveMySelfRequest(int IdGame, int UserId) : IRequest<RemoveMySelfResponse>;
    public record RemoveMySelfResponse(bool Success);

    public class RemoveMySelfRequestHandler : IRequestHandler<RemoveMySelfRequest, RemoveMySelfResponse>
    {
        private readonly IParticipantRepository participantRepository;

        public RemoveMySelfRequestHandler(IParticipantRepository participantRepository)
        {
            this.participantRepository = participantRepository;
        }

        public async Task<RemoveMySelfResponse> Handle(RemoveMySelfRequest request, CancellationToken cancellationToken)
        {
            var participant = await participantRepository.GetParticipantByUserIdAndGameIdAsync(request.UserId, request.IdGame);

            if (participant is null)
                throw new Exception("Participant Not Found");

            await participantRepository.DeleteParticipant(participant.IdGame, participant.IdUser);

            return new RemoveMySelfResponse(true);
        }
    }
}
