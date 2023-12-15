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
            var request = new AddUserToGameRequest(input.GameId, UserId, input.UserId, input.Option1, input.Option2, input.Option3);
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
