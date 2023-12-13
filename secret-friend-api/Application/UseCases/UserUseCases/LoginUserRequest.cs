using Application.Interfaces;
using domain.Entities;
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
        private readonly ISecurity security;

        public LoginUserRequestHandler(IUserRepository userRepository, ISecurity security)
        {
            this.userRepository = userRepository;
            this.security = security;
        }

        async public Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByPhoneAsync(request.phoneNumber);

            if (user is null)
                throw new Exception("User or Password is wrong!");

            var cryptoPassword = security.EncryptPassword(request.password);

            user.CheckPassword(cryptoPassword);
            return new LoginUserResponse(user);
        }
    }
}
