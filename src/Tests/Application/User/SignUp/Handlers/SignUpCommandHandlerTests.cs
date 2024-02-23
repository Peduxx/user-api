namespace Tests.Application.User.SignUp.Handlers
{
    public class SignUpCommandHandlerTests
    {
        [Fact]
        public async Task SignUp_ReturnsSuccessResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = new DateTime(2001, 7, 27);

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email))
                              .ReturnsAsync((UserEntity)null);


            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(true));
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task SignUp_EmptyEmail_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "";
            var birthDate = new DateTime(2001, 7, 27);

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("Email cannot be empty."));
        }

        [Fact]
        public async Task SignUp_InvalidEmail_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "falseMail";
            var birthDate = new DateTime(2001, 7, 27);

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("Invalid Email."));
        }

        [Fact]
        public async Task SignUp_InvalidBirthDateBeforeThePossible_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = new DateTime(1500, 7, 27);

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("Invalid Birth Date."));
        }

        [Fact]
        public async Task SignUp_InvalidBirthDateAfterThePossible_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = DateTime.Now;

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();

            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("Invalid Birth Date."));
        }

        [Fact]
        public async Task SignUp_UserAlreadyExists_ReturnsFailureResponse()
        {
            // Arrange
            var name = "testUser";
            var email = "test@hotmail.com";
            var birthDate = new DateTime(2001, 7, 27);

            var user = UserEntity.Create(name, email, birthDate);

            var signUpCommand = new SignUpCommand(name, email, birthDate);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email))
                              .ReturnsAsync(user);

            var handler = new SignUpCommandHandler(userRepositoryMock.Object);

            // Act
            var response = await handler.Handle(signUpCommand, CancellationToken.None);

            // Assert
            Assert.True(response.Success.Equals(false));
            Assert.True(response.Message.Equals("This email already exists."));
        }
    }
}
