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
    public record UpdateNicknameRequest(int UserId, string NickName) : IRequest<UpdateNicknameResponse>;
    public record UpdateNicknameResponse(User User);

    public class UpdateNicknameHandler : IRequestHandler<UpdateNicknameRequest, UpdateNicknameResponse>
    {
        private readonly IUserRepository userRepository;

        public UpdateNicknameHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UpdateNicknameResponse> Handle(UpdateNicknameRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserById(request.UserId);

            if (user is null)
                throw new Exception("User not found");

            user.AddNickname(request.NickName);
            await userRepository.UpdateUser(request.UserId, user);
            return new UpdateNicknameResponse(user);
        }
    }
}
