using Application.Interfaces;
using domain.Entities;
using MediatR;
using System.Linq;

namespace Application.UseCases.GameUseCases
{
    public record RunGameRequest(int IdGame, int UserId) : IRequest<RunGameResponse>;
    public record RunGameResponse(bool Success);

    public class RunGameRequestHandler : IRequestHandler<RunGameRequest, RunGameResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IParticipantRepository participantRepository;

        public RunGameRequestHandler(IGameRepository gameRepository, IParticipantRepository participantRepository)
        {
            this.gameRepository = gameRepository;
            this.participantRepository = participantRepository;
        }

        public async Task<RunGameResponse> Handle(RunGameRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.IdGame);

            if (game is null) throw new Exception("Game not found");
            if (game.IsFinished) throw new Exception("Game already finished");
            if (game.IdUserHost != request.UserId) throw new Exception("Operation Not Authorized");

            var participants = await participantRepository.GetParticipantsByGame(game.IdUserHost);

            if (participants.Count() < 3)
                throw new Exception("Not enought participants to play");

            var shuffleParticipants = Shuffle(participants);

            foreach (var participant in shuffleParticipants)
                await participantRepository.UpdateSecretFriend(participant.Id, participant.IdUserSecretFriend!.Value);

            await gameRepository.UpdateGameStatus(game.Id, true);
            
            return new RunGameResponse(true);
        }

        private List<Participant> Shuffle(List<Participant> participants)
        {
            var shuffleParticipants = new List<Participant>();
            Sort(participants);

            for (int i = 0; i < participants.Count; i++)
            {
                var secretfriendPosition = i + 1;

                if (secretfriendPosition == participants.Count)
                    secretfriendPosition = 0;
                else
                    secretfriendPosition = i + 1;

                var participant = participants[i];
                var secretfriend = participants[secretfriendPosition];
                participant.AddSecretFriend(secretfriend.IdUser);
                shuffleParticipants.Add(participant);
            }

            return shuffleParticipants;
        }

        private void Sort(List<Participant> participants)
        {
            var i = 0;
            while (i < participants.Count)
            {
                participants.Sort((x, y) => Guid.NewGuid().CompareTo(Guid.NewGuid()));
                i++;
            }
        }
    }
}
