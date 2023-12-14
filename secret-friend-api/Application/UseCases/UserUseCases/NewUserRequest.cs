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
    public record NewUserRequest(string UserName, string NickName, string password) : IRequest<NewUserResponse>;
    public record NewUserResponse(int Id);

    public class NewUserRequestHandler : IRequestHandler<NewUserRequest, NewUserResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly ISecurity security;

        public NewUserRequestHandler(IUserRepository userRepository, ISecurity security)
        {
            this.userRepository = userRepository;
            this.security = security;
        }

        public async Task<NewUserResponse> Handle(NewUserRequest request, CancellationToken cancellationToken)
        {
            var checkUser = await userRepository.GetUserByUserNameAsync(request.UserName);
            
            if (checkUser is not null)
                throw new Exception("User already exists");

            var cryptoPassword = security.EncryptPassword(request.password);

            var user = new User(request.UserName, request.NickName, cryptoPassword);
            var userId = await userRepository.AddUserAsync(user);

            return new NewUserResponse(userId);
        }
    }
}
