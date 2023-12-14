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

            if (participants.Count() <= 1)
                throw new Exception("Not enought participants to play");

            return null;
        }

        private List<Participant> Shuffle(List<Participant> participants)
        {
            var shuffleParticipants = new List<Participant>();

            //while (participants.Count > 0)
            //{
            //    var participantsWithoutSelf = participants.fir(x => x.IdUser != )
            //};

            return shuffleParticipants;
        }
    }
}
