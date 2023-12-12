using Application.Interfaces;
using domain.Entities;
using Infra.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public record LoginUserRequest(long phoneNumber, string password) : IRequest<LoginUserResponse>;
    public record LoginUserResponse(User User);

    public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUserRepository userRepository;

        public LoginUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async public Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByPhoneAsync(request.phoneNumber);

            if (user is null)
                throw new Exception("User or Password is wrong!");

            var cryptoPassword = Crypto.CriptografarSenha(request.password);

            user.CheckPassword(cryptoPassword);
            return new LoginUserResponse(user);
        }
    }
}
