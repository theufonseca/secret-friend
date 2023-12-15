using Application.UseCases.Participants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secret_friend_api.Models.ParticipantControllerDtos;
using Twilio.TwiML.Messaging;

namespace secret_friend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipantController : BaseController
    {
        private readonly IMediator mediator;

        public ParticipantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserToGameDto input)
        {
            var request = new AddUserToGameRequest(input.GameCode, UserId, input.Option1, input.Option2, input.Option3);
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> Get(int gameId)
        {
            var request = new GetParticipantsRequest(gameId, UserId);
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveParticipantByHost([FromBody] RemoveParticipantByHostDto input)
        {
            var request = new RemoveParticipantByHostRequest(UserId, input.GameId, input.UserIdToRemove);
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("removeMySelf/{gameId}")]
        public async Task<IActionResult> RemoveMySelf(int gameId)
        {
            var request = new RemoveMySelfRequest(gameId, UserId);
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
