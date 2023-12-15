using Application.UseCases.GameUseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secret_friend_api.Models.GameControllerDtos;

namespace secret_friend_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class GameController : BaseController
    {
        private readonly IMediator mediator;

        public GameController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewGameDto input)
        {
            var response = await mediator.Send(new NewGameRequest(UserId, input.Name, input.MinPrice, input.MaxPrice));
            return Ok(response);
        }

        [HttpPost("run/{gameId}")]
        public async Task<IActionResult> Run(int gameId)
        {
            var response = await mediator.Send(new RunGameRequest(gameId, UserId));
            return Ok(response);
        }

        [HttpGet("secretfriend/{gameId}")]
        public async Task<IActionResult> GetSecretFriend(int gameId)
        {
            var response = await mediator.Send(new GetSecretFriendRequest(UserId, gameId));
            return Ok(response);
        }
    }
}