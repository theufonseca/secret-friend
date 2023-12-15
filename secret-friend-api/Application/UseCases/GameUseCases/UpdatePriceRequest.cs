using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.GameUseCases
{
    public record UpdatePriceRequest(int GameId, int UserHostId, int? MaxPrice, int? MinPrice) : IRequest<UpdatePriceResponse>;
    public record UpdatePriceResponse(bool Success);

    public class UpdatePriceRequestHandler : IRequestHandler<UpdatePriceRequest, UpdatePriceResponse>
    {
        private readonly IGameRepository gameRepository;

        public UpdatePriceRequestHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<UpdatePriceResponse> Handle(UpdatePriceRequest request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.GetById(request.GameId);

            if (game is null)
                throw new Exception("Game not found");

            if (game.IdUserHost != request.UserHostId)
                throw new Exception("User is not the host");

            await gameRepository.UpdateGamePrice(request.GameId, request.MinPrice, request.MaxPrice);

            return new UpdatePriceResponse(true);
        }
    }

}
