using Application.Interfaces;
using Application.UseCases.UserUseCases;
using domain.Entities;
using FluentAssertions;
using Infra.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UseCasesTest.UserUseCasesTests
{
    public class NewUserRequestTest
    {
        [Test]
        public async Task CreateNewUser_ShouldBe_Correctly()
        {
            var userRepository = new Mock<IUserRepository>();
            var notificationService = new Mock<INotificationService>();
            userRepository.Setup(x => x.AddUserAsync(It.IsAny<User>())).ReturnsAsync(1);
            userRepository.Setup(x => x.GetUserByPhoneAsync(It.IsAny<long>())).ReturnsAsync((User?)null);

            var newUserRequest = new NewUserRequest(11940028922, "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, notificationService.Object);

            var newUserResponse = await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            newUserResponse.Id.Should().Be(1);
            userRepository.Verify(x => x.AddUserAsync(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.PhoneNumber == 11940028922)));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.Nickname == "Yudi Tamashiro")));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.Password == Crypto.CriptografarSenha("password"))));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.Confirmed == false)));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => !string.IsNullOrEmpty(o.ConfirmationCode))));
            
            notificationService.Verify(x => x.SendConfirmationLinkAsync(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenUserAlreadyExists()
        {
            var userRepository = new Mock<IUserRepository>();
            var notificationService = new Mock<INotificationService>();
            userRepository.Setup(x => x.GetUserByPhoneAsync(It.IsAny<long>())).ReturnsAsync(new User(11940028922, "Yudi Tamashiro", "password"));

            var newUserRequest = new NewUserRequest(11940028922, "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, notificationService.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("User already exists");
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenPhoneNumberIsLessthen10Digits()
        {
            var userRepository = new Mock<IUserRepository>();
            var notificationService = new Mock<INotificationService>();
            userRepository.Setup(x => x.GetUserByPhoneAsync(It.IsAny<long>())).ReturnsAsync((User?)null);

            var newUserRequest = new NewUserRequest(1194002892, "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, notificationService.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("Phone number is invalid");
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenNicknameIsEmpty()
        {
            var userRepository = new Mock<IUserRepository>();
            var notificationService = new Mock<INotificationService>();
            userRepository.Setup(x => x.GetUserByPhoneAsync(It.IsAny<long>())).ReturnsAsync((User?)null);

            var newUserRequest = new NewUserRequest(11940028922, "", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, notificationService.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("Nickname is invalid");
        }
    }
}
