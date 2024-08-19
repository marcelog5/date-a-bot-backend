using Application.UseCases.ChatUseCases;
using Domain.Abstracts;
using Domain.Bots;
using Domain.Chats;
using Domain.Shared;
using Domain.Users;
using Moq;

namespace Test.Application
{
    public class CreateChatUseCaseTests
    {
        [Fact]
        public async Task Execute_WhenCalledWithValidParameters_CreatesChat()
        {
            // Arrange
            User user = new User(new Name("UserName"), new Email("user@example.com"), "Password");
            var validUserId = user.Id;

            Bot bot = new Bot(new Name("BotName"), "Role", "Goal", "Backstory", "Avatar");
            var validBotId = bot.Id;

            var createChatInput = new CreateChatInput(validUserId, validBotId);

            var botRepoMock = new Mock<IBotRepository>();
            botRepoMock.Setup(x => x.GetById(validBotId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(bot);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(x => x.GetById(validUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(user);

            var chatRepoMock = new Mock<IChatRepository>();
            chatRepoMock.Setup(x => x.Exist(validBotId, validUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
            chatRepoMock.Setup(x => x.Add(It.IsAny<Chat>(), It.IsAny<CancellationToken>()));

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            var createChatUseCase = new CreateChatUseCase(
                botRepoMock.Object,
                userRepoMock.Object,
                chatRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createChatUseCase.Execute(createChatInput, new CancellationToken());

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Execute_WhenBotNotFound_ReturnsFailure()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var invalidBotId = Guid.NewGuid();

            var createChatInput = new CreateChatInput(validUserId, invalidBotId);

            var botRepoMock = new Mock<IBotRepository>();
            botRepoMock.Setup(x => x.GetById(invalidBotId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Bot)null);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(x => x.GetById(validUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new User(new Name("UserName"), new Email("user@example.com"), "Password"));

            var chatRepoMock = new Mock<IChatRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var createChatUseCase = new CreateChatUseCase(
                botRepoMock.Object,
                userRepoMock.Object,
                chatRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createChatUseCase.Execute(createChatInput, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(BotErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Execute_WhenUserNotFound_ReturnsFailure()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
            var validBotId = Guid.NewGuid();

            var createChatInput = new CreateChatInput(invalidUserId, validBotId);

            var botRepoMock = new Mock<IBotRepository>();
            botRepoMock.Setup(x => x.GetById(validBotId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new Bot(new Name("BotName"), "Role", "Goal", "Backstory", "Avatar"));

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(x => x.GetById(invalidUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((User)null);

            var chatRepoMock = new Mock<IChatRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var createChatUseCase = new CreateChatUseCase(
                botRepoMock.Object,
                userRepoMock.Object,
                chatRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createChatUseCase.Execute(createChatInput, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotFound, result.Error);
        }

        [Fact]
        public async Task Execute_WhenChatAlreadyExists_ReturnsFailure()
        {
            // Arrange
            var validUserId = Guid.NewGuid();
            var validBotId = Guid.NewGuid();

            var createChatInput = new CreateChatInput(validUserId, validBotId);

            var botRepoMock = new Mock<IBotRepository>();
            botRepoMock.Setup(x => x.GetById(validBotId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new Bot(new Name("BotName"), "Role", "Goal", "Backstory", "Avatar"));

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(x => x.GetById(validUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new User(new Name("UserName"), new Email("user@example.com"), "Password"));

            var chatRepoMock = new Mock<IChatRepository>();
            chatRepoMock.Setup(x => x.Exist(validBotId, validUserId, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var createChatUseCase = new CreateChatUseCase(
                botRepoMock.Object,
                userRepoMock.Object,
                chatRepoMock.Object,
                unitOfWorkMock.Object);

            // Act
            var result = await createChatUseCase.Execute(createChatInput, new CancellationToken());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ChatErrors.AlreadyExist, result.Error);
        }
    }
}
