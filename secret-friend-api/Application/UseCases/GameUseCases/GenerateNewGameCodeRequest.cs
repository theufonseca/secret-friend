using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GameUseCases
{
    public record GenerateNewGameCodeRequest(int IdUserHost, int GameId) : IRequest<GenerateNewGameCodeResponse>;
    public record GenerateNewGameCodeResponse(string GameCode);

    public class GenerateNewGameCodeRequestHandler : IRequestHandler<GenerateNewGameCodeRequest, GenerateNewGameCodeResponse>
    {
        private readonly IGameRepository gameRepository;

        public GenerateNewGameCodeRequestHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<GenerateNewGameCodeResponse> Handle(GenerateNewGameCodeRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.GameId);

            if (game is null) throw new Exception("Game not found");

            if (game.IdUserHost != request.IdUserHost) throw new Exception("Not Authorized");

            game.GenerateGameCode();

            await gameRepository.UpdateNewGameCode(game.Id, game.GameCode);
            
            return new GenerateNewGameCodeResponse(game.GameCode);
        }
    }
}
