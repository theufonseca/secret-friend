using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace secret_friend_api.Controllers
{
    public class ErrorController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context?.Error;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred",
                Detail = exception?.Message
            };

            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
