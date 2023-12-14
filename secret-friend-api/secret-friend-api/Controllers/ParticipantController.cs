using Application.UseCases.Participants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.TwiML.Messaging;

namespace secret_friend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipantController : ControllerBase
    {
        private readonly IMediator mediator;

        public ParticipantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserToGameRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
