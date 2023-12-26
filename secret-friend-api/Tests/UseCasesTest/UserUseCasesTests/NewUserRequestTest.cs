using Application.Interfaces;
using Application.UseCases.UserUseCases;
using domain.Entities;
using FluentAssertions;
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
            var security = new Mock<ISecurity>();
            security.Setup(Object => Object.EncryptPassword(It.IsAny<string>())).Returns("password");

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.AddUserAsync(It.IsAny<User>())).ReturnsAsync(1);

            var newUserRequest = new NewUserRequest("YudiT", "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, security.Object);

            var newUserResponse = await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            newUserResponse.Id.Should().Be(1);
            userRepository.Verify(x => x.AddUserAsync(It.IsAny<User>()), Times.Once);
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.UserName == "YudiT")));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.Nickname == "Yudi Tamashiro")));
            userRepository.Verify(x => x.AddUserAsync(It.Is<User>(o => o.Password == "passworde")));
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenUserAlreadyExists()
        {
            var security = new Mock<ISecurity>();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>())).ReturnsAsync(new User("YudiT", "Yudi Tamashiro", "password"));

            var newUserRequest = new NewUserRequest("YudiT", "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, security.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("User already exists");
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenUserNameIslessThen5Characteres()
        {
            var security = new Mock<ISecurity>();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

            var newUserRequest = new NewUserRequest("Yudi", "Yudi Tamashiro", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, security.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("Username is to short");
        }

        [Test]
        public void CreateNewUser_ShouldBe_ThrowException_WhenNicknameIsEmpty()
        {
            var security = new Mock<ISecurity>();
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

            var newUserRequest = new NewUserRequest("YudiT", "", "password");
            var newUserRequestHandler = new NewUserRequestHandler(userRepository.Object, security.Object);

            Func<Task> act = async () => await newUserRequestHandler.Handle(newUserRequest, new CancellationToken());
            act.Should().ThrowAsync<Exception>().WithMessage("Nickname is invalid");
        }
    }
}
