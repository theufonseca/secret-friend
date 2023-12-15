using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GameUseCases
{
    public record ShareGameCodeRequest(int UserHostId, int GameId) : IRequest<ShareGameCodeResponse>;
    public record ShareGameCodeResponse(string GameCode);

    public class ShareGameCodeHandler : IRequestHandler<ShareGameCodeRequest, ShareGameCodeResponse>
    {
        private readonly IGameRepository gameRepository;

        public ShareGameCodeHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<ShareGameCodeResponse> Handle(ShareGameCodeRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.GameId);

            if (game is null)
                throw new Exception("Game not found!");

            if (game.IdUserHost != request.UserHostId)
                throw new Exception("User is not the host of this game!");

            return new ShareGameCodeResponse(game.GameCode);
        }
    }
}
