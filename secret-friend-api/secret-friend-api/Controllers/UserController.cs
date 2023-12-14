using Application.UseCases.UserUseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using secret_friend_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace secret_friend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly TokenConfiguration tokenConfig;

        public UserController(IMediator mediator, TokenConfiguration tokenConfig)
        {
            this.mediator = mediator;
            this.tokenConfig = tokenConfig;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] NewUserRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await mediator.Send(request);

            var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, response.User.Nickname),
                new (ClaimTypes.Sid, response.User.UserName),
            };

            var token = GetToken(authClaims);
            return Ok(token);
        }

        [HttpPost("update")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateNickname([FromBody] UpdateNicknameRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        private TokenModel GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.SecretJwtKey!));

            var token = new JwtSecurityToken(
                issuer: tokenConfig.Issuer,
                audience: tokenConfig.Audience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
