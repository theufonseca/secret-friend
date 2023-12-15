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
    public record GetParticipantsRequest(int gameId, int userId) : IRequest<GetParticipantsResponse>;
    public record GetParticipantsResponse(List<User> UserParticipants);

    public class GetParticipantsRequestHandler : IRequestHandler<GetParticipantsRequest, GetParticipantsResponse>
    {
        private readonly IParticipantRepository participantRepository;
        private readonly IGameRepository gameRepository;
        private readonly IUserRepository userRepository;

        public GetParticipantsRequestHandler(IParticipantRepository participantRepository, IGameRepository gameRepository, IUserRepository userRepository)
        {
            this.participantRepository = participantRepository;
            this.gameRepository = gameRepository;
            this.userRepository = userRepository;
        }

        public async Task<GetParticipantsResponse> Handle(GetParticipantsRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetGamesByUserId(request.userId);

            if (game is null)
                throw new Exception("Game not found");

            if(!game.Any(x => x.Id == request.gameId))
                throw new Exception("User not authorized");

            var participants = await participantRepository.GetParticipantsByGame(request.gameId);

            var usersParticipants = new List<User>();

            foreach (var item in participants)
            {
                var user = await userRepository.GetUserById(item.IdUser);

                if (user is null)
                    continue;

                usersParticipants.Add(user);
            }

            return new GetParticipantsResponse(usersParticipants);
        }
    }
}
