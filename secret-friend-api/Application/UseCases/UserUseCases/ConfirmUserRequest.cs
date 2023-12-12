using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserUseCases
{
    public record ConfirmUserRequest(string confirmCode) : IRequest<ConfirmUserResponse>;
    public record ConfirmUserResponse(bool success);

    public class ConfirmUserRequestHandler : IRequestHandler<ConfirmUserRequest, ConfirmUserResponse>
    {
        private readonly IUserRepository userRepository;

        public ConfirmUserRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<ConfirmUserResponse> Handle(ConfirmUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByConfirmCodeAsync(request.confirmCode);
            if (user is null)
                throw new Exception("Confirmation not found or expirated");

            user.ConfirmUser();
            await userRepository.UpdateUserAsync(user);
            return new ConfirmUserResponse(true);
        }
    }
}
