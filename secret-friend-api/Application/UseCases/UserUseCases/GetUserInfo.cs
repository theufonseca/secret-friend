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
    public record GetUserInfoRequest(int userId) : IRequest<GetUserInfoResponse>;
    public record GetUserInfoResponse(User user);

    public class GetUserInfoRequestHandler : IRequestHandler<GetUserInfoRequest, GetUserInfoResponse>
    {
        private readonly IUserRepository userRepository;

        public GetUserInfoRequestHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        async public Task<GetUserInfoResponse> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserById(request.userId);

            if (user is null)
                throw new Exception("User not found!");

            return new GetUserInfoResponse(user);
        }
    }
}
