using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace secret_friend_api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int UserId => int.TryParse(User.FindFirst(ClaimTypes.Sid)?.Value, out var result) ? result : throw new Exception("Invalid token");
    }
}
