using Application.UseCases.UserUseCases;
using Domain.Abstracts;
using Domain.Shared;
using Domain.Users;
using Moq;

namespace Test.Application
{
    public class CreateUserUseCaseTests
    {
        [Fact]
        public async Task Execute_WhenUserDoesNotExist_CreatesUser()
        {
            // Arrange
            var createUserInput = new CreateUserInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                "Password123");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.AlreadyExists(createUserInput.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            userRepoMock.Setup(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var createUserUseCase = new CreateUserUseCase(
                userRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createUserUseCase.Execute(createUserInput, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Value.Id);
            userRepoMock.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WhenUserAlreadyExists_ReturnsFailure()
        {
            // Arrange
            var createUserInput = new CreateUserInput(
                new Name("John Doe"),
                new Email("john.doe@gmail.com"),
                "Password123");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(x => x.AlreadyExists(createUserInput.Email, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var createUserUseCase = new CreateUserUseCase(
                userRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createUserUseCase.Execute(createUserInput, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.AlreadyExists, result.Error);
        }
    }
}
