using FluentValidation;

namespace Tests.Application.User.SignIn.Handlers
{
    public class SignInCommandHandlerTests
    {
        [Fact]
        public async Task SignIn_ReturnsSuccessResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = new DateTime(2001, 7, 27);
            var password = "testPassword";

            var user = UserEntity.Create(name, email, birthDate);

            user.IsActive = true;

            var signInCommand = new SignInCommand(email, password);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email))
                              .ReturnsAsync(user);

            var passwordRepositoryMock = new Mock<IPasswordRepository>();
            passwordRepositoryMock.Setup(repo => repo.GetByUserId(user.Id))
                                  .ReturnsAsync(password);

            var passwordProviderMock = new Mock<IPasswordProvider>();
            passwordProviderMock.Setup(provider => provider.Compare(signInCommand.Password, password, It.IsAny<byte[]>()))
                                .Returns(true);

            var jwtProviderMock = new Mock<IJwtProvider>();
            jwtProviderMock.Setup(provider => provider.Generate(user))
                           .Returns("fake_jwt_token");

            var handler = new SignInCommandHandler(userRepositoryMock.Object, jwtProviderMock.Object, passwordRepositoryMock.Object, passwordProviderMock.Object);

            // Act
            var response = await handler.Handle(signInCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(true));
            Assert.NotNull(response.Data);
            Assert.IsType<SignInDTO>(response.Data);
            var signInDto = (SignInDTO)response.Data;
            Assert.Equal("fake_jwt_token", signInDto.Token);
        }

        [Fact]
        public async Task SignUp_IncorrectEmailOrPassword_ReturnsFailureResponse()
        {
            // Arrange
            var email = "test@hotmail.com";
            var password = "testPassword";

            var signInCommand = new SignInCommand(email, password);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email))
                              .ReturnsAsync((UserEntity)null);

            var handler = new SignInCommandHandler(userRepositoryMock.Object, null, null, null);

            // Act
            var response = await handler.Handle(signInCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.Equal("Your email or password is incorrect.", response.Message);
        }

        [Fact]
        public async Task SignUp_InactiveUser_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = new DateTime(2001, 7, 27);
            var password = "testPassword";

            var user = UserEntity.Create(name, email, birthDate);

            var signInCommand = new SignInCommand(email, password);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email))
                              .ReturnsAsync(user);

            var handler = new SignInCommandHandler(userRepositoryMock.Object, null, null, null);

            // Act
            var response = await handler.Handle(signInCommand, CancellationToken.None);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("You need to activate you account. Check your email.", response.Message);
        }
    }
}
