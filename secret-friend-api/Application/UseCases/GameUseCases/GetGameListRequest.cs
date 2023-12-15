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
    public record GetGameListRequest(int userId) : IRequest<GetGameListResponse>;
    public record GetGameListResponse(List<Game> Games);

    public class GetGameListRequestHandler : IRequestHandler<GetGameListRequest, GetGameListResponse>
    {
        private readonly IGameRepository gameRepository;

        public GetGameListRequestHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<GetGameListResponse> Handle(GetGameListRequest request, CancellationToken cancellationToken)
        {
            var games = await gameRepository.GetGamesByUserId(request.userId);

            return new GetGameListResponse(games);
        }
    }
}
