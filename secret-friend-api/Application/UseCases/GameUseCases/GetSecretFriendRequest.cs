using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GameUseCases
{
    public record GetSecretFriendRequest(int userId, int GameId) : IRequest<GetSecretFriendResponse>;
    public record GetSecretFriendResponse(string SecretFriendName, string? Option1, string? Option2, string? Option3, int? MaxPrice, int? MinPrice);

    public class GetSecretFriendRequestHandler : IRequestHandler<GetSecretFriendRequest, GetSecretFriendResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IParticipantRepository participantRepository;
        private readonly IUserRepository userRepository;

        public GetSecretFriendRequestHandler(IGameRepository gameRepository, IParticipantRepository participantRepository, IUserRepository userRepository)
        {
            this.gameRepository = gameRepository;
            this.participantRepository = participantRepository;
            this.userRepository = userRepository;
        }

        public async Task<GetSecretFriendResponse> Handle(GetSecretFriendRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.GameId);

            if (game is null) throw new Exception("Game not found");

            if (!game.IsFinished) throw new Exception("Game waiting to run");

            var participant = await participantRepository.GetParticipantByUserIdAndGameIdAsync(request.userId, request.GameId);

            if (participant is null) throw new Exception("You are not participant of this game");

            if (participant.IdUserSecretFriend is null) throw new Exception("You have no secrete friend binded");

            var secretfriend = await userRepository.GetUserById(participant.IdUserSecretFriend.Value);

            if (secretfriend is null) throw new Exception("Secret friend not found");

            return new GetSecretFriendResponse(secretfriend.Nickname, participant.Option1, participant.Option2, participant.Option3, game.MaxPrice, game.MinPrice);
        }
    }
}
