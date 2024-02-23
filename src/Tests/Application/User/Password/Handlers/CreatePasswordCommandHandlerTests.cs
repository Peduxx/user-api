namespace Application.Tests.User.Password.Commands.Handlers
{
    public class CreatePasswordCommandHandlerTests
    {
        [Fact]
        public async Task Password_Create_ReturnsSuccessResponse()
        {
            // Arrange
            var password = "passwordTest";

            var passwordProviderMock = new Mock<IPasswordProvider>();
            var passwordRepositoryMock = new Mock<IPasswordRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new CreatePasswordCommandHandler(passwordProviderMock.Object, passwordRepositoryMock.Object, userRepositoryMock.Object);
            var createPasswordCommand = new CreatePasswordCommand(Guid.NewGuid(), password);

            // Act
            var response = await handler.Handle(createPasswordCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(true));
        }

        [Fact]
        public async Task Password_TryEmptyValue_ReturnsFailureResponse()
        {
            // Arrange
            var passwordProviderMock = new Mock<IPasswordProvider>();
            var passwordRepositoryMock = new Mock<IPasswordRepository>();
            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new CreatePasswordCommandHandler(passwordProviderMock.Object, passwordRepositoryMock.Object, userRepositoryMock.Object);
            var command = new CreatePasswordCommand
            {
                UserId = Guid.NewGuid(),
                Password = ""
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("The password cannot be empty."));
        }
    }
}
