using Application.Interfaces;
using domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GameUseCases
{
    public record NewGameRequest(int UserId, string Name, int? MinValue, int? MaxValue) : IRequest<NewGameResponse>;
    public record NewGameResponse(int GameId);

    public class NewGameHandler : IRequestHandler<NewGameRequest, NewGameResponse>
    {
        private readonly IGameRepository gameRepository;

        public NewGameHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<NewGameResponse> Handle(NewGameRequest request, CancellationToken cancellationToken)
        {
            var game = new Game(request.UserId, request.Name);

            if (request.MinValue is not null)
                game.AddMinValue(request.MinValue.Value);

            if (request.MaxValue is not null)
                game.AddMaxValue(request.MaxValue.Value);

            var gameId = await gameRepository.AddGameAsync(game);
            return new NewGameResponse(gameId);
        }
    }
}
